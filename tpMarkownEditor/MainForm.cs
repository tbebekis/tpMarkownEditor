using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MarkdownEditor
{
    public partial class MainForm : Form
    { 
        void AnyClick(object sender, EventArgs ea)
        {
            if (mnuExit == sender)
            {
                Close();
            }
            else if (mnuNewDocument == sender)
            {
                CreateNewDocument();
            }
            else if (mnuOpenDocument == sender)
            {
                OpenDocument();
            }
            else if (mnuSaveDocument == sender)
            {
                SaveDocument();
            }
            else if (mnuSaveDocumentAs == sender)
            {
                SaveDocumentAs();
            }
            else if (mnuSaveAllDocuments == sender)
            {
                SaveAllDocuments();
            }
            else if (mnuSaveDocumentToHtml == sender)
            {
                SaveDocumentToHtml();
            }
            else if (mnuCloseDocument == sender)
            {
                CloseDocument();
            }
            else if (mnuCloseAllDocuments == sender)
            {
                CloseAllDocuments();
            }
        }
        void Pager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                for (int i = 0; i < Pager.TabCount; i++)
                {
                    Rectangle r = Pager.GetTabRect(i);
                    if (r.Contains(e.Location))
                    {
                        var Page = Pager.TabPages[i] as MarkdownTabPage;
                        Page.Close();
                    }
                }
            }
        }
        void Pager_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Page = Pager.SelectedTab as MarkdownTabPage;
            if (Page != null)
                Page.SetCurrentDirectory();
        }

        void CreateNewDocument()
        {
            var Page = new MarkdownTabPage(Pager);
        }
        void OpenDocument()
        {
            using (var F = new OpenFileDialog())
            {
                F.Filter = "Markdown file (.md)|*.md|All files|*.*";
                F.DefaultExt = "md";

                if (F.ShowDialog() == DialogResult.OK)
                {
                    var Page = new MarkdownTabPage(Pager, F.FileName);
                }
            }            
        }
        void SaveDocument()
        {
            var Page = Pager.SelectedTab as MarkdownTabPage;
            if (Page != null)
                Page.Save();
        }
        void SaveDocumentAs()
        {
            var Page = Pager.SelectedTab as MarkdownTabPage;
            if (Page != null)
                Page.SaveAs();
        }
        void SaveAllDocuments()
        {
            var Pages = Pager.TabPages;
            foreach (var Page in Pages)
                (Page as MarkdownTabPage).Save();
        }
        void SaveDocumentToHtml()
        {
            var Page = Pager.SelectedTab as MarkdownTabPage;
            if (Page != null)
                Page.SaveToHtml();
        }
        void CloseDocument()
        {
            var Page = Pager.SelectedTab as MarkdownTabPage;
            if (Page != null)
                Page.Close();
        }
        void CloseAllDocuments()
        {
            var Pages = Pager.TabPages;
            foreach (var Page in Pages)
                (Page as MarkdownTabPage).Close();
        }
 
        void FormInitialize()
        {

            //LoadTestFile();
            //Editor.TextChanged += Editor_TextChanged;

            //var Page = new MarkdownTabPage("Test", Pager);
            KeyPreview = true;

            mnuNewDocument.Click += AnyClick;
            mnuOpenDocument.Click += AnyClick;
            mnuSaveDocument.Click += AnyClick;
            mnuSaveDocumentAs.Click += AnyClick;
            mnuSaveAllDocuments.Click += AnyClick;
            mnuSaveDocumentToHtml.Click += AnyClick;
 
            mnuCloseDocument.Click += AnyClick;
            mnuCloseAllDocuments.Click += AnyClick;
 
            mnuExit.Click += AnyClick;

            Pager.MouseDoubleClick += Pager_MouseDoubleClick;
            Pager.SelectedIndexChanged += Pager_SelectedIndexChanged;

            if (File.Exists("CheatSheet.md"))
            {
                new MarkdownTabPage(Pager, "CheatSheet.md");
            }


             

        }



        protected override void OnShown(EventArgs e)
        {
            if (!DesignMode)
                FormInitialize();
            base.OnShown(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                SaveDocument();
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e); 

            var Pages = Pager.TabPages;
            foreach (TabPage Page in Pages)
            {
                Pager.SelectedTab = Page;

                if (!(Page as MarkdownTabPage).CanClose())
                {
                    e.Cancel = true;
                    return;
                }
            } 
        }


        /* construction */
        public MainForm()
        {
            InitializeComponent();
        }
    }
}