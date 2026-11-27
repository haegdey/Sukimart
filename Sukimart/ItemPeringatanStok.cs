namespace Sukimart
{
    public partial class ItemPeringatanStok : UserControl
    {
        public ItemPeringatanStok()
        {
            InitializeComponent();
            DoubleBuffered = true;
            FontInter.Terapkan(this);
        }

        public void SetData(string namaProduk, int stok, string status, bool tampilkanGarisBawah)
        {
            lblNamaProduk.Text = namaProduk;
            lblStok.Text = $"Sisa {stok} pcs";
            lblStatus.Text = status;
            pnlGarisBawah.Visible = tampilkanGarisBawah;

            switch (status)
            {
                case "HABIS":
                    lblStatus.BackColor = Color.FromArgb(254, 226, 226);
                    lblStatus.ForeColor = Color.FromArgb(185, 28, 28);
                    lblStok.ForeColor = Color.FromArgb(185, 28, 28);
                    break;
                case "KRITIS":
                    lblStatus.BackColor = Color.FromArgb(255, 237, 213);
                    lblStatus.ForeColor = Color.FromArgb(194, 65, 12);
                    lblStok.ForeColor = Color.FromArgb(194, 65, 12);
                    break;
                default: // MINIMUM
                    lblStatus.BackColor = Color.FromArgb(254, 243, 199);
                    lblStatus.ForeColor = Color.FromArgb(180, 83, 9);
                    lblStok.ForeColor = Color.FromArgb(180, 83, 9);
                    break;
            }
        }
    }
}
