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
    // https://stackoverflow.com/questions/67898346/synchronize-the-scroll-position-of-two-controls-with-different-content

    static public class OS
    {
        const int WM_VSCROLL = 0x0115;
        const int WM_MOUSEWHEEL = 0x020A;
        const int SB_HORZ = 0;
        const int SB_VERT = 1;

        const int EM_GETSCROLLPOS = 0x4DD;
        const int EM_SETSCROLLPOS = 0x4DE;
        const uint SB_THUMBPOSITION = 4;
        const int VERTICAL_SCROLL_BAR = 1;

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
        public enum SBOrientation : int
        {
            SB_HORZ = 0x0,
            SB_VERT = 0x1,
            SB_CTL = 0x2,
            SB_BOTH = 0x3
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

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);
 
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, ref Point lParam);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 wMsg, Int32 wParam, IntPtr lParam);


        static public SCROLLINFO GetAbsoluteMaxVScroll(IntPtr hwnd)
        {
            SCROLLINFO si = new SCROLLINFO();
            si.cbSize = Marshal.SizeOf(si);
            si.fMask = (int)ScrollInfoMask.SIF_POSRANGE;
            GetScrollInfo(hwnd, (int)SBOrientation.SB_HORZ, ref si);
            return si;
        }
        static public int GetAbsoluteMaxVScrollValue(IntPtr hwnd)
        {
            return GetAbsoluteMaxVScroll(hwnd).nMax;
        }

        static public double GetRelativeScrollDiff(int SourceMaxScroll, int DestMaxScroll, Control Source, Control Dest)
        {
            double Border = 1;
            double Result = (DestMaxScroll - Dest.ClientSize.Height) / (SourceMaxScroll - Source.ClientSize.Height - Border);
            return Result;
        }
        static public void SyncScrollPosition2(Control Source, Control Dest)
        {
            SCROLLINFO infoSource = GetAbsoluteMaxVScroll(Source.Handle);
            SCROLLINFO infoDest = GetAbsoluteMaxVScroll(Dest.Handle);
            double relScrollDiff = GetRelativeScrollDiff(infoSource.nMax, infoDest.nMax, Source, Dest);

            Point P = new Point();
            P.X = 0;
            P.Y = Convert.ToInt32((infoSource.nPos + 0.5F) * relScrollDiff);
            SendMessage(Dest.Handle, EM_SETSCROLLPOS, 0, ref P);
        }

        static public void SyncScrollPosition(Control Source, WebBrowser Dest)
        {
            SCROLLINFO infoSource = GetAbsoluteMaxVScroll(Source.Handle);
            //SCROLLINFO infoDest = GetAbsoluteMaxVScroll(Dest.Handle);
            int BrowserMaxVScroll = Dest.Document.Body.ScrollRectangle.Height;
            double relScrollDiff = GetRelativeScrollDiff(infoSource.nMax, BrowserMaxVScroll, Source, Dest);

            Point P = new Point();
            P.X = 0;
            P.Y = Convert.ToInt32((infoSource.nPos + 0.5F) * relScrollDiff);
            //SendMessage(Dest.Handle, EM_SETSCROLLPOS, 0, ref P);
            Dest.Document.Window.ScrollTo(0, P.Y);
        }

 
    }

    public class FastColoredTextBoxEx: FastColoredTextBox
    {
        const int WM_VSCROLL = 0x0115;
        const int WM_MOUSEWHEEL = 0x020A;
        const int SB_HORZ = 0;
        const int SB_VERT = 1;

        const int EM_GETSCROLLPOS = 0x4DD;
        const int EM_SETSCROLLPOS = 0x4DE;
        const uint SB_THUMBPOSITION = 4;
        const int VERTICAL_SCROLL_BAR = 1;
        
 
        const int WM_GETDLGCODE = 0x87;   //sent when the caret is going out of the 'visible area' (so scroll is needed)
        const int WM_MOUSEFIRST = 0x200;  //scrolls if the mouse leaves the 'visible area' (example when you select text)

        [DllImport("User32.dll")]
        static extern int GetScrollPos(IntPtr hWnd, int nBar);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            int GetBrowserScrollableHeight()
            {
                var wbAll = Browser.Document.All.Cast<HtmlElement>();                
                var maxHeight = wbAll.Max(x => Math.Max(x.ClientRectangle.Height, x.ScrollRectangle.Height));
                return maxHeight;

                //return Browser.Document.Body.ScrollRectangle.Height;
            } 

            if ((m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL) && Browser != null && Browser.Document != null && Browser.Document.Window != null )
            {
                int Max = OS.GetAbsoluteMaxVScrollValue(Handle);
                int BrowserMax = GetBrowserScrollableHeight(); //Browser.Document.Body.ScrollRectangle.Height;
                double RelativeScrollDiff = (BrowserMax - Browser.Document.Body.ClientRectangle.Height) / (Max - ClientSize.Height); //

                int Pos = GetScrollPos(Handle, SB_VERT);
                Pos = Convert.ToInt32(Pos * RelativeScrollDiff);
                Browser.Document.Body.ScrollTop = Pos;
                //Browser.Document.Window.ScrollTo(0, Pos);
            }
 
        }
 
 
        public FastColoredTextBoxEx()
            : base()
        {
        }


        public WebBrowser Browser { get; set; }
 
    }

}
