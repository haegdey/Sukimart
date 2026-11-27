using System.Globalization;
using System.Reflection;

namespace Sukimart
{
    public partial class FormDashboardKasir : Form
    {
        private static readonly CultureInfo BudayaIndonesia = new CultureInfo("id-ID");

        public FormDashboardKasir()
        {
            InitializeComponent();
            UIHelper.InisialisasiForm(this);
            AktifkanBebasFlicker();
            MaximizeBox = true;

            btnKasir.Click += (s, e) => Navigasi.BukaKasir(this);
            btnKatalog.Click += btnKatalog_Click;
            btnRiwayat.Click += btnRiwayat_Click;
            btnGantiSandi.Click += (s, e) => Navigasi.BukaGantiKataSandi(this);
            btnLihatSemua.Click += btnRiwayat_Click;

            TerapkanGayaKartuDashboard();

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
                Panel[] panels = { pnlKanan, pnlTransaksi, pnlPenjualan, pnlTerjual, pnlStok, pnlTransaksiTerbaru, pnlPeringatan };
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
            Panel[] kartu = { pnlTransaksi, pnlPenjualan, pnlTerjual, pnlStok, pnlTransaksiTerbaru, pnlPeringatan };
            foreach (Panel p in kartu)
            {
                PasangGayaKartu(p);
            }

            FontInter.Terapkan(this);
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

            // Baris 1: 4 KPI Cards sejajar
            int yCards = 88;
            int tinggiKpi = 104;
            int lebarKpi = (lebarTersedia - 3 * jarak) / 4;
            int lebarKpiAkhir = lebarTersedia - 3 * (lebarKpi + jarak);

            pnlTransaksi.Location = new Point(margin, yCards);
            pnlTransaksi.Size = new Size(lebarKpi, tinggiKpi);

            pnlPenjualan.Location = new Point(margin + lebarKpi + jarak, yCards);
            pnlPenjualan.Size = new Size(lebarKpi, tinggiKpi);

            pnlTerjual.Location = new Point(margin + 2 * (lebarKpi + jarak), yCards);
            pnlTerjual.Size = new Size(lebarKpi, tinggiKpi);

            pnlStok.Location = new Point(margin + 3 * (lebarKpi + jarak), yCards);
            pnlStok.Size = new Size(lebarKpiAkhir, tinggiKpi);

            // Baris 2: Transaksi Terbaru Saya (kiri) & Peringatan Stok (kanan) dengan tinggi sama
            int yKonten = yCards + tinggiKpi + 18;
            int tinggiBawah = Math.Max(260, pnlKanan.ClientSize.Height - yKonten - 24);
            int lebarKiri = (int)(lebarTersedia * 0.58);
            int lebarKanan = lebarTersedia - jarak - lebarKiri;
            int xKanan = margin + lebarKiri + jarak;

            pnlTransaksiTerbaru.Location = new Point(margin, yKonten);
            pnlTransaksiTerbaru.Size = new Size(lebarKiri, tinggiBawah);
            lblTransaksiTerbaru.Location = new Point(20, 16);
            btnLihatSemua.Location = new Point(pnlTransaksiTerbaru.ClientSize.Width - btnLihatSemua.Width - 20, 12);
            dataGridView1.Location = new Point(20, 48);
            dataGridView1.Size = new Size(Math.Max(100, pnlTransaksiTerbaru.ClientSize.Width - 40), Math.Max(100, pnlTransaksiTerbaru.ClientSize.Height - 64));

            pnlPeringatan.Location = new Point(xKanan, yKonten);
            pnlPeringatan.Size = new Size(lebarKanan, tinggiBawah);
            lblPeringatan.Location = new Point(20, 16);
            flpPeringatan.Location = new Point(20, 48);
            flpPeringatan.Size = new Size(Math.Max(100, pnlPeringatan.ClientSize.Width - 40), Math.Max(100, pnlPeringatan.ClientSize.Height - 64));
            lblPeringatanKosong.Location = new Point(20, 56);
            SesuaikanLebarItemFlow(flpPeringatan);
        }

        private void IsiDashboard()
        {
            lblTanggal.Text = DateTime.Now.ToString("dd/MM/yyyy");
            AturTataLetakResponsif();

            try
            {
                List<Transaksi> daftarTransaksi = PenyimpananData.BacaTransaksi();
                List<DetailTransaksi> daftarDetail = PenyimpananData.BacaDetailTransaksi();
                List<Barang> daftarBarang = PenyimpananData.BacaBarang();

                string usernameKasir = Navigasi.PenggunaAktif;
                DateTime hariIni = DateTime.Today;

                List<Transaksi> transaksiSaya = daftarTransaksi
                    .Where(t => string.Equals(t.Kasir, usernameKasir, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                List<Transaksi> transaksiSayaHariIni = transaksiSaya
                    .Where(t => t.Tanggal.Date == hariIni)
                    .ToList();

                HashSet<string> idTransaksiSayaHariIni = transaksiSayaHariIni
                    .Select(t => t.Id)
                    .ToHashSet();

                int jumlahTransaksiSaya = transaksiSayaHariIni.Count;
                int totalPenjualanSaya = transaksiSayaHariIni.Sum(t => t.Total);
                int itemTerjualSaya = daftarDetail
                    .Where(d => idTransaksiSayaHariIni.Contains(d.IdTransaksi))
                    .Sum(d => d.Jumlah);
                int stokPerhatian = daftarBarang
                    .Where(b => b.Aktif)
                    .Count(b => FormLaporanInventaris.TentukanStatusStok(b.Stok, b.StokMinimum) != "AMAN");

                lblTransaksiHariIni.Text = "TRANSAKSI SAYA HARI INI";
                lblNilaiTransaksi.Text = jumlahTransaksiSaya.ToString("N0", BudayaIndonesia);
                lblNilaiTransaksi.ForeColor = Color.FromArgb(31, 41, 55);

                lblPenjualan.Text = "PENJUALAN SAYA HARI INI";
                lblNilaiPenjualan.Text = "Rp " + totalPenjualanSaya.ToString("N0", BudayaIndonesia);
                lblNilaiPenjualan.ForeColor = Color.FromArgb(16, 149, 102);

                lblTerjual.Text = "ITEM TERJUAL HARI INI";
                lblNilaiTerjual.Text = itemTerjualSaya.ToString("N0", BudayaIndonesia);
                lblNilaiTerjual.ForeColor = Color.FromArgb(31, 41, 55);

                lblStok.Text = "STOK PERLU PERHATIAN";
                lblNilaiStok.Text = stokPerhatian.ToString("N0", BudayaIndonesia);
                lblNilaiStok.ForeColor = stokPerhatian > 0 ? Color.FromArgb(220, 38, 38) : Color.FromArgb(16, 149, 102);

                IsiTransaksiTerbaru(transaksiSaya, daftarDetail);
                IsiPeringatanStok(daftarBarang);
            }
            catch (Exception ex)
            {
                lblNilaiTransaksi.Text = "0";
                lblNilaiPenjualan.Text = "Rp 0";
                lblNilaiTerjual.Text = "0";
                lblNilaiStok.Text = "0";
                dataGridView1.DataSource = null;
                BersihkanKontrolFlow(flpPeringatan);

                MessageBox.Show(
                    "Dashboard gagal membaca data JSON. Periksa file data dan coba lagi.\n\n" + ex.Message,
                    "Kesalahan Membaca Data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void IsiTransaksiTerbaru(List<Transaksi> transaksiSaya, List<DetailTransaksi> daftarDetail)
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
            colWaktu.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colWaktu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colItem.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colItem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTotal.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            Dictionary<string, int> jumlahItemPerTransaksi = daftarDetail
                .GroupBy(d => d.IdTransaksi)
                .ToDictionary(g => g.Key, g => g.Sum(d => d.Jumlah));

            dataGridView1.DataSource = transaksiSaya
                .OrderByDescending(transaksi => transaksi.Tanggal)
                .Take(10)
                .Select(transaksi => new
                {
                    Nomor = transaksi.Id,
                    Waktu = transaksi.Tanggal.ToString("dd/MM/yyyy HH:mm"),
                    Item = jumlahItemPerTransaksi.TryGetValue(transaksi.Id, out int qty) ? qty : 0,
                    Total = "Rp " + transaksi.Total.ToString("N0", BudayaIndonesia)
                })
                .ToList();

            UIHelper.TerapkanWarnaGrid(dataGridView1);
            dataGridView1.ClearSelection();
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

        private void btnKeluar_Click(object? sender, EventArgs e)
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
    }
}
