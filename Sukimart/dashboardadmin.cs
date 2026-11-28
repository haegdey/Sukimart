using System.Drawing.Drawing2D;
using System.Globalization;
using System.Reflection;

namespace Sukimart
{
    public partial class FormDashboardAdmin : Form
    {
        private static readonly CultureInfo BudayaIndonesia = new CultureInfo("id-ID");
        private readonly List<(DateTime Tanggal, long Total)> dataGrafik7Hari = new List<(DateTime, long)>();

        public FormDashboardAdmin()
        {
            InitializeComponent();
            UIHelper.InisialisasiForm(this);
            AktifkanBebasFlicker();
            MaximizeBox = true;

            btnKasir.Click += btnKasir_Click;
            btnGudang.Click += btnGudang_Click;
            btnLaporan.Click += btnLaporan_Click;
            btnLaporanInventaris.Click += (s, e) => Navigasi.BukaLaporanInventaris(this);
            btnPengguna.Click += (s, e) => Navigasi.BukaPengguna(this);
            btnLogAktivitas.Click += (s, e) => Navigasi.BukaLogAktivitas(this);
            btnGantiSandi.Click += (s, e) => Navigasi.BukaGantiKataSandi(this);
            btnLihatInventaris.Click += (s, e) => Navigasi.BukaLaporanInventaris(this);
            btnLihatSemuaTransaksi.Click += (s, e) => Navigasi.BukaRiwayat(this);
            btnLihatPengajuan.Click += (s, e) => Navigasi.BukaGudangOpname(this);

            pnlCanvasGrafik.Paint += PnlCanvasGrafik_Paint;
            pnlCanvasGrafik.Resize += (s, e) => pnlCanvasGrafik.Invalidate();

            TerapkanGayaKartuDashboard();
            SesuaikanPeran();

            pnlKanan.Resize += (s, e) => AturTataLetakResponsif();
            AturTataLetakResponsif();

            IsiDashboard();
        }

        private void AktifkanBebasFlicker()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            try
            {
                var flag = BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic;
                Panel[] panels = { pnlKanan, pnlTerjual, pnlLaba, pnlTransaksi, pnlStok, pnlGrafik, pnlCanvasGrafik, pnlPeringatan, pnlTransaksiTerbaru, pnlOpname, pnlInfoInventaris };
                foreach (Panel p in panels)
                {
                    typeof(Panel).InvokeMember("DoubleBuffered", flag, null, p, new object[] { true });
                }

                typeof(FlowLayoutPanel).InvokeMember("DoubleBuffered", flag, null, flpPeringatan, new object[] { true });
                typeof(DataGridView).InvokeMember("DoubleBuffered", flag, null, dataGridView1, new object[] { true });
            }
            catch
            {
            }
        }

        private void TerapkanGayaKartuDashboard()
        {
            pnlKiri.BackColor = UIHelper.WarnaPutih;
            pnlKanan.BackColor = UIHelper.WarnaLatar;

            PanelKartu[] kartu = { pnlTerjual, pnlLaba, pnlTransaksi, pnlStok, pnlGrafik, pnlPeringatan, pnlTransaksiTerbaru, pnlOpname, pnlInfoInventaris };
            foreach (PanelKartu p in kartu)
            {
                p.BackColor = UIHelper.WarnaPutih;
                p.WarnaAksen = UIHelper.WarnaKuning;
                p.WarnaGaris = UIHelper.WarnaGaris;
            }

            FontInter.Terapkan(this);
        }

        private void SesuaikanPeran()
        {
            if (Navigasi.PeranAktif == "gudang")
            {
                btnKasir.Visible = false;
                btnRiwayat.Visible = false;
                btnLaporan.Visible = false;
                btnLaporanInventaris.Visible = true;
                btnPengguna.Visible = false;
                btnLogAktivitas.Visible = false;
                btnGudang.Visible = true;
                btnKatalog.Visible = true;
                lblJudul.Text = "Dashboard Gudang";
                btnLihatPengajuan.Text = "Kelola Opname";
            }
            else if (Navigasi.PeranAktif == "kasir")
            {
                btnGudang.Visible = false;
                btnLaporan.Visible = false;
                btnLaporanInventaris.Visible = false;
                btnPengguna.Visible = false;
                btnLogAktivitas.Visible = false;
                btnKasir.Visible = true;
                btnKatalog.Visible = true;
                btnRiwayat.Visible = true;
                lblJudul.Text = "Dashboard Kasir";
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
                lblJudul.Text = "Dashboard Admin";
                btnLihatPengajuan.Text = "Lihat Pengajuan";
            }

            UIHelper.SusunTombolSidebar(pnlKiri, btnDashboard);
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
            lblJudul.Location = new Point(margin, 28);
            lblTanggal.Location = new Point(margin + lebarTersedia - lblTanggal.Width, 40);

            // Baris 1: 4 KPI Cards
            int yCards = 88;
            int tinggiKpi = 104;
            int lebarKpi = (lebarTersedia - 3 * jarak) / 4;
            int lebarKpiAkhir = lebarTersedia - 3 * (lebarKpi + jarak);

            pnlTerjual.Location = new Point(margin, yCards);
            pnlTerjual.Size = new Size(lebarKpi, tinggiKpi);

            pnlLaba.Location = new Point(margin + lebarKpi + jarak, yCards);
            pnlLaba.Size = new Size(lebarKpi, tinggiKpi);

            pnlTransaksi.Location = new Point(margin + 2 * (lebarKpi + jarak), yCards);
            pnlTransaksi.Size = new Size(lebarKpi, tinggiKpi);

            pnlStok.Location = new Point(margin + 3 * (lebarKpi + jarak), yCards);
            pnlStok.Size = new Size(lebarKpiAkhir, tinggiKpi);

            int yKonten = yCards + tinggiKpi + 18;
            int tinggiTersisa = Math.Max(360, pnlKanan.ClientSize.Height - yKonten - 24);

            bool isGudang = Navigasi.PeranAktif == "gudang";
            pnlGrafik.Visible = !isGudang;
            pnlTransaksiTerbaru.Visible = !isGudang;
            pnlInfoInventaris.Visible = isGudang;

            if (isGudang)
            {
                int lebarKiri = (int)(lebarTersedia * 0.56);
                int lebarKanan = lebarTersedia - jarak - lebarKiri;
                int xKanan = margin + lebarKiri + jarak;

                // Kiri: Peringatan Stok mengisi seluruh tinggi area bawah
                pnlPeringatan.Location = new Point(margin, yKonten);
                pnlPeringatan.Size = new Size(lebarKiri, tinggiTersisa);
                AturIsiPanelPeringatan();

                // Kanan atas: Ringkasan Stok Opname
                int tinggiOpname = 252;
                pnlOpname.Location = new Point(xKanan, yKonten);
                pnlOpname.Size = new Size(lebarKanan, tinggiOpname);
                AturIsiPanelOpname();

                // Kanan bawah: Informasi Inventaris
                int yInfoInv = yKonten + tinggiOpname + 18;
                int tinggiInfoInv = Math.Max(220, tinggiTersisa - tinggiOpname - 18);
                pnlInfoInventaris.Location = new Point(xKanan, yInfoInv);
                pnlInfoInventaris.Size = new Size(lebarKanan, tinggiInfoInv);
                AturIsiPanelInfoInventaris();
            }
            else
            {
                int lebarKiri = (int)(lebarTersedia * 0.58);
                int lebarKanan = lebarTersedia - jarak - lebarKiri;
                int xKanan = margin + lebarKiri + jarak;

                int tinggiTengah = Math.Max(230, (int)((tinggiTersisa - 18) * 0.48));
                int yBawah = yKonten + tinggiTengah + 18;
                int tinggiBawah = Math.Max(230, tinggiTersisa - tinggiTengah - 18);

                // Baris 2: Grafik Penjualan (Kiri) & Peringatan Stok (Kanan)
                pnlGrafik.Location = new Point(margin, yKonten);
                pnlGrafik.Size = new Size(lebarKiri, tinggiTengah);
                lblJudulGrafik.Location = new Point(20, 16);
                lblSubGrafik.Location = new Point(pnlGrafik.ClientSize.Width - lblSubGrafik.Width - 20, 18);
                pnlCanvasGrafik.Location = new Point(16, 44);
                pnlCanvasGrafik.Size = new Size(Math.Max(100, pnlGrafik.ClientSize.Width - 32), Math.Max(100, pnlGrafik.ClientSize.Height - 56));

                pnlPeringatan.Location = new Point(xKanan, yKonten);
                pnlPeringatan.Size = new Size(lebarKanan, tinggiTengah);
                AturIsiPanelPeringatan();

                // Baris 3: Transaksi Terbaru (Kiri) & Ringkasan Stok Opname (Kanan)
                pnlTransaksiTerbaru.Location = new Point(margin, yBawah);
                pnlTransaksiTerbaru.Size = new Size(lebarKiri, tinggiBawah);
                lblTransaksiTerbaru.Location = new Point(20, 16);
                btnLihatSemuaTransaksi.Location = new Point(pnlTransaksiTerbaru.ClientSize.Width - btnLihatSemuaTransaksi.Width - 20, 12);
                dataGridView1.Location = new Point(20, 48);
                dataGridView1.Size = new Size(Math.Max(100, pnlTransaksiTerbaru.ClientSize.Width - 40), Math.Max(100, pnlTransaksiTerbaru.ClientSize.Height - 64));

                pnlOpname.Location = new Point(xKanan, yBawah);
                pnlOpname.Size = new Size(lebarKanan, tinggiBawah);
                AturIsiPanelOpname();
            }
        }

        private void AturIsiPanelPeringatan()
        {
            lblPeringatan.Location = new Point(20, 16);
            btnLihatInventaris.Location = new Point(pnlPeringatan.ClientSize.Width - btnLihatInventaris.Width - 20, 12);
            flpPeringatan.Location = new Point(20, 48);
            flpPeringatan.Size = new Size(Math.Max(100, pnlPeringatan.ClientSize.Width - 40), Math.Max(100, pnlPeringatan.ClientSize.Height - 64));
            lblPeringatanKosong.Location = new Point(20, 56);
            SesuaikanLebarItemFlow(flpPeringatan);
        }

        private void AturIsiPanelOpname()
        {
            lblJudulOpname.Location = new Point(20, 16);
            btnLihatPengajuan.Location = new Point(pnlOpname.ClientSize.Width - btnLihatPengajuan.Width - 20, 12);

            int lebarBaris = Math.Max(120, pnlOpname.ClientSize.Width - 40);
            int tinggiTersedia = Math.Max(160, pnlOpname.ClientSize.Height - 72);
            int tinggiBaris = Math.Min(60, Math.Max(42, (tinggiTersedia - 24) / 3));
            int jarakBaris = 12;
            int y = 56;

            Panel[] barisOpname = { pnlOpnameMenunggu, pnlOpnameRevisi, pnlOpnameDisetujui };
            foreach (Panel p in barisOpname)
            {
                p.Location = new Point(20, y);
                p.Size = new Size(lebarBaris, tinggiBaris);
                y += tinggiBaris + jarakBaris;
            }
        }

        private void AturIsiPanelInfoInventaris()
        {
            lblJudulInfoInventaris.Location = new Point(20, 16);
            int lebarBaris = Math.Max(120, pnlInfoInventaris.ClientSize.Width - 40);
            int tinggiTersedia = Math.Max(160, pnlInfoInventaris.ClientSize.Height - 64);
            int jarakBaris = 8;
            int tinggiBaris = Math.Min(40, Math.Max(28, (tinggiTersedia - 4 * jarakBaris) / 5));
            int y = 48;

            Panel[] barisInv = { pnlInvHabis, pnlInvKritis, pnlInvMinimum, pnlInvMasuk, pnlInvKeluar };
            foreach (Panel p in barisInv)
            {
                p.Location = new Point(20, y);
                p.Size = new Size(lebarBaris, tinggiBaris);
                y += tinggiBaris + jarakBaris;
            }
        }

        private void IsiDashboard()
        {
            lblTanggal.Text = DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            AturTataLetakResponsif();

            try
            {
                List<Barang> daftarBarang = PenyimpananData.BacaBarang();
                List<SesiOpname> daftarSesiOpname = PenyimpananData.BacaSesiOpname();

                IsiPeringatanStok(daftarBarang);
                IsiRingkasanOpname(daftarSesiOpname);

                if (Navigasi.PeranAktif == "gudang")
                {
                    List<BarangMasuk> daftarMasuk = PenyimpananData.BacaBarangMasuk();
                    List<BarangKeluar> daftarKeluar = PenyimpananData.BacaBarangKeluar();
                    IsiKpiGudang(daftarBarang, daftarSesiOpname);
                    IsiInfoInventaris(daftarBarang, daftarMasuk, daftarKeluar);
                }
                else
                {
                    List<Transaksi> daftarTransaksi = PenyimpananData.BacaTransaksi();
                    List<DetailTransaksi> daftarDetail = PenyimpananData.BacaDetailTransaksi();

                    IsiKpiAdmin(daftarTransaksi, daftarDetail, daftarBarang);
                    IsiGrafik7Hari(daftarTransaksi);
                    IsiTransaksiTerbaru(daftarTransaksi);
                }
            }
            catch (Exception ex)
            {
                lblNilaiTransaksi.Text = "0";
                lblNilaiPenjualan.Text = Navigasi.PeranAktif == "gudang" ? "0" : "Rp 0";
                lblNilaiLaba.Text = Navigasi.PeranAktif == "gudang" ? "0" : "Rp 0";
                lblNilaiStok.Text = "0";
                BersihkanKontrolFlow(flpPeringatan);
                dataGridView1.DataSource = null;

                MessageBox.Show(
                    "Data dashboard gagal dibaca. Pastikan file JSON tersedia dan formatnya benar.\n\n" + ex.Message,
                    "Kesalahan Data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void IsiKpiAdmin(List<Transaksi> daftarTransaksi, List<DetailTransaksi> daftarDetail, List<Barang> daftarBarang)
        {
            DateTime hariIni = DateTime.Today;
            List<Transaksi> transaksiHariIni = daftarTransaksi
                .Where(t => t.Tanggal.Date == hariIni)
                .ToList();

            HashSet<string> idTransaksiHariIni = transaksiHariIni
                .Select(t => t.Id)
                .ToHashSet();

            List<DetailTransaksi> detailHariIni = daftarDetail
                .Where(d => idTransaksiHariIni.Contains(d.IdTransaksi))
                .ToList();

            int totalPenjualan = transaksiHariIni.Sum(t => t.Total);
            long totalPendapatanDetail = detailHariIni.Sum(d => (long)d.Harga * d.Jumlah);
            long totalModalDetail = detailHariIni.Sum(d => (long)d.HargaBeli * d.Jumlah);
            long labaHariIni = totalPendapatanDetail - totalModalDetail;
            int jumlahPerhatian = daftarBarang
                .Where(b => b.Aktif)
                .Count(b => FormLaporanInventaris.TentukanStatusStok(b.Stok, b.StokMinimum) != "AMAN");

            lblPenjualan.Text = "PENJUALAN HARI INI";
            lblNilaiPenjualan.Text = "Rp " + totalPenjualan.ToString("N0", BudayaIndonesia);
            lblNilaiPenjualan.ForeColor = UIHelper.WarnaAman;

            lblLaba.Text = "LABA HARI INI";
            lblNilaiLaba.Text = "Rp " + labaHariIni.ToString("N0", BudayaIndonesia);
            lblNilaiLaba.ForeColor = labaHariIni >= 0 ? UIHelper.WarnaAman : UIHelper.WarnaHabis;

            lblTransaksiHariIni.Text = "TRANSAKSI HARI INI";
            lblNilaiTransaksi.Text = transaksiHariIni.Count.ToString("N0", BudayaIndonesia);
            lblNilaiTransaksi.ForeColor = UIHelper.WarnaTeksUtama;

            lblStok.Text = "STOK PERLU PERHATIAN";
            lblNilaiStok.Text = jumlahPerhatian.ToString("N0", BudayaIndonesia);
            lblNilaiStok.ForeColor = jumlahPerhatian > 0 ? UIHelper.WarnaHabis : UIHelper.WarnaAman;
        }

        private void IsiKpiGudang(List<Barang> daftarBarang, List<SesiOpname> daftarSesiOpname)
        {
            List<Barang> produkAktif = daftarBarang.Where(b => b.Aktif).ToList();
            int totalProdukAktif = produkAktif.Count;
            int stokAman = produkAktif.Count(b => FormLaporanInventaris.TentukanStatusStok(b.Stok, b.StokMinimum) == "AMAN");
            int stokPerhatian = produkAktif.Count(b => FormLaporanInventaris.TentukanStatusStok(b.Stok, b.StokMinimum) != "AMAN");
            int sesiAktif = daftarSesiOpname.Count(s =>
                s.Status == "Draft" || s.Status == "Menunggu Persetujuan" || s.Status == "Perlu Revisi");

            lblPenjualan.Text = "TOTAL PRODUK AKTIF";
            lblNilaiPenjualan.Text = totalProdukAktif.ToString("N0", BudayaIndonesia);
            lblNilaiPenjualan.ForeColor = UIHelper.WarnaTeksUtama;

            lblLaba.Text = "STOK AMAN";
            lblNilaiLaba.Text = stokAman.ToString("N0", BudayaIndonesia);
            lblNilaiLaba.ForeColor = UIHelper.WarnaAman;

            lblTransaksiHariIni.Text = "STOK PERLU PERHATIAN";
            lblNilaiTransaksi.Text = stokPerhatian.ToString("N0", BudayaIndonesia);
            lblNilaiTransaksi.ForeColor = stokPerhatian > 0 ? UIHelper.WarnaHabis : UIHelper.WarnaAman;

            lblStok.Text = "SESI OPNAME AKTIF";
            lblNilaiStok.Text = sesiAktif.ToString("N0", BudayaIndonesia);
            lblNilaiStok.ForeColor = sesiAktif > 0 ? UIHelper.WarnaMinimum : UIHelper.WarnaTeksUtama;
        }

        private void IsiGrafik7Hari(List<Transaksi> daftarTransaksi)
        {
            dataGrafik7Hari.Clear();

            DateTime akhir = DateTime.Today;
            DateTime awal = akhir.AddDays(-6);

            Dictionary<DateTime, long> perTanggal = daftarTransaksi
                .Where(t => t.Tanggal.Date >= awal && t.Tanggal.Date <= akhir)
                .GroupBy(t => t.Tanggal.Date)
                .ToDictionary(g => g.Key, g => g.Sum(t => (long)t.Total));

            for (int i = 0; i < 7; i++)
            {
                DateTime tgl = awal.AddDays(i);
                long total = perTanggal.TryGetValue(tgl, out long nilai) ? nilai : 0;
                dataGrafik7Hari.Add((tgl, total));
            }

            lblSubGrafik.Text = $"{awal:dd MMM} – {akhir:dd MMM yyyy}";
            pnlCanvasGrafik.Invalidate();
        }

        private void PnlCanvasGrafik_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(UIHelper.WarnaPutih);

            if (!pnlCanvasGrafik.Visible || dataGrafik7Hari.Count == 0 || pnlCanvasGrafik.ClientSize.Width < 180 || pnlCanvasGrafik.ClientSize.Height < 100)
                return;

            int padKiri = 74;
            int padKanan = 24;
            int padAtas = 22;
            int padBawah = 30;

            Rectangle areaPlot = new Rectangle(
                padKiri,
                padAtas,
                Math.Max(40, pnlCanvasGrafik.ClientSize.Width - padKiri - padKanan),
                Math.Max(40, pnlCanvasGrafik.ClientSize.Height - padAtas - padBawah));

            long maxNilai = dataGrafik7Hari.Max(x => x.Total);
            long skalaMax = maxNilai > 0 ? HitungSkalaAtas(maxNilai) : 100000;

            Font fontAxis = FontInter.Buat(8F, FontStyle.Regular);
            Font fontNilai = FontInter.Buat(7.5F, FontStyle.Bold);
            using var brushTeksAxis = new SolidBrush(UIHelper.WarnaTeksSekunder);
            using var brushTeksGelap = new SolidBrush(UIHelper.WarnaTeksUtama);
            using var penGrid = new Pen(UIHelper.WarnaHeaderTabel, 1);
            using var penAxis = new Pen(UIHelper.WarnaGaris, 1);

            // Garis grid horizontal (4 langkah)
            int jumlahGaris = 4;
            for (int i = 0; i <= jumlahGaris; i++)
            {
                float rasio = (float)i / jumlahGaris;
                float y = areaPlot.Bottom - rasio * areaPlot.Height;
                g.DrawLine(i == 0 ? penAxis : penGrid, areaPlot.Left, y, areaPlot.Right, y);

                long nilaiLabel = (long)(skalaMax * rasio);
                string teksY = FormatSingkatRupiah(nilaiLabel);
                SizeF ukuranY = g.MeasureString(teksY, fontAxis);
                g.DrawString(teksY, fontAxis, brushTeksAxis, areaPlot.Left - ukuranY.Width - 8, y - ukuranY.Height / 2);
            }

            PointF[] titik = new PointF[dataGrafik7Hari.Count];
            float langkahX = dataGrafik7Hari.Count > 1 ? (float)areaPlot.Width / (dataGrafik7Hari.Count - 1) : 0;

            for (int i = 0; i < dataGrafik7Hari.Count; i++)
            {
                float x = areaPlot.Left + i * langkahX;
                float rasio = skalaMax > 0 ? (float)dataGrafik7Hari[i].Total / skalaMax : 0;
                float y = areaPlot.Bottom - rasio * areaPlot.Height;
                titik[i] = new PointF(x, y);

                // Label tanggal sumbu X
                string teksX = dataGrafik7Hari[i].Tanggal.ToString("dd-MM", CultureInfo.InvariantCulture);
                SizeF ukuranX = g.MeasureString(teksX, fontAxis);
                g.DrawString(teksX, fontAxis, brushTeksAxis, x - ukuranX.Width / 2, areaPlot.Bottom + 6);
            }

            // Area isian lembut di bawah garis
            using (var pathArea = new GraphicsPath())
            {
                pathArea.AddLine(titik[0].X, areaPlot.Bottom, titik[0].X, titik[0].Y);
                pathArea.AddLines(titik);
                pathArea.AddLine(titik[^1].X, titik[^1].Y, titik[^1].X, areaPlot.Bottom);
                pathArea.CloseFigure();

                using var brushArea = new LinearGradientBrush(
                    areaPlot,
                    Color.FromArgb(65, UIHelper.WarnaKuning),
                    Color.FromArgb(8, UIHelper.WarnaKuning),
                    LinearGradientMode.Vertical);
                g.FillPath(brushArea, pathArea);
            }

            // Garis grafik utama (aksen kuning-amber SukiMart)
            using (var penGaris = new Pen(UIHelper.WarnaKuningHover, 2.5f))
            {
                g.DrawLines(penGaris, titik);
            }

            // Titik data & label nominal untuk nilai > 0
            using var brushTitikIsi = new SolidBrush(UIHelper.WarnaKuning);
            using var penTitikPinggir = new Pen(UIHelper.WarnaTeksUtama, 2f);

            for (int i = 0; i < titik.Length; i++)
            {
                PointF pt = titik[i];
                g.FillEllipse(brushTitikIsi, pt.X - 4.5f, pt.Y - 4.5f, 9f, 9f);
                g.DrawEllipse(penTitikPinggir, pt.X - 4.5f, pt.Y - 4.5f, 9f, 9f);

                if (dataGrafik7Hari[i].Total > 0)
                {
                    string teksTitik = FormatSingkatRupiah(dataGrafik7Hari[i].Total);
                    SizeF ukuranTeks = g.MeasureString(teksTitik, fontNilai);
                    float maxPosX = Math.Max(areaPlot.Left, areaPlot.Right - ukuranTeks.Width);
                    float posX = Math.Clamp(pt.X - ukuranTeks.Width / 2, areaPlot.Left, maxPosX);
                    float posY = Math.Max(4, pt.Y - ukuranTeks.Height - 6);
                    g.DrawString(teksTitik, fontNilai, brushTeksGelap, posX, posY);
                }
            }
        }

        private static long HitungSkalaAtas(long nilaiMax)
        {
            if (nilaiMax <= 0) return 100000;
            double pangkat = Math.Pow(10, Math.Floor(Math.Log10(nilaiMax)));
            double pecahan = nilaiMax / pangkat;
            double pengali = pecahan <= 1 ? 1 : pecahan <= 2 ? 2 : pecahan <= 5 ? 5 : 10;
            return (long)(pengali * pangkat);
        }

        private static string FormatSingkatRupiah(long nilai)
        {
            if (nilai == 0) return "Rp 0";
            if (nilai >= 1_000_000_000) return "Rp " + (nilai / 1_000_000_000.0).ToString("0.#", BudayaIndonesia) + "M";
            if (nilai >= 1_000_000) return "Rp " + (nilai / 1_000_000.0).ToString("0.#", BudayaIndonesia) + "jt";
            if (nilai >= 1_000) return "Rp " + (nilai / 1_000.0).ToString("0.#", BudayaIndonesia) + "rb";
            return "Rp " + nilai.ToString("N0", BudayaIndonesia);
        }

        private void IsiPeringatanStok(List<Barang> daftarBarang)
        {
            flpPeringatan.SuspendLayout();
            try
            {
                BersihkanKontrolFlow(flpPeringatan);

                var itemPerhatian = daftarBarang
                    .Where(b => b.Aktif)
                    .Select(b => new
                    {
                        Barang = b,
                        Status = FormLaporanInventaris.TentukanStatusStok(b.Stok, b.StokMinimum)
                    })
                    .Where(x => x.Status != "AMAN")
                    .OrderBy(x => x.Status switch
                    {
                        "HABIS" => 1,
                        "KRITIS" => 2,
                        "MINIMUM" => 3,
                        _ => 4
                    })
                    .ThenBy(x => x.Barang.Stok)
                    .ThenBy(x => x.Barang.Nama)
                    .Take(5)
                    .ToList();

                lblPeringatanKosong.Visible = itemPerhatian.Count == 0;
                flpPeringatan.Visible = itemPerhatian.Count > 0;

                for (int i = 0; i < itemPerhatian.Count; i++)
                {
                    var data = itemPerhatian[i];
                    var baris = new ItemPeringatanStok();
                    baris.SetData(data.Barang.Nama, data.Barang.Stok, data.Status, i < itemPerhatian.Count - 1);
                    flpPeringatan.Controls.Add(baris);
                }
            }
            finally
            {
                flpPeringatan.ResumeLayout();
            }

            SesuaikanLebarItemFlow(flpPeringatan);
        }

        private void IsiTransaksiTerbaru(List<Transaksi> daftarTransaksi)
        {
            dataGridView1.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            colNomor.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colNomor.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colTanggal.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colTanggal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colKasir.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colKasir.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colTotal.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.DataSource = daftarTransaksi
                .OrderByDescending(transaksi => transaksi.Tanggal)
                .Take(5)
                .Select(transaksi => new
                {
                    Nomor = transaksi.Id,
                    Tanggal = transaksi.Tanggal.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Kasir = transaksi.Kasir,
                    Total = "Rp " + transaksi.Total.ToString("N0", BudayaIndonesia)
                })
                .ToList();

            UIHelper.TerapkanWarnaGrid(dataGridView1);
            dataGridView1.ClearSelection();
        }

        private void IsiRingkasanOpname(List<SesiOpname> daftarSesiOpname)
        {
            DateTime hariIni = DateTime.Today;
            int menunggu = daftarSesiOpname.Count(s => s.Status == "Menunggu Persetujuan");
            int perluRevisi = daftarSesiOpname.Count(s => s.Status == "Perlu Revisi");
            int disetujuiHariIni = daftarSesiOpname.Count(s =>
                s.Status == "Disetujui" && s.TanggalDisetujui.HasValue && s.TanggalDisetujui.Value.Date == hariIni);

            lblOpnameMenungguNilai.Text = menunggu.ToString("N0", BudayaIndonesia);
            lblOpnameRevisiNilai.Text = perluRevisi.ToString("N0", BudayaIndonesia);
            lblOpnameDisetujuiNilai.Text = disetujuiHariIni.ToString("N0", BudayaIndonesia);
        }

        private void IsiInfoInventaris(List<Barang> daftarBarang, List<BarangMasuk> daftarMasuk, List<BarangKeluar> daftarKeluar)
        {
            List<Barang> produkAktif = daftarBarang.Where(b => b.Aktif).ToList();
            int habis = produkAktif.Count(b => FormLaporanInventaris.TentukanStatusStok(b.Stok, b.StokMinimum) == "HABIS");
            int kritis = produkAktif.Count(b => FormLaporanInventaris.TentukanStatusStok(b.Stok, b.StokMinimum) == "KRITIS");
            int minimum = produkAktif.Count(b => FormLaporanInventaris.TentukanStatusStok(b.Stok, b.StokMinimum) == "MINIMUM");

            DateTime hariIni = DateTime.Today;
            int masukHariIni = daftarMasuk.Where(m => m.Tanggal.Date == hariIni).Sum(m => m.Jumlah);
            int keluarHariIni = daftarKeluar.Where(k => k.Tanggal.Date == hariIni).Sum(k => k.Jumlah);

            lblInvHabisNilai.Text = habis.ToString("N0", BudayaIndonesia);
            lblInvKritisNilai.Text = kritis.ToString("N0", BudayaIndonesia);
            lblInvMinimumNilai.Text = minimum.ToString("N0", BudayaIndonesia);
            lblInvMasukNilai.Text = masukHariIni.ToString("N0", BudayaIndonesia) + " pcs";
            lblInvKeluarNilai.Text = keluarHariIni.ToString("N0", BudayaIndonesia) + " pcs";
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

        private void button1_Click(object? sender, EventArgs e)
        {
            Navigasi.KonfirmasiDanKeluar(this);
        }

        private void btnKatalog_Click(object? sender, EventArgs e)
        {
            Navigasi.BukaKatalog(this);
        }

        private void btnRiwayat_Click(object? sender, EventArgs e)
        {
            Navigasi.BukaRiwayat(this);
        }

        private void btnKasir_Click(object? sender, EventArgs e)
        {
            Navigasi.BukaKasir(this);
        }

        private void btnGudang_Click(object? sender, EventArgs e)
        {
            Navigasi.BukaGudang(this);
        }

        private void btnLaporan_Click(object? sender, EventArgs e)
        {
            Navigasi.BukaLaporan(this);
        }
    }
}
