namespace Sukimart
{
    public partial class FormGudang : Form
    {
        private readonly string peran;
        private List<Barang> daftarBarang = new List<Barang>();
        private Barang? barangTerpilih;
        private bool hanyaMenipis = false;

        public FormGudang() : this("admin")
        {
        }

        public FormGudang(string peran)
        {
            this.peran = string.IsNullOrEmpty(peran) ? "admin" : peran;
            InitializeComponent();
            UIHelper.InisialisasiForm(this);
            MaximizeBox = true;
            SiapkanFormPergerakanStok();
            UIHelper.RatakanTengah(colStok, colMinStok);
            gridGudang.AllowUserToResizeRows = false;
            cmbJenisStok.SelectedIndex = 0;
            SesuaikanPeran();
            PasangEventHandlers();
            MuatDataGudang();
        }

        private void SesuaikanPeran()
        {
            if (this.peran == "kasir")
            {
                btnGudang.Visible = false;
                btnLaporan.Visible = false;
                btnPengguna.Visible = false;
            }
            else if (this.peran == "gudang")
            {
                btnKasir.Visible = false;
                btnRiwayat.Visible = false;
                btnLaporan.Visible = false;
                btnPengguna.Visible = false;
                btnGudang.Visible = true;
            }
            else
            {
                btnKasir.Visible = true;
                btnRiwayat.Visible = true;
                btnGudang.Visible = true;
                btnLaporan.Visible = true;
                btnPengguna.Visible = true;
            }

            UIHelper.SusunTombolSidebar(pnlKiri);
        }

        private void PasangEventHandlers()
        {
            btnDashboard.Click += (s, e) => Navigasi.BukaDashboard(this);
            btnKasir.Click += (s, e) => Navigasi.BukaKasir(this);
            btnKatalog.Click += (s, e) => Navigasi.BukaKatalog(this);
            btnRiwayat.Click += (s, e) => Navigasi.BukaRiwayat(this);
            btnGudang.Click += (s, e) => { /* Sedang di form gudang */ };
            btnLaporan.Click += (s, e) => Navigasi.BukaLaporan(this);
            btnPengguna.Click += (s, e) => Navigasi.BukaPengguna(this);
            btnKeluar.Click += (s, e) => Navigasi.KonfirmasiDanKeluar(this);

            txtCariGudang.TextChanged += (s, e) => FilterDaftarGudang();
            btnFilterMenipis.Click += (s, e) => { hanyaMenipis = true; FilterDaftarGudang(); };
            btnSemuaStok.Click += (s, e) => { hanyaMenipis = false; FilterDaftarGudang(); };
            cmbJenisStok.SelectedIndexChanged += (s, e) => AturInputPergerakanStok();

            gridGudang.SelectionChanged += GridGudang_SelectionChanged;
            btnSimpanStok.Click += BtnSimpanStok_Click;
            btnRiwayatStok.Click += (s, e) =>
            {
                if (barangTerpilih == null)
                {
                    MessageBox.Show("Pilih barang pada tabel terlebih dahulu untuk melihat kartu stok.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using FormRiwayatStok formRiwayat = new FormRiwayatStok(barangTerpilih.Kode);
                formRiwayat.ShowDialog(this);
            };
        }

        private void SiapkanFormPergerakanStok()
        {
            AturInputPergerakanStok();
        }

        private void AturInputPergerakanStok()
        {
            bool stokKeluar = cmbJenisStok.SelectedIndex == 1;
            lblPemasok.Visible = !stokKeluar;
            txtPemasok.Visible = !stokKeluar;
            lblAlasan.Visible = stokKeluar;
            cmbAlasan.Visible = stokKeluar;
            txtPemasok.Text = stokKeluar ? "" : txtPemasok.Text;
            cmbAlasan.SelectedIndex = stokKeluar ? cmbAlasan.SelectedIndex : -1;
        }

        public void MuatDataGudang()
        {
            try
            {
                daftarBarang = PenyimpananData.BacaBarang();
                FilterDaftarGudang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data stok gudang: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterDaftarGudang()
        {
            string keyword = txtCariGudang.Text.Trim().ToLower();

            var query = daftarBarang.Where(b => b.Aktif).AsEnumerable();

            if (hanyaMenipis)
            {
                query = query.Where(b => b.Stok <= b.StokMinimum);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(b =>
                    b.Nama.ToLower().Contains(keyword) ||
                    b.Kode.ToLower().Contains(keyword) ||
                    b.Kategori.ToLower().Contains(keyword));
            }

            var hasil = query.ToList();

            gridGudang.AutoGenerateColumns = false;
            gridGudang.DataSource = hasil.Select(b => new
            {
                b.Kode,
                b.Nama,
                b.Kategori,
                b.Stok,
                b.StokMinimum,
                StatusStok = b.Stok == 0 ? "HABIS" : b.Stok <= b.StokMinimum ? "MINIMUM" : "AMAN"
            }).ToList();
            gridGudang.ClearSelection();
            barangTerpilih = null;
            txtBarangTerpilih.Clear();
        }

        private void GridGudang_SelectionChanged(object? sender, EventArgs e)
        {
            if (gridGudang.CurrentRow == null || gridGudang.CurrentRow.Index < 0)
            {
                barangTerpilih = null;
                txtBarangTerpilih.Clear();
                return;
            }

            var item = gridGudang.CurrentRow.DataBoundItem;
            if (item == null) return;

            string kode = (string)item.GetType().GetProperty("Kode")?.GetValue(item, null)!;
            barangTerpilih = daftarBarang.FirstOrDefault(b => b.Kode == kode);

            if (barangTerpilih != null)
            {
                txtBarangTerpilih.Text = $"{barangTerpilih.Kode} - {barangTerpilih.Nama} (Stok Saat Ini: {barangTerpilih.Stok})";
            }
        }

        private void BtnSimpanStok_Click(object? sender, EventArgs e)
        {
            if (barangTerpilih == null)
            {
                MessageBox.Show("Pilih barang pada tabel terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int jumlah = (int)nudJumlahMasuk.Value;
            if (jumlah <= 0)
            {
                MessageBox.Show("Jumlah stok harus lebih dari 0.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool stokKeluar = cmbJenisStok.SelectedIndex == 1;
            string pemasok = txtPemasok.Text.Trim();
            string alasan = cmbAlasan.SelectedItem?.ToString() ?? "";
            if (!stokKeluar && string.IsNullOrEmpty(pemasok))
            {
                MessageBox.Show("Pemasok wajib diisi untuk barang masuk.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPemasok.Focus();
                return;
            }

            if (stokKeluar && string.IsNullOrEmpty(alasan))
            {
                MessageBox.Show("Pilih alasan untuk barang keluar.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAlasan.Focus();
                return;
            }

            if (stokKeluar && jumlah > barangTerpilih.Stok)
            {
                MessageBox.Show($"Stok keluar tidak boleh melebihi stok saat ini ({barangTerpilih.Stok}).", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int stokSebelum = barangTerpilih.Stok;
            int stokSesudah = stokKeluar ? stokSebelum - jumlah : stokSebelum + jumlah;
            string jenis = stokKeluar ? "KELUAR" : "MASUK";
            string id = stokKeluar ? PenyimpananData.BuatIdBarangKeluar() : PenyimpananData.BuatIdBarangMasuk();

            try
            {
                barangTerpilih.Stok = stokSesudah;
                PenyimpananData.SimpanBarang(daftarBarang);
                if (stokKeluar)
                {
                    PenyimpananData.TambahBarangKeluar(new BarangKeluar
                    {
                        Id = id,
                        Tanggal = DateTime.Now,
                        KodeBarang = barangTerpilih.Kode,
                        NamaBarang = barangTerpilih.Nama,
                        Jumlah = jumlah,
                        Alasan = alasan,
                        Keterangan = txtKeterangan.Text.Trim(),
                        Petugas = Navigasi.PenggunaAktif
                    });
                }
                else
                {
                    PenyimpananData.TambahBarangMasuk(new BarangMasuk
                    {
                        Id = id,
                        Tanggal = DateTime.Now,
                        KodeBarang = barangTerpilih.Kode,
                        NamaBarang = barangTerpilih.Nama,
                        Jumlah = jumlah,
                        Pemasok = pemasok,
                        Keterangan = txtKeterangan.Text.Trim(),
                        Petugas = Navigasi.PenggunaAktif
                    });
                }
            }
            catch (Exception ex)
            {
                barangTerpilih.Stok = stokSebelum;
                try
                {
                    PenyimpananData.SimpanBarang(daftarBarang);
                }
                catch
                {
                }
                MessageBox.Show("Gagal menyimpan perubahan stok: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string keteranganRiwayat;
                if (stokKeluar)
                {
                    string ketTambahan = txtKeterangan.Text.Trim();
                    string alasanBersih = alasan.Trim();
                    if (!string.IsNullOrWhiteSpace(alasanBersih) && !string.IsNullOrWhiteSpace(ketTambahan))
                    {
                        keteranganRiwayat = $"{alasanBersih} \u2022 {ketTambahan}";
                    }
                    else if (!string.IsNullOrWhiteSpace(alasanBersih))
                    {
                        keteranganRiwayat = alasanBersih;
                    }
                    else if (!string.IsNullOrWhiteSpace(ketTambahan))
                    {
                        keteranganRiwayat = ketTambahan;
                    }
                    else
                    {
                        keteranganRiwayat = "-";
                    }
                }
                else
                {
                    string ketTambahan = txtKeterangan.Text.Trim();
                    keteranganRiwayat = !string.IsNullOrWhiteSpace(ketTambahan) ? ketTambahan : "-";
                }

                PenyimpananData.TambahRiwayatStok(new RiwayatStok
                {
                    Tanggal = DateTime.Now,
                    KodeBarang = barangTerpilih.Kode,
                    NamaBarang = barangTerpilih.Nama,
                    Jenis = jenis,
                    Jumlah = jumlah,
                    StokSebelum = stokSebelum,
                    StokSesudah = stokSesudah,
                    Pemasok = stokKeluar ? "" : pemasok,
                    Keterangan = keteranganRiwayat,
                    Pengguna = Navigasi.PenggunaAktif
                });
                string jenisLog = stokKeluar ? "Stok Keluar" : "Stok Masuk";
                string detailLog = stokKeluar
                    ? $"{jenis} {id}: '{barangTerpilih.Nama}' ({barangTerpilih.Kode}) keluar sejumlah -{jumlah}. Alasan: '{alasan}'. Stok: {stokSebelum} -> {stokSesudah}."
                    : $"{jenis} {id}: '{barangTerpilih.Nama}' ({barangTerpilih.Kode}) masuk sejumlah +{jumlah} dari '{pemasok}'. Stok: {stokSebelum} -> {stokSesudah}.";
                PenyimpananData.CatatAktivitas(Navigasi.PenggunaAktif, jenisLog, detailLog);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Perubahan stok tersimpan, tetapi riwayat atau log gagal dicatat: " + ex.Message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            MessageBox.Show(
                $"{jenis} {id} untuk '{barangTerpilih.Nama}' berhasil disimpan.\nStok sekarang: {stokSesudah}",
                "Stok Diperbarui",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            string kodeTerakhir = barangTerpilih.Kode;
            MuatDataGudang();
            PilihBaris(kodeTerakhir);
            nudJumlahMasuk.Value = 10;
            txtPemasok.Clear();
            cmbAlasan.SelectedIndex = -1;
            txtKeterangan.Clear();
        }

        private void PilihBaris(string kode)
        {
            foreach (DataGridViewRow baris in gridGudang.Rows)
            {
                var item = baris.DataBoundItem;
                if (item?.GetType().GetProperty("Kode")?.GetValue(item, null) as string == kode)
                {
                    gridGudang.CurrentCell = baris.Cells[0];
                    return;
                }
            }
        }
    }
}
