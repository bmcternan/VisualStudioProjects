using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenSyncedNukeFile
{
    public partial class GetParamsForm : Form
    {
        private const string _invalidDirChars = "\\<>:\"/|?*\n\t";
        private string _cameraPath;
        public string GetCameraPath() { return _cameraPath; }
        private string _xmlToPluralEyesPath;
        public string GetXMLToPluralEyesPath() { return _xmlToPluralEyesPath; }

        public FinalCutProXMLBuilder _xmlBldr = new FinalCutProXMLBuilder();
        public FinalCutProXMLBuilder GetXMLBuilder () { return _xmlBldr; }

        public NukeBuilder _nukeBldr = new NukeBuilder();
        public NukeBuilder GetNukeBuilder() { return _nukeBldr; }

        Process _PluralizeProc = null;

        public GetParamsForm()
        {
            InitializeComponent();
        }

        private void Browse_button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fc = new FolderBrowserDialog();

            // Set the help text description for the FolderBrowserDialog.
            fc.Description = "Select the directory that contains the cameras for this shot.";

            // Do not allow the user to create new files via the FolderBrowserDialog.
            fc.ShowNewFolderButton = true;

            // Show the FolderBrowserDialog.
            DialogResult result = fc.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.CameraPath_textBox.Text = fc.SelectedPath;
            }

        }

        private void LaunchPluralEyes_button_Click(object sender, EventArgs e)
        {
            if (_PluralizeProc != null)
                _PluralizeProc.Kill();
            _PluralizeProc = Process.Start(@"C:\Program Files\Red Giant\PluralEyes 4\PluralEyes 4.exe") ;
        }

        private void BuildXML_button_Click(object sender, EventArgs e)
        {
            bool bDirExists = false;
            try
            {
                if (Directory.Exists(this.CameraPath_textBox.Text))
                    bDirExists = true;
                else
                    bDirExists = false;
            }
            catch (Exception exp)
            {
                Exception foo = exp;
            };

            if (bDirExists)
            {
                _cameraPath = this.CameraPath_textBox.Text;
                _xmlToPluralEyesPath = this.FinalCutProXMLPath_textBox.Text;
                if (this._xmlBldr.GenPluralEyesXML(_cameraPath, _xmlToPluralEyesPath) == FinalCutProXMLBuilder.tGENPLURALEYESXML_ERROR.GEN_OK)
                    this.LaunchPluralEyes_button.Enabled = true;

            }
            else
            {
                _cameraPath = "";
                MessageBox.Show("Path does not exist \"" + this.CameraPath_textBox.Text + "\"", "Build Final Cut Pro XML",  MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }

        private void GenerateNuke_button_Click(object sender, EventArgs e)
        {
            //_nukeBldr.GenerateNukeFile(PluralEyes_export_textBox.Text, GeneratedNukePath_textBox.Text, NukeTemplatePath_textBox.Text);
            NukeBuilder.tNUKEFILE_ERROR err = _nukeBldr.ModifyNukeFile(PluralEyes_export_textBox.Text, NukeTemplatePath_textBox.Text, SourceBaseName_textBox.Text, GeneratedNukePath_textBox.Text, DestBaseName_textBox.Text) ;

        }

    private void BrowseForPluralEyesExport_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog fc = new OpenFileDialog();

            // Set the help text description for the FolderBrowserDialog.
            fc.Title = "Select the XML File to use to create the Nuke file";
            fc.Filter = "XML Files|*.xml";

            // Show the FolderBrowserDialog.
            DialogResult result = fc.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.PluralEyes_export_textBox.Text = fc.FileName ;
                if (PluralEyes_export_textBox.Text.Length > 0)
                {
                    PluralEyes_export_textBox.SelectionStart = PluralEyes_export_textBox.Text.Length - 1;
                    PluralEyes_export_textBox.SelectionLength = 0;
                }
            }
        }

        private void BrowseNukeFile_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog fc = new SaveFileDialog();

            // Set the help text description for the FolderBrowserDialog.
            fc.Title = "Select the Nuke file to be generated";
            fc.Filter = "Nuke Files|*.nk";

            fc.CreatePrompt = true;
            fc.OverwritePrompt = true;

            // Show the FolderBrowserDialog.
            DialogResult result = fc.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.GeneratedNukePath_textBox.Text = fc.FileName;
                if (GeneratedNukePath_textBox.Text.Length > 0)
                {
                    GeneratedNukePath_textBox.SelectionStart = GeneratedNukePath_textBox.Text.Length - 1;
                    GeneratedNukePath_textBox.SelectionLength = 0;
                }
            }
        }

        private void DisableAllButtons()
        {
            //this.GenerateNuke_button.Enabled = false;
            this.LaunchPluralEyes_button.Enabled = false;
            this.BuildXML_button.Enabled = false;
        }

        private void EnableAllButtons()
        {
            this.GenerateNuke_button.Enabled = true;
            this.LaunchPluralEyes_button.Enabled = true;
            this.BuildXML_button.Enabled = true;
        }

        private void CameraPath_textBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }
        private void CameraPath_textBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            CameraPath_textBox.Text = FileList[0];
            if (CameraPath_textBox.Text.Length > 0)
            {
                CameraPath_textBox.SelectionStart = CameraPath_textBox.Text.Length - 1;
                CameraPath_textBox.SelectionLength = 0;
            }
        }

        private void CameraPath_textBox_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void CameraPath_textBox_TextChanged(object sender, EventArgs e)
        {
            string sendertext = sender.ToString();
            string thisString = this.CameraPath_textBox.Text;

            bool bDirExists = false;
            try
            {
                if (Directory.Exists(this.CameraPath_textBox.Text))
                    bDirExists = true;
                else
                    bDirExists = false;
            }
            catch (Exception exp)
            {
                Exception foo = exp;
            };

            if (!bDirExists)
                DisableAllButtons();
            else
            {
                this.BuildXML_button.Enabled = true;
                //if (this.FinalCutProXMLPath_textBox.Text.Length == 0)
                {
                    this.FinalCutProXMLPath_textBox.Text = this.CameraPath_textBox.Text + "\\syncToPluralEyes.xml";
                    if (FinalCutProXMLPath_textBox.Text.Length > 0)
                    {
                        FinalCutProXMLPath_textBox.SelectionStart = FinalCutProXMLPath_textBox.Text.Length - 1;
                        FinalCutProXMLPath_textBox.SelectionLength = 0;
                    }

                }
                string baseName = Path.GetFileName(this.CameraPath_textBox.Text);
                string tempNukeName = this.CameraPath_textBox.Text + "\\" + baseName + "_qs.nk";
                this.GeneratedNukePath_textBox.Text = tempNukeName;
                if (GeneratedNukePath_textBox.Text.Length > 0)
                {
                    GeneratedNukePath_textBox.SelectionStart = GeneratedNukePath_textBox.Text.Length - 1;
                    GeneratedNukePath_textBox.SelectionLength = 0;
                }
            }

        }

        private void FinalCutProXMLPath_textBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }
        private void FinalCutProXMLPath_textBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            FinalCutProXMLPath_textBox.Text = FileList[0];
            if (FinalCutProXMLPath_textBox.Text.Length > 0)
            {
                FinalCutProXMLPath_textBox.SelectionStart = FinalCutProXMLPath_textBox.Text.Length - 1;
                FinalCutProXMLPath_textBox.SelectionLength = 0;
            }
        }

        private void FinalCutProXMLPath_textBox_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void FinalCutProXMLPath_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void BrowseFinalCutProXML_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog fc = new SaveFileDialog();

            // Set the help text description for the FolderBrowserDialog.
            fc.Title = "Select the File to write the XML to.";
            fc.Filter = "XML Files|*.xml";
            fc.CreatePrompt = true;
            fc.OverwritePrompt = true;

            // Show the FolderBrowserDialog.
            DialogResult result = fc.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.FinalCutProXMLPath_textBox.Text = fc.FileName;
                if (FinalCutProXMLPath_textBox.Text.Length > 0)
                {
                    FinalCutProXMLPath_textBox.SelectionStart = FinalCutProXMLPath_textBox.Text.Length - 1;
                    FinalCutProXMLPath_textBox.SelectionLength = 0;
                }
            }
        }

        private void PluralEyes_export_textBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }
        private void PluralEyes_export_textBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            PluralEyes_export_textBox.Text = FileList[0];
            if (PluralEyes_export_textBox.Text.Length > 0)
            {
                PluralEyes_export_textBox.SelectionStart = PluralEyes_export_textBox.Text.Length - 1;
                PluralEyes_export_textBox.SelectionLength = 0;
            }
        }

        private void PluralEyes_export_textBox_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void PluralEyes_export_textBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(PluralEyes_export_textBox.Text))
            {
                if ((GeneratedNukePath_textBox.Text.Length > 0) &&
                    (NukeTemplatePath_textBox.Text.Length > 0))
                    this.GenerateNuke_button.Enabled = true ;
                //else
                //    this.GenerateNuke_button.Enabled = false;
            }
            //else
            //    this.GenerateNuke_button.Enabled = false ;

        }

        private void GeneratedNukePath_textBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }
        private void GeneratedNukePath_textBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            GeneratedNukePath_textBox.Text = FileList[0];
            if (GeneratedNukePath_textBox.Text.Length > 0)
            {
                GeneratedNukePath_textBox.SelectionStart = GeneratedNukePath_textBox.Text.Length - 1;
                GeneratedNukePath_textBox.SelectionLength = 0;
            }
        }

        private void GeneratedNukePath_textBox_DragEnter(object sender, DragEventArgs e)
        {
        }
        private void GeneratedNukePath_textBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(GeneratedNukePath_textBox.Text))
            {
                string nukePath = Path.GetDirectoryName(GeneratedNukePath_textBox.Text);
                string baseName = Path.GetFileNameWithoutExtension(nukePath);

                this.DestBaseName_textBox.Text = baseName;
            }

            if (File.Exists(PluralEyes_export_textBox.Text))
            {
                if ((GeneratedNukePath_textBox.Text.Length > 0) &&
                    (NukeTemplatePath_textBox.Text.Length > 0))
                    this.GenerateNuke_button.Enabled = true;
                //else
                //    this.GenerateNuke_button.Enabled = false;
            }
            //else
            //    this.GenerateNuke_button.Enabled = false;
        }

        private void NukeTemplatePath_textBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }
        private void NukeTemplatePath_textBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            NukeTemplatePath_textBox.Text = FileList[0];
            if (NukeTemplatePath_textBox.Text.Length > 0)
            {
                NukeTemplatePath_textBox.SelectionStart = NukeTemplatePath_textBox.Text.Length - 1;
                NukeTemplatePath_textBox.SelectionLength = 0;
            }
        }

        private void NukeTemplatePath_textBox_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void NukeTemplatePath_textBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(NukeTemplatePath_textBox.Text))
            {
                string nukePath = Path.GetDirectoryName(NukeTemplatePath_textBox.Text);
                string baseName = Path.GetFileNameWithoutExtension(nukePath);

                this.SourceBaseName_textBox.Text = baseName;
            }

            if (File.Exists(PluralEyes_export_textBox.Text))
            {
                    if ((GeneratedNukePath_textBox.Text.Length > 0) &&
                    (NukeTemplatePath_textBox.Text.Length > 0))
                    this.GenerateNuke_button.Enabled = true;
            //    else
            //        this.GenerateNuke_button.Enabled = false;
            }
            //else
            //    this.GenerateNuke_button.Enabled = false;
        }

        private void BrowseNukeTemplatePath_button_Click(object sender, EventArgs e)
        {

        }
    }
}
