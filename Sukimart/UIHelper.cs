using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sukimart
{
    /// <summary>
    /// Helper utilitas UI Sukimart:
    /// 1. Fitur drag/geser form ketika FormBorderStyle = None.
    /// 2. Menjaga warna seleksi DataGridView tetap kuning khas Sukimart (bukan biru bawaan Windows).
    /// </summary>
    public static class UIHelper
    {
        // Warna seleksi kuning muda & teks gelap serasi dengan tema Sukimart
        public static readonly Color WarnaSeleksiKuning = Color.FromArgb(255, 244, 204);
        public static readonly Color WarnaTeksSeleksi = Color.FromArgb(31, 41, 55);

        // Win32 API resmi untuk memindahkan jendela tanpa title bar secara halus dan responsif
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        /// <summary>
        /// Mendaftarkan kontrol agar ketika diklik dan digeser (drag),
        /// form utama akan bergerak mengikuti mouse.
        /// </summary>
        public static void PasangDrag(Form targetForm, Control kontrol)
        {
            if (targetForm == null || kontrol == null) return;

            kontrol.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (e.Clicks == 2 && targetForm.MaximizeBox)
                    {
                        AlihkanMaximize(targetForm);
                        return;
                    }

                    ReleaseCapture();
                    SendMessage(targetForm.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                }
            };
        }

        public static void AlihkanMaximize(Form form)
        {
            if (form == null || !form.MaximizeBox) return;

            if (form.WindowState == FormWindowState.Maximized)
            {
                form.WindowState = FormWindowState.Normal;
                form.Size = new Size(1280, 720);
                PusatkanForm(form);
            }
            else
            {
                form.WindowState = FormWindowState.Maximized;
            }
        }

        public static void PusatkanForm(Form form)
        {
            if (form == null) return;
            Screen layar = Screen.FromControl(form);
            form.Location = new Point(
                layar.WorkingArea.Left + Math.Max(0, (layar.WorkingArea.Width - form.Width) / 2),
                layar.WorkingArea.Top + Math.Max(0, (layar.WorkingArea.Height - form.Height) / 2)
            );
        }

        /// <summary>
        /// Merapatkan tombol-tombol menu sidebar yang terlihat sehingga tidak ada lubang kosong
        /// ketika sebagian tombol disembunyikan berdasarkan peran pengguna (Kasir / Gudang).
        /// </summary>
        public static void SusunTombolSidebar(Panel pnlKiri)
        {
            if (pnlKiri == null) return;

            if (!pnlKiri.Visible)
            {
                PastikanTombolBawah(pnlKiri);
                pnlKiri.VisibleChanged -= SusunSaatTampil;
                pnlKiri.VisibleChanged += SusunSaatTampil;
                return;
            }

            PastikanTombolLogAktivitas(pnlKiri);

            // Dapatkan tombol acuan (seperti btnPengguna atau btnDashboard) untuk memastikan ukuran & padding seragam
            Control[] acuanArr = pnlKiri.Controls.Find("btnPengguna", false);
            if (acuanArr.Length == 0) acuanArr = pnlKiri.Controls.Find("btnDashboard", false);
            Button? acuan = acuanArr.Length > 0 ? acuanArr[0] as Button : null;

            int x = acuan != null ? acuan.Location.X : 16;
            int lebar = acuan != null ? acuan.Width : 208;
            int tinggi = acuan != null ? acuan.Height : 40;
            Padding padding = acuan != null ? acuan.Padding : new Padding(16, 0, 0, 0);

            Control[] btnDashArr = pnlKiri.Controls.Find("btnDashboard", false);
            int y = btnDashArr.Length > 0 ? btnDashArr[0].Location.Y : 112;
            int jarak = 8;
            if (tinggi != 40)
            {
                jarak = Math.Max(6, (int)Math.Round(tinggi * 0.2));
            }

            string[] urutanTombol = { "btnDashboard", "btnKasir", "btnKatalog", "btnRiwayat", "btnGudang", "btnLaporan", "btnLaporanInventaris", "btnPengguna", "btnLogAktivitas" };

            foreach (string nama in urutanTombol)
            {
                Control[] ditemukan = pnlKiri.Controls.Find(nama, false);
                if (ditemukan.Length > 0 && ditemukan[0] is Button btn && btn.Visible)
                {
                    btn.Size = new Size(lebar, tinggi);
                    btn.Padding = padding;
                    btn.Location = new Point(x, y);
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 244, 204);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.UseVisualStyleBackColor = false;

                    float ukuranTombol = btn.Font != null ? btn.Font.SizeInPoints : (acuan != null ? acuan.Font.SizeInPoints : 10F);
                    FontStyle gayaTombol = btn.Font != null ? btn.Font.Style : FontStyle.Regular;
                    btn.Font = FontInter.Buat(ukuranTombol, gayaTombol);

                    y += btn.Height + jarak;
                }
            }

            Control[] lblNamaArr = pnlKiri.Controls.Find("lblNama", false);
            if (lblNamaArr.Length > 0 && lblNamaArr[0] is Label lblNama)
            {
                float ukuranNama = lblNama.Font != null ? lblNama.Font.SizeInPoints : 14F;
                FontStyle gayaNama = lblNama.Font != null ? lblNama.Font.Style : FontStyle.Bold;
                lblNama.Font = FontInter.Buat(ukuranNama, gayaNama);
            }

            PastikanTombolBawah(pnlKiri);
        }

        private static readonly ConditionalWeakTable<Button, ToolTip> TooltipTombol = new ConditionalWeakTable<Button, ToolTip>();

        public static void PastikanTombolBawah(Panel pnlKiri)
        {
            if (pnlKiri == null) return;

            Form? formInduk = pnlKiri.FindForm();
            if (formInduk is FormLogin) return;

            Control[] acuanArr = pnlKiri.Controls.Find("btnPengguna", false);
            if (acuanArr.Length == 0) acuanArr = pnlKiri.Controls.Find("btnDashboard", false);
            Button? acuan = acuanArr.Length > 0 ? acuanArr[0] as Button : null;

            int x = acuan != null ? acuan.Location.X : 16;
            int lebar = acuan != null ? acuan.Width : 208;
            int tinggi = acuan != null ? acuan.Height : 40;
            float ukuranFont = acuan != null ? acuan.Font.SizeInPoints : 10F;

            // Cari tombol Keluar
            Control[] keluarArr = pnlKiri.Controls.Find("btnKeluar", false);
            if (keluarArr.Length == 0) keluarArr = pnlKiri.Controls.Find("btnKeluarBenar", false);
            Button? btnKeluar = keluarArr.Length > 0 ? keluarArr[0] as Button : null;

            int paddingBawah = 16;
            int yKeluar;
            if (btnKeluar != null)
            {
                if (pnlKiri.ClientSize.Height > tinggi + paddingBawah + 60)
                {
                    yKeluar = pnlKiri.ClientSize.Height - tinggi - paddingBawah;
                }
                else
                {
                    yKeluar = btnKeluar.Location.Y;
                }

                btnKeluar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                btnKeluar.Size = new Size(lebar, tinggi);
                btnKeluar.Location = new Point(x, yKeluar);
                btnKeluar.Font = FontInter.Buat(ukuranFont, FontStyle.Bold);
                btnKeluar.BackColor = Color.White;
                btnKeluar.ForeColor = Color.FromArgb(220, 38, 38);
                btnKeluar.FlatStyle = FlatStyle.Flat;
                btnKeluar.FlatAppearance.BorderSize = 1;
                btnKeluar.FlatAppearance.BorderColor = Color.FromArgb(239, 68, 68);
                btnKeluar.FlatAppearance.MouseOverBackColor = Color.FromArgb(254, 242, 242);
                btnKeluar.Cursor = Cursors.Hand;
                btnKeluar.TextAlign = ContentAlignment.MiddleCenter;
                btnKeluar.Padding = new Padding(0);
                btnKeluar.Text = "Keluar";
                btnKeluar.UseVisualStyleBackColor = false;
                btnKeluar.Visible = true;
            }
            else
            {
                yKeluar = pnlKiri.ClientSize.Height - tinggi - paddingBawah;
            }

            int lebarBtnGanti = 38;
            int tinggiBtnGanti = 38;
            int jarakKeluarInfo = 12;
            int tinggiInfo = 42;
            int yInfo = yKeluar - tinggiInfo - jarakKeluarInfo;
            int lebarTeksAkun = Math.Max(50, lebar - lebarBtnGanti - 8);

            // Cari atau buat tombol Ganti Kata Sandi (ikon kunci saja)
            Control[] gantiArr = pnlKiri.Controls.Find("btnGantiSandi", false);
            if (gantiArr.Length == 0) gantiArr = pnlKiri.Controls.Find("btnGantiPassword", false);
            Button btnGanti;
            if (gantiArr.Length > 0 && gantiArr[0] is Button btnAda)
            {
                btnGanti = btnAda;
            }
            else
            {
                btnGanti = new Button
                {
                    Name = "btnGantiSandi",
                    Cursor = Cursors.Hand,
                    FlatStyle = FlatStyle.Flat,
                    UseVisualStyleBackColor = false,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                pnlKiri.Controls.Add(btnGanti);
            }

            PasangHandlerGantiSandi(btnGanti);

            btnGanti.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnGanti.Size = new Size(lebarBtnGanti, tinggiBtnGanti);
            btnGanti.Location = new Point(x + lebar - lebarBtnGanti, yInfo + 2);
            btnGanti.Text = string.Empty;
            btnGanti.Image = Properties.Resources.kunci;
            btnGanti.ImageAlign = ContentAlignment.MiddleCenter;
            btnGanti.FlatStyle = FlatStyle.Flat;
            btnGanti.FlatAppearance.BorderSize = 0;
            btnGanti.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 244, 204);
            btnGanti.BackColor = Color.White;
            btnGanti.Cursor = Cursors.Hand;
            btnGanti.Padding = new Padding(0);
            btnGanti.Visible = true;

            if (!TooltipTombol.TryGetValue(btnGanti, out _))
            {
                ToolTip tip = new ToolTip();
                tip.SetToolTip(btnGanti, "Ganti Kata Sandi");
                TooltipTombol.Add(btnGanti, tip);
            }

            // Cari atau buat lblAkunNama
            Control[] namaArr = pnlKiri.Controls.Find("lblAkunNama", false);
            Label lblAkunNama;
            if (namaArr.Length > 0 && namaArr[0] is Label lNama)
            {
                lblAkunNama = lNama;
            }
            else
            {
                lblAkunNama = new Label
                {
                    Name = "lblAkunNama",
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                pnlKiri.Controls.Add(lblAkunNama);
            }

            lblAkunNama.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblAkunNama.Location = new Point(x, yInfo);
            lblAkunNama.Size = new Size(lebarTeksAkun, 22);
            lblAkunNama.Font = FontInter.Buat(ukuranFont >= 10F ? 10.5F : 9.5F, FontStyle.Bold);
            lblAkunNama.ForeColor = Color.FromArgb(31, 41, 55);
            lblAkunNama.AutoEllipsis = true;
            lblAkunNama.TextAlign = ContentAlignment.MiddleLeft;
            lblAkunNama.Text = Navigasi.DapatkanNamaTampilan();
            lblAkunNama.BackColor = Color.Transparent;
            lblAkunNama.Visible = true;

            // Cari atau buat lblAkunRole
            Control[] roleArr = pnlKiri.Controls.Find("lblAkunRole", false);
            Label lblAkunRole;
            if (roleArr.Length > 0 && roleArr[0] is Label lRole)
            {
                lblAkunRole = lRole;
            }
            else
            {
                lblAkunRole = new Label
                {
                    Name = "lblAkunRole",
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                };
                pnlKiri.Controls.Add(lblAkunRole);
            }

            lblAkunRole.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblAkunRole.Location = new Point(x, yInfo + 22);
            lblAkunRole.Size = new Size(lebarTeksAkun, 18);
            lblAkunRole.Font = FontInter.Buat(ukuranFont >= 10F ? 8.5F : 8F, FontStyle.Regular);
            lblAkunRole.ForeColor = Color.FromArgb(107, 114, 128);
            lblAkunRole.AutoEllipsis = true;
            lblAkunRole.TextAlign = ContentAlignment.MiddleLeft;
            lblAkunRole.Text = Navigasi.DapatkanLabelRole();
            lblAkunRole.BackColor = Color.Transparent;
            lblAkunRole.Visible = true;

            lblAkunNama.BringToFront();
            lblAkunRole.BringToFront();
            btnGanti.BringToFront();
            if (btnKeluar != null) btnKeluar.BringToFront();
        }

        public static void PastikanTombolLogAktivitas(Panel pnlKiri)
        {
            if (pnlKiri == null) return;

            Form? formInduk = pnlKiri.FindForm();
            bool isAdmin = string.Equals(Navigasi.PeranAktif, "admin", StringComparison.OrdinalIgnoreCase);

            Control[] acuanArr = pnlKiri.Controls.Find("btnPengguna", false);
            if (acuanArr.Length == 0) acuanArr = pnlKiri.Controls.Find("btnDashboard", false);
            Button? acuan = acuanArr.Length > 0 ? acuanArr[0] as Button : null;

            int lebar = acuan != null ? acuan.Width : 208;
            int tinggi = acuan != null ? acuan.Height : 40;
            int x = acuan != null ? acuan.Location.X : 16;
            Padding padding = acuan != null ? acuan.Padding : new Padding(16, 0, 0, 0);
            float ukuranFont = acuan != null ? acuan.Font.SizeInPoints : 10F;

            Control[] ditemukan = pnlKiri.Controls.Find("btnLogAktivitas", false);
            if (ditemukan.Length > 0 && ditemukan[0] is Button btnAda)
            {
                btnAda.Visible = isAdmin;
                btnAda.Size = new Size(lebar, tinggi);
                btnAda.Padding = padding;
                btnAda.Location = new Point(x, btnAda.Location.Y);
                btnAda.FlatAppearance.BorderSize = 0;
                btnAda.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 244, 204);
                btnAda.FlatStyle = FlatStyle.Flat;
                btnAda.UseVisualStyleBackColor = false;
                if (formInduk is FormLogAktivitas)
                {
                    btnAda.BackColor = Color.FromArgb(255, 196, 0);
                    btnAda.Font = FontInter.Buat(ukuranFont, FontStyle.Bold);
                }
                else
                {
                    btnAda.BackColor = Color.White;
                    btnAda.Font = FontInter.Buat(ukuranFont, FontStyle.Regular);
                    PasangHandlerLogAktivitas(btnAda);
                }
                return;
            }

            Button btnLog = new Button
            {
                Name = "btnLogAktivitas",
                Text = "Log Aktivitas",
                Size = new Size(lebar, tinggi),
                Location = new Point(x, 448),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = (formInduk is FormLogAktivitas) ? FontInter.Buat(ukuranFont, FontStyle.Bold) : FontInter.Buat(ukuranFont, FontStyle.Regular),
                BackColor = (formInduk is FormLogAktivitas) ? Color.FromArgb(255, 196, 0) : Color.White,
                ForeColor = Color.FromArgb(31, 41, 55),
                Padding = padding,
                TextAlign = ContentAlignment.MiddleLeft,
                UseVisualStyleBackColor = false,
                Visible = isAdmin
            };
            btnLog.FlatAppearance.BorderSize = 0;
            btnLog.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 244, 204);

            PasangHandlerLogAktivitas(btnLog);

            pnlKiri.Controls.Add(btnLog);
        }

        private static readonly ConditionalWeakTable<Button, object> TombolTerpasang = new ConditionalWeakTable<Button, object>();

        private static void PasangHandlerGantiSandi(Button btn)
        {
            if (TombolTerpasang.TryGetValue(btn, out _)) return;
            TombolTerpasang.Add(btn, new object());
            btn.Click += (s, e) =>
            {
                Form? f = btn.FindForm();
                if (f != null)
                {
                    Navigasi.BukaGantiKataSandi(f);
                }
            };
        }

        private static void PasangHandlerLogAktivitas(Button btn)
        {
            if (TombolTerpasang.TryGetValue(btn, out _)) return;
            TombolTerpasang.Add(btn, new object());
            btn.Click += (s, e) =>
            {
                Form? f = btn.FindForm();
                if (f != null && !(f is FormLogAktivitas))
                {
                    Navigasi.BukaLogAktivitas(f);
                }
            };
        }

        public static void RatakanTengah(params DataGridViewColumn[] daftarKolom)
        {
            foreach (DataGridViewColumn kolom in daftarKolom)
            {
                kolom.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                kolom.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private static void SusunSaatTampil(object? sender, EventArgs e)
        {
            if (sender is Panel pnl && pnl.Visible)
            {
                pnl.VisibleChanged -= SusunSaatTampil;
                SusunTombolSidebar(pnl);
            }
        }

        /// <summary>
        /// Menerapkan warna latar belakang seleksi kuning pada DataGridView.
        /// </summary>
        public static void TerapkanWarnaGrid(DataGridView grid)
        {
            if (grid == null) return;

            // Baris data (DefaultCellStyle)
            grid.DefaultCellStyle.SelectionBackColor = WarnaSeleksiKuning;
            grid.DefaultCellStyle.SelectionForeColor = WarnaTeksSeleksi;

            // Header Kolom
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = grid.ColumnHeadersDefaultCellStyle.BackColor.IsEmpty ? Color.FromArgb(243, 244, 246) : grid.ColumnHeadersDefaultCellStyle.BackColor;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = grid.ColumnHeadersDefaultCellStyle.ForeColor.IsEmpty ? Color.FromArgb(55, 65, 81) : grid.ColumnHeadersDefaultCellStyle.ForeColor;

            // Header Baris
            grid.RowHeadersDefaultCellStyle.SelectionBackColor = WarnaSeleksiKuning;
            grid.RowHeadersDefaultCellStyle.SelectionForeColor = WarnaTeksSeleksi;
        }

        /// <summary>
        /// Inisialisasi otomatis untuk satu form:
        /// 1. Memasang fitur drag pada area kosong (Panel, Label, Logo).
        /// 2. Memastikan seluruh DataGridView memiliki warna seleksi kuning.
        /// </summary>
        public static void InisialisasiForm(Form form)
        {
            if (form == null) return;

            FontInter.Terapkan(form);

            PasangDrag(form, form);

            // Pasang drag dan tema grid secara menyeluruh ke semua kontrol di form
            PasangRekursif(form, form);

            Control[] pnlKiriArr = form.Controls.Find("pnlKiri", true);
            if (pnlKiriArr.Length > 0 && pnlKiriArr[0] is Panel pnlKiri && !(form is FormLogin))
            {
                PastikanTombolBawah(pnlKiri);
                SusunTombolSidebar(pnlKiri);
            }
        }

        private static void PasangRekursif(Form targetForm, Control induk)
        {
            foreach (Control ctrl in induk.Controls)
            {
                // Kontrol yang aman untuk drag: Panel, Label, PictureBox
                // Tidak mengganggu tombol (Button), input (TextBox, ComboBox, NumericUpDown), maupun tabel (DataGridView)
                if (ctrl is Panel || ctrl is Label || ctrl is PictureBox)
                {
                    // Hindari kontrol box ReaLTaiizor (tombol close/minimize di pojok kanan atas)
                    if (!ctrl.GetType().Name.Contains("NightControlBox") && !ctrl.GetType().Name.Contains("ControlBox"))
                    {
                        PasangDrag(targetForm, ctrl);
                    }
                }

                // Jika kontrol adalah tabel DataGridView, set warna seleksi ke kuning
                if (ctrl is DataGridView grid)
                {
                    TerapkanWarnaGrid(grid);
                    RapikanGrid(grid);
                }

                if (PerluGarisTepi(ctrl))
                {
                    PasangGarisTepi(induk);
                }

                // Telusuri kontrol di dalam container (panel di dalam panel)
                if (ctrl.HasChildren)
                {
                    PasangRekursif(targetForm, ctrl);
                }
            }
        }

        public static readonly Color WarnaGarisTepi = Color.FromArgb(229, 231, 235);

        private static readonly ConditionalWeakTable<Control, object> IndukBergaris = new ConditionalWeakTable<Control, object>();

        public static void RapikanGrid(DataGridView grid)
        {
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            foreach (DataGridViewColumn kolom in grid.Columns)
            {
                RapikanKolom(kolom);
            }
            grid.ColumnAdded += (s, e) => RapikanKolom(e.Column);
        }

        // Kolom mata uang dan angka laporan: rata kanan (Harga, Total, Subtotal, Terjual, dsb.)
        private static readonly string[] KataKolomUang =
            { "Harga", "Total", "Subtotal", "Omzet", "Bayar", "Kembalian", "Pendapatan", "Modal", "Laba", "Terjual" };

        // Kolom angka biasa (Stok, Qty, Jumlah): rata tengah supaya rapi
        private static readonly string[] KataKolomAngka =
            { "Stok", "Qty", "Jumlah", "Min" };

        private static void RapikanKolom(DataGridViewColumn kolom)
        {
            if (kolom.InheritedAutoSizeMode == DataGridViewAutoSizeColumnMode.Fill)
            {
                kolom.MinimumWidth = 64;
            }

            string propOrHeader = (kolom.DataPropertyName ?? "") + "|" + (kolom.HeaderText ?? "");

            // Kolom identitas dan teks: rata kiri
            string[] kataKolomTeks = { "Kode", "Nama", "Produk", "Barang", "Kategori", "Kasir", "Faktur", "Waktu", "Tanggal", "Keterangan", "Pemasok", "Alasan", "Petugas" };
            foreach (string kata in kataKolomTeks)
            {
                if (propOrHeader.IndexOf(kata, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    kolom.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    kolom.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    return;
                }
            }

            string gabungan = (kolom.Name ?? "") + "|" + propOrHeader;

            // Kolom mata uang dan angka laporan: rata kanan, header pun rata kanan
            foreach (string kata in KataKolomUang)
            {
                if (gabungan.IndexOf(kata, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    kolom.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    kolom.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    return;
                }
            }

            // Kolom angka stok/qty: rata tengah, header juga tengah
            foreach (string kata in KataKolomAngka)
            {
                if (gabungan.IndexOf(kata, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    kolom.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    kolom.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    return;
                }
            }
        }

        private static bool PerluGarisTepi(Control ctrl)
        {
            if (ctrl is DataGridView)
            {
                return true;
            }
            return ctrl is Panel && ctrl.Dock == DockStyle.None && ctrl.BackColor.ToArgb() == Color.White.ToArgb();
        }

        private static void PasangGarisTepi(Control induk)
        {
            if (IndukBergaris.TryGetValue(induk, out _))
            {
                return;
            }
            IndukBergaris.Add(induk, new object());

            induk.Paint += (s, e) =>
            {
                using Pen pena = new Pen(WarnaGarisTepi);
                foreach (Control anak in induk.Controls)
                {
                    if (!anak.Visible || !PerluGarisTepi(anak))
                    {
                        continue;
                    }
                    Rectangle r = anak.Bounds;
                    r.Inflate(1, 1);
                    e.Graphics.DrawRectangle(pena, r.X, r.Y, r.Width - 1, r.Height - 1);
                }
            };

            foreach (Control anak in induk.Controls)
            {
                if (PerluGarisTepi(anak))
                {
                    anak.VisibleChanged += (s, e) => induk.Invalidate();
                    anak.LocationChanged += (s, e) => induk.Invalidate();
                    anak.SizeChanged += (s, e) => induk.Invalidate();
                }
            }
            induk.Invalidate();
        }
    }
}
