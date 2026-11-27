namespace Sukimart
{
    public partial class ItemBarangTerlaris : UserControl
    {
        public ItemBarangTerlaris()
        {
            InitializeComponent();
            DoubleBuffered = true;
            FontInter.Terapkan(this);
        }

        public void SetData(int peringkat, string namaProduk, string kodeBarang, string jumlahTerjualText, bool tampilkanGarisBawah)
        {
            lblPeringkat.Text = peringkat.ToString();
            lblNamaProduk.Text = string.IsNullOrWhiteSpace(namaProduk) ? kodeBarang : namaProduk;
            lblJumlahTerjual.Text = jumlahTerjualText;
            pnlGarisBawah.Visible = tampilkanGarisBawah;

            if (peringkat == 1)
            {
                lblPeringkat.BackColor = Color.FromArgb(255, 196, 0);
                lblPeringkat.ForeColor = Color.FromArgb(31, 41, 55);
            }
            else if (peringkat <= 3)
            {
                lblPeringkat.BackColor = Color.FromArgb(254, 243, 199);
                lblPeringkat.ForeColor = Color.FromArgb(146, 64, 14);
            }
            else
            {
                lblPeringkat.BackColor = Color.FromArgb(243, 244, 246);
                lblPeringkat.ForeColor = Color.FromArgb(75, 85, 99);
            }
        }
    }
}
