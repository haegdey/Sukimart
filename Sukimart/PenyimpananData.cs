using System.Text.Json;

namespace Sukimart
{
    public static class PenyimpananData
    {
        private static readonly JsonSerializerOptions opsiJson = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        private static string LokasiFile(string namaFile)
        {
            string folderSumber = Path.Combine(Application.StartupPath, "Data");
            string folderData = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Sukimart", "Data");
            Directory.CreateDirectory(folderData);

            string lokasiTujuan = Path.Combine(folderData, namaFile);
            string lokasiAwal = Path.Combine(folderSumber, namaFile);
            if (!File.Exists(lokasiTujuan) && File.Exists(lokasiAwal))
            {
                File.Copy(lokasiAwal, lokasiTujuan);
            }

            return lokasiTujuan;
        }

        private static List<T> BacaData<T>(string namaFile)
        {
            string lokasi = LokasiFile(namaFile);

            if (!File.Exists(lokasi))
            {
                return new List<T>();
            }

            string json = File.ReadAllText(lokasi);

            try
            {
                return JsonSerializer.Deserialize<List<T>>(json, opsiJson) ?? new List<T>();
            }
            catch (JsonException ex)
            {
                string cadangan = lokasi + ".rusak";
                try
                {
                    if (!File.Exists(cadangan))
                    {
                        File.Copy(lokasi, cadangan);
                    }
                }
                catch (IOException)
                {
                }

                throw new InvalidDataException(
                    $"File {namaFile} bukan JSON yang valid. Periksa tanda kurung, koma, dan tanda kutip. Salinan aslinya disimpan sebagai {Path.GetFileName(cadangan)}.", ex);
            }
        }

        private static void SimpanData<T>(string namaFile, List<T> data)
        {
            string lokasi = LokasiFile(namaFile);
            string fileSementara = lokasi + ".tmp";
            string json = JsonSerializer.Serialize(data, opsiJson);
            File.WriteAllText(fileSementara, json);
            File.Move(fileSementara, lokasi, true);
        }

        public static List<string> BacaKategori()
        {
            List<string> daftarKategori = BacaData<string>("kategori.json");
            if (daftarKategori.Count == 0)
            {
                daftarKategori = new List<string> { "Makanan", "Minuman", "Perlengkapan", "Lainnya" };
                foreach (Barang barang in BacaBarang())
                {
                    if (!string.IsNullOrWhiteSpace(barang.Kategori) && !daftarKategori.Contains(barang.Kategori, StringComparer.OrdinalIgnoreCase))
                    {
                        daftarKategori.Add(barang.Kategori);
                    }
                }
                SimpanKategori(daftarKategori);
            }
            return daftarKategori;
        }

        public static void SimpanKategori(List<string> daftarKategori)
        {
            SimpanData("kategori.json", daftarKategori);
        }

        public static List<Barang> BacaBarang()
        {
            return BacaData<Barang>("barang.json");
        }

        public static void SimpanBarang(List<Barang> daftarBarang)
        {
            SimpanData("barang.json", daftarBarang);
        }

        public static List<Transaksi> BacaTransaksi()
        {
            return BacaData<Transaksi>("transaksi.json");
        }

        public static void SimpanTransaksi(List<Transaksi> daftarTransaksi)
        {
            SimpanData("transaksi.json", daftarTransaksi);
        }

        public static void TambahTransaksi(Transaksi transaksi)
        {
            List<Transaksi> daftarTransaksi = BacaTransaksi();
            daftarTransaksi.Add(transaksi);
            SimpanTransaksi(daftarTransaksi);
        }

        public static void TambahTransaksi(Transaksi transaksi, List<DetailTransaksi> rincian)
        {
            List<Transaksi> daftarTransaksi = BacaTransaksi();
            daftarTransaksi.Add(transaksi);
            SimpanTransaksi(daftarTransaksi);

            if (rincian != null && rincian.Count > 0)
            {
                List<DetailTransaksi> daftarDetail = BacaDetailTransaksi();
                daftarDetail.AddRange(rincian);
                SimpanDetailTransaksi(daftarDetail);
            }
        }

        public static List<DetailTransaksi> BacaDetailTransaksi()
        {
            List<DetailTransaksi> daftarDetail = BacaData<DetailTransaksi>("detail_transaksi.json");
            bool adaMigrasi = false;
            if (daftarDetail.Any(d => d.HargaBeli <= 0))
            {
                List<Barang> daftarBarang = BacaBarang();
                var mapBarangByKode = daftarBarang
                    .Where(b => !string.IsNullOrEmpty(b.Kode))
                    .GroupBy(b => b.Kode, StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

                var mapBarangByNama = daftarBarang
                    .Where(b => !string.IsNullOrEmpty(b.Nama))
                    .GroupBy(b => b.Nama, StringComparer.OrdinalIgnoreCase)
                    .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

                foreach (var d in daftarDetail)
                {
                    if (d.HargaBeli <= 0)
                    {
                        if (mapBarangByKode.TryGetValue(d.KodeBarang, out var brgKode) && brgKode.HargaBeli > 0)
                        {
                            d.HargaBeli = brgKode.HargaBeli;
                            adaMigrasi = true;
                        }
                        else if (mapBarangByNama.TryGetValue(d.NamaBarang, out var brgNama) && brgNama.HargaBeli > 0)
                        {
                            d.HargaBeli = brgNama.HargaBeli;
                            adaMigrasi = true;
                        }
                        else
                        {
                            // Estimasi rasio modal wajar (70% - 75% dari harga jual transaksi)
                            d.HargaBeli = Math.Max(0, (int)Math.Round(d.Harga * 0.75));
                            adaMigrasi = true;
                        }
                    }
                }

                if (adaMigrasi)
                {
                    try
                    {
                        SimpanDetailTransaksi(daftarDetail);
                    }
                    catch { }
                }
            }
            return daftarDetail;
        }

        public static void SimpanDetailTransaksi(List<DetailTransaksi> daftarDetail)
        {
            SimpanData("detail_transaksi.json", daftarDetail);
        }

        public static List<Pengguna> BacaPengguna()
        {
            List<Pengguna> daftarPengguna = BacaData<Pengguna>("pengguna.json");

            // Hanya buat pengguna awal kalau file benar-benar kosong
            if (daftarPengguna.Count == 0)
            {
                daftarPengguna = BuatPenggunaAwal();
                SimpanPengguna(daftarPengguna);
            }

            return daftarPengguna;
        }

        public static void SimpanPengguna(List<Pengguna> daftarPengguna)
        {
            SimpanData("pengguna.json", daftarPengguna);
        }

        private static List<Pengguna> BuatPenggunaAwal()
        {
            Pengguna admin = new Pengguna { NamaPengguna = "admin", NamaLengkap = "Administrator Suki MART", Peran = "admin", Aktif = true, Dibuat = DateTime.Now };
            Pengguna kasir = new Pengguna { NamaPengguna = "kasir1", NamaLengkap = "Dewi Lestari", Peran = "kasir", Aktif = true, Dibuat = DateTime.Now };
            Pengguna kasirNonaktif = new Pengguna { NamaPengguna = "kasir3", NamaLengkap = "Sari Wulandari", Peran = "kasir", Aktif = false, Dibuat = DateTime.Now };
            Pengguna gudang = new Pengguna { NamaPengguna = "gudang1", NamaLengkap = "Budi Santoso", Peran = "gudang", Aktif = true, Dibuat = DateTime.Now };
            KeamananSandi.IsiSandi(admin, "admin123");
            KeamananSandi.IsiSandi(kasir, "kasir123");
            KeamananSandi.IsiSandi(kasirNonaktif, "kasir123");
            KeamananSandi.IsiSandi(gudang, "gudang123");
            return new List<Pengguna> { admin, kasir, kasirNonaktif, gudang };
        }

        public static List<Aktivitas> BacaAktivitas()
        {
            List<Aktivitas> daftar = BacaData<Aktivitas>("log_aktivitas.json");
            bool adaMigrasi = false;
            foreach (var a in daftar)
            {
                if (string.IsNullOrWhiteSpace(a.JenisAktivitas))
                {
                    a.JenisAktivitas = TentukanJenisAktivitas(a.Aksi);
                    adaMigrasi = true;
                }

                // Normalisasi catatan log lama pergantian password mandiri yang bertumpuk
                if (!string.IsNullOrEmpty(a.Aksi) && a.Aksi.StartsWith("Aktivitas: Mengganti Kata Sandi", StringComparison.OrdinalIgnoreCase))
                {
                    a.Aksi = "Pengguna berhasil mengganti kata sandi akunnya sendiri.";
                    adaMigrasi = true;
                }
            }

            if (adaMigrasi)
            {
                try
                {
                    SimpanData("log_aktivitas.json", daftar);
                }
                catch { }
            }

            return daftar;
        }

        public static void CatatAktivitas(string namaPengguna, string jenisAktivitas, string aksi)
        {
            List<Aktivitas> daftarAktivitas = BacaAktivitas();
            daftarAktivitas.Add(new Aktivitas
            {
                Tanggal = DateTime.Now,
                NamaPengguna = string.IsNullOrWhiteSpace(namaPengguna) ? "sistem" : namaPengguna,
                JenisAktivitas = string.IsNullOrWhiteSpace(jenisAktivitas) ? TentukanJenisAktivitas(aksi) : jenisAktivitas,
                Aksi = aksi ?? ""
            });
            SimpanData("log_aktivitas.json", daftarAktivitas);
        }

        public static void CatatAktivitas(string namaPengguna, string aksi)
        {
            string jenis = TentukanJenisAktivitas(aksi);
            CatatAktivitas(namaPengguna, jenis, aksi);
        }

        public static void BersihkanAktivitas()
        {
            SimpanData("log_aktivitas.json", new List<Aktivitas>());
        }

        public static string TentukanJenisAktivitas(string aksi)
        {
            if (string.IsNullOrWhiteSpace(aksi)) return "Umum";
            string a = aksi.Trim();
            if (a.StartsWith("LOGIN GAGAL", StringComparison.OrdinalIgnoreCase)) return "Login Gagal";
            if (a.StartsWith("LOGIN", StringComparison.OrdinalIgnoreCase)) return "Login";
            if (a.StartsWith("LOGOUT", StringComparison.OrdinalIgnoreCase)) return "Logout";
            if (a.StartsWith("TAMBAH BARANG", StringComparison.OrdinalIgnoreCase)) return "Tambah Barang";
            if (a.StartsWith("UBAH BARANG", StringComparison.OrdinalIgnoreCase)) return "Ubah Barang";
            if (a.StartsWith("HAPUS BARANG", StringComparison.OrdinalIgnoreCase)) return "Hapus Barang";
            if (a.StartsWith("NONAKTIFKAN BARANG", StringComparison.OrdinalIgnoreCase)) return "Nonaktifkan Barang";
            if (a.StartsWith("AKTIFKAN BARANG", StringComparison.OrdinalIgnoreCase)) return "Aktifkan Barang";
            if (a.StartsWith("TAMBAH KATEGORI", StringComparison.OrdinalIgnoreCase)) return "Tambah Kategori";
            if (a.StartsWith("UBAH KATEGORI", StringComparison.OrdinalIgnoreCase)) return "Ubah Kategori";
            if (a.StartsWith("HAPUS KATEGORI", StringComparison.OrdinalIgnoreCase)) return "Hapus Kategori";
            if (a.StartsWith("TAMBAH PENGGUNA", StringComparison.OrdinalIgnoreCase) || a.StartsWith("TAMBAH AKUN", StringComparison.OrdinalIgnoreCase)) return "Tambah Akun";
            if (a.StartsWith("UBAH PASSWORD", StringComparison.OrdinalIgnoreCase) || a.StartsWith("UBAH KATA SANDI", StringComparison.OrdinalIgnoreCase) || a.StartsWith("AKTIVITAS: MENGGANTI KATA SANDI", StringComparison.OrdinalIgnoreCase) || a.StartsWith("MENGGANTI KATA SANDI", StringComparison.OrdinalIgnoreCase)) return "Ubah Password";
            if (a.StartsWith("UBAH NAMA", StringComparison.OrdinalIgnoreCase) || a.StartsWith("UBAH PROFIL", StringComparison.OrdinalIgnoreCase)) return "Ubah Nama";
            if (a.StartsWith("UBAH PENGGUNA", StringComparison.OrdinalIgnoreCase) || a.StartsWith("UBAH AKUN", StringComparison.OrdinalIgnoreCase)) return "Ubah Akun";
            if (a.StartsWith("HAPUS PENGGUNA", StringComparison.OrdinalIgnoreCase) || a.StartsWith("HAPUS AKUN", StringComparison.OrdinalIgnoreCase)) return "Hapus Akun";
            if (a.StartsWith("TRANSAKSI", StringComparison.OrdinalIgnoreCase)) return "Transaksi";
            if (a.StartsWith("Masuk", StringComparison.OrdinalIgnoreCase) || a.StartsWith("STOK MASUK", StringComparison.OrdinalIgnoreCase)) return "Stok Masuk";
            if (a.StartsWith("Keluar", StringComparison.OrdinalIgnoreCase) || a.StartsWith("STOK KELUAR", StringComparison.OrdinalIgnoreCase)) return "Stok Keluar";
            if (a.StartsWith("STOK OPNAME", StringComparison.OrdinalIgnoreCase) || a.StartsWith("OPNAME", StringComparison.OrdinalIgnoreCase) || a.Contains("opname", StringComparison.OrdinalIgnoreCase)) return "Stok Opname";
            return "Umum";
        }

        public static List<RiwayatStok> BacaRiwayatStok()
        {
            return BacaData<RiwayatStok>("riwayat_stok.json");
        }

        public static void TambahRiwayatStok(RiwayatStok catatan)
        {
            List<RiwayatStok> daftarRiwayat = BacaRiwayatStok();
            daftarRiwayat.Add(catatan);
            SimpanData("riwayat_stok.json", daftarRiwayat);
        }

        public static List<BarangMasuk> BacaBarangMasuk()
        {
            return BacaData<BarangMasuk>("barang_masuk.json");
        }

        public static void TambahBarangMasuk(BarangMasuk barangMasuk)
        {
            List<BarangMasuk> daftarBarangMasuk = BacaBarangMasuk();
            daftarBarangMasuk.Add(barangMasuk);
            SimpanData("barang_masuk.json", daftarBarangMasuk);
        }

        public static List<BarangKeluar> BacaBarangKeluar()
        {
            return BacaData<BarangKeluar>("barang_keluar.json");
        }

        public static void TambahBarangKeluar(BarangKeluar barangKeluar)
        {
            List<BarangKeluar> daftarBarangKeluar = BacaBarangKeluar();
            daftarBarangKeluar.Add(barangKeluar);
            SimpanData("barang_keluar.json", daftarBarangKeluar);
        }

        public static string BuatIdBarangMasuk()
        {
            return BuatIdBerikutnya(BacaBarangMasuk().Select(data => data.Id), "MSK");
        }

        public static string BuatIdBarangKeluar()
        {
            return BuatIdBerikutnya(BacaBarangKeluar().Select(data => data.Id), "KLR");
        }

        private static string BuatIdBerikutnya(IEnumerable<string> daftarId, string awalan)
        {
            int nomorTerbesar = 0;
            foreach (string id in daftarId)
            {
                if (id.StartsWith(awalan, StringComparison.OrdinalIgnoreCase) && int.TryParse(id[awalan.Length..], out int nomor))
                {
                    nomorTerbesar = Math.Max(nomorTerbesar, nomor);
                }
            }
            return awalan + (nomorTerbesar + 1).ToString("D3");
        }

        public static void TambahDetailTransaksi(List<DetailTransaksi> rincian)
        {
            if (rincian != null && rincian.Count > 0)
            {
                List<DetailTransaksi> daftarDetail = BacaDetailTransaksi();
                daftarDetail.AddRange(rincian);
                SimpanDetailTransaksi(daftarDetail);
            }
        }

        #region Stok Opname

        public static List<SesiOpname> BacaSesiOpname()
        {
            return BacaData<SesiOpname>("sesi_opname.json");
        }

        public static void SimpanSesiOpname(List<SesiOpname> daftarSesi)
        {
            SimpanData("sesi_opname.json", daftarSesi);
        }

        public static string BuatIdSesiOpname()
        {
            string prefix = $"OPN-{DateTime.Now:yyyyMMdd}-";
            var daftar = BacaSesiOpname();
            int maxNo = 0;
            foreach (var s in daftar)
            {
                if (!string.IsNullOrEmpty(s.Id) && s.Id.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    string suffix = s.Id.Substring(prefix.Length);
                    if (int.TryParse(suffix, out int no) && no > maxNo)
                    {
                        maxNo = no;
                    }
                }
            }
            return $"{prefix}{(maxNo + 1):D3}";
        }

        public static SesiOpname? AmbilSesiOpnameAktif()
        {
            var daftar = BacaSesiOpname();
            return daftar.FirstOrDefault(s => s.Status == "Draft" || s.Status == "Menunggu Persetujuan" || s.Status == "Perlu Revisi");
        }

        public static bool ApakahAdaOpnameAktif()
        {
            return AmbilSesiOpnameAktif() != null;
        }

        public static HashSet<string> AmbilKodeBarangTerkunciOpname()
        {
            var daftar = BacaSesiOpname();
            var aktif = daftar.Where(s => s.Status == "Draft" || s.Status == "Menunggu Persetujuan" || s.Status == "Perlu Revisi");
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var sesi in aktif)
            {
                foreach (var item in sesi.Items)
                {
                    set.Add(item.KodeBarang);
                }
            }
            return set;
        }

        public static bool ApakahBarangTerkunciOpname(string kodeBarang)
        {
            return ApakahBarangTerkunciOpname(kodeBarang, out _);
        }

        public static bool ApakahBarangTerkunciOpname(string kodeBarang, out string nomorOpname)
        {
            nomorOpname = "";
            if (string.IsNullOrWhiteSpace(kodeBarang)) return false;

            var daftar = BacaSesiOpname();
            var aktif = daftar.FirstOrDefault(s => (s.Status == "Draft" || s.Status == "Menunggu Persetujuan" || s.Status == "Perlu Revisi")
                                                 && s.Items.Any(i => string.Equals(i.KodeBarang, kodeBarang, StringComparison.OrdinalIgnoreCase)));
            if (aktif != null)
            {
                nomorOpname = aktif.Id;
                return true;
            }
            return false;
        }

        public static void SimpanAtauPerbaruiSesiOpname(SesiOpname sesi)
        {
            if (sesi == null) return;
            List<SesiOpname> daftar = BacaSesiOpname();
            int idx = daftar.FindIndex(s => s.Id.Equals(sesi.Id, StringComparison.OrdinalIgnoreCase));
            if (idx >= 0)
            {
                daftar[idx] = sesi;
            }
            else
            {
                daftar.Add(sesi);
            }
            SimpanSesiOpname(daftar);
        }

        public static SesiOpname BuatSesiOpnameBaru(string pembuat)
        {
            // Validasi hak akses
            if (Navigasi.PeranAktif != "admin" && Navigasi.PeranAktif != "gudang")
            {
                throw new UnauthorizedAccessException("Hanya Petugas Gudang dan Admin yang dapat membuat sesi Stok Opname.");
            }

            // Validasi apakah sudah ada sesi opname aktif
            SesiOpname? sesiAktif = AmbilSesiOpnameAktif();
            if (sesiAktif != null)
            {
                throw new InvalidOperationException($"Masih terdapat sesi Stok Opname aktif ({sesiAktif.Id}) dengan status '{sesiAktif.Status}'. Harap selesaikan atau batalkan sesi tersebut terlebih dahulu.");
            }

            List<Barang> barangAktif = BacaBarang().Where(b => b.Aktif).ToList();
            if (barangAktif.Count == 0)
            {
                throw new InvalidOperationException("Tidak ada produk aktif dalam sistem untuk dilakukan Stok Opname.");
            }

            string idBaru = BuatIdSesiOpname();
            SesiOpname sesi = new SesiOpname
            {
                Id = idBaru,
                TanggalDibuat = DateTime.Now,
                DibuatOleh = pembuat,
                Status = "Draft",
                Items = barangAktif.Select(b => new ItemOpname
                {
                    KodeBarang = b.Kode,
                    NamaBarang = b.Nama,
                    StokSistem = b.Stok,
                    StokFisik = null,
                    SudahDihitung = false,
                    Selisih = 0,
                    Keterangan = ""
                }).ToList()
            };

            sesi.RiwayatStatus.Add(new RiwayatStatusOpname
            {
                Waktu = DateTime.Now,
                Status = "Draft",
                Oleh = pembuat,
                Catatan = $"Sesi opname dibuat dengan {sesi.Items.Count} produk aktif."
            });

            SimpanAtauPerbaruiSesiOpname(sesi);
            CatatAktivitas(pembuat, "Stok Opname", $"Membuat sesi opname {sesi.Id} dengan {sesi.Items.Count} produk aktif.");
            return sesi;
        }

        public static void TerapkanPersetujuanOpname(string idSesi, string adminPengguna)
        {
            // Validasi hak akses Admin
            if (Navigasi.PeranAktif != "admin")
            {
                throw new UnauthorizedAccessException("Hanya Admin yang berwenang menyetujui sesi Stok Opname.");
            }

            List<SesiOpname> daftarSesi = BacaSesiOpname();
            SesiOpname? sesi = daftarSesi.FirstOrDefault(s => s.Id.Equals(idSesi, StringComparison.OrdinalIgnoreCase));
            if (sesi == null)
            {
                throw new InvalidOperationException($"Sesi opname {idSesi} tidak ditemukan.");
            }

            if (sesi.Status == "Disetujui")
            {
                throw new InvalidOperationException($"Sesi opname {idSesi} sudah pernah disetujui sebelumnya dan tidak dapat diterapkan dua kali.");
            }

            if (sesi.Status != "Menunggu Persetujuan")
            {
                throw new InvalidOperationException($"Sesi opname {idSesi} berstatus '{sesi.Status}', hanya sesi berstatus 'Menunggu Persetujuan' yang dapat disetujui.");
            }

            // Validasi integritas data: semua item harus memiliki stok fisik valid (>= 0)
            foreach (var item in sesi.Items)
            {
                if (!item.StokFisik.HasValue || item.StokFisik.Value < 0)
                {
                    throw new InvalidOperationException($"Nilai stok fisik untuk produk '{item.NamaBarang}' ({item.KodeBarang}) belum diisi atau tidak valid.");
                }
            }

            // Atomic All-or-Nothing
            List<Barang> daftarBarang = BacaBarang();
            List<RiwayatStok> daftarRiwayat = BacaRiwayatStok();

            // Salinan cadangan untuk rollback jika gagal di tengah jalan
            var backupBarang = daftarBarang.Select(b => new Barang
            {
                Kode = b.Kode,
                Nama = b.Nama,
                Kategori = b.Kategori,
                HargaBeli = b.HargaBeli,
                Harga = b.Harga,
                Stok = b.Stok,
                StokMinimum = b.StokMinimum,
                Aktif = b.Aktif
            }).ToList();

            var backupRiwayat = new List<RiwayatStok>(daftarRiwayat);

            try
            {
                DateTime waktuSetuju = DateTime.Now;
                List<RiwayatStok> riwayatBaru = new List<RiwayatStok>();

                foreach (var item in sesi.Items)
                {
                    var barang = daftarBarang.FirstOrDefault(b => b.Kode.Equals(item.KodeBarang, StringComparison.OrdinalIgnoreCase));
                    if (barang == null)
                    {
                        throw new InvalidOperationException($"Produk '{item.NamaBarang}' ({item.KodeBarang}) tidak ditemukan di inventaris.");
                    }

                    int stokSebelum = barang.Stok;
                    int stokFisik = item.StokFisik!.Value;
                    int selisih = stokFisik - stokSebelum;

                    barang.Stok = stokFisik;
                    item.Selisih = selisih;

                    if (selisih != 0)
                    {
                        riwayatBaru.Add(new RiwayatStok
                        {
                            Tanggal = waktuSetuju,
                            KodeBarang = item.KodeBarang,
                            NamaBarang = item.NamaBarang,
                            Jenis = "Penyesuaian Opname",
                            Jumlah = Math.Abs(selisih),
                            StokSebelum = stokSebelum,
                            StokSesudah = stokFisik,
                            Pemasok = "",
                            Keterangan = $"Penyesuaian stok berdasarkan Opname {sesi.Id}",
                            Pengguna = adminPengguna
                        });
                    }
                }

                sesi.Status = "Disetujui";
                sesi.TanggalDisetujui = waktuSetuju;
                sesi.DisetujuiOleh = adminPengguna;
                sesi.RiwayatStatus.Add(new RiwayatStatusOpname
                {
                    Waktu = waktuSetuju,
                    Status = "Disetujui",
                    Oleh = adminPengguna,
                    Catatan = $"Sesi opname disetujui. {riwayatBaru.Count} produk mengalami penyesuaian stok."
                });

                // Simpan secara berurutan dan aman
                daftarRiwayat.AddRange(riwayatBaru);
                SimpanBarang(daftarBarang);
                SimpanData("riwayat_stok.json", daftarRiwayat);
                SimpanSesiOpname(daftarSesi);

                CatatAktivitas(adminPengguna, "Stok Opname", $"Menyetujui sesi opname {sesi.Id} ({sesi.Items.Count} produk diperiksa, {riwayatBaru.Count} produk disesuaikan).");
            }
            catch (Exception)
            {
                // Rollback seluruh perubahan
                try
                {
                    SimpanBarang(backupBarang);
                    SimpanData("riwayat_stok.json", backupRiwayat);
                }
                catch { }

                throw;
            }
        }

        public static void BatalkanSesiOpname(string idSesi, string pengguna, string alasan)
        {
            if (string.IsNullOrWhiteSpace(alasan))
            {
                throw new ArgumentException("Alasan pembatalan wajib diisi.");
            }

            List<SesiOpname> daftarSesi = BacaSesiOpname();
            SesiOpname? sesi = daftarSesi.FirstOrDefault(s => s.Id.Equals(idSesi, StringComparison.OrdinalIgnoreCase));
            if (sesi == null)
            {
                throw new InvalidOperationException($"Sesi opname {idSesi} tidak ditemukan.");
            }

            if (sesi.Status == "Disetujui")
            {
                throw new InvalidOperationException($"Sesi opname {idSesi} sudah disetujui dan tidak dapat dibatalkan.");
            }

            if (sesi.Status == "Dibatalkan")
            {
                throw new InvalidOperationException($"Sesi opname {idSesi} sudah dibatalkan sebelumnya.");
            }

            // Validasi peran
            if (Navigasi.PeranAktif != "admin" && Navigasi.PeranAktif != "gudang")
            {
                throw new UnauthorizedAccessException("Kasir tidak memiliki akses untuk membatalkan sesi Stok Opname.");
            }

            if (Navigasi.PeranAktif == "gudang" && !sesi.DibuatOleh.Equals(pengguna, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Petugas Gudang hanya dapat membatalkan sesi opname miliknya sendiri.");
            }

            DateTime waktuBatal = DateTime.Now;
            sesi.Status = "Dibatalkan";
            sesi.TanggalDibatalkan = waktuBatal;
            sesi.DibatalkanOleh = pengguna;
            sesi.AlasanPembatalan = alasan;
            sesi.RiwayatStatus.Add(new RiwayatStatusOpname
            {
                Waktu = waktuBatal,
                Status = "Dibatalkan",
                Oleh = pengguna,
                Catatan = $"Sesi dibatalkan. Alasan: {alasan}"
            });

            SimpanSesiOpname(daftarSesi);
            CatatAktivitas(pengguna, "Stok Opname", $"Membatalkan sesi opname {sesi.Id}. Alasan: {alasan}");
        }

        #endregion
    }
}
