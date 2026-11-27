namespace Sukimart
{
    partial class FormDashboardKasir
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            pnlKiri = new Panel();
            btnKeluar = new Button();
            lblAkunNama = new Label();
            lblAkunRole = new Label();
            btnGantiSandi = new Button();
            btnRiwayat = new Button();
            btnKatalog = new Button();
            btnKasir = new Button();
            btnDashboard = new Button();
            pictureLogo = new PictureBox();
            lblNama = new Label();
            pnlKanan = new Panel();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            lblJudul = new Label();
            lblTanggal = new Label();
            pnlTransaksi = new Panel();
            lblTransaksiHariIni = new Label();
            lblNilaiTransaksi = new Label();
            pnlPenjualan = new Panel();
            lblPenjualan = new Label();
            lblNilaiPenjualan = new Label();
            pnlTerjual = new Panel();
            lblTerjual = new Label();
            lblNilaiTerjual = new Label();
            pnlStok = new Panel();
            lblStok = new Label();
            lblNilaiStok = new Label();
            pnlTransaksiTerbaru = new Panel();
            lblTransaksiTerbaru = new Label();
            btnLihatSemua = new Button();
            dataGridView1 = new DataGridView();
            colNomor = new DataGridViewTextBoxColumn();
            colWaktu = new DataGridViewTextBoxColumn();
            colItem = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            pnlPeringatan = new Panel();
            lblPeringatan = new Label();
            flpPeringatan = new FlowLayoutPanel();
            lblPeringatanKosong = new Label();
            pnlKiri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).BeginInit();
            pnlKanan.SuspendLayout();
            pnlTransaksi.SuspendLayout();
            pnlPenjualan.SuspendLayout();
            pnlTerjual.SuspendLayout();
            pnlStok.SuspendLayout();
            pnlTransaksiTerbaru.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            pnlPeringatan.SuspendLayout();
            SuspendLayout();
            // 
            // pnlKiri
            // 
            pnlKiri.BackColor = Color.White;
            pnlKiri.Controls.Add(btnKeluar);
            pnlKiri.Controls.Add(lblAkunNama);
            pnlKiri.Controls.Add(lblAkunRole);
            pnlKiri.Controls.Add(btnGantiSandi);
            pnlKiri.Controls.Add(btnRiwayat);
            pnlKiri.Controls.Add(btnKatalog);
            pnlKiri.Controls.Add(btnKasir);
            pnlKiri.Controls.Add(btnDashboard);
            pnlKiri.Controls.Add(pictureLogo);
            pnlKiri.Controls.Add(lblNama);
            pnlKiri.Dock = DockStyle.Left;
            pnlKiri.Location = new Point(0, 0);
            pnlKiri.Margin = new Padding(3, 4, 3, 4);
            pnlKiri.Name = "pnlKiri";
            pnlKiri.Size = new Size(274, 960);
            pnlKiri.TabIndex = 0;
            // 
            // btnKeluar
            // 
            btnKeluar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnKeluar.BackColor = Color.White;
            btnKeluar.Cursor = Cursors.Hand;
            btnKeluar.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68);
            btnKeluar.FlatAppearance.BorderSize = 1;
            btnKeluar.FlatAppearance.MouseOverBackColor = Color.FromArgb(254, 242, 242);
            btnKeluar.FlatStyle = FlatStyle.Flat;
            btnKeluar.Font = new Font("Inter", 10F, FontStyle.Bold);
            btnKeluar.ForeColor = Color.FromArgb(220, 38, 38);
            btnKeluar.Location = new Point(18, 853);
            btnKeluar.Margin = new Padding(3, 4, 3, 4);
            btnKeluar.Name = "btnKeluar";
            btnKeluar.Padding = new Padding(0);
            btnKeluar.Size = new Size(238, 53);
            btnKeluar.TabIndex = 7;
            btnKeluar.Text = "Keluar";
            btnKeluar.TextAlign = ContentAlignment.MiddleCenter;
            btnKeluar.UseVisualStyleBackColor = false;
            btnKeluar.Click += btnKeluar_Click;
            // 
            // lblAkunNama
            // 
            lblAkunNama.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblAkunNama.AutoEllipsis = true;
            lblAkunNama.Font = new Font("Inter", 10.5F, FontStyle.Bold);
            lblAkunNama.ForeColor = Color.FromArgb(31, 41, 55);
            lblAkunNama.Location = new Point(18, 797);
            lblAkunNama.Name = "lblAkunNama";
            lblAkunNama.Size = new Size(192, 22);
            lblAkunNama.TabIndex = 15;
            lblAkunNama.Text = "Nama Pengguna";
            lblAkunNama.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAkunRole
            // 
            lblAkunRole.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblAkunRole.AutoEllipsis = true;
            lblAkunRole.Font = new Font("Inter", 8.5F);
            lblAkunRole.ForeColor = Color.FromArgb(107, 114, 128);
            lblAkunRole.Location = new Point(18, 819);
            lblAkunRole.Name = "lblAkunRole";
            lblAkunRole.Size = new Size(192, 18);
            lblAkunRole.TabIndex = 16;
            lblAkunRole.Text = "Role";
            lblAkunRole.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnGantiSandi
            // 
            btnGantiSandi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnGantiSandi.BackColor = Color.White;
            btnGantiSandi.Cursor = Cursors.Hand;
            btnGantiSandi.FlatAppearance.BorderSize = 0;
            btnGantiSandi.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 244, 204);
            btnGantiSandi.FlatStyle = FlatStyle.Flat;
            btnGantiSandi.Font = new Font("Inter", 10F);
            btnGantiSandi.ForeColor = Color.FromArgb(75, 85, 99);
            btnGantiSandi.Image = Properties.Resources.kunci;
            btnGantiSandi.ImageAlign = ContentAlignment.MiddleCenter;
            btnGantiSandi.Location = new Point(218, 799);
            btnGantiSandi.Margin = new Padding(3, 4, 3, 4);
            btnGantiSandi.Name = "btnGantiSandi";
            btnGantiSandi.Padding = new Padding(0);
            btnGantiSandi.Size = new Size(38, 38);
            btnGantiSandi.TabIndex = 8;
            btnGantiSandi.Text = "";
            btnGantiSandi.UseVisualStyleBackColor = false;
            // 
            // btnRiwayat
            // 
            btnRiwayat.BackColor = Color.White;
            btnRiwayat.Cursor = Cursors.Hand;
            btnRiwayat.FlatAppearance.BorderSize = 0;
            btnRiwayat.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 244, 204);
            btnRiwayat.FlatStyle = FlatStyle.Flat;
            btnRiwayat.Font = new Font("Inter", 10F);
            btnRiwayat.ForeColor = Color.FromArgb(31, 41, 55);
            btnRiwayat.Location = new Point(18, 341);
            btnRiwayat.Margin = new Padding(3, 4, 3, 4);
            btnRiwayat.Name = "btnRiwayat";
            btnRiwayat.Padding = new Padding(18, 0, 0, 0);
            btnRiwayat.Size = new Size(238, 53);
            btnRiwayat.TabIndex = 6;
            btnRiwayat.Text = "Riwayat Transaksi";
            btnRiwayat.TextAlign = ContentAlignment.MiddleLeft;
            btnRiwayat.UseVisualStyleBackColor = false;
            // 
            // btnKatalog
            // 
            btnKatalog.BackColor = Color.White;
            btnKatalog.Cursor = Cursors.Hand;
            btnKatalog.FlatAppearance.BorderSize = 0;
            btnKatalog.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 244, 204);
            btnKatalog.FlatStyle = FlatStyle.Flat;
            btnKatalog.Font = new Font("Inter", 10F);
            btnKatalog.ForeColor = Color.FromArgb(31, 41, 55);
            btnKatalog.Location = new Point(18, 277);
            btnKatalog.Margin = new Padding(3, 4, 3, 4);
            btnKatalog.Name = "btnKatalog";
            btnKatalog.Padding = new Padding(18, 0, 0, 0);
            btnKatalog.Size = new Size(238, 53);
            btnKatalog.TabIndex = 5;
            btnKatalog.Text = "Katalog Produk";
            btnKatalog.TextAlign = ContentAlignment.MiddleLeft;
            btnKatalog.UseVisualStyleBackColor = false;
            btnKatalog.Click += btnKatalog_Click;
            // 
            // btnKasir
            // 
            btnKasir.BackColor = Color.White;
            btnKasir.Cursor = Cursors.Hand;
            btnKasir.FlatAppearance.BorderSize = 0;
            btnKasir.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 244, 204);
            btnKasir.FlatStyle = FlatStyle.Flat;
            btnKasir.Font = new Font("Inter", 10F);
            btnKasir.ForeColor = Color.FromArgb(31, 41, 55);
            btnKasir.Location = new Point(18, 213);
            btnKasir.Margin = new Padding(3, 4, 3, 4);
            btnKasir.Name = "btnKasir";
            btnKasir.Padding = new Padding(18, 0, 0, 0);
            btnKasir.Size = new Size(238, 53);
            btnKasir.TabIndex = 4;
            btnKasir.Text = "Kasir / POS";
            btnKasir.TextAlign = ContentAlignment.MiddleLeft;
            btnKasir.UseVisualStyleBackColor = false;
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(255, 196, 0);
            btnDashboard.Cursor = Cursors.Hand;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 244, 204);
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Inter", 10F, FontStyle.Bold);
            btnDashboard.ForeColor = Color.FromArgb(31, 41, 55);
            btnDashboard.Location = new Point(18, 149);
            btnDashboard.Margin = new Padding(3, 4, 3, 4);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(18, 0, 0, 0);
            btnDashboard.Size = new Size(238, 53);
            btnDashboard.TabIndex = 3;
            btnDashboard.Text = "Dashboard";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = false;
            // 
            // pictureLogo
            // 
            pictureLogo.Image = Properties.Resources.logo;
            pictureLogo.Location = new Point(27, 43);
            pictureLogo.Margin = new Padding(3, 4, 3, 4);
            pictureLogo.Name = "pictureLogo";
            pictureLogo.Size = new Size(46, 53);
            pictureLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureLogo.TabIndex = 2;
            pictureLogo.TabStop = false;
            // 
            // lblNama
            // 
            lblNama.AutoSize = true;
            lblNama.Font = new Font("Inter", 14F, FontStyle.Bold);
            lblNama.Location = new Point(82, 53);
            lblNama.Name = "lblNama";
            lblNama.Size = new Size(144, 29);
            lblNama.TabIndex = 1;
            lblNama.Text = "SUKIMART";
            // 
            // pnlKanan
            // 
            pnlKanan.BackColor = Color.FromArgb(247, 247, 245);
            pnlKanan.Controls.Add(nightControlBox1);
            pnlKanan.Controls.Add(lblJudul);
            pnlKanan.Controls.Add(lblTanggal);
            pnlKanan.Controls.Add(pnlTransaksi);
            pnlKanan.Controls.Add(pnlPenjualan);
            pnlKanan.Controls.Add(pnlTerjual);
            pnlKanan.Controls.Add(pnlStok);
            pnlKanan.Controls.Add(pnlTransaksiTerbaru);
            pnlKanan.Controls.Add(pnlPeringatan);
            pnlKanan.Dock = DockStyle.Fill;
            pnlKanan.Location = new Point(274, 0);
            pnlKanan.Margin = new Padding(3, 4, 3, 4);
            pnlKanan.Name = "pnlKanan";
            pnlKanan.Size = new Size(1189, 960);
            pnlKanan.TabIndex = 1;
            // 
            // nightControlBox1
            // 
            nightControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nightControlBox1.BackColor = Color.Transparent;
            nightControlBox1.CloseHoverColor = Color.FromArgb(199, 80, 80);
            nightControlBox1.CloseHoverForeColor = Color.White;
            nightControlBox1.DefaultLocation = true;
            nightControlBox1.DisableMaximizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.DisableMinimizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.EnableCloseColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMaximizeButton = true;
            nightControlBox1.EnableMaximizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMinimizeButton = true;
            nightControlBox1.EnableMinimizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.Location = new Point(1050, 0);
            nightControlBox1.MaximizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MaximizeHoverForeColor = Color.White;
            nightControlBox1.MinimizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MinimizeHoverForeColor = Color.White;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(139, 31);
            nightControlBox1.TabIndex = 14;
            // 
            // lblJudul
            // 
            lblJudul.AutoSize = true;
            lblJudul.Font = new Font("Inter", 20F, FontStyle.Bold);
            lblJudul.ForeColor = Color.FromArgb(31, 41, 55);
            lblJudul.Location = new Point(40, 28);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new Size(286, 39);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "Dashboard Kasir";
            // 
            // lblTanggal
            // 
            lblTanggal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTanggal.AutoSize = true;
            lblTanggal.Font = new Font("Inter", 9F);
            lblTanggal.ForeColor = Color.FromArgb(107, 114, 128);
            lblTanggal.Location = new Point(1060, 42);
            lblTanggal.Name = "lblTanggal";
            lblTanggal.Size = new Size(80, 18);
            lblTanggal.TabIndex = 1;
            lblTanggal.Text = "27/11/2026";
            // 
            // pnlTransaksi
            // 
            pnlTransaksi.BackColor = Color.White;
            pnlTransaksi.Controls.Add(lblTransaksiHariIni);
            pnlTransaksi.Controls.Add(lblNilaiTransaksi);
            pnlTransaksi.Location = new Point(40, 92);
            pnlTransaksi.Margin = new Padding(3, 4, 3, 4);
            pnlTransaksi.Name = "pnlTransaksi";
            pnlTransaksi.Size = new Size(262, 104);
            pnlTransaksi.TabIndex = 2;
            // 
            // lblTransaksiHariIni
            // 
            lblTransaksiHariIni.AutoSize = true;
            lblTransaksiHariIni.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            lblTransaksiHariIni.ForeColor = Color.FromArgb(30, 41, 59);
            lblTransaksiHariIni.Location = new Point(20, 18);
            lblTransaksiHariIni.Name = "lblTransaksiHariIni";
            lblTransaksiHariIni.Size = new Size(166, 18);
            lblTransaksiHariIni.TabIndex = 0;
            lblTransaksiHariIni.Text = "TRANSAKSI SAYA HARI INI";
            // 
            // lblNilaiTransaksi
            // 
            lblNilaiTransaksi.AutoSize = true;
            lblNilaiTransaksi.Font = new Font("Inter", 18F, FontStyle.Bold);
            lblNilaiTransaksi.ForeColor = Color.FromArgb(31, 41, 55);
            lblNilaiTransaksi.Location = new Point(20, 48);
            lblNilaiTransaksi.Name = "lblNilaiTransaksi";
            lblNilaiTransaksi.Size = new Size(37, 36);
            lblNilaiTransaksi.TabIndex = 1;
            lblNilaiTransaksi.Text = "0";
            // 
            // pnlPenjualan
            // 
            pnlPenjualan.BackColor = Color.White;
            pnlPenjualan.Controls.Add(lblPenjualan);
            pnlPenjualan.Controls.Add(lblNilaiPenjualan);
            pnlPenjualan.Location = new Point(322, 92);
            pnlPenjualan.Margin = new Padding(3, 4, 3, 4);
            pnlPenjualan.Name = "pnlPenjualan";
            pnlPenjualan.Size = new Size(262, 104);
            pnlPenjualan.TabIndex = 3;
            // 
            // lblPenjualan
            // 
            lblPenjualan.AutoSize = true;
            lblPenjualan.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            lblPenjualan.ForeColor = Color.FromArgb(30, 41, 59);
            lblPenjualan.Location = new Point(20, 18);
            lblPenjualan.Name = "lblPenjualan";
            lblPenjualan.Size = new Size(172, 18);
            lblPenjualan.TabIndex = 0;
            lblPenjualan.Text = "PENJUALAN SAYA HARI INI";
            // 
            // lblNilaiPenjualan
            // 
            lblNilaiPenjualan.AutoSize = true;
            lblNilaiPenjualan.Font = new Font("Inter", 18F, FontStyle.Bold);
            lblNilaiPenjualan.ForeColor = Color.FromArgb(16, 149, 102);
            lblNilaiPenjualan.Location = new Point(20, 48);
            lblNilaiPenjualan.Name = "lblNilaiPenjualan";
            lblNilaiPenjualan.Size = new Size(90, 36);
            lblNilaiPenjualan.TabIndex = 1;
            lblNilaiPenjualan.Text = "Rp 0";
            // 
            // pnlTerjual
            // 
            pnlTerjual.BackColor = Color.White;
            pnlTerjual.Controls.Add(lblTerjual);
            pnlTerjual.Controls.Add(lblNilaiTerjual);
            pnlTerjual.Location = new Point(604, 92);
            pnlTerjual.Margin = new Padding(3, 4, 3, 4);
            pnlTerjual.Name = "pnlTerjual";
            pnlTerjual.Size = new Size(262, 104);
            pnlTerjual.TabIndex = 4;
            // 
            // lblTerjual
            // 
            lblTerjual.AutoSize = true;
            lblTerjual.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            lblTerjual.ForeColor = Color.FromArgb(30, 41, 59);
            lblTerjual.Location = new Point(20, 18);
            lblTerjual.Name = "lblTerjual";
            lblTerjual.Size = new Size(156, 18);
            lblTerjual.TabIndex = 0;
            lblTerjual.Text = "ITEM TERJUAL HARI INI";
            // 
            // lblNilaiTerjual
            // 
            lblNilaiTerjual.AutoSize = true;
            lblNilaiTerjual.Font = new Font("Inter", 18F, FontStyle.Bold);
            lblNilaiTerjual.ForeColor = Color.FromArgb(31, 41, 55);
            lblNilaiTerjual.Location = new Point(20, 48);
            lblNilaiTerjual.Name = "lblNilaiTerjual";
            lblNilaiTerjual.Size = new Size(37, 36);
            lblNilaiTerjual.TabIndex = 1;
            lblNilaiTerjual.Text = "0";
            // 
            // pnlStok
            // 
            pnlStok.BackColor = Color.White;
            pnlStok.Controls.Add(lblStok);
            pnlStok.Controls.Add(lblNilaiStok);
            pnlStok.Location = new Point(886, 92);
            pnlStok.Margin = new Padding(3, 4, 3, 4);
            pnlStok.Name = "pnlStok";
            pnlStok.Size = new Size(262, 104);
            pnlStok.TabIndex = 5;
            // 
            // lblStok
            // 
            lblStok.AutoSize = true;
            lblStok.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            lblStok.ForeColor = Color.FromArgb(30, 41, 59);
            lblStok.Location = new Point(20, 18);
            lblStok.Name = "lblStok";
            lblStok.Size = new Size(160, 18);
            lblStok.TabIndex = 0;
            lblStok.Text = "STOK PERLU PERHATIAN";
            // 
            // lblNilaiStok
            // 
            lblNilaiStok.AutoSize = true;
            lblNilaiStok.Font = new Font("Inter", 18F, FontStyle.Bold);
            lblNilaiStok.ForeColor = Color.FromArgb(220, 38, 38);
            lblNilaiStok.Location = new Point(20, 48);
            lblNilaiStok.Name = "lblNilaiStok";
            lblNilaiStok.Size = new Size(37, 36);
            lblNilaiStok.TabIndex = 1;
            lblNilaiStok.Text = "0";
            // 
            // pnlTransaksiTerbaru
            // 
            pnlTransaksiTerbaru.BackColor = Color.White;
            pnlTransaksiTerbaru.Controls.Add(dataGridView1);
            pnlTransaksiTerbaru.Controls.Add(btnLihatSemua);
            pnlTransaksiTerbaru.Controls.Add(lblTransaksiTerbaru);
            pnlTransaksiTerbaru.Location = new Point(40, 214);
            pnlTransaksiTerbaru.Name = "pnlTransaksiTerbaru";
            pnlTransaksiTerbaru.Size = new Size(644, 722);
            pnlTransaksiTerbaru.TabIndex = 6;
            // 
            // lblTransaksiTerbaru
            // 
            lblTransaksiTerbaru.AutoSize = true;
            lblTransaksiTerbaru.Font = new Font("Inter", 9.5F, FontStyle.Bold);
            lblTransaksiTerbaru.ForeColor = Color.FromArgb(30, 41, 59);
            lblTransaksiTerbaru.Location = new Point(20, 16);
            lblTransaksiTerbaru.Name = "lblTransaksiTerbaru";
            lblTransaksiTerbaru.Size = new Size(188, 19);
            lblTransaksiTerbaru.TabIndex = 0;
            lblTransaksiTerbaru.Text = "TRANSAKSI TERBARU SAYA";
            // 
            // btnLihatSemua
            // 
            btnLihatSemua.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLihatSemua.BackColor = Color.White;
            btnLihatSemua.Cursor = Cursors.Hand;
            btnLihatSemua.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnLihatSemua.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 244, 204);
            btnLihatSemua.FlatStyle = FlatStyle.Flat;
            btnLihatSemua.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            btnLihatSemua.ForeColor = Color.FromArgb(30, 41, 59);
            btnLihatSemua.Location = new Point(524, 12);
            btnLihatSemua.Name = "btnLihatSemua";
            btnLihatSemua.Size = new Size(100, 28);
            btnLihatSemua.TabIndex = 1;
            btnLihatSemua.Text = "Lihat Semua";
            btnLihatSemua.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(249, 250, 251);
            dataGridViewCellStyle1.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(75, 85, 99);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(255, 244, 204);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 36;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colNomor, colWaktu, colItem, colTotal });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Inter", 9.5F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(255, 244, 204);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(31, 41, 55);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.FromArgb(229, 231, 235);
            dataGridView1.Location = new Point(20, 48);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 36;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(604, 658);
            dataGridView1.TabIndex = 2;
            // 
            // colNomor
            // 
            colNomor.DataPropertyName = "Nomor";
            colNomor.FillWeight = 36F;
            colNomor.HeaderText = "Nomor Faktur";
            colNomor.MinimumWidth = 6;
            colNomor.Name = "colNomor";
            colNomor.ReadOnly = true;
            // 
            // colWaktu
            // 
            colWaktu.DataPropertyName = "Waktu";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colWaktu.DefaultCellStyle = dataGridViewCellStyle3;
            colWaktu.FillWeight = 24F;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colWaktu.HeaderCell.Style = dataGridViewCellStyle4;
            colWaktu.HeaderText = "Waktu";
            colWaktu.MinimumWidth = 6;
            colWaktu.Name = "colWaktu";
            colWaktu.ReadOnly = true;
            // 
            // colItem
            // 
            colItem.DataPropertyName = "Item";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colItem.DefaultCellStyle = dataGridViewCellStyle5;
            colItem.FillWeight = 14F;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colItem.HeaderCell.Style = dataGridViewCellStyle6;
            colItem.HeaderText = "Item";
            colItem.MinimumWidth = 6;
            colItem.Name = "colItem";
            colItem.ReadOnly = true;
            // 
            // colTotal
            // 
            colTotal.DataPropertyName = "Total";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTotal.DefaultCellStyle = dataGridViewCellStyle7;
            colTotal.FillWeight = 26F;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTotal.HeaderCell.Style = dataGridViewCellStyle8;
            colTotal.HeaderText = "Total";
            colTotal.MinimumWidth = 6;
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            // 
            // pnlPeringatan
            // 
            pnlPeringatan.BackColor = Color.White;
            pnlPeringatan.Controls.Add(lblPeringatanKosong);
            pnlPeringatan.Controls.Add(flpPeringatan);
            pnlPeringatan.Controls.Add(lblPeringatan);
            pnlPeringatan.Location = new Point(704, 214);
            pnlPeringatan.Name = "pnlPeringatan";
            pnlPeringatan.Size = new Size(444, 722);
            pnlPeringatan.TabIndex = 7;
            // 
            // lblPeringatan
            // 
            lblPeringatan.AutoSize = true;
            lblPeringatan.Font = new Font("Inter", 9.5F, FontStyle.Bold);
            lblPeringatan.ForeColor = Color.FromArgb(30, 41, 59);
            lblPeringatan.Location = new Point(20, 16);
            lblPeringatan.Name = "lblPeringatan";
            lblPeringatan.Size = new Size(136, 19);
            lblPeringatan.TabIndex = 0;
            lblPeringatan.Text = "PERINGATAN STOK";
            // 
            // flpPeringatan
            // 
            flpPeringatan.AutoScroll = true;
            flpPeringatan.BackColor = Color.White;
            flpPeringatan.FlowDirection = FlowDirection.TopDown;
            flpPeringatan.Location = new Point(20, 48);
            flpPeringatan.Name = "flpPeringatan";
            flpPeringatan.Size = new Size(404, 658);
            flpPeringatan.TabIndex = 1;
            flpPeringatan.WrapContents = false;
            // 
            // lblPeringatanKosong
            // 
            lblPeringatanKosong.AutoSize = true;
            lblPeringatanKosong.Font = new Font("Inter", 9F);
            lblPeringatanKosong.ForeColor = Color.FromArgb(16, 149, 102);
            lblPeringatanKosong.Location = new Point(20, 56);
            lblPeringatanKosong.Name = "lblPeringatanKosong";
            lblPeringatanKosong.Size = new Size(260, 18);
            lblPeringatanKosong.TabIndex = 2;
            lblPeringatanKosong.Text = "Semua stok produk dalam kondisi aman.";
            lblPeringatanKosong.Visible = false;
            // 
            // FormDashboardKasir
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1463, 960);
            Controls.Add(pnlKanan);
            Controls.Add(pnlKiri);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormDashboardKasir";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "dashboardkasir";
            pnlKiri.ResumeLayout(false);
            pnlKiri.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).EndInit();
            pnlKanan.ResumeLayout(false);
            pnlKanan.PerformLayout();
            pnlTransaksi.ResumeLayout(false);
            pnlTransaksi.PerformLayout();
            pnlPenjualan.ResumeLayout(false);
            pnlPenjualan.PerformLayout();
            pnlTerjual.ResumeLayout(false);
            pnlTerjual.PerformLayout();
            pnlStok.ResumeLayout(false);
            pnlStok.PerformLayout();
            pnlTransaksiTerbaru.ResumeLayout(false);
            pnlTransaksiTerbaru.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            pnlPeringatan.ResumeLayout(false);
            pnlPeringatan.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlKiri;
        private PictureBox pictureLogo;
        private Button btnDashboard;
        private Panel pnlKanan;
        private Label lblTransaksiTerbaru;
        private Button btnKasir;
        private Button btnKeluar;
        private Button btnGantiSandi;
        private Label lblAkunNama;
        private Label lblAkunRole;
        private Button btnRiwayat;
        private Button btnKatalog;
        private Label lblJudul;
        private Label lblNama;
        private Label lblTanggal;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colNomor;
        private DataGridViewTextBoxColumn colWaktu;
        private DataGridViewTextBoxColumn colItem;
        private DataGridViewTextBoxColumn colTotal;
        private Panel pnlTransaksi;
        private Panel pnlPenjualan;
        private Label lblPenjualan;
        private Label lblNilaiPenjualan;
        private Panel pnlStok;
        private Panel pnlTerjual;
        private Label lblTerjual;
        private Label lblTransaksiHariIni;
        private Label lblStok;
        private Label lblNilaiTransaksi;
        private Label lblNilaiStok;
        private Label lblNilaiTerjual;
        private Panel pnlTransaksiTerbaru;
        private Button btnLihatSemua;
        private Panel pnlPeringatan;
        private Label lblPeringatan;
        private FlowLayoutPanel flpPeringatan;
        private Label lblPeringatanKosong;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
    }
}