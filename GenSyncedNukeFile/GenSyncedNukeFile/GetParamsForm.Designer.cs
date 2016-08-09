namespace GenSyncedNukeFile
{
    partial class GetParamsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.CameraPath_textBox = new System.Windows.Forms.TextBox();
            this.Browse_button = new System.Windows.Forms.Button();
            this.LaunchPluralEyes_button = new System.Windows.Forms.Button();
            this.BuildXML_button = new System.Windows.Forms.Button();
            this.BrowseForPluralEyesExport_button = new System.Windows.Forms.Button();
            this.PluralEyes_export_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BrowseNukeFile_button = new System.Windows.Forms.Button();
            this.GeneratedNukePath_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GenerateNuke_button = new System.Windows.Forms.Button();
            this.BrowseFinalCutProXML_button = new System.Windows.Forms.Button();
            this.FinalCutProXMLPath_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BrowseNukeTemplatePath_button = new System.Windows.Forms.Button();
            this.NukeTemplatePath_textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SourceBaseName_textBox = new System.Windows.Forms.TextBox();
            this.DestBaseName_textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path to Camera Files For This Shot";
            // 
            // CameraPath_textBox
            // 
            this.CameraPath_textBox.AllowDrop = true;
            this.CameraPath_textBox.Location = new System.Drawing.Point(196, 8);
            this.CameraPath_textBox.Name = "CameraPath_textBox";
            this.CameraPath_textBox.Size = new System.Drawing.Size(362, 20);
            this.CameraPath_textBox.TabIndex = 1;
            this.CameraPath_textBox.TextChanged += new System.EventHandler(this.CameraPath_textBox_TextChanged);
            this.CameraPath_textBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.CameraPath_textBox_DragDrop);
            this.CameraPath_textBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.CameraPath_textBox_DragEnter);
            this.CameraPath_textBox.DragOver += new System.Windows.Forms.DragEventHandler(this.CameraPath_textBox_DragOver);
            // 
            // Browse_button
            // 
            this.Browse_button.Location = new System.Drawing.Point(575, 6);
            this.Browse_button.Name = "Browse_button";
            this.Browse_button.Size = new System.Drawing.Size(43, 21);
            this.Browse_button.TabIndex = 2;
            this.Browse_button.Text = "...";
            this.Browse_button.UseVisualStyleBackColor = true;
            this.Browse_button.Click += new System.EventHandler(this.Browse_button_Click);
            // 
            // LaunchPluralEyes_button
            // 
            this.LaunchPluralEyes_button.Enabled = false;
            this.LaunchPluralEyes_button.Location = new System.Drawing.Point(196, 84);
            this.LaunchPluralEyes_button.Name = "LaunchPluralEyes_button";
            this.LaunchPluralEyes_button.Size = new System.Drawing.Size(131, 27);
            this.LaunchPluralEyes_button.TabIndex = 3;
            this.LaunchPluralEyes_button.Text = "Launch PluralEyes";
            this.LaunchPluralEyes_button.UseVisualStyleBackColor = true;
            this.LaunchPluralEyes_button.Click += new System.EventHandler(this.LaunchPluralEyes_button_Click);
            // 
            // BuildXML_button
            // 
            this.BuildXML_button.Enabled = false;
            this.BuildXML_button.Location = new System.Drawing.Point(196, 34);
            this.BuildXML_button.Name = "BuildXML_button";
            this.BuildXML_button.Size = new System.Drawing.Size(131, 22);
            this.BuildXML_button.TabIndex = 4;
            this.BuildXML_button.Text = "Build Final Cut Pro XML";
            this.BuildXML_button.UseVisualStyleBackColor = true;
            this.BuildXML_button.Click += new System.EventHandler(this.BuildXML_button_Click);
            // 
            // BrowseForPluralEyesExport_button
            // 
            this.BrowseForPluralEyesExport_button.Location = new System.Drawing.Point(575, 124);
            this.BrowseForPluralEyesExport_button.Name = "BrowseForPluralEyesExport_button";
            this.BrowseForPluralEyesExport_button.Size = new System.Drawing.Size(43, 21);
            this.BrowseForPluralEyesExport_button.TabIndex = 7;
            this.BrowseForPluralEyesExport_button.Text = "...";
            this.BrowseForPluralEyesExport_button.UseVisualStyleBackColor = true;
            this.BrowseForPluralEyesExport_button.Click += new System.EventHandler(this.BrowseForPluralEyesExport_button_Click);
            // 
            // PluralEyes_export_textBox
            // 
            this.PluralEyes_export_textBox.AllowDrop = true;
            this.PluralEyes_export_textBox.Location = new System.Drawing.Point(196, 126);
            this.PluralEyes_export_textBox.Name = "PluralEyes_export_textBox";
            this.PluralEyes_export_textBox.Size = new System.Drawing.Size(362, 20);
            this.PluralEyes_export_textBox.TabIndex = 6;
            this.PluralEyes_export_textBox.TextChanged += new System.EventHandler(this.PluralEyes_export_textBox_TextChanged);
            this.PluralEyes_export_textBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.PluralEyes_export_textBox_DragDrop);
            this.PluralEyes_export_textBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.PluralEyes_export_textBox_DragEnter);
            this.PluralEyes_export_textBox.DragOver += new System.Windows.Forms.DragEventHandler(this.PluralEyes_export_textBox_DragOver);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Path to PluralEyes Export";
            // 
            // BrowseNukeFile_button
            // 
            this.BrowseNukeFile_button.Location = new System.Drawing.Point(575, 176);
            this.BrowseNukeFile_button.Name = "BrowseNukeFile_button";
            this.BrowseNukeFile_button.Size = new System.Drawing.Size(43, 21);
            this.BrowseNukeFile_button.TabIndex = 10;
            this.BrowseNukeFile_button.Text = "...";
            this.BrowseNukeFile_button.UseVisualStyleBackColor = true;
            this.BrowseNukeFile_button.Click += new System.EventHandler(this.BrowseNukeFile_button_Click);
            // 
            // GeneratedNukePath_textBox
            // 
            this.GeneratedNukePath_textBox.AllowDrop = true;
            this.GeneratedNukePath_textBox.Location = new System.Drawing.Point(196, 178);
            this.GeneratedNukePath_textBox.Name = "GeneratedNukePath_textBox";
            this.GeneratedNukePath_textBox.Size = new System.Drawing.Size(362, 20);
            this.GeneratedNukePath_textBox.TabIndex = 9;
            this.GeneratedNukePath_textBox.TextChanged += new System.EventHandler(this.GeneratedNukePath_textBox_TextChanged);
            this.GeneratedNukePath_textBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.GeneratedNukePath_textBox_DragDrop);
            this.GeneratedNukePath_textBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.GeneratedNukePath_textBox_DragEnter);
            this.GeneratedNukePath_textBox.DragOver += new System.Windows.Forms.DragEventHandler(this.GeneratedNukePath_textBox_DragOver);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Path to Generated Nuke File";
            // 
            // GenerateNuke_button
            // 
            this.GenerateNuke_button.Location = new System.Drawing.Point(196, 204);
            this.GenerateNuke_button.Name = "GenerateNuke_button";
            this.GenerateNuke_button.Size = new System.Drawing.Size(131, 27);
            this.GenerateNuke_button.TabIndex = 11;
            this.GenerateNuke_button.Text = "Generate Nuke File";
            this.GenerateNuke_button.UseVisualStyleBackColor = true;
            this.GenerateNuke_button.Click += new System.EventHandler(this.GenerateNuke_button_Click);
            // 
            // BrowseFinalCutProXML_button
            // 
            this.BrowseFinalCutProXML_button.Location = new System.Drawing.Point(575, 60);
            this.BrowseFinalCutProXML_button.Name = "BrowseFinalCutProXML_button";
            this.BrowseFinalCutProXML_button.Size = new System.Drawing.Size(43, 21);
            this.BrowseFinalCutProXML_button.TabIndex = 13;
            this.BrowseFinalCutProXML_button.Text = "...";
            this.BrowseFinalCutProXML_button.UseVisualStyleBackColor = true;
            this.BrowseFinalCutProXML_button.Click += new System.EventHandler(this.BrowseFinalCutProXML_button_Click);
            // 
            // FinalCutProXMLPath_textBox
            // 
            this.FinalCutProXMLPath_textBox.AllowDrop = true;
            this.FinalCutProXMLPath_textBox.Location = new System.Drawing.Point(196, 62);
            this.FinalCutProXMLPath_textBox.Name = "FinalCutProXMLPath_textBox";
            this.FinalCutProXMLPath_textBox.Size = new System.Drawing.Size(362, 20);
            this.FinalCutProXMLPath_textBox.TabIndex = 12;
            this.FinalCutProXMLPath_textBox.TextChanged += new System.EventHandler(this.FinalCutProXMLPath_textBox_TextChanged);
            this.FinalCutProXMLPath_textBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.FinalCutProXMLPath_textBox_DragDrop);
            this.FinalCutProXMLPath_textBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.FinalCutProXMLPath_textBox_DragEnter);
            this.FinalCutProXMLPath_textBox.DragOver += new System.Windows.Forms.DragEventHandler(this.FinalCutProXMLPath_textBox_DragOver);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Path to Generated Final Cut Pro XML";
            // 
            // BrowseNukeTemplatePath_button
            // 
            this.BrowseNukeTemplatePath_button.Location = new System.Drawing.Point(575, 150);
            this.BrowseNukeTemplatePath_button.Name = "BrowseNukeTemplatePath_button";
            this.BrowseNukeTemplatePath_button.Size = new System.Drawing.Size(43, 21);
            this.BrowseNukeTemplatePath_button.TabIndex = 17;
            this.BrowseNukeTemplatePath_button.Text = "...";
            this.BrowseNukeTemplatePath_button.UseVisualStyleBackColor = true;
            this.BrowseNukeTemplatePath_button.Click += new System.EventHandler(this.BrowseNukeTemplatePath_button_Click);
            // 
            // NukeTemplatePath_textBox
            // 
            this.NukeTemplatePath_textBox.AllowDrop = true;
            this.NukeTemplatePath_textBox.Location = new System.Drawing.Point(196, 152);
            this.NukeTemplatePath_textBox.Name = "NukeTemplatePath_textBox";
            this.NukeTemplatePath_textBox.Size = new System.Drawing.Size(362, 20);
            this.NukeTemplatePath_textBox.TabIndex = 16;
            this.NukeTemplatePath_textBox.TextChanged += new System.EventHandler(this.NukeTemplatePath_textBox_TextChanged);
            this.NukeTemplatePath_textBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.NukeTemplatePath_textBox_DragDrop);
            this.NukeTemplatePath_textBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.NukeTemplatePath_textBox_DragEnter);
            this.NukeTemplatePath_textBox.DragOver += new System.Windows.Forms.DragEventHandler(this.NukeTemplatePath_textBox_DragOver);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Path to Template Nuke File";
            // 
            // SourceBaseName_textBox
            // 
            this.SourceBaseName_textBox.Location = new System.Drawing.Point(627, 153);
            this.SourceBaseName_textBox.Name = "SourceBaseName_textBox";
            this.SourceBaseName_textBox.Size = new System.Drawing.Size(83, 20);
            this.SourceBaseName_textBox.TabIndex = 18;
            // 
            // DestBaseName_textBox
            // 
            this.DestBaseName_textBox.Location = new System.Drawing.Point(627, 179);
            this.DestBaseName_textBox.Name = "DestBaseName_textBox";
            this.DestBaseName_textBox.Size = new System.Drawing.Size(83, 20);
            this.DestBaseName_textBox.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(634, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Base Names";
            // 
            // GetParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 249);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DestBaseName_textBox);
            this.Controls.Add(this.SourceBaseName_textBox);
            this.Controls.Add(this.BrowseNukeTemplatePath_button);
            this.Controls.Add(this.NukeTemplatePath_textBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BrowseFinalCutProXML_button);
            this.Controls.Add(this.FinalCutProXMLPath_textBox);
            this.Controls.Add(this.GenerateNuke_button);
            this.Controls.Add(this.BrowseNukeFile_button);
            this.Controls.Add(this.GeneratedNukePath_textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BrowseForPluralEyesExport_button);
            this.Controls.Add(this.PluralEyes_export_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BuildXML_button);
            this.Controls.Add(this.LaunchPluralEyes_button);
            this.Controls.Add(this.Browse_button);
            this.Controls.Add(this.CameraPath_textBox);
            this.Controls.Add(this.label1);
            this.Name = "GetParamsForm";
            this.Text = "Set Parameters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CameraPath_textBox;
        private System.Windows.Forms.Button Browse_button;
        private System.Windows.Forms.Button LaunchPluralEyes_button;
        private System.Windows.Forms.Button BuildXML_button;
        private System.Windows.Forms.Button BrowseForPluralEyesExport_button;
        private System.Windows.Forms.TextBox PluralEyes_export_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BrowseNukeFile_button;
        private System.Windows.Forms.TextBox GeneratedNukePath_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button GenerateNuke_button;
        private System.Windows.Forms.Button BrowseFinalCutProXML_button;
        private System.Windows.Forms.TextBox FinalCutProXMLPath_textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BrowseNukeTemplatePath_button;
        private System.Windows.Forms.TextBox NukeTemplatePath_textBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SourceBaseName_textBox;
        private System.Windows.Forms.TextBox DestBaseName_textBox;
        private System.Windows.Forms.Label label6;
    }
}

