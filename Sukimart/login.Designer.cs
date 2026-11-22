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
            ((System.ComponentModel.ISupportInitialize)pictureLogo).BeginInit();
            pnlKanan.SuspendLayout();
            SuspendLayout();
            // 
            // pnlKiri
            // 
            pnlKiri.BackColor = Color.White;
            pnlKiri.Controls.Add(lblPassSalah);
            pnlKiri.Controls.Add(lblUsnSalah);
            pnlKiri.Controls.Add(lblPassword);
            pnlKiri.Controls.Add(lblUsername);
            pnlKiri.Controls.Add(btnMasuk);
            pnlKiri.Controls.Add(txtPassword);
            pnlKiri.Controls.Add(txtUsername);
            pnlKiri.Controls.Add(lblJudul);
            pnlKiri.Controls.Add(pictureLogo);
            pnlKiri.Controls.Add(lblMerk);
            pnlKiri.Dock = DockStyle.Left;
            pnlKiri.Location = new Point(0, 0);
            pnlKiri.Margin = new Padding(3, 4, 3, 4);
            pnlKiri.Name = "pnlKiri";
            pnlKiri.Size = new Size(640, 960);
            pnlKiri.TabIndex = 1;
            // 
            // lblPassSalah
            // 
            lblPassSalah.AutoSize = true;
            lblPassSalah.Font = new Font("Microsoft Sans Serif", 9F);
            lblPassSalah.ForeColor = Color.FromArgb(220, 38, 38);
            lblPassSalah.Location = new Point(91, 547);
            lblPassSalah.Name = "lblPassSalah";
            lblPassSalah.Size = new Size(0, 18);
            lblPassSalah.TabIndex = 9;
            // 
            // lblUsnSalah
            // 
            lblUsnSalah.AutoSize = true;
            lblUsnSalah.Font = new Font("Microsoft Sans Serif", 9F);
            lblUsnSalah.ForeColor = Color.FromArgb(220, 38, 38);
            lblUsnSalah.Location = new Point(91, 440);
            lblUsnSalah.Name = "lblUsnSalah";
            lblUsnSalah.Size = new Size(0, 18);
            lblUsnSalah.TabIndex = 8;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblPassword.Location = new Point(91, 480);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(89, 18);
            lblPassword.TabIndex = 7;
            lblPassword.Text = "Kata Sandi";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblUsername.Location = new Point(91, 373);
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
            btnMasuk.FlatStyle = FlatStyle.Flat;
            btnMasuk.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnMasuk.ForeColor = Color.Black;
            btnMasuk.Location = new Point(91, 597);
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
            txtPassword.Font = new Font("Microsoft Sans Serif", 10F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Location = new Point(91, 507);
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
            txtUsername.Font = new Font("Microsoft Sans Serif", 10F);
            txtUsername.ForeColor = Color.Black;
            txtUsername.Location = new Point(91, 400);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Masukkan nama pengguna";
            txtUsername.Size = new Size(457, 26);
            txtUsername.TabIndex = 3;
            // 
            // lblJudul
            // 
            lblJudul.AutoSize = true;
            lblJudul.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Bold);
            lblJudul.ForeColor = Color.Black;
            lblJudul.Location = new Point(91, 277);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new Size(122, 39);
            lblJudul.TabIndex = 2;
            lblJudul.Text = "Masuk";
            // 
            // pictureLogo
            // 
            pictureLogo.BackColor = Color.Transparent;
            pictureLogo.Image = Properties.Resources.logo;
            pictureLogo.Location = new Point(91, 85);
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
            lblMerk.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            lblMerk.ForeColor = Color.Black;
            lblMerk.Location = new Point(146, 96);
            lblMerk.Name = "lblMerk";
            lblMerk.Size = new Size(144, 29);
            lblMerk.TabIndex = 0;
            lblMerk.Text = "SUKIMART";
            // 
            // pnlKanan
            // 
            pnlKanan.BackColor = Color.FromArgb(255, 244, 204);
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
            nightControlBox1.EnableCloseColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMaximizeButton = false;
            nightControlBox1.EnableMaximizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMinimizeButton = true;
            nightControlBox1.EnableMinimizeColor = Color.FromArgb(160, 160, 160);
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
            lblKecil.Font = new Font("Microsoft Sans Serif", 12F);
            lblKecil.Location = new Point(73, 501);
            lblKecil.Name = "lblKecil";
            lblKecil.Size = new Size(400, 25);
            lblKecil.TabIndex = 2;
            lblKecil.Text = "\"Kasir, stok, dan laporan dalam satu aplikasi\"";
            // 
            // lblBesar
            // 
            lblBesar.AutoSize = true;
            lblBesar.Font = new Font("Microsoft Sans Serif", 28F, FontStyle.Bold);
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
            pnlKiri.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).EndInit();
            pnlKanan.ResumeLayout(false);
            pnlKanan.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlKiri;
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