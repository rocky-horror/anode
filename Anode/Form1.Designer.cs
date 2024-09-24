using FastColoredTextBoxNS;

namespace Anode
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txt = new FastColoredTextBoxNS.FastColoredTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fileDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyAsHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildExecutableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveExecutableDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.txt)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt
            // 
            this.txt.AutoCompleteBrackets = true;
            this.txt.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"'};
            this.txt.AutoIndentChars = false;
            this.txt.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:" +
    "]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.txt.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txt.BackBrush = null;
            this.txt.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.txt.CharHeight = 14;
            this.txt.CharWidth = 8;
            this.txt.CommentPrefix = ";";
            this.txt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt.DefaultMarkerSize = 8;
            this.txt.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt.InsertSpaceBetweenAutocompleteBrackets = false;
            this.txt.IsReplaceMode = false;
            this.txt.LeftBracket = '(';
            this.txt.LeftBracket2 = '{';
            this.txt.Location = new System.Drawing.Point(0, 25);
            this.txt.Name = "txt";
            this.txt.Paddings = new System.Windows.Forms.Padding(0);
            this.txt.RightBracket = ')';
            this.txt.RightBracket2 = '}';
            this.txt.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txt.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txt.ServiceColors")));
            this.txt.Size = new System.Drawing.Size(800, 425);
            this.txt.TabIndex = 2;
            this.txt.Zoom = 100;
            this.txt.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txt_TextChanged);
            this.txt.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txt_TextChangedDelayed);
            this.txt.AutoIndentNeeded += new System.EventHandler<FastColoredTextBoxNS.AutoIndentEventArgs>(this.txt_AutoIndentNeeded);
            this.txt.Load += new System.EventHandler(this.txt_Load);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileDropDown,
            this.programDropDownButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fileDropDown
            // 
            this.fileDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyAsHTMLToolStripMenuItem});
            this.fileDropDown.Image = ((System.Drawing.Image)(resources.GetObject("fileDropDown.Image")));
            this.fileDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileDropDown.Name = "fileDropDown";
            this.fileDropDown.Size = new System.Drawing.Size(38, 22);
            this.fileDropDown.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // copyAsHTMLToolStripMenuItem
            // 
            this.copyAsHTMLToolStripMenuItem.Name = "copyAsHTMLToolStripMenuItem";
            this.copyAsHTMLToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.copyAsHTMLToolStripMenuItem.Text = "Copy As HTML";
            this.copyAsHTMLToolStripMenuItem.Click += new System.EventHandler(this.copyAsHTMLToolStripMenuItem_Click);
            // 
            // programDropDownButton
            // 
            this.programDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.programDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.buildExecutableToolStripMenuItem});
            this.programDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("programDropDownButton.Image")));
            this.programDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.programDropDownButton.Name = "programDropDownButton";
            this.programDropDownButton.Size = new System.Drawing.Size(66, 22);
            this.programDropDownButton.Text = "&Program";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.runToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.runToolStripMenuItem.Text = "&Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // buildExecutableToolStripMenuItem
            // 
            this.buildExecutableToolStripMenuItem.Enabled = false;
            this.buildExecutableToolStripMenuItem.Name = "buildExecutableToolStripMenuItem";
            this.buildExecutableToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.buildExecutableToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.buildExecutableToolStripMenuItem.Text = "&Build Executable";
            this.buildExecutableToolStripMenuItem.Click += new System.EventHandler(this.buildExecutableToolStripMenuItem_Click);
            // 
            // openDialog
            // 
            this.openDialog.Filter = "Cathode Source Files (*.ctd)|*.ctd";
            this.openDialog.Title = "...";
            this.openDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openDialog_FileOk);
            // 
            // saveDialog
            // 
            this.saveDialog.Filter = "Cathode Source Files (*.ctd)|*.ctd";
            this.saveDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveDialog_FileOk);
            // 
            // saveExecutableDialog
            // 
            this.saveExecutableDialog.Filter = "Executable Files (*.exe)|*.exe";
            this.saveExecutableDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveExecutableDialog_FileOk);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Anode | Untitled Document";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastColoredTextBox txt;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton fileDropDown;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.ToolStripDropDownButton programDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyAsHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildExecutableToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveExecutableDialog;
    }
}

