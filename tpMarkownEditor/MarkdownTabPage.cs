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

    /*
     * 
     * https://www.codeproject.com/Articles/161871/Fast-Colored-TextBox-for-syntax-highlighting-2

     ]\s*\(([^\)]+)\)	> ](....)
    ^\[([^\)]+)\]		> []
    ^!\[([^\)]+)\]		> ![]
    [`]\w*[^`][`]     	> `monospace`
    [_]\w*[_]		> _italic_
    (_.*?_)			> _italic_
    [**]\w*[^*][**]
    ([=])\1+$		> ====
    ([#])			> # ## ###	
    ^```|```

    _.*_
    _\w*_
    [**]\w*[**]
    string1.*string2

    [_][^_]*[_]

    ^_([^\)]+)_

    ]\s*\(.*?\)

     */
    public class MarkdownTabPage: TabPage
    {

        /* private */
        bool fModified;
        string fFilePath;

        Panel fBrowserPanel;
        SplitContainer Splitter;
        FastColoredTextBoxEx Editor;
        WebBrowserEx Browser;
 

        TextStyle HeadingStyle = new TextStyle(Brushes.SteelBlue, null, FontStyle.Bold);

        TextStyle BoldStyle = new TextStyle(Brushes.Black, null, FontStyle.Bold);        
        TextStyle ItalicStyle = new TextStyle(Brushes.Black, null, FontStyle.Italic);
        TextStyle MonospaceStyle = new TextStyle(Brushes.Black, Brushes.LightYellow, FontStyle.Regular);

        TextStyle LinkStyle = new TextStyle(Brushes.DarkMagenta, null, FontStyle.Bold);
        TextStyle ImageStyle = new TextStyle(Brushes.Orange, null, FontStyle.Bold);

        TextStyle SourceCodeStyle = new TextStyle(Brushes.Black, Brushes.LightYellow, FontStyle.Regular);
        TextStyle BlockquotStyle = new TextStyle(Brushes.Black, Brushes.LightGreen, FontStyle.Regular);
        TextStyle SubOrSuperscript = new TextStyle(Brushes.Black, Brushes.FloralWhite, FontStyle.Regular);


        /// <summary>
        /// # ## ###
        /// </summary>
        const string SHeading1 = @"(#+)(.*)";  
        /// <summary>
        /// =======
        /// </summary>
        const string SHeading2 = @"^=+";  
        /// <summary>
        /// -----------
        /// </summary>
        const string SHeading3 = @"^-+";
        /// <summary>
        /// **bold**
        /// </summary>
        const string SBold = @"[\*\*].*[\*\*]";  
        /// <summary>
        /// _italic_
        /// </summary>
        const string SItalic = @"[_][^_].*[^_][_]";
        /// <summary>
        /// `monospace`
        /// </summary>
        const string SMonospace = @"[`][^`].*[^`][`]";
        /// <summary>
        /// []()
        /// </summary>
        const string SLink = @"^\[(.*?)\]\((.*?)\)";  
        /// <summary>
        /// ![]()
        /// </summary>
        const string SImage = @"^!\[(.*?)\]\((.*?)\)";
        /// <summary>
        /// ``` code here ```
        /// </summary>
        const string SSourceCode = @"^```((.*|\n|\r)*)```";
        /// <summary>
        /// &gt; 
        /// <para>&gt;</para>
        /// </summary>
        const string SBlockQuote = @"^\>(.*)";
        /// <summary>
        /// ~subscript~
        /// </summary>
        const string SSubscript = @"~(.*?)~";
        /// <summary>
        /// ^superscript^
        /// </summary>
        const string SSuperscript = @"\^(.*?)\^";

 
        static RegexOptions MultiLine = RegexOptions.Multiline | SyntaxHighlighter.RegexCompiledOption;

        void Editor_TextChanged(object sender, EventArgs ea)
        { 
            Modified = true;
            Browser.DocumentText = App.ToHtml(Editor.Text);
        }
        void Editor_KeyDown(object sender, KeyEventArgs e)
        {

        }
        
        /* construction */
        public MarkdownTabPage(TabControl Pager, string OpenFilePath = "")
        {
            this.Text = "untitled";

            this.SuspendLayout();

            Splitter = new SplitContainer();
            Splitter.Dock = DockStyle.Fill;

            Editor = new FastColoredTextBoxEx(); // new RichTextBox();
            Editor.Dock = DockStyle.Fill;
            Editor.Parent = Splitter.Panel1;
            Editor.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Editor.Language = Language.Custom;
            Editor.DescriptionFile = SyntaxHighlighterDef.FilePath;

            fBrowserPanel = new Panel();
            fBrowserPanel.BorderStyle = BorderStyle.Fixed3D;
            fBrowserPanel.Dock = DockStyle.Fill;
            fBrowserPanel.Parent = Splitter.Panel2;

            Browser = new WebBrowserEx();
            Browser.Dock = DockStyle.Fill;
            Browser.Parent = fBrowserPanel;

            Pager.TabPages.Add(this);

            Splitter.Parent = this;

            this.ResumeLayout(false);
            this.PerformLayout();

            Splitter.SplitterDistance = Splitter.Width / 2;
            
            Editor.Browser = Browser; 

            Pager.SelectedTab = this;

            Application.DoEvents();

            Editor.TextChanged += Editor_TextChanged;
            Editor.KeyDown += Editor_KeyDown;


            if (!string.IsNullOrWhiteSpace(OpenFilePath))
            {
                FilePath = OpenFilePath; 

                this.Text = Path.GetFileName(FilePath);
                string MarkdownText = File.ReadAllText(FilePath);

                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                try
                {
                    Editor.Text = MarkdownText;
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
                
                Application.DoEvents();
            }
 
            Modified = false;
 
        }
       


        /* public */
        public void Save()
        {
            if (string.IsNullOrWhiteSpace(FilePath))
            {
                SaveAs();
            }
            else
            {
                string MarkupText = Editor.Text;
                File.WriteAllText(FilePath, MarkupText);
                Modified = false;

            }
        }
        public void SaveAs()
        {
            using (var F = new SaveFileDialog())
            {
                F.Filter = "Markdown file (.md)|*.md|All files|*.*";
                F.DefaultExt = "md";
                F.ValidateNames = true;

                if (F.ShowDialog() == DialogResult.OK)
                {
                    FilePath = F.FileName;
                    Environment.CurrentDirectory = Path.GetDirectoryName(FilePath);
                    string MarkdownText = Editor.Text;
                    File.WriteAllText(FilePath, MarkdownText);
                    Modified = false;

                    this.Text = Path.GetFileName(FilePath);
                }
            }
        }
        public void SaveToHtml()
        {
            using (var F = new SaveFileDialog())
            {
                F.Filter = "Html file (.html)|*.html|All files|*.*";
                F.DefaultExt = "html";
                F.ValidateNames = true;

                if (F.ShowDialog() == DialogResult.OK)
                { 
                    string HtmlText = App.ToHtmlDefault(Editor.Text);
                    File.WriteAllText(FilePath, HtmlText); 
                }
            }
        }
        public void Close()
        {
            if (Modified)
            {
                if (App.QuestionBox("Save changes?"))
                    Save();
            }               

            (this.Parent as TabControl).TabPages.Remove(this);
        }

        public bool CanClose()
        {
            if (Modified)
                return App.QuestionBox("Discard changes?");

            return true;
        }
        public void SetCurrentDirectory()
        {
            if (!string.IsNullOrWhiteSpace(fFilePath) && File.Exists(fFilePath))
            {
                fFilePath = Path.GetFullPath(fFilePath);
                Environment.CurrentDirectory = Path.GetDirectoryName(fFilePath);
            }
                
        }

        /* properties */
        public bool Modified
        {
            get { return fModified; }
            set 
            { 
                fModified = value; 

                if (value)
                {
                    if (!Text.EndsWith("*"))
                        Text += "*";
                }
                else
                {
                    if (Text.EndsWith("*"))
                        Text = Text.Substring(0, Text.Length - 1);
                }
            }
        }
        public string FilePath
        {
            get { return fFilePath; }
            set
            {
                fFilePath = value;
                SetCurrentDirectory();
            }
        }
          
    }
}
