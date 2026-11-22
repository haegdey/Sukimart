using System.Globalization;

namespace Sukimart
{
    public partial class KartuProduk : UserControl
    {
        private static readonly CultureInfo BudayaIndonesia = new CultureInfo("id-ID");
        private bool terpilih;
        private Barang? barang;

        public event EventHandler? KartuDiklik;
        public event EventHandler? KartuDoubleKlik;

        public Barang? Barang => barang;

        public bool Terpilih
        {
            get => terpilih;
            set
            {
                if (terpilih != value)
                {
                    terpilih = value;
                    PerbaruiTampilanSeleksi();
                }
            }
        }

        public KartuProduk()
        {
            InitializeComponent();
            DoubleBuffered = true;
            FontInter.Terapkan(this);

            lblNama.Click += (s, e) => KartuDiklik?.Invoke(this, EventArgs.Empty);
            lblStok.Click += (s, e) => KartuDiklik?.Invoke(this, EventArgs.Empty);
            lblHarga.Click += (s, e) => KartuDiklik?.Invoke(this, EventArgs.Empty);
            Click += (s, e) => KartuDiklik?.Invoke(this, EventArgs.Empty);

            lblNama.DoubleClick += (s, e) => KartuDoubleKlik?.Invoke(this, EventArgs.Empty);
            lblStok.DoubleClick += (s, e) => KartuDoubleKlik?.Invoke(this, EventArgs.Empty);
            lblHarga.DoubleClick += (s, e) => KartuDoubleKlik?.Invoke(this, EventArgs.Empty);
            DoubleClick += (s, e) => KartuDoubleKlik?.Invoke(this, EventArgs.Empty);
        }

        public void SetBarang(Barang b, bool isTerpilih = false)
        {
            this.barang = b;
            this.terpilih = isTerpilih;

            lblNama.Text = b.Nama;
            lblHarga.Text = "Rp " + b.Harga.ToString("N0", BudayaIndonesia);

            if (b.Stok <= 0)
            {
                lblStok.Text = "Stok: Habis";
                lblStok.ForeColor = Color.FromArgb(220, 38, 38);
                lblNama.ForeColor = Color.FromArgb(156, 163, 175);
                lblHarga.ForeColor = Color.FromArgb(156, 163, 175);
            }
            else
            {
                lblStok.Text = $"Stok: {b.Stok}";
                lblStok.ForeColor = Color.FromArgb(107, 114, 128);
                lblNama.ForeColor = Color.FromArgb(31, 41, 55);
                lblHarga.ForeColor = Color.FromArgb(16, 149, 102);
            }

            PerbaruiTampilanSeleksi();
        }

        private void PerbaruiTampilanSeleksi()
        {
            if (terpilih)
            {
                BackColor = Color.FromArgb(255, 250, 230); // Kuning muda lembut aksen SukiMart
            }
            else
            {
                BackColor = Color.White;
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color warnaGaris = terpilih ? Color.FromArgb(255, 196, 0) : Color.FromArgb(229, 231, 235);
            int tebal = terpilih ? 2 : 1;
            using Pen pena = new Pen(warnaGaris, tebal);
            e.Graphics.DrawRectangle(pena, tebal - 1, tebal - 1, ClientSize.Width - tebal, ClientSize.Height - tebal);
        }
    }
}
