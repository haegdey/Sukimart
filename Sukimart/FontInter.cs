using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sukimart
{
    public static class FontInter
    {
        private const string NamaFont = "Inter";
        private const string FontCadangan = "Segoe UI";

        private static readonly PrivateFontCollection koleksi = new PrivateFontCollection();
        private static readonly List<IntPtr> memoriFont = new List<IntPtr>();
        private static readonly Dictionary<(int, FontStyle), Font> cacheFont = new Dictionary<(int, FontStyle), Font>();
        private static readonly HashSet<Font> fontInterDibuat = new HashSet<Font>();
        private static readonly object kunciKoleksi = new object();

        private static FontFamily? keluarga;
        private static bool sudahDimuat;

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, ref uint pcFonts);

        public static bool Tersedia => keluarga != null;

        public static void Muat()
        {
            if (sudahDimuat) return;

            lock (kunciKoleksi)
            {
                if (sudahDimuat) return;
                sudahDimuat = true;

                try
                {
                    Assembly assembly = typeof(FontInter).Assembly;
                    foreach (string namaResource in assembly.GetManifestResourceNames())
                    {
                        if (!namaResource.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase)) continue;

                        using Stream? aliran = assembly.GetManifestResourceStream(namaResource);
                        if (aliran == null) continue;

                        byte[] data = new byte[aliran.Length];
                        aliran.ReadExactly(data);

                        IntPtr penunjuk = Marshal.AllocCoTaskMem(data.Length);
                        Marshal.Copy(data, 0, penunjuk, data.Length);
                        memoriFont.Add(penunjuk);

                        koleksi.AddMemoryFont(penunjuk, data.Length);

                        uint jumlahFont = 0;
                        AddFontMemResourceEx(penunjuk, (uint)data.Length, IntPtr.Zero, ref jumlahFont);
                    }

                    keluarga = koleksi.Families.FirstOrDefault(f =>
                        string.Equals(f.Name, NamaFont, StringComparison.OrdinalIgnoreCase));
                }
                catch (Exception)
                {
                    keluarga = null;
                }
            }
        }

        public static Font Buat(float ukuran, FontStyle gaya = FontStyle.Regular)
        {
            Muat();

            int kunciUkuran = (int)Math.Round(ukuran * 100);
            var kunci = (kunciUkuran, gaya);

            lock (kunciKoleksi)
            {
                if (cacheFont.TryGetValue(kunci, out Font? fontCached))
                {
                    return fontCached;
                }

                Font fontBaru;
                if (keluarga != null && keluarga.IsStyleAvailable(gaya))
                {
                    fontBaru = new Font(keluarga, ukuran, gaya, GraphicsUnit.Point);
                }
                else
                {
                    fontBaru = new Font(FontCadangan, ukuran, gaya, GraphicsUnit.Point);
                }

                cacheFont[kunci] = fontBaru;
                fontInterDibuat.Add(fontBaru);
                return fontBaru;
            }
        }

        public static void Terapkan(Control kontrol)
        {
            if (kontrol == null) return;

            Muat();

            GantiFontKontrol(kontrol);

            if (kontrol is DataGridView grid)
            {
                GantiFontGrid(grid);
            }

            // Pasang event agar kontrol yang ditambahkan secara dinamis ke container ini otomatis mendapatkan font Inter
            kontrol.ControlAdded -= Kontrol_ControlAdded;
            kontrol.ControlAdded += Kontrol_ControlAdded;

            foreach (Control anak in kontrol.Controls)
            {
                Terapkan(anak);
            }
        }

        private static void Kontrol_ControlAdded(object? sender, ControlEventArgs e)
        {
            if (e.Control != null)
            {
                Terapkan(e.Control);
            }
        }

        public static bool SudahFontInter(Font? font)
        {
            if (font == null) return false;

            lock (kunciKoleksi)
            {
                return fontInterDibuat.Contains(font);
            }
        }

        private static bool PerluDiganti(Font? font)
        {
            if (font == null) return false;

            // Jika font sudah dibuat oleh FontInter, tidak perlu diganti lagi
            if (SudahFontInter(font))
            {
                return false;
            }

            // Ganti seluruh font yang belum berasal dari FontInter (misal: "Microsoft Sans Serif", "Inter" dari Designer, "Segoe UI", dll)
            return true;
        }

        private static Font Salin(Font font)
        {
            return Buat(font.SizeInPoints, font.Style);
        }

        private static void GantiFontKontrol(Control kontrol)
        {
            if (PerluDiganti(kontrol.Font))
            {
                kontrol.Font = Salin(kontrol.Font);
            }
        }

        private static void GantiFontGaya(DataGridViewCellStyle? gaya)
        {
            if (gaya != null && gaya.Font != null && PerluDiganti(gaya.Font))
            {
                gaya.Font = Salin(gaya.Font);
            }
        }

        private static void GantiFontGrid(DataGridView grid)
        {
            GantiFontGaya(grid.DefaultCellStyle);
            GantiFontGaya(grid.ColumnHeadersDefaultCellStyle);
            GantiFontGaya(grid.RowHeadersDefaultCellStyle);
            GantiFontGaya(grid.RowsDefaultCellStyle);
            GantiFontGaya(grid.AlternatingRowsDefaultCellStyle);

            foreach (DataGridViewColumn kolom in grid.Columns)
            {
                if (kolom.HasDefaultCellStyle)
                {
                    GantiFontGaya(kolom.DefaultCellStyle);
                }

                if (kolom.HeaderCell.HasStyle)
                {
                    GantiFontGaya(kolom.HeaderCell.Style);
                }
            }
        }
    }
}
