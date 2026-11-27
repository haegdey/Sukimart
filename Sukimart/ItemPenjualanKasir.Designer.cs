namespace Sukimart
{
    partial class ItemPenjualanKasir
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
            pnlAksenKasir = new Panel();
            lblUsernameKasir = new Label();
            lblTotalPenjualan = new Label();
            pnlGarisBawah = new Panel();
            SuspendLayout();
            // 
            // pnlAksenKasir
            // 
            pnlAksenKasir.BackColor = Color.FromArgb(255, 196, 0);
            pnlAksenKasir.Location = new Point(0, 9);
            pnlAksenKasir.Name = "pnlAksenKasir";
            pnlAksenKasir.Size = new Size(4, 14);
            pnlAksenKasir.TabIndex = 0;
            // 
            // lblUsernameKasir
            // 
            lblUsernameKasir.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblUsernameKasir.AutoEllipsis = true;
            lblUsernameKasir.Font = new Font("Inter", 9.5F, FontStyle.Regular);
            lblUsernameKasir.ForeColor = Color.FromArgb(31, 41, 55);
            lblUsernameKasir.Location = new Point(12, 4);
            lblUsernameKasir.Name = "lblUsernameKasir";
            lblUsernameKasir.Size = new Size(250, 24);
            lblUsernameKasir.TabIndex = 1;
            lblUsernameKasir.Text = "kasir";
            lblUsernameKasir.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalPenjualan
            // 
            lblTotalPenjualan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblTotalPenjualan.Font = new Font("Inter", 9.5F, FontStyle.Bold);
            lblTotalPenjualan.ForeColor = Color.FromArgb(16, 149, 102);
            lblTotalPenjualan.Location = new Point(268, 4);
            lblTotalPenjualan.Name = "lblTotalPenjualan";
            lblTotalPenjualan.Size = new Size(148, 24);
            lblTotalPenjualan.TabIndex = 2;
            lblTotalPenjualan.Text = "Rp 0";
            lblTotalPenjualan.TextAlign = ContentAlignment.MiddleRight;
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
            // ItemPenjualanKasir
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pnlAksenKasir);
            Controls.Add(lblUsernameKasir);
            Controls.Add(lblTotalPenjualan);
            Controls.Add(pnlGarisBawah);
            Margin = new Padding(0);
            Name = "ItemPenjualanKasir";
            Size = new Size(420, 32);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlAksenKasir;
        private Label lblUsernameKasir;
        private Label lblTotalPenjualan;
        private Panel pnlGarisBawah;
    }
}
