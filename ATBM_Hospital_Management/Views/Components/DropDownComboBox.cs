using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ATBM_Hospital_Management.Views
{
    /// <summary>
    /// ComboBox that always opens its dropdown downward, never upward.
    /// </summary>
    public class DropDownComboBox : ComboBox
    {
        private const int CBEM_FIRST = 0x1700;
        private const int WM_CTLCOLORLISTBOX = 0x0134;
        private const int WM_LBUTTONDOWN = 0x0201;

        // CB_SETDROPPEDWIDTH not needed; we intercept the dropdown position via WndProc
        private const int WM_CTLCOLOR = 0x0019;

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT { public int Left, Top, Right, Bottom; }

        private const int WM_COMMAND = 0x0111;
        private const int CBN_DROPDOWN = 7;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // After the dropdown opens, reposition it below the combobox
            if (m.Msg == WM_COMMAND && ((int)m.WParam >> 16) == CBN_DROPDOWN)
            {
                RepositionDropdown();
            }
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            RepositionDropdown();
        }

        private void RepositionDropdown()
        {
            try
            {
                // Find the dropdown list window (child of the combo)
                IntPtr hList = IntPtr.Zero;
                EnumChildWindows(this.Handle, (hWnd, lParam) =>
                {
                    var sb = new System.Text.StringBuilder(256);
                    GetClassName(hWnd, sb, 256);
                    if (sb.ToString() == "ComboLBox")
                    {
                        hList = hWnd;
                        return false;
                    }
                    return true;
                }, IntPtr.Zero);

                if (hList == IntPtr.Zero) return;

                // Get combobox screen position
                var cmbPt = this.PointToScreen(System.Drawing.Point.Empty);
                int cmbBottom = cmbPt.Y + this.Height;

                // Get current dropdown rect
                GetWindowRect(hList, out RECT r);
                int dropH = r.Bottom - r.Top;
                int dropW = r.Right - r.Left;

                // Force position: always below the combobox
                MoveWindow(hList, cmbPt.X, cmbBottom, dropW, dropH, true);
            }
            catch { /* ignore positioning errors */ }
        }

        private delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);
    }
}
