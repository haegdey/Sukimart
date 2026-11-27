using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sukimart
{
    public class PanelKartu : Panel
    {
        private bool tampilkanAksen = true;
        private int tinggiAksen = 3;
        private Color warnaAksen = Color.FromArgb(255, 196, 0);
        private Color warnaGaris = Color.FromArgb(229, 231, 235);

        public PanelKartu()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.White;
        }

        [Category("Tampilan Sukimart"), DefaultValue(true)]
        public bool TampilkanAksen { get => tampilkanAksen; set { tampilkanAksen = value; Invalidate(); } }

        [Category("Tampilan Sukimart"), DefaultValue(3)]
        public int TinggiAksen { get => tinggiAksen; set { tinggiAksen = Math.Max(0, value); Invalidate(); } }

        [Category("Tampilan Sukimart"), DefaultValue(typeof(Color), "255, 196, 0")]
        public Color WarnaAksen { get => warnaAksen; set { warnaAksen = value; Invalidate(); } }

        [Category("Tampilan Sukimart"), DefaultValue(typeof(Color), "229, 231, 235")]
        public Color WarnaGaris { get => warnaGaris; set { warnaGaris = value; Invalidate(); } }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen p = new Pen(WarnaGaris)) e.Graphics.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
            if (TampilkanAksen && TinggiAksen > 0) using (SolidBrush b = new SolidBrush(WarnaAksen)) e.Graphics.FillRectangle(b, 0, 0, Width, TinggiAksen);
        }
    }
}
