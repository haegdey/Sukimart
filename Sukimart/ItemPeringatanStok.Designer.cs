namespace Sukimart
{
    partial class ItemPeringatanStok
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
            lblNamaProduk = new Label();
            lblStok = new Label();
            lblStatus = new Label();
            pnlGarisBawah = new Panel();
            SuspendLayout();
            // 
            // lblNamaProduk
            // 
            lblNamaProduk.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblNamaProduk.AutoEllipsis = true;
            lblNamaProduk.Font = new Font("Inter", 9.5F, FontStyle.Regular);
            lblNamaProduk.ForeColor = Color.FromArgb(31, 41, 55);
            lblNamaProduk.Location = new Point(0, 6);
            lblNamaProduk.Name = "lblNamaProduk";
            lblNamaProduk.Size = new Size(190, 24);
            lblNamaProduk.TabIndex = 0;
            lblNamaProduk.Text = "Nama Produk";
            lblNamaProduk.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStok
            // 
            lblStok.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblStok.Font = new Font("Inter", 8.5F, FontStyle.Bold);
            lblStok.ForeColor = Color.FromArgb(180, 83, 9);
            lblStok.Location = new Point(196, 6);
            lblStok.Name = "lblStok";
            lblStok.Size = new Size(82, 24);
            lblStok.TabIndex = 1;
            lblStok.Text = "Sisa 0 pcs";
            lblStok.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblStatus.BackColor = Color.FromArgb(254, 226, 226);
            lblStatus.Font = new Font("Inter", 7.5F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(185, 28, 28);
            lblStatus.Location = new Point(284, 7);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(76, 22);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "HABIS";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlGarisBawah
            // 
            pnlGarisBawah.BackColor = Color.FromArgb(243, 244, 246);
            pnlGarisBawah.Dock = DockStyle.Bottom;
            pnlGarisBawah.Location = new Point(0, 35);
            pnlGarisBawah.Name = "pnlGarisBawah";
            pnlGarisBawah.Size = new Size(360, 1);
            pnlGarisBawah.TabIndex = 3;
            // 
            // ItemPeringatanStok
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblNamaProduk);
            Controls.Add(lblStok);
            Controls.Add(lblStatus);
            Controls.Add(pnlGarisBawah);
            Margin = new Padding(0);
            Name = "ItemPeringatanStok";
            Size = new Size(360, 36);
            ResumeLayout(false);
        }

        #endregion

        private Label lblNamaProduk;
        private Label lblStok;
        private Label lblStatus;
        private Panel pnlGarisBawah;
    }
}
