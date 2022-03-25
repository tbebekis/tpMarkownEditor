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

        Panel fBrowserPanel;
        SplitContainer Splitter;
        //RichTextBox Editor;
        FastColoredTextBox Editor;
        WebBrowser Browser;
        string FilePath;

        TextStyle brownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);
        string Underscores = @"([_].*?[_])";
        string A = @"^\[[^\)]+\]";

        void Editor_TextChanged(object sender, EventArgs ea)
        {
            TextChangedEventArgs e = ea as TextChangedEventArgs;

            Browser.DocumentText = App.ToHtml(Editor.Text);
            Modified = true;


            //clear previous highlighting
           // e.ChangedRange.ClearStyle(brownStyle);
            //highlight tags
           

            e.ChangedRange.SetStyle(brownStyle, @"=.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(brownStyle, Underscores);
            e.ChangedRange.SetStyle(brownStyle, A);
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

            Editor = new FastColoredTextBox(); // new RichTextBox();
            Editor.Dock = DockStyle.Fill;
            Editor.Parent = Splitter.Panel1;
            Editor.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            //Editor.Multiline = true;
            //Editor.AcceptsTab = true;
            Editor.Language = Language.Custom;

            fBrowserPanel = new Panel();
            fBrowserPanel.BorderStyle = BorderStyle.Fixed3D;
            fBrowserPanel.Dock = DockStyle.Fill;
            fBrowserPanel.Parent = Splitter.Panel2;

            Browser = new WebBrowser();
            Browser.Dock = DockStyle.Fill;
            Browser.Parent = fBrowserPanel;

            Pager.TabPages.Add(this);

            Splitter.Parent = this;

            this.ResumeLayout(false);
            this.PerformLayout();

            Splitter.SplitterDistance = Splitter.Width / 2;

            Editor.TextChanged += Editor_TextChanged;

            if (!string.IsNullOrWhiteSpace(OpenFilePath))
            {
                FilePath = OpenFilePath;

                Environment.CurrentDirectory = Path.GetDirectoryName(FilePath);

                this.Text = Path.GetFileName(FilePath);
                string MarkdownText = File.ReadAllText(FilePath);
                Editor.Text = MarkdownText;
                Browser.DocumentText = App.ToHtml(Editor.Text);               
            }
            
            Editor.KeyDown += Editor_KeyDown;

            Pager.SelectedTab = this;

            Modified = false;
        }
        public bool CanClose()
        {
            if (Modified)
                return App.QuestionBox("Discard changes?");

            return true;
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
          
    }
}
