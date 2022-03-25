using System.Windows.Forms;

namespace MarkdownEditor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.ToolBar = new System.Windows.Forms.ToolStrip();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.Pager = new System.Windows.Forms.TabControl();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSaveDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveDocumentAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAllDocuments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveDocumentToHtml = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCloseDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseAllDocuments = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.MenuBar.Size = new System.Drawing.Size(755, 24);
            this.MenuBar.TabIndex = 0;
            this.MenuBar.Text = "menuStrip1";
            // 
            // ToolBar
            // 
            this.ToolBar.Location = new System.Drawing.Point(0, 24);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.Size = new System.Drawing.Size(755, 25);
            this.ToolBar.TabIndex = 1;
            this.ToolBar.Text = "toolStrip1";
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 501);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.StatusBar.Size = new System.Drawing.Size(755, 22);
            this.StatusBar.TabIndex = 2;
            this.StatusBar.Text = "statusStrip1";
            // 
            // Pager
            // 
            this.Pager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pager.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pager.Location = new System.Drawing.Point(0, 49);
            this.Pager.Name = "Pager";
            this.Pager.SelectedIndex = 0;
            this.Pager.Size = new System.Drawing.Size(755, 452);
            this.Pager.TabIndex = 3;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewDocument,
            this.mnuOpenDocument,
            this.toolStripMenuItem1,
            this.mnuSaveDocument,
            this.mnuSaveDocumentAs,
            this.mnuSaveAllDocuments,
            this.mnuSaveDocumentToHtml,
            this.toolStripMenuItem2,
            this.mnuCloseDocument,
            this.mnuCloseAllDocuments,
            this.toolStripMenuItem3,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuNewDocument
            // 
            this.mnuNewDocument.Name = "mnuNewDocument";
            this.mnuNewDocument.Size = new System.Drawing.Size(184, 22);
            this.mnuNewDocument.Text = "New";
            // 
            // mnuOpenDocument
            // 
            this.mnuOpenDocument.Name = "mnuOpenDocument";
            this.mnuOpenDocument.Size = new System.Drawing.Size(184, 22);
            this.mnuOpenDocument.Text = "Open";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(181, 6);
            // 
            // mnuSaveDocument
            // 
            this.mnuSaveDocument.Name = "mnuSaveDocument";
            this.mnuSaveDocument.Size = new System.Drawing.Size(184, 22);
            this.mnuSaveDocument.Text = "Save";
            // 
            // mnuSaveDocumentAs
            // 
            this.mnuSaveDocumentAs.Name = "mnuSaveDocumentAs";
            this.mnuSaveDocumentAs.Size = new System.Drawing.Size(184, 22);
            this.mnuSaveDocumentAs.Text = "Save As";
            // 
            // mnuSaveAllDocuments
            // 
            this.mnuSaveAllDocuments.Name = "mnuSaveAllDocuments";
            this.mnuSaveAllDocuments.Size = new System.Drawing.Size(184, 22);
            this.mnuSaveAllDocuments.Text = "Save All";
            // 
            // mnuSaveDocumentToHtml
            // 
            this.mnuSaveDocumentToHtml.Name = "mnuSaveDocumentToHtml";
            this.mnuSaveDocumentToHtml.Size = new System.Drawing.Size(184, 22);
            this.mnuSaveDocumentToHtml.Text = "Save To Html";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(181, 6);
            // 
            // mnuCloseDocument
            // 
            this.mnuCloseDocument.Name = "mnuCloseDocument";
            this.mnuCloseDocument.Size = new System.Drawing.Size(184, 22);
            this.mnuCloseDocument.Text = "Close Document";
            // 
            // mnuCloseAllDocuments
            // 
            this.mnuCloseAllDocuments.Name = "mnuCloseAllDocuments";
            this.mnuCloseAllDocuments.Size = new System.Drawing.Size(184, 22);
            this.mnuCloseAllDocuments.Text = "Close All Documents";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(181, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(184, 22);
            this.mnuExit.Text = "Exit";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 523);
            this.Controls.Add(this.Pager);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.MenuBar);
            this.MainMenuStrip = this.MenuBar;
            this.Name = "MainForm";
            this.Text = "AntyxSoft Markdown Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip MenuBar;
        private ToolStrip ToolBar;
        private StatusStrip StatusBar;
        private TabControl Pager;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuNewDocument;
        private ToolStripMenuItem mnuOpenDocument;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem mnuSaveDocument;
        private ToolStripMenuItem mnuSaveDocumentAs;
        private ToolStripMenuItem mnuSaveAllDocuments;
        private ToolStripMenuItem mnuSaveDocumentToHtml;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem mnuCloseDocument;
        private ToolStripMenuItem mnuCloseAllDocuments;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem mnuExit;
    }
}