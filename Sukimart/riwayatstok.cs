using System.Globalization;

namespace Sukimart
{
    public partial class FormRiwayatStok : Form
    {
        private static readonly CultureInfo BudayaIndonesia = new CultureInfo("id-ID");
        private readonly string? kodeBarang;

        public FormRiwayatStok(string? kodeBarang = null)
        {
            this.kodeBarang = kodeBarang;
            InitializeComponent();
            UIHelper.InisialisasiForm(this);
            Paint += (s, e) =>
            {
                using Pen pena = new Pen(UIHelper.WarnaGarisTepi);
                e.Graphics.DrawRectangle(pena, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            };
            btnTutup.Click += (s, e) => Close();
            gridRiwayat.CellFormatting += GridRiwayat_CellFormatting;
            MuatRiwayat();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MuatRiwayat()
        {
            try
            {
                List<RiwayatStok> daftar = PenyimpananData.BacaRiwayatStok();
                List<BarangMasuk> daftarBarangMasuk = PenyimpananData.BacaBarangMasuk();
                List<BarangKeluar> daftarBarangKeluar = PenyimpananData.BacaBarangKeluar();
                Barang? barang = null;

                if (!string.IsNullOrEmpty(kodeBarang))
                {
                    barang = PenyimpananData.BacaBarang().FirstOrDefault(data => data.Kode == kodeBarang);
                    daftar = daftar.Where(data => data.KodeBarang == kodeBarang).ToList();
                }

                var riwayat = daftar
                    .OrderByDescending(r => r.Tanggal)
                    .ToList();

                gridRiwayat.AutoGenerateColumns = false;
                gridRiwayat.DataSource = riwayat.Select(r =>
                {
                    string pemasokTampil = "-";
                    string keteranganTampil = "-";
                    bool isMasuk = r.Jenis.Equals("MASUK", StringComparison.OrdinalIgnoreCase) ||
                                   r.Jenis.Equals("Stok Masuk", StringComparison.OrdinalIgnoreCase);
                    bool isKeluar = r.Jenis.Equals("KELUAR", StringComparison.OrdinalIgnoreCase) ||
                                    r.Jenis.Equals("Stok Keluar", StringComparison.OrdinalIgnoreCase);

                    if (isMasuk)
                    {
                        // Ambil referensi data barang masuk jika ada (untuk migrasi data lama)
                        var bm = daftarBarangMasuk.FirstOrDefault(b =>
                            (!string.IsNullOrEmpty(b.Id) && r.Keterangan.StartsWith(b.Id, StringComparison.OrdinalIgnoreCase)) ||
                            (b.KodeBarang == r.KodeBarang && Math.Abs((b.Tanggal - r.Tanggal).TotalSeconds) < 10));

                        // 1. Kolom Pemasok
                        if (!string.IsNullOrWhiteSpace(r.Pemasok))
                        {
                            pemasokTampil = r.Pemasok;
                        }
                        else if (bm != null && !string.IsNullOrWhiteSpace(bm.Pemasok))
                        {
                            pemasokTampil = bm.Pemasok;
                        }

                        // 2. Kolom Keterangan Stok Masuk
                        if (r.Keterangan.StartsWith("MSK", StringComparison.OrdinalIgnoreCase))
                        {
                            if (bm != null && !string.IsNullOrWhiteSpace(bm.Keterangan))
                            {
                                keteranganTampil = bm.Keterangan.Trim();
                            }
                            else
                            {
                                keteranganTampil = "-";
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(r.Keterangan) && r.Keterangan != "-")
                        {
                            keteranganTampil = r.Keterangan.Trim();
                        }
                        else
                        {
                            keteranganTampil = "-";
                        }
                    }
                    else if (isKeluar)
                    {
                        pemasokTampil = "-";

                        // Kolom Keterangan Stok Keluar
                        if (r.Keterangan.StartsWith("KLR", StringComparison.OrdinalIgnoreCase))
                        {
                            var bk = daftarBarangKeluar.FirstOrDefault(b =>
                                (!string.IsNullOrEmpty(b.Id) && r.Keterangan.StartsWith(b.Id, StringComparison.OrdinalIgnoreCase)) ||
                                (b.KodeBarang == r.KodeBarang && Math.Abs((b.Tanggal - r.Tanggal).TotalSeconds) < 10));

                            if (bk != null)
                            {
                                string alasanBersih = bk.Alasan?.Trim() ?? "";
                                string ketTambahan = bk.Keterangan?.Trim() ?? "";

                                if (!string.IsNullOrWhiteSpace(alasanBersih) && !string.IsNullOrWhiteSpace(ketTambahan))
                                {
                                    keteranganTampil = $"{alasanBersih} \u2022 {ketTambahan}";
                                }
                                else if (!string.IsNullOrWhiteSpace(alasanBersih))
                                {
                                    keteranganTampil = alasanBersih;
                                }
                                else if (!string.IsNullOrWhiteSpace(ketTambahan))
                                {
                                    keteranganTampil = ketTambahan;
                                }
                                else
                                {
                                    keteranganTampil = "-";
                                }
                            }
                            else
                            {
                                string sisa = r.Keterangan;
                                int pos = sisa.IndexOf('·');
                                if (pos >= 0 && pos + 1 < sisa.Length)
                                {
                                    sisa = sisa.Substring(pos + 1).Trim();
                                }
                                keteranganTampil = !string.IsNullOrWhiteSpace(sisa) ? sisa : "-";
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(r.Keterangan) && r.Keterangan != "-")
                        {
                            keteranganTampil = r.Keterangan.Trim();
                        }
                        else
                        {
                            keteranganTampil = "-";
                        }
                    }
                    else
                    {
                        pemasokTampil = "-";
                        keteranganTampil = !string.IsNullOrWhiteSpace(r.Keterangan) ? r.Keterangan.Trim() : "-";
                    }

                    return new
                    {
                        Tanggal = r.Tanggal.ToString("dd/MM/yyyy HH:mm", BudayaIndonesia),
                        r.Jenis,
                        StokAwal = r.StokSebelum,
                        JumlahFormat = (isMasuk ? "+" : "−") + r.Jumlah.ToString("N0", BudayaIndonesia),
                        r.StokSesudah,
                        Pemasok = pemasokTampil,
                        Keterangan = keteranganTampil,
                        r.Pengguna
                    };
                }).ToList();

                string namaBarang = barang?.Nama ?? riwayat.FirstOrDefault()?.NamaBarang ?? "";
                if (!string.IsNullOrEmpty(kodeBarang))
                {
                    lblJudul.Text = !string.IsNullOrEmpty(namaBarang)
                        ? $"Kartu Stok • {kodeBarang}: {namaBarang}"
                        : $"Kartu Stok • {kodeBarang}";
                    Text = lblJudul.Text;

                    if (barang != null)
                    {
                        lblInfo.Text = $"{barang.Nama} · Stok saat ini: {barang.Stok:N0} · {riwayat.Count} catatan";
                    }
                    else
                    {
                        lblInfo.Text = riwayat.Count == 0 ? "Belum ada catatan perubahan stok" : $"{riwayat.Count} catatan";
                    }
                }
                else
                {
                    lblJudul.Text = "Kartu Stok";
                    Text = lblJudul.Text;
                    lblInfo.Text = riwayat.Count == 0 ? "Belum ada catatan perubahan stok" : $"{riwayat.Count} catatan";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat riwayat stok: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GridRiwayat_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || gridRiwayat.Columns[e.ColumnIndex].Name != "colJumlah" || e.CellStyle == null)
            {
                return;
            }

            string teks = e.Value?.ToString() ?? "";
            e.CellStyle.ForeColor = teks.StartsWith("−") ? Color.FromArgb(220, 38, 38) : Color.FromArgb(16, 149, 102);
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }
    }
}
