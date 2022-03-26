using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

using FastColoredTextBoxNS;


namespace MarkdownEditor
{
    internal class ScrollSyncer
    {
        enum ScrollBarType : uint
        {
            SbHorz = 0,
            SbVert = 1,
            SbCtl = 2,
            SbBoth = 3
        }

        enum Message : uint
        {
            WM_VSCROLL = 0x0115
        }

        enum ScrollBarCommands : uint
        {
            SB_THUMBPOSITION = 4
        }

        Control Control1; 
        Control Control2;

        public ScrollSyncer(Control Control1, Control Control2)
        {
            this.Control1 = Control1;
            this.Control2 = Control2;

             
        }
    }
}
