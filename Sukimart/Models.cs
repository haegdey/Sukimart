namespace Sukimart
{
    public class Barang
    {
        public string Kode { get; set; } = "";
        public string Nama { get; set; } = "";
        public string Kategori { get; set; } = "";
        public int HargaBeli { get; set; }
        public int Harga { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public int HargaJual
        {
            get => Harga;
            set => Harga = value;
        }

        public int Stok { get; set; }
        public int StokMinimum { get; set; }
        public bool Aktif { get; set; } = true;
    }

    public class Transaksi
    {
        public string Id { get; set; } = "";
        public DateTime Tanggal { get; set; }
        public string Kasir { get; set; } = "";
        public int Total { get; set; }
        public int Bayar { get; set; }
        public int Kembalian { get; set; }
    }

    public class DetailTransaksi
    {
        public DateTime Tanggal { get; set; }
        public string IdTransaksi { get; set; } = "";
        public string KodeBarang { get; set; } = "";
        public string NamaBarang { get; set; } = "";
        public int HargaBeli { get; set; }
        public int Harga { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public int HargaSatuan
        {
            get => Harga;
            set => Harga = value;
        }

        public int Jumlah { get; set; }

        private int _subtotal;
        public int Subtotal
        {
            get => _subtotal != 0 ? _subtotal : (Harga * Jumlah);
            set => _subtotal = value;
        }
    }

    public class Pengguna
    {
        public string NamaPengguna { get; set; } = "";
        public string NamaLengkap { get; set; } = "";
        public string Peran { get; set; } = "kasir";
        public bool Aktif { get; set; } = true;
        public string HashSandi { get; set; } = "";
        public string Garam { get; set; } = "";
        public int Iterasi { get; set; }
        public DateTime Dibuat { get; set; }
    }

    public class Aktivitas
    {
        public DateTime Tanggal { get; set; } = DateTime.Now;

        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime Waktu
        {
            get => Tanggal;
            set => Tanggal = value;
        }

        public string NamaPengguna { get; set; } = "";

        [System.Text.Json.Serialization.JsonIgnore]
        public string Pengguna
        {
            get => NamaPengguna;
            set => NamaPengguna = value;
        }

        public string JenisAktivitas { get; set; } = "Umum";

        public string Aksi { get; set; } = "";

        [System.Text.Json.Serialization.JsonIgnore]
        public string DetailAktivitas
        {
            get => Aksi;
            set => Aksi = value;
        }
    }

    public class RiwayatStok
    {
        public DateTime Tanggal { get; set; }
        public string KodeBarang { get; set; } = "";
        public string NamaBarang { get; set; } = "";
        public string Jenis { get; set; } = "";
        public int Jumlah { get; set; }
        public int StokSebelum { get; set; }
        public int StokSesudah { get; set; }
        public string Pemasok { get; set; } = "";
        public string Keterangan { get; set; } = "";
        public string Pengguna { get; set; } = "";
    }

    public class BarangMasuk
    {
        public string Id { get; set; } = "";
        public DateTime Tanggal { get; set; }
        public string KodeBarang { get; set; } = "";
        public string NamaBarang { get; set; } = "";
        public int Jumlah { get; set; }
        public string Pemasok { get; set; } = "";
        public string Petugas { get; set; } = "";
        public string Keterangan { get; set; } = "";
    }

    public class BarangKeluar
    {
        public string Id { get; set; } = "";
        public DateTime Tanggal { get; set; }
        public string KodeBarang { get; set; } = "";
        public string NamaBarang { get; set; } = "";
        public int Jumlah { get; set; }
        public string Alasan { get; set; } = "";
        public string Keterangan { get; set; } = "";
        public string Petugas { get; set; } = "";
    }

    public class ItemKeranjang
    {
        public string KodeBarang { get; set; } = "";
        public string NamaBarang { get; set; } = "";
        public int HargaBeli { get; set; }
        public int Harga { get; set; }
        public int Jumlah { get; set; }
        public int Subtotal
        {
            get { return Harga * Jumlah; }
        }
    }

    public class ItemLabaProduk
    {
        public string Kode { get; set; } = "";
        public string Nama { get; set; } = "";
        public string Terjual { get; set; } = "";
        public string Pendapatan { get; set; } = "";
        public string TotalModal { get; set; } = "";
        public string LabaKotor { get; set; } = "";
        public int RawTerjual { get; set; }
        public long RawPendapatan { get; set; }
        public long RawModal { get; set; }
        public long RawLaba { get; set; }
    }

    public class SesiOpname
    {
        public string Id { get; set; } = "";
        public string IdSesi => Id;
        public DateTime TanggalDibuat { get; set; } = DateTime.Now;
        public DateTime TanggalMulai => TanggalDibuat;
        public string DibuatOleh { get; set; } = "";
        public string Pembuat => DibuatOleh;
        public string Status { get; set; } = "Draft"; // Draft, Menunggu Persetujuan, Perlu Revisi, Disetujui, Dibatalkan

        public DateTime? TanggalDiajukan { get; set; }

        public DateTime? TanggalDisetujui { get; set; }
        public string DisetujuiOleh { get; set; } = "";

        public DateTime? TanggalDibatalkan { get; set; }
        public string DibatalkanOleh { get; set; } = "";
        public string AlasanPembatalan { get; set; } = "";

        public DateTime? TanggalSelesai => TanggalDisetujui ?? TanggalDibatalkan;

        public List<ItemOpname> Items { get; set; } = new List<ItemOpname>();
        public List<RiwayatRevisiOpname> RiwayatRevisi { get; set; } = new List<RiwayatRevisiOpname>();
        public List<RiwayatStatusOpname> RiwayatStatus { get; set; } = new List<RiwayatStatusOpname>();
    }

    public class ItemOpname
    {
        public string KodeBarang { get; set; } = "";
        public string NamaBarang { get; set; } = "";
        public int StokSistem { get; set; }
        public int? StokFisik { get; set; }
        public bool SudahDihitung { get; set; } = false;
        public int Selisih { get; set; } // StokFisik - StokSistem
        public string Keterangan { get; set; } = "";

        // Status revisi admin
        public bool PerluRevisi { get; set; } = false;
        public string AlasanRevisi { get; set; } = "";
        public int? StokFisikSebelumRevisi { get; set; }
    }

    public class RiwayatRevisiOpname
    {
        public DateTime Tanggal { get; set; } = DateTime.Now;
        public string DimintaOleh { get; set; } = "";
        public string KodeBarang { get; set; } = "";
        public string NamaBarang { get; set; } = "";
        public int StokSistem { get; set; }
        public int StokFisikLama { get; set; }
        public int SelisihLama { get; set; }
        public string AlasanRevisi { get; set; } = "";
        public int? StokFisikBaru { get; set; }
        public DateTime? TanggalDiperbaiki { get; set; }
        public string DiperbaikiOleh { get; set; } = "";
    }

    public class RiwayatStatusOpname
    {
        public DateTime Waktu { get; set; } = DateTime.Now;
        public string Status { get; set; } = "";
        public string Oleh { get; set; } = "";
        public string Catatan { get; set; } = "";
    }
}
