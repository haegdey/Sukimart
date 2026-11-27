namespace Sukimart
{
    partial class ItemBarangTerlaris
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            lblPeringkat = new Label();
            lblNamaProduk = new Label();
            lblJumlahTerjual = new Label();
            pnlGarisBawah = new Panel();
            SuspendLayout();
            // 
            // lblPeringkat
            // 
            lblPeringkat.BackColor = Color.FromArgb(255, 196, 0);
            lblPeringkat.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            lblPeringkat.ForeColor = Color.FromArgb(31, 41, 55);
            lblPeringkat.Location = new Point(0, 5);
            lblPeringkat.Name = "lblPeringkat";
            lblPeringkat.Size = new Size(24, 22);
            lblPeringkat.TabIndex = 0;
            lblPeringkat.Text = "1";
            lblPeringkat.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblNamaProduk
            // 
            lblNamaProduk.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblNamaProduk.AutoEllipsis = true;
            lblNamaProduk.Font = new Font("Inter", 9.5F, FontStyle.Regular);
            lblNamaProduk.ForeColor = Color.FromArgb(31, 41, 55);
            lblNamaProduk.Location = new Point(34, 4);
            lblNamaProduk.Name = "lblNamaProduk";
            lblNamaProduk.Size = new Size(280, 24);
            lblNamaProduk.TabIndex = 1;
            lblNamaProduk.Text = "Nama Produk";
            lblNamaProduk.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblJumlahTerjual
            // 
            lblJumlahTerjual.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblJumlahTerjual.Font = new Font("Inter", 9.5F, FontStyle.Bold);
            lblJumlahTerjual.ForeColor = Color.FromArgb(31, 41, 55);
            lblJumlahTerjual.Location = new Point(320, 4);
            lblJumlahTerjual.Name = "lblJumlahTerjual";
            lblJumlahTerjual.Size = new Size(96, 24);
            lblJumlahTerjual.TabIndex = 2;
            lblJumlahTerjual.Text = "0 pcs";
            lblJumlahTerjual.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlGarisBawah
            // 
            pnlGarisBawah.BackColor = Color.FromArgb(243, 244, 246);
            pnlGarisBawah.Dock = DockStyle.Bottom;
            pnlGarisBawah.Location = new Point(0, 31);
            pnlGarisBawah.Name = "pnlGarisBawah";
            pnlGarisBawah.Size = new Size(420, 1);
            pnlGarisBawah.TabIndex = 3;
            // 
            // ItemBarangTerlaris
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblPeringkat);
            Controls.Add(lblNamaProduk);
            Controls.Add(lblJumlahTerjual);
            Controls.Add(pnlGarisBawah);
            Margin = new Padding(0);
            Name = "ItemBarangTerlaris";
            Size = new Size(420, 32);
            ResumeLayout(false);
        }

        #endregion

        private Label lblPeringkat;
        private Label lblNamaProduk;
        private Label lblJumlahTerjual;
        private Panel pnlGarisBawah;
    }
}
