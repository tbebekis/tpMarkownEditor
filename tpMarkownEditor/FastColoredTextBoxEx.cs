using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

using FastColoredTextBoxNS;

namespace MarkdownEditor
{
     



    public class FastColoredTextBoxEx: FastColoredTextBox
    {
        const int WM_VSCROLL = 0x0115;
        const int WM_MOUSEWHEEL = 0x020A;

        public enum SBOrientation : int
        {
            SB_HORZ = 0x0,
            SB_VERT = 0x1,
            SB_CTL = 0x2,
            SB_BOTH = 0x3
        }

        public enum ScrollInfoMask : uint
        {
            SIF_RANGE = 0x1,
            SIF_PAGE = 0x2,
            SIF_POS = 0x4,
            SIF_DISABLENOSCROLL = 0x8,
            SIF_TRACKPOS = 0x10,
            SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS),
            SIF_POSRANGE = (SIF_RANGE | SIF_POS | SIF_PAGE)
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct SCROLLINFO
        {
            public int cbSize; // (uint) int is because of Marshal.SizeOf
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }

        /* private */
        [DllImport("User32.dll")]
        static extern int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);

        static SCROLLINFO GetAbsoluteMaxVScroll(IntPtr hwnd)
        {
            SCROLLINFO si = new SCROLLINFO();
            si.cbSize = Marshal.SizeOf(si);
            si.fMask = (int)ScrollInfoMask.SIF_POSRANGE;
            GetScrollInfo(hwnd, (int)SBOrientation.SB_VERT, ref si);
            return si;
        }
        static int GetAbsoluteMaxVScrollValue(IntPtr hwnd)
        {
            return GetAbsoluteMaxVScroll(hwnd).nMax;
        }

        int GetBrowserScrollableHeight()
        {
            var wbAll = Browser.Document.All.Cast<HtmlElement>();
            var maxHeight = wbAll.Max(x => Math.Max(x.ClientRectangle.Height, x.ScrollRectangle.Height));
            return maxHeight;
            //return Browser.Document.Body.ScrollRectangle.Height;
        }

        /* overrides */
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if ((m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL) && Browser != null && Browser.Document != null && Browser.Document.Window != null )
            {
                int Max = GetAbsoluteMaxVScrollValue(Handle);
                int BrowserMax = GetBrowserScrollableHeight(); 
                double RelativeScrollDiff = (BrowserMax - Browser.Document.Body.ClientRectangle.Height) / (Max - ClientSize.Height); //

                int Pos = GetScrollPos(Handle, (int)SBOrientation.SB_VERT);
                Pos = Convert.ToInt32(Pos * RelativeScrollDiff);
                //Browser.Document.Body.ScrollTop = Pos;
                Browser.Document.Window.ScrollTo(0, Pos);
            } 
 
        }
 
        /* construction */
        public FastColoredTextBoxEx()
            : base()
        {
        }

        /* properties */
        public WebBrowser Browser { get; set; }
 
    }

}
