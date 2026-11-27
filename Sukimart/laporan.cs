using System.Globalization;
using System.Reflection;

namespace Sukimart
{
    public partial class FormLaporan : Form
    {
        private static readonly CultureInfo BudayaIndonesia = new CultureInfo("id-ID");
        private readonly string peran;
        private List<Transaksi> semuaTransaksi = new List<Transaksi>();
        private List<DetailTransaksi> semuaDetail = new List<DetailTransaksi>();
        private string subJenisPenjualan = "transaksi";
        private List<ItemLabaProduk> daftarLabaSaatIni = new List<ItemLabaProduk>();
        private string kolomUrutLaba = "LabaKotor";
        private SortOrder urutanSortLaba = SortOrder.Descending;

        public FormLaporan() : this("admin") { }

        public FormLaporan(string peran)
        {
            this.peran = string.IsNullOrEmpty(peran) ? "admin" : peran;
            InitializeComponent();
            UIHelper.InisialisasiForm(this);
            AktifkanBebasFlicker();
            MaximizeBox = true;
            gridLaporan.AllowUserToResizeRows = false;
            gridLaba.AllowUserToResizeRows = false;
            SiapkanFilterLaporan();
            SesuaikanPeran();
            PasangEventHandlers();
            pnlKanan.Resize += (s, e) => AturTataLetakResponsif();
            AturTataLetakResponsif();

            if (this.peran != "admin")
            {
                Shown += (s, e) =>
                {
                    MessageBox.Show("Menu Laporan Penjualan hanya dapat diakses oleh Admin.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Navigasi.BukaDashboard(this);
                };
                return;
            }

            MuatDataLaporan();
        }

        private void AktifkanBebasFlicker()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            try
            {
                var flag = BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic;
                typeof(Panel).InvokeMember("DoubleBuffered", flag, null, pnlKanan, new object[] { true });
                typeof(Panel).InvokeMember("DoubleBuffered", flag, null, pnlCard1, new object[] { true });
                typeof(Panel).InvokeMember("DoubleBuffered", flag, null, pnlCard2, new object[] { true });
                typeof(Panel).InvokeMember("DoubleBuffered", flag, null, pnlCard3, new object[] { true });
                typeof(Panel).InvokeMember("DoubleBuffered", flag, null, pnlCard4, new object[] { true });
                typeof(Panel).InvokeMember("DoubleBuffered", flag, null, pnlCardTerlaris, new object[] { true });
                typeof(Panel).InvokeMember("DoubleBuffered", flag, null, pnlCardKasir, new object[] { true });
                typeof(FlowLayoutPanel).InvokeMember("DoubleBuffered", flag, null, flpTerlaris, new object[] { true });
                typeof(FlowLayoutPanel).InvokeMember("DoubleBuffered", flag, null, flpKasir, new object[] { true });
                typeof(DataGridView).InvokeMember("DoubleBuffered", flag, null, gridLaporan, new object[] { true });
                typeof(DataGridView).InvokeMember("DoubleBuffered", flag, null, gridLaba, new object[] { true });
            }
            catch
            {
            }
        }

        private void SiapkanFilterLaporan()
        {
            dtpMulai.Format = DateTimePickerFormat.Custom;
            dtpMulai.CustomFormat = "dd/MM/yyyy";
            dtpSelesai.Format = DateTimePickerFormat.Custom;
            dtpSelesai.CustomFormat = "dd/MM/yyyy";

            dtpMulai.Value = DateTime.Today.AddDays(-30);
            dtpSelesai.Value = DateTime.Today;

            btnLaporan.BackColor = Color.FromArgb(255, 196, 0);
            btnLaporan.ForeColor = Color.FromArgb(31, 41, 55);
            btnLaporan.Font = FontInter.Buat(10F, FontStyle.Bold);

            btnLaporanInventaris.BackColor = Color.White;
            btnLaporanInventaris.ForeColor = Color.FromArgb(31, 41, 55);
            btnLaporanInventaris.Font = FontInter.Buat(10F, FontStyle.Regular);

            AturTombolSub(btnSubTransaksi, "Transaksi", true, 110);
            AturTombolSub(btnSubLaba, "Laba", false, 110);

            lblRingkasan.Font = FontInter.Buat(9F);
            lblRingkasan.ForeColor = Color.FromArgb(75, 85, 99);

            Color warnaNavyJudul = Color.FromArgb(30, 41, 59);
            lblCard1Judul.Font = FontInter.Buat(8.5F, FontStyle.Bold);
            lblCard1Judul.ForeColor = warnaNavyJudul;
            lblCard2Judul.Font = FontInter.Buat(8.5F, FontStyle.Bold);
            lblCard2Judul.ForeColor = warnaNavyJudul;
            lblCard3Judul.Font = FontInter.Buat(8.5F, FontStyle.Bold);
            lblCard3Judul.ForeColor = warnaNavyJudul;
            lblCard4Judul.Font = FontInter.Buat(8.5F, FontStyle.Bold);
            lblCard4Judul.ForeColor = warnaNavyJudul;

            lblTerlarisJudul.Font = FontInter.Buat(9F, FontStyle.Bold);
            lblTerlarisJudul.ForeColor = warnaNavyJudul;
            lblTerlarisKosong.Font = FontInter.Buat(9F);
            lblTerlarisKosong.ForeColor = Color.FromArgb(107, 114, 128);

            lblKasirJudul.Font = FontInter.Buat(9F, FontStyle.Bold);
            lblKasirJudul.ForeColor = warnaNavyJudul;
            lblKasirKosong.Font = FontInter.Buat(9F);
            lblKasirKosong.ForeColor = Color.FromArgb(107, 114, 128);

            PasangGayaKartu(pnlCard1);
            PasangGayaKartu(pnlCard2);
            PasangGayaKartu(pnlCard3);
            PasangGayaKartu(pnlCard4);
            PasangGayaKartu(pnlCardTerlaris);
            PasangGayaKartu(pnlCardKasir);

            lblInfoHitung.Font = FontInter.Buat(8.5F);
            lblInfoHitung.ForeColor = Color.FromArgb(107, 114, 128);
            lblInfoHitung.Text = "Laba kotor dihitung dari (harga jual saat transaksi − harga beli saat transaksi) × jumlah terjual.";

            lblJudulTabel.Font = FontInter.Buat(11F, FontStyle.Bold);
            lblJudulTabel.ForeColor = Color.FromArgb(31, 41, 55);
            lblJudulTabel.Text = "Rincian Laba per Produk";

            lblSampai.Font = FontInter.Buat(9F);
            dtpMulai.Font = FontInter.Buat(9.5F);
            dtpSelesai.Font = FontInter.Buat(9.5F);
            btnTerapkanTanggal.Font = FontInter.Buat(9.5F, FontStyle.Bold);

            lblTotalJudul.Font = FontInter.Buat(9.5F, FontStyle.Bold);
            lblTotalJudul.ForeColor = Color.FromArgb(55, 65, 81);
            lblTotalTerjualLabel.Font = FontInter.Buat(9F);
            lblTotalTerjualLabel.ForeColor = Color.FromArgb(107, 114, 128);
            lblTotalTerjualNilai.Font = FontInter.Buat(9.5F, FontStyle.Bold);
            lblTotalTerjualNilai.ForeColor = Color.FromArgb(31, 41, 55);
            lblTotalPendapatanLabel.Font = FontInter.Buat(9F);
            lblTotalPendapatanLabel.ForeColor = Color.FromArgb(107, 114, 128);
            lblTotalPendapatanNilai.Font = FontInter.Buat(9.5F, FontStyle.Bold);
            lblTotalPendapatanNilai.ForeColor = Color.FromArgb(31, 41, 55);
            lblTotalModalLabel.Font = FontInter.Buat(9F);
            lblTotalModalLabel.ForeColor = Color.FromArgb(107, 114, 128);
            lblTotalModalNilai.Font = FontInter.Buat(9.5F, FontStyle.Bold);
            lblTotalModalNilai.ForeColor = Color.FromArgb(31, 41, 55);
            lblTotalLabaLabel.Font = FontInter.Buat(9F);
            lblTotalLabaLabel.ForeColor = Color.FromArgb(107, 114, 128);
            lblTotalLabaNilai.Font = FontInter.Buat(9.5F, FontStyle.Bold);
            lblTotalLabaNilai.ForeColor = Color.FromArgb(16, 149, 102);

            pnlTotalLaba.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(229, 231, 235), 1);
                e.Graphics.DrawLine(pen, 0, 0, pnlTotalLaba.Width, 0);
            };
        }

        private static void PasangGayaKartu(Panel kartu)
        {
            kartu.BackColor = Color.White;
            kartu.Paint += (s, e) =>
            {
                using var penBatas = new Pen(Color.FromArgb(229, 231, 235), 1);
                e.Graphics.DrawRectangle(penBatas, 0, 0, kartu.Width - 1, kartu.Height - 1);

                using var brushAksen = new SolidBrush(Color.FromArgb(255, 196, 0));
                e.Graphics.FillRectangle(brushAksen, 1, 1, Math.Max(0, kartu.Width - 2), 3);
            };
        }

        private static void AturTombolSub(Button tombol, string teks, bool aktif, int lebar = 110)
        {
            tombol.BackColor = aktif ? Color.FromArgb(255, 196, 0) : Color.White;
            tombol.Cursor = Cursors.Hand;
            tombol.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            tombol.FlatAppearance.BorderSize = aktif ? 0 : 1;
            tombol.FlatStyle = FlatStyle.Flat;
            tombol.Font = FontInter.Buat(9.5F, aktif ? FontStyle.Bold : FontStyle.Regular);
            tombol.ForeColor = aktif ? Color.FromArgb(31, 41, 55) : Color.FromArgb(75, 85, 99);
            tombol.Size = new Size(lebar, 32);
            tombol.Text = teks;
        }

        private void AturTataLetakResponsif()
        {
            if (nightControlBox1 != null)
            {
                nightControlBox1.Location = new Point(pnlKanan.ClientSize.Width - nightControlBox1.Width, 0);
                nightControlBox1.BringToFront();
            }

            int margin = 40;
            int lebarTersedia = pnlKanan.ClientSize.Width - (margin * 2);
            if (lebarTersedia < 500) return;
            int jarak = 20;
            int lebarKartu = (lebarTersedia - 3 * jarak) / 4;

            // 1. Header & Sub-Navigasi (Transaksi & Laba) di atas 4 kartu
            lblJudul.Location = new Point(margin, 24);
            btnSubTransaksi.Location = new Point(margin, 68);
            btnSubTransaksi.Size = new Size(110, 32);
            btnSubTransaksi.Visible = true;
            btnSubLaba.Location = new Point(margin + 118, 68);
            btnSubLaba.Size = new Size(110, 32);
            btnSubLaba.Visible = true;

            // 2. 4 Panel KPI Cards (tersusun horizontal di bawah tombol sub-navigasi)
            int yCards = 112;
            int tinggiKartu = 104;
            pnlCard1.Location = new Point(margin, yCards);
            pnlCard1.Size = new Size(lebarKartu, tinggiKartu);
            pnlCard2.Location = new Point(margin + lebarKartu + jarak, yCards);
            pnlCard2.Size = new Size(lebarKartu, tinggiKartu);
            pnlCard3.Location = new Point(margin + 2 * (lebarKartu + jarak), yCards);
            pnlCard3.Size = new Size(lebarKartu, tinggiKartu);
            pnlCard4.Location = new Point(margin + 3 * (lebarKartu + jarak), yCards);
            pnlCard4.Size = new Size(lebarKartu, tinggiKartu);

            bool isLaba = subJenisPenjualan == "laba";

            // 2b. 2 Analytics Cards (hanya tampil pada mode Transaksi)
            pnlCardTerlaris.Visible = !isLaba;
            pnlCardKasir.Visible = !isLaba;

            int yFilter;
            if (isLaba)
            {
                yFilter = 232;
            }
            else
            {
                int yAnalytics = yCards + tinggiKartu + 16;
                int tinggiAnalytics = 216;
                int lebarAnalyticsKiri = (lebarTersedia - jarak) / 2;
                int lebarAnalyticsKanan = lebarTersedia - jarak - lebarAnalyticsKiri;

                pnlCardTerlaris.Location = new Point(margin, yAnalytics);
                pnlCardTerlaris.Size = new Size(lebarAnalyticsKiri, tinggiAnalytics);

                pnlCardKasir.Location = new Point(margin + lebarAnalyticsKiri + jarak, yAnalytics);
                pnlCardKasir.Size = new Size(lebarAnalyticsKanan, tinggiAnalytics);

                lblTerlarisJudul.Location = new Point(20, 16);
                flpTerlaris.Location = new Point(20, 42);
                flpTerlaris.Size = new Size(Math.Max(100, pnlCardTerlaris.ClientSize.Width - 40), tinggiAnalytics - 54);
                lblTerlarisKosong.Location = new Point(20, 50);
                SesuaikanLebarItemFlow(flpTerlaris);

                lblKasirJudul.Location = new Point(20, 16);
                flpKasir.Location = new Point(20, 42);
                flpKasir.Size = new Size(Math.Max(100, pnlCardKasir.ClientSize.Width - 40), tinggiAnalytics - 54);
                lblKasirKosong.Location = new Point(20, 50);
                SesuaikanLebarItemFlow(flpKasir);

                yFilter = yAnalytics + tinggiAnalytics + 16;
            }

            // 3. Filter Tanggal (di bawah 2 Analytics Card pada mode Transaksi, atau di bawah 4 KPI Card pada mode Laba)
            dtpMulai.Location = new Point(margin, yFilter + 2);
            dtpMulai.Size = new Size(130, 27);
            lblSampai.Location = new Point(margin + 138, yFilter + 7);
            lblSampai.Size = new Size(28, 20);
            dtpSelesai.Location = new Point(margin + 172, yFilter + 2);
            dtpSelesai.Size = new Size(130, 27);
            btnTerapkanTanggal.Location = new Point(margin + 312, yFilter);
            btnTerapkanTanggal.Size = new Size(96, 32);

            // 4. Keterangan Perhitungan & Judul Tabel
            int yInfo = yFilter + 44;
            lblInfoHitung.Visible = isLaba;
            lblJudulTabel.Visible = isLaba;
            lblRingkasan.Visible = false;

            if (isLaba)
            {
                lblInfoHitung.Location = new Point(margin, yInfo);
                lblJudulTabel.Location = new Point(margin, yInfo + 22);
            }

            // 5. DataGridView
            int yGrid = isLaba ? (yFilter + 96) : (yFilter + 48);
            int tinggiTotal = 46;
            int tinggiGrid = Math.Max(180, pnlKanan.ClientSize.Height - yGrid - (isLaba ? (tinggiTotal + 24) : 20));

            gridLaporan.Location = new Point(margin, yGrid);
            gridLaporan.Size = new Size(lebarTersedia, tinggiGrid);
            gridLaba.Location = new Point(margin, yGrid);
            gridLaba.Size = new Size(lebarTersedia, tinggiGrid);

            gridLaba.Visible = isLaba;
            gridLaporan.Visible = !isLaba;

            // 6. Area Ringkasan Total di Bawah DataGridView
            pnlTotalLaba.Visible = isLaba;
            if (isLaba)
            {
                int yTotal = yGrid + tinggiGrid + 6;
                pnlTotalLaba.Location = new Point(margin, yTotal);
                pnlTotalLaba.Size = new Size(lebarTersedia, tinggiTotal);

                // Kolom pembagian: Kode 13%, Nama 23%, Terjual 10%, Pendapatan 18%, Total Modal 18%, Laba Kotor 18%
                int col1W = (int)(lebarTersedia * 0.13);
                int col2W = (int)(lebarTersedia * 0.23);
                int col3W = (int)(lebarTersedia * 0.10);
                int col4W = (int)(lebarTersedia * 0.18);
                int col5W = (int)(lebarTersedia * 0.18);

                int xCol3 = col1W + col2W;
                int xCol4 = xCol3 + col3W;
                int xCol5 = xCol4 + col4W;
                int xCol6 = xCol5 + col5W;

                lblTotalJudul.Location = new Point(14, 13);

                // Terjual
                lblTotalTerjualLabel.Location = new Point(Math.Max(xCol3 - 60, 200), 14);
                lblTotalTerjualNilai.Location = new Point(lblTotalTerjualLabel.Right + 4, 13);

                // Pendapatan
                lblTotalPendapatanLabel.Location = new Point(xCol4, 14);
                lblTotalPendapatanNilai.Location = new Point(lblTotalPendapatanLabel.Right + 4, 13);

                // Modal
                lblTotalModalLabel.Location = new Point(xCol5, 14);
                lblTotalModalNilai.Location = new Point(lblTotalModalLabel.Right + 4, 13);

                // Laba Kotor
                lblTotalLabaLabel.Location = new Point(xCol6, 14);
                lblTotalLabaNilai.Location = new Point(lblTotalLabaLabel.Right + 4, 13);
            }
        }

        private void SesuaikanPeran()
        {
            if (this.peran == "kasir")
            {
                btnKasir.Visible = true;
                btnRiwayat.Visible = true;
                btnGudang.Visible = false;
                btnLaporan.Visible = false;
                btnLaporanInventaris.Visible = false;
                btnPengguna.Visible = false;
                btnLogAktivitas.Visible = false;
            }
            else if (this.peran == "gudang")
            {
                btnKasir.Visible = false;
                btnRiwayat.Visible = false;
                btnGudang.Visible = true;
                btnLaporan.Visible = false;
                btnLaporanInventaris.Visible = true;
                btnPengguna.Visible = false;
                btnLogAktivitas.Visible = false;
            }
            else
            {
                btnKasir.Visible = true;
                btnRiwayat.Visible = true;
                btnGudang.Visible = true;
                btnLaporan.Visible = true;
                btnLaporanInventaris.Visible = true;
                btnPengguna.Visible = true;
                btnLogAktivitas.Visible = true;
            }

            UIHelper.SusunTombolSidebar(pnlKiri);
        }

        private void PasangEventHandlers()
        {
            btnDashboard.Click += (s, e) => Navigasi.BukaDashboard(this);
            btnKasir.Click += (s, e) => Navigasi.BukaKasir(this);
            btnKatalog.Click += (s, e) => Navigasi.BukaKatalog(this);
            btnRiwayat.Click += (s, e) => Navigasi.BukaRiwayat(this);
            btnGudang.Click += (s, e) => Navigasi.BukaGudang(this);
            btnLaporanInventaris.Click += (s, e) => Navigasi.BukaLaporanInventaris(this);
            btnPengguna.Click += (s, e) => Navigasi.BukaPengguna(this);
            btnLogAktivitas.Click += (s, e) => Navigasi.BukaLogAktivitas(this);
            btnGantiSandi.Click += (s, e) => Navigasi.BukaGantiKataSandi(this);
            btnKeluar.Click += (s, e) => Navigasi.KonfirmasiDanKeluar(this);
            btnSubTransaksi.Click += (s, e) => UbahSubJenisPenjualan("transaksi");
            btnSubLaba.Click += (s, e) => UbahSubJenisPenjualan("laba");
            btnTerapkanTanggal.Click += (s, e) => TerapkanLaporan();
            gridLaba.ColumnHeaderMouseClick += GridLaba_ColumnHeaderMouseClick;

            dtpMulai.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; TerapkanLaporan(); } };
            dtpSelesai.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; TerapkanLaporan(); } };
        }

        public void MuatDataLaporan()
        {
            try
            {
                semuaTransaksi = PenyimpananData.BacaTransaksi();
                semuaDetail = PenyimpananData.BacaDetailTransaksi();
                TerapkanLaporan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data laporan: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UbahSubJenisPenjualan(string subJenis)
        {
            subJenisPenjualan = subJenis;
            AturTombolSub(btnSubTransaksi, "Transaksi", subJenis == "transaksi", 110);
            AturTombolSub(btnSubLaba, "Laba", subJenis == "laba", 110);
            TerapkanLaporan();
        }

        private void TerapkanLaporan()
        {
            if (dtpMulai.Value.Date > dtpSelesai.Value.Date)
            {
                MessageBox.Show("Tanggal mulai tidak boleh setelah tanggal selesai.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                semuaTransaksi = PenyimpananData.BacaTransaksi();
                semuaDetail = PenyimpananData.BacaDetailTransaksi();
            }
            catch
            {
            }

            pnlKanan.SuspendLayout();
            try
            {
                AturTataLetakResponsif();

                if (subJenisPenjualan == "laba")
                {
                    TerapkanLaporanLaba();
                }
                else
                {
                    TerapkanLaporanPenjualan();
                }
            }
            finally
            {
                pnlKanan.ResumeLayout();
            }
        }

        private void TerapkanLaporanPenjualan()
        {
            lblJudul.Text = "Laporan Penjualan";
            lblCard1Judul.Text = "TOTAL PENDAPATAN";
            lblCard2Judul.Text = "JUMLAH TRANSAKSI";
            lblCard3Judul.Text = "ITEM TERJUAL";
            lblCard4Judul.Text = "RATA-RATA TRANSAKSI";

            DateTime mulai = dtpMulai.Value.Date;
            DateTime selesai = dtpSelesai.Value.Date.AddDays(1).AddTicks(-1);
            List<Transaksi> transaksi = semuaTransaksi.Where(data => data.Tanggal >= mulai && data.Tanggal <= selesai).ToList();
            HashSet<string> idTransaksi = transaksi.Select(data => data.Id).ToHashSet();
            List<DetailTransaksi> detail = semuaDetail.Where(data => idTransaksi.Contains(data.IdTransaksi)).ToList();
            int totalOmzet = transaksi.Sum(data => data.Total);
            int totalTransaksi = transaksi.Count;
            int totalItem = detail.Sum(data => data.Jumlah);
            int rataRata = totalTransaksi > 0 ? totalOmzet / totalTransaksi : 0;

            lblOmzetNilai.Text = "Rp " + totalOmzet.ToString("N0", BudayaIndonesia);
            lblOmzetNilai.ForeColor = Color.FromArgb(16, 149, 102);
            lblTrxNilai.Text = totalTransaksi.ToString("N0", BudayaIndonesia);
            lblTrxNilai.ForeColor = Color.FromArgb(31, 41, 55);
            lblItemTerjualNilai.Text = totalItem.ToString("N0", BudayaIndonesia);
            lblItemTerjualNilai.ForeColor = Color.FromArgb(31, 41, 55);
            lblRataNilai.Text = "Rp " + rataRata.ToString("N0", BudayaIndonesia);
            lblRataNilai.ForeColor = Color.FromArgb(31, 41, 55);

            PerbaruiKartuBarangTerlaris(detail);
            PerbaruiKartuPenjualanKasir(transaksi);

            AturKolomLaporan();
            gridLaporan.DataSource = transaksi.OrderByDescending(data => data.Tanggal).Select(data => new { Faktur = data.Id, Waktu = data.Tanggal.ToString("dd/MM/yyyy HH:mm"), data.Kasir, Item = detail.Where(rincian => rincian.IdTransaksi == data.Id).Sum(rincian => rincian.Jumlah), Total = "Rp " + data.Total.ToString("N0", BudayaIndonesia) }).ToList();
        }

        private void PerbaruiKartuBarangTerlaris(List<DetailTransaksi> detail)
        {
            flpTerlaris.SuspendLayout();
            try
            {
                BersihkanKontrolFlow(flpTerlaris);

                var terlaris = detail
                    .GroupBy(data => new { data.KodeBarang, data.NamaBarang })
                    .Select(grup => new
                    {
                        grup.Key.KodeBarang,
                        grup.Key.NamaBarang,
                        Jumlah = grup.Sum(data => data.Jumlah)
                    })
                    .OrderByDescending(item => item.Jumlah)
                    .ThenBy(item => item.KodeBarang)
                    .Take(5)
                    .ToList();

                lblTerlarisKosong.Visible = terlaris.Count == 0;
                flpTerlaris.Visible = terlaris.Count > 0;

                for (int i = 0; i < terlaris.Count; i++)
                {
                    var data = terlaris[i];
                    var baris = new ItemBarangTerlaris();
                    baris.SetData(
                        i + 1,
                        data.NamaBarang,
                        data.KodeBarang,
                        data.Jumlah.ToString("N0", BudayaIndonesia) + " pcs",
                        i < terlaris.Count - 1);
                    flpTerlaris.Controls.Add(baris);
                }
            }
            finally
            {
                flpTerlaris.ResumeLayout();
            }

            SesuaikanLebarItemFlow(flpTerlaris);
        }

        private void PerbaruiKartuPenjualanKasir(List<Transaksi> transaksi)
        {
            flpKasir.SuspendLayout();
            try
            {
                BersihkanKontrolFlow(flpKasir);

                var perKasir = transaksi
                    .GroupBy(data => data.Kasir)
                    .Select(grup => new
                    {
                        Kasir = grup.Key,
                        Total = grup.Sum(data => data.Total)
                    })
                    .OrderByDescending(item => item.Total)
                    .ThenBy(item => item.Kasir)
                    .ToList();

                lblKasirKosong.Visible = perKasir.Count == 0;
                flpKasir.Visible = perKasir.Count > 0;

                for (int i = 0; i < perKasir.Count; i++)
                {
                    var data = perKasir[i];
                    var baris = new ItemPenjualanKasir();
                    baris.SetData(
                        data.Kasir,
                        "Rp " + data.Total.ToString("N0", BudayaIndonesia),
                        i < perKasir.Count - 1);
                    flpKasir.Controls.Add(baris);
                }
            }
            finally
            {
                flpKasir.ResumeLayout();
            }

            SesuaikanLebarItemFlow(flpKasir);
        }

        private static void BersihkanKontrolFlow(FlowLayoutPanel panel)
        {
            while (panel.Controls.Count > 0)
            {
                Control ctrl = panel.Controls[0];
                panel.Controls.RemoveAt(0);
                ctrl.Dispose();
            }
        }

        private static void SesuaikanLebarItemFlow(FlowLayoutPanel panel)
        {
            int lebarItem = Math.Max(120, panel.ClientSize.Width - 2);
            foreach (Control ctrl in panel.Controls)
            {
                ctrl.Width = lebarItem;
            }
        }

        private void TerapkanLaporanLaba()
        {
            lblJudul.Text = "Analisis Laba Penjualan";
            lblCard1Judul.Text = "TOTAL PENDAPATAN";
            lblCard2Judul.Text = "TOTAL MODAL";
            lblCard3Judul.Text = "LABA KOTOR";
            lblCard4Judul.Text = "MARGIN LABA";

            DateTime mulai = dtpMulai.Value.Date;
            DateTime selesai = dtpSelesai.Value.Date.AddDays(1).AddTicks(-1);

            List<Transaksi> transaksi = semuaTransaksi.Where(data => data.Tanggal >= mulai && data.Tanggal <= selesai).ToList();
            HashSet<string> idTransaksi = transaksi.Select(data => data.Id).ToHashSet();
            List<DetailTransaksi> detail = semuaDetail.Where(data => idTransaksi.Contains(data.IdTransaksi)).ToList();

            var rincianProduk = detail
                .GroupBy(d => new { d.KodeBarang, d.NamaBarang })
                .Select(g =>
                {
                    int terjual = g.Sum(x => x.Jumlah);
                    long pendapatan = g.Sum(x => (long)x.Harga * x.Jumlah);
                    long totalModal = g.Sum(x => (long)x.HargaBeli * x.Jumlah);
                    long labaKotor = pendapatan - totalModal;

                    return new ItemLabaProduk
                    {
                        Kode = g.Key.KodeBarang,
                        Nama = g.Key.NamaBarang,
                        Terjual = terjual.ToString("N0", BudayaIndonesia),
                        Pendapatan = "Rp " + pendapatan.ToString("N0", BudayaIndonesia),
                        TotalModal = "Rp " + totalModal.ToString("N0", BudayaIndonesia),
                        LabaKotor = "Rp " + labaKotor.ToString("N0", BudayaIndonesia),
                        RawTerjual = terjual,
                        RawPendapatan = pendapatan,
                        RawModal = totalModal,
                        RawLaba = labaKotor
                    };
                })
                .ToList();

            long totalPendapatan = rincianProduk.Sum(x => x.RawPendapatan);
            long totalModal = rincianProduk.Sum(x => x.RawModal);
            long totalLabaKotor = totalPendapatan - totalModal;
            int totalTerjual = rincianProduk.Sum(x => x.RawTerjual);

            // Update 4 KPI Cards
            lblOmzetNilai.Text = "Rp " + totalPendapatan.ToString("N0", BudayaIndonesia);
            lblOmzetNilai.ForeColor = Color.FromArgb(31, 41, 55);

            lblTrxNilai.Text = "Rp " + totalModal.ToString("N0", BudayaIndonesia);
            lblTrxNilai.ForeColor = Color.FromArgb(31, 41, 55);

            lblItemTerjualNilai.Text = "Rp " + totalLabaKotor.ToString("N0", BudayaIndonesia);
            lblItemTerjualNilai.ForeColor = Color.FromArgb(16, 149, 102); // Hijau untuk Laba Kotor

            if (totalPendapatan > 0)
            {
                double margin = ((double)totalLabaKotor / totalPendapatan) * 100.0;
                lblRataNilai.Text = margin.ToString("0.0", BudayaIndonesia) + "%";
            }
            else
            {
                lblRataNilai.Text = "0,0%";
            }
            lblRataNilai.ForeColor = Color.FromArgb(31, 41, 55);

            // Update Ringkasan Total Keseluruhan di Bawah DataGridView
            lblTotalTerjualNilai.Text = totalTerjual.ToString("N0", BudayaIndonesia);
            lblTotalPendapatanNilai.Text = "Rp " + totalPendapatan.ToString("N0", BudayaIndonesia);
            lblTotalModalNilai.Text = "Rp " + totalModal.ToString("N0", BudayaIndonesia);
            lblTotalLabaNilai.Text = "Rp " + totalLabaKotor.ToString("N0", BudayaIndonesia);

            daftarLabaSaatIni = rincianProduk;
            UrutkanDanTampilkanDataLaba();
        }

        private void GridLaba_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.ColumnIndex >= gridLaba.Columns.Count) return;
            if (daftarLabaSaatIni == null || daftarLabaSaatIni.Count == 0) return;

            var kolom = gridLaba.Columns[e.ColumnIndex];
            if (kolom == colLabaKotor)
            {
                if (kolomUrutLaba == "LabaKotor")
                    urutanSortLaba = (urutanSortLaba == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
                else
                {
                    kolomUrutLaba = "LabaKotor";
                    urutanSortLaba = SortOrder.Descending;
                }
                UrutkanDanTampilkanDataLaba();
            }
            else if (kolom == colLabaPendapatan)
            {
                if (kolomUrutLaba == "Pendapatan")
                    urutanSortLaba = (urutanSortLaba == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
                else
                {
                    kolomUrutLaba = "Pendapatan";
                    urutanSortLaba = SortOrder.Descending;
                }
                UrutkanDanTampilkanDataLaba();
            }
            else if (kolom == colLabaModal)
            {
                if (kolomUrutLaba == "TotalModal")
                    urutanSortLaba = (urutanSortLaba == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
                else
                {
                    kolomUrutLaba = "TotalModal";
                    urutanSortLaba = SortOrder.Descending;
                }
                UrutkanDanTampilkanDataLaba();
            }
            else if (kolom == colLabaTerjual)
            {
                if (kolomUrutLaba == "Terjual")
                    urutanSortLaba = (urutanSortLaba == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
                else
                {
                    kolomUrutLaba = "Terjual";
                    urutanSortLaba = SortOrder.Descending;
                }
                UrutkanDanTampilkanDataLaba();
            }
            else if (kolom == colLabaNama)
            {
                if (kolomUrutLaba == "Nama")
                    urutanSortLaba = (urutanSortLaba == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
                else
                {
                    kolomUrutLaba = "Nama";
                    urutanSortLaba = SortOrder.Ascending;
                }
                UrutkanDanTampilkanDataLaba();
            }
            else if (kolom == colLabaKode)
            {
                if (kolomUrutLaba == "Kode")
                    urutanSortLaba = (urutanSortLaba == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
                else
                {
                    kolomUrutLaba = "Kode";
                    urutanSortLaba = SortOrder.Ascending;
                }
                UrutkanDanTampilkanDataLaba();
            }
        }

        private void UrutkanDanTampilkanDataLaba()
        {
            if (daftarLabaSaatIni == null || daftarLabaSaatIni.Count == 0) return;

            IEnumerable<ItemLabaProduk> terurut;
            switch (kolomUrutLaba)
            {
                case "LabaKotor":
                    terurut = (urutanSortLaba == SortOrder.Ascending)
                        ? daftarLabaSaatIni.OrderBy(x => x.RawLaba).ThenBy(x => x.Kode)
                        : daftarLabaSaatIni.OrderByDescending(x => x.RawLaba).ThenBy(x => x.Kode);
                    break;
                case "Pendapatan":
                    terurut = (urutanSortLaba == SortOrder.Ascending)
                        ? daftarLabaSaatIni.OrderBy(x => x.RawPendapatan)
                        : daftarLabaSaatIni.OrderByDescending(x => x.RawPendapatan);
                    break;
                case "TotalModal":
                    terurut = (urutanSortLaba == SortOrder.Ascending)
                        ? daftarLabaSaatIni.OrderBy(x => x.RawModal)
                        : daftarLabaSaatIni.OrderByDescending(x => x.RawModal);
                    break;
                case "Terjual":
                    terurut = (urutanSortLaba == SortOrder.Ascending)
                        ? daftarLabaSaatIni.OrderBy(x => x.RawTerjual)
                        : daftarLabaSaatIni.OrderByDescending(x => x.RawTerjual);
                    break;
                case "Nama":
                    terurut = (urutanSortLaba == SortOrder.Ascending)
                        ? daftarLabaSaatIni.OrderBy(x => x.Nama)
                        : daftarLabaSaatIni.OrderByDescending(x => x.Nama);
                    break;
                case "Kode":
                    terurut = (urutanSortLaba == SortOrder.Ascending)
                        ? daftarLabaSaatIni.OrderBy(x => x.Kode)
                        : daftarLabaSaatIni.OrderByDescending(x => x.Kode);
                    break;
                default:
                    terurut = daftarLabaSaatIni.OrderByDescending(x => x.RawLaba);
                    break;
            }

            daftarLabaSaatIni = terurut.ToList();
            gridLaba.DataSource = null;
            gridLaba.DataSource = daftarLabaSaatIni;

            foreach (DataGridViewColumn col in gridLaba.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            if (kolomUrutLaba == "LabaKotor") colLabaKotor.HeaderCell.SortGlyphDirection = urutanSortLaba;
            else if (kolomUrutLaba == "Pendapatan") colLabaPendapatan.HeaderCell.SortGlyphDirection = urutanSortLaba;
            else if (kolomUrutLaba == "TotalModal") colLabaModal.HeaderCell.SortGlyphDirection = urutanSortLaba;
            else if (kolomUrutLaba == "Terjual") colLabaTerjual.HeaderCell.SortGlyphDirection = urutanSortLaba;
            else if (kolomUrutLaba == "Nama") colLabaNama.HeaderCell.SortGlyphDirection = urutanSortLaba;
            else if (kolomUrutLaba == "Kode") colLabaKode.HeaderCell.SortGlyphDirection = urutanSortLaba;
        }

        private void AturKolomLaporan()
        {
            gridLaporan.DataSource = null;
            gridLaporan.Columns.Clear();
            gridLaporan.AutoGenerateColumns = true;
        }
    }
}
