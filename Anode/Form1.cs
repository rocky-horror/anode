using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Anode
{
    public partial class frmMain : Form
    {
        FileStream OpenFile = null;
        string OpenFilePath = string.Empty;

        bool SuppressTextChanged = false;

        bool _edited = false;
        public bool Edited
        {
            get
            {
                return _edited;
            }
            set
            {
                if (value && !Text.EndsWith("*"))
                    Text += "*";
                else if (!value)
                    Text = Text.TrimEnd('*');

                _edited = value;
            }
        }

        AutocompleteMenu Sense;

        string[] stdLibFuncNames = new string[]
        {
            // core
            "RuntimeInfo",
            "fAbs",
            "Abs",
            "BytesToStr",
            "StrToBytes",
            "Arraylen",
            "Strlen",
            "Format",
            "Strcat",
            "HasField",
            "Field",
            "SetField",
            "Struct",
            "CloneStruct",
            "CloneString",
            "CloneArray",
            "Assert",
            "Except",
            "Negate",
            "Both",
            "Either",
            "LessThan",
            "GreaterThan",
            "Equal",
            "NotEqual",
            "Exit",
            "Byte",
            "Integer",
            "String",
            "Float",
            "Strcmp",
            "Uppercase",
            "Lowercase",

            // array
            "aIdxOf",
            "aCount",
            "aRemoveAll",
            "aRemoveIdx",
            "aAppend",
            "aSection",
            "aConcat",

            // string
            "sIdxOf",

            // random
            "rSeed",
            "RandomInt",
            "RandomFloat",
            "RandomBytes",

            // system
            "Sys",
            "Sleep",
            "Env",
            "Ticks",
            "Time",

            // tcpio
            "nTCPSetReadTimeout",
            "nTCPSetWriteTimeout",
            "nTCPWrite",
            "nTCPRead",
            "nTCPIsOpen",
            "nTCPCanRead",
            "nTCPClose",
            "nTCPConnect",

            // network
            "ResolveHostname",
            "Ping",

            // fileio
            "Getcwd",
            "Chdir",
            "dEnumDirectories",
            "dEnumFiles",
            "dExists",
            "dCreate",
            "dUnlink",
            "fUnlink",
            "fOpen",
            "fExists",
            "fCreate",
            "fGetPath",
            "fClose",
            "fReadLn",
            "fLen",
            "fGetPos",
            "fSetPos",
            "fRead",
            "fWrite",
            "fWriteLn",

            // conio
            "cClear",
            "cWidth",
            "cHeight",
            "Title",
            "Print",
            "PrintLn",
            "ReadLn"
        };

        public frmMain()
        {
            InitializeComponent();

            Sense = new AutocompleteMenu(txt);
            Sense.AppearInterval = 1;
            Sense.Items.SetAutocompleteItems(stdLibFuncNames);

            Sense.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Edited = false;
            SuppressTextChanged = true;
        }

        private void txt_Load(object sender, EventArgs e)
        {

        }

        Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        Style QuoteStyle = new TextStyle(Brushes.Red, null, FontStyle.Italic);
        Style FnDefStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
        Style WhileStyle = new TextStyle(Brushes.OrangeRed, null, FontStyle.Bold);
        Style OperatorFnStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        Style IfStyle = new TextStyle(Brushes.SteelBlue, null, FontStyle.Bold);
        Style HighlightedBlack = new TextStyle(Brushes.Black, Brushes.Yellow, FontStyle.Regular);
        Style PostRetStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Bold | FontStyle.Italic);

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (SuppressTextChanged)
            //{
            //    SuppressTextChanged = !SuppressTextChanged;
            //    return;
            //}

            Edited = true;

            e.ChangedRange.ClearStyle(GreenStyle, QuoteStyle, FnDefStyle, WhileStyle, IfStyle);
            e.ChangedRange.ClearFoldingMarkers();

            // Comment highlighting
            e.ChangedRange.SetStyle(GreenStyle, @";.*", RegexOptions.Multiline);

            // FnDef, FnRet, Dim
            e.ChangedRange.SetStyle(FnDefStyle, @"^\s*fndef\s*", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(FnDefStyle, @"^\s*fnret\s*", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(FnDefStyle, @"(?<!""\s*)(?<=\W*)dim\s*", RegexOptions.Multiline);

            // import(), typeof(), inc(), mkref(), deref()
            e.ChangedRange.SetStyle(OperatorFnStyle, @"(?<!""\s*)(?<=\W*)import(?=\s*\()", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(OperatorFnStyle, @"(?<!""\s*)(?<=\W*)typeof(?=\s*\()", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(OperatorFnStyle, @"(?<!""\s*)(?<=\W*)inc(?=\s*\()", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(OperatorFnStyle, @"(?<!""\s*)(?<=\W*)mkref(?=\s*\()", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(OperatorFnStyle, @"(?<!""\s*)(?<=\W*)deref(?=\s*\()", RegexOptions.Multiline);

            // While, Loop
            e.ChangedRange.SetStyle(WhileStyle, @"^\s*while\s*", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(WhileStyle, @"^\s*loop\s*", RegexOptions.Multiline);

            // Post, Ret
            e.ChangedRange.SetStyle(PostRetStyle, @"^\s*post\s*", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(PostRetStyle, @"^\s*ret\s*", RegexOptions.Multiline);

            // If, Then
            e.ChangedRange.SetStyle(IfStyle, @"^\s*if\s*", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(IfStyle, @"^\s*then\s*", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(IfStyle, @"^\s*else\s*", RegexOptions.Multiline);

            // Return value
            //e.ChangedRange.SetStyle(HighlightedBlack, @"\b(ret|post)\s+(?<range>[\w_]+?)\b");
            e.ChangedRange.SetStyle(HighlightedBlack, @"\b(ret|post)\s+(.+)\b");

            // Quotes
            e.ChangedRange.SetStyle(QuoteStyle, "\"(.*)\"");

            // Folding markers
            // Commented out cuz they're broken
            // e.ChangedRange.SetFoldingMarkers(@"^\s*fndef", @"^\s*fnret", RegexOptions.Multiline);
            // e.ChangedRange.SetFoldingMarkers(@"^\s*while", @"^\s*loop", RegexOptions.Multiline);
            // e.ChangedRange.SetFoldingMarkers(@"^\s*if", @"^\s*then", RegexOptions.Multiline);
        }

        private void txt_AutoIndentNeeded(object sender, AutoIndentEventArgs e)
        {
            if (e.LineText.Trim().StartsWith("fndef ") || e.LineText.Trim().StartsWith("while(")
                 || e.LineText.Trim().StartsWith("while (") || e.LineText.Trim().StartsWith("if(")
                  || e.LineText.Trim().StartsWith("if ("))
            {
                e.ShiftNextLines = e.TabLength;
                return;
            }

            if (e.LineText.Trim() == "fnret" || e.LineText.Trim() == "loop" || e.LineText.Trim() == "then")
            {
                e.Shift -= e.TabLength;
                e.ShiftNextLines -= e.TabLength;
            }

            if (e.LineText.Trim() == "else")
                e.Shift -= e.TabLength;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openDialog.ShowDialog();
        }

        private void Save()
        {
            string text = txt.Text;
            byte[] binary = Encoding.ASCII.GetBytes(text);

            OpenFile.Seek(0, SeekOrigin.Begin);
            OpenFile.SetLength(binary.Length);
            OpenFile.Write(binary, 0, binary.Length);

            Edited = false;
        }

        private void openDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (Edited)
            {
                DialogResult msgResult = MessageBox.Show("The current file has not yet been saved. Would you like to do so now?", "Anode",
                    MessageBoxButtons.YesNoCancel);

                switch (msgResult)
                {
                    case DialogResult.Yes:
                        Save();
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            string fname = openDialog.FileName;

            try
            {
                OpenFilePath = fname;
                OpenFile = File.Open(fname, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
                Text = string.Format("Anode | {0}", Path.GetFileName(fname));

                byte[] data = new byte[OpenFile.Length];
                OpenFile.Read(data, 0, (int)OpenFile.Length);

                txt.Text = Encoding.ASCII.GetString(data);

                Edited = false;
                SuppressTextChanged = true;
            }
            catch
            {
                MessageBox.Show("Failed to open the given source file. (Is it running?)", "Anode", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Edited && !(OpenFile == null && string.IsNullOrEmpty(txt.Text)))
            {
                DialogResult msgResult = MessageBox.Show("The current file has not yet been saved. Would you like to do so now?", "Anode",
                    MessageBoxButtons.YesNoCancel);

                switch (msgResult)
                {
                    case DialogResult.Yes:
                        Save();
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            if (OpenFile != null)
                OpenFile.Dispose();
            txt.Clear();
            Text = "Anode | Untitled Document";
            SuppressTextChanged = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFile == null)
            {
                saveAsToolStripMenuItem.PerformClick();
                return;
            }

            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveDialog.ShowDialog();
        }

        private void saveDialog_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                FileStream newFile = File.Open(saveDialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
                Text = string.Format("Anode | {0}", Path.GetFileName(saveDialog.FileName));

                if (OpenFile != null)
                    OpenFile.Dispose();

                OpenFile = newFile;
                OpenFilePath = saveDialog.FileName;

                Save();
            }
            catch
            {
                MessageBox.Show("Failed to open the file for writing. (Is it running?)", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenFile == null)
            {
                MessageBox.Show("The source must be saved before running.", "Anode", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Save();

            try
            {
                // Little trickery here
                OpenFile.Dispose();

                Enabled = false;
                Process.Start(OpenFilePath).WaitForExit();

                try
                {
                    FileStream newStream = File.Open(OpenFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    OpenFile = newStream;
                }
                catch
                {
                    MessageBox.Show("Failed to re-open source file after running.", "Anode", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Failed to start process.", "Anode", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
            Activate();
        }

        private void txt_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            
        }

        private void copyAsHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txt.Html);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Edited)
            {
                DialogResult msgResult = MessageBox.Show("The current file has not yet been saved. Would you like to do so now?", "Anode",
                    MessageBoxButtons.YesNoCancel);

                switch (msgResult)
                {
                    case DialogResult.Yes:
                        Save();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void buildExecutableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveExecutableDialog.ShowDialog();
        }

        private void saveExecutableDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (OpenFile == null)
            {
                MessageBox.Show("The source must be saved before running.", "Anode", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Save();

            try
            {
                // Little trickery here
                OpenFile.Dispose();

                Enabled = false;
                {
                    string interpreterPath = "C:\\Users\\Jay\\AppData\\Local\\Programs\\Cathode Runtime\\" +
                    "cathode-rt.exe";
                    string nativeStubPath = Path.Combine(Directory.GetParent(Process.GetCurrentProcess().MainModule.FileName)
                        .FullName, "NativeStub.exe");
                    string proggyPath = OpenFilePath;

                    string fname = saveExecutableDialog.FileName;
                    if (Headers.CraftExecutable(fname, nativeStubPath, interpreterPath,
                        proggyPath) == 0)
                        MessageBox.Show("Executable built successfully!", "Anode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed to build executable.", "Anode", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                try
                {
                    FileStream newStream = File.Open(OpenFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    OpenFile = newStream;
                }
                catch
                {
                    MessageBox.Show("Failed to re-open source file after running.", "Anode",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Failed to start process.", "Anode", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Enabled = true;
            Activate();
        }
    }
}