namespace Sukimart
{
    public partial class ItemPenjualanKasir : UserControl
    {
        public ItemPenjualanKasir()
        {
            InitializeComponent();
            DoubleBuffered = true;
            FontInter.Terapkan(this);
        }

        public void SetData(string usernameKasir, string totalNominalText, bool tampilkanGarisBawah)
        {
            lblUsernameKasir.Text = string.IsNullOrWhiteSpace(usernameKasir) ? "-" : usernameKasir;
            lblTotalPenjualan.Text = totalNominalText;
            pnlGarisBawah.Visible = tampilkanGarisBawah;
        }
    }
}
