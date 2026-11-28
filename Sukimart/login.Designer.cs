namespace Sukimart
{
    partial class FormLogin
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
            pnlKiri = new Panel();
            pnlKartuLogin = new PanelKartu();
            lblPassSalah = new Label();
            lblUsnSalah = new Label();
            lblPassword = new Label();
            lblUsername = new Label();
            btnMasuk = new Button();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            lblJudul = new Label();
            pictureLogo = new PictureBox();
            lblMerk = new Label();
            pnlKanan = new Panel();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            lblKecil = new Label();
            lblBesar = new Label();
            pnlKiri.SuspendLayout();
            pnlKartuLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).BeginInit();
            pnlKanan.SuspendLayout();
            SuspendLayout();
            // 
            // pnlKiri
            // 
            pnlKiri.BackColor = Color.FromArgb(247, 247, 245);
            pnlKiri.Controls.Add(pnlKartuLogin);
            pnlKiri.Dock = DockStyle.Left;
            pnlKiri.Location = new Point(0, 0);
            pnlKiri.Margin = new Padding(3, 4, 3, 4);
            pnlKiri.Name = "pnlKiri";
            pnlKiri.Size = new Size(640, 960);
            pnlKiri.TabIndex = 1;
            // 
            // pnlKartuLogin
            // 
            pnlKartuLogin.BackColor = Color.White;
            pnlKartuLogin.Controls.Add(lblPassSalah);
            pnlKartuLogin.Controls.Add(lblUsnSalah);
            pnlKartuLogin.Controls.Add(lblPassword);
            pnlKartuLogin.Controls.Add(lblUsername);
            pnlKartuLogin.Controls.Add(btnMasuk);
            pnlKartuLogin.Controls.Add(txtPassword);
            pnlKartuLogin.Controls.Add(txtUsername);
            pnlKartuLogin.Controls.Add(lblJudul);
            pnlKartuLogin.Controls.Add(pictureLogo);
            pnlKartuLogin.Controls.Add(lblMerk);
            pnlKartuLogin.Location = new Point(52, 190);
            pnlKartuLogin.Margin = new Padding(3, 4, 3, 4);
            pnlKartuLogin.Name = "pnlKartuLogin";
            pnlKartuLogin.Size = new Size(537, 580);
            pnlKartuLogin.TabIndex = 0;
            // 
            // lblPassSalah
            // 
            lblPassSalah.AutoSize = true;
            lblPassSalah.Font = new Font("Inter", 9F);
            lblPassSalah.ForeColor = Color.FromArgb(220, 38, 38);
            lblPassSalah.Location = new Point(40, 397);
            lblPassSalah.Name = "lblPassSalah";
            lblPassSalah.Size = new Size(0, 18);
            lblPassSalah.TabIndex = 9;
            // 
            // lblUsnSalah
            // 
            lblUsnSalah.AutoSize = true;
            lblUsnSalah.Font = new Font("Inter", 9F);
            lblUsnSalah.ForeColor = Color.FromArgb(220, 38, 38);
            lblUsnSalah.Location = new Point(40, 297);
            lblUsnSalah.Name = "lblUsnSalah";
            lblUsnSalah.Size = new Size(0, 18);
            lblUsnSalah.TabIndex = 8;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Inter", 9F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(31, 41, 55);
            lblPassword.Location = new Point(40, 336);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(89, 18);
            lblPassword.TabIndex = 7;
            lblPassword.Text = "Kata Sandi";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Inter", 9F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(31, 41, 55);
            lblUsername.Location = new Point(40, 236);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(131, 18);
            lblUsername.TabIndex = 6;
            lblUsername.Text = "Nama Pengguna";
            // 
            // btnMasuk
            // 
            btnMasuk.BackColor = Color.FromArgb(255, 196, 0);
            btnMasuk.Cursor = Cursors.Hand;
            btnMasuk.FlatAppearance.BorderSize = 0;
            btnMasuk.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 176, 0);
            btnMasuk.FlatStyle = FlatStyle.Flat;
            btnMasuk.Font = new Font("Inter", 10F, FontStyle.Bold);
            btnMasuk.ForeColor = Color.FromArgb(31, 41, 55);
            btnMasuk.Location = new Point(40, 460);
            btnMasuk.Margin = new Padding(3, 4, 3, 4);
            btnMasuk.Name = "btnMasuk";
            btnMasuk.Size = new Size(457, 53);
            btnMasuk.TabIndex = 5;
            btnMasuk.Text = "Masuk";
            btnMasuk.UseVisualStyleBackColor = false;
            btnMasuk.Click += btnMasuk_Click;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.Font = new Font("Inter", 10F);
            txtPassword.ForeColor = Color.FromArgb(31, 41, 55);
            txtPassword.Location = new Point(40, 363);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Masukkan kata sandi";
            txtPassword.Size = new Size(457, 26);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.Font = new Font("Inter", 10F);
            txtUsername.ForeColor = Color.FromArgb(31, 41, 55);
            txtUsername.Location = new Point(40, 263);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Masukkan nama pengguna";
            txtUsername.Size = new Size(457, 26);
            txtUsername.TabIndex = 3;
            // 
            // lblJudul
            // 
            lblJudul.AutoSize = true;
            lblJudul.Font = new Font("Inter", 20F, FontStyle.Bold);
            lblJudul.ForeColor = Color.FromArgb(31, 41, 55);
            lblJudul.Location = new Point(40, 156);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new Size(122, 39);
            lblJudul.TabIndex = 2;
            lblJudul.Text = "Masuk";
            // 
            // pictureLogo
            // 
            pictureLogo.BackColor = Color.Transparent;
            pictureLogo.Image = Properties.Resources.logo;
            pictureLogo.Location = new Point(40, 56);
            pictureLogo.Margin = new Padding(3, 4, 3, 4);
            pictureLogo.Name = "pictureLogo";
            pictureLogo.Size = new Size(46, 53);
            pictureLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureLogo.TabIndex = 1;
            pictureLogo.TabStop = false;
            // 
            // lblMerk
            // 
            lblMerk.AutoSize = true;
            lblMerk.Font = new Font("Inter", 14F, FontStyle.Bold);
            lblMerk.ForeColor = Color.FromArgb(31, 41, 55);
            lblMerk.Location = new Point(95, 67);
            lblMerk.Name = "lblMerk";
            lblMerk.Size = new Size(144, 29);
            lblMerk.TabIndex = 0;
            lblMerk.Text = "SUKIMART";
            // 
            // pnlKanan
            // 
            pnlKanan.BackColor = Color.FromArgb(255, 196, 0);
            pnlKanan.Controls.Add(nightControlBox1);
            pnlKanan.Controls.Add(lblKecil);
            pnlKanan.Controls.Add(lblBesar);
            pnlKanan.Dock = DockStyle.Fill;
            pnlKanan.Location = new Point(640, 0);
            pnlKanan.Margin = new Padding(3, 4, 3, 4);
            pnlKanan.Name = "pnlKanan";
            pnlKanan.Size = new Size(823, 960);
            pnlKanan.TabIndex = 2;
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
            nightControlBox1.EnableCloseColor = Color.FromArgb(31, 41, 55);
            nightControlBox1.EnableMaximizeButton = false;
            nightControlBox1.EnableMaximizeColor = Color.FromArgb(31, 41, 55);
            nightControlBox1.EnableMinimizeButton = true;
            nightControlBox1.EnableMinimizeColor = Color.FromArgb(31, 41, 55);
            nightControlBox1.Location = new Point(684, 0);
            nightControlBox1.MaximizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MaximizeHoverForeColor = Color.White;
            nightControlBox1.MinimizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MinimizeHoverForeColor = Color.White;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(139, 31);
            nightControlBox1.TabIndex = 3;
            // 
            // lblKecil
            // 
            lblKecil.AutoSize = true;
            lblKecil.Font = new Font("Inter", 12F);
            lblKecil.ForeColor = Color.FromArgb(31, 41, 55);
            lblKecil.Location = new Point(73, 501);
            lblKecil.Name = "lblKecil";
            lblKecil.Size = new Size(400, 25);
            lblKecil.TabIndex = 2;
            lblKecil.Text = "\"Kasir, stok, dan laporan dalam satu aplikasi\"";
            // 
            // lblBesar
            // 
            lblBesar.AutoSize = true;
            lblBesar.Font = new Font("Inter", 28F, FontStyle.Bold);
            lblBesar.ForeColor = Color.FromArgb(31, 41, 55);
            lblBesar.Location = new Point(73, 427);
            lblBesar.Name = "lblBesar";
            lblBesar.Size = new Size(541, 54);
            lblBesar.TabIndex = 1;
            lblBesar.Text = "Kelola toko lebih mudah";
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1463, 960);
            Controls.Add(pnlKanan);
            Controls.Add(pnlKiri);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Masuk";
            pnlKiri.ResumeLayout(false);
            pnlKartuLogin.ResumeLayout(false);
            pnlKartuLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).EndInit();
            pnlKanan.ResumeLayout(false);
            pnlKanan.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlKiri;
        private PanelKartu pnlKartuLogin;
        private Label lblMerk;
        private Panel pnlKanan;
        private PictureBox pictureLogo;
        private Label lblJudul;
        private TextBox txtUsername;
        private Button btnMasuk;
        private TextBox txtPassword;
        private Label lblKecil;
        private Label lblBesar;
        private Label lblPassword;
        private Label lblUsername;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
        private Label lblPassSalah;
        private Label lblUsnSalah;
    }
}