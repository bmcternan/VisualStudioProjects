namespace Ingress
{
    partial class IngressForm
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
            this.JobDirTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.GetCameraButton = new System.Windows.Forms.Button();
            this.CameraTreeView = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.JobTreeView = new System.Windows.Forms.TreeView();
            this.JobDirLabel = new System.Windows.Forms.Label();
            this.SceneListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AddSceneButton = new System.Windows.Forms.Button();
            this.AddCopyButton = new System.Windows.Forms.Button();
            this.SourceListBox = new System.Windows.Forms.ListBox();
            this.DestListBox = new System.Windows.Forms.ListBox();
            this.ExecuteCopyButton = new System.Windows.Forms.Button();
            this.CameraLabel = new System.Windows.Forms.Label();
            this.DeleteCopyButton = new System.Windows.Forms.Button();
            this.CamNumListBox = new System.Windows.Forms.ListBox();
            this.GuessAllButton = new System.Windows.Forms.Button();
            this.GuessType_radioButton1 = new System.Windows.Forms.RadioButton();
            this.GuessType_radioButton2 = new System.Windows.Forms.RadioButton();
            this.CameraFileSort_radioButton1 = new System.Windows.Forms.RadioButton();
            this.CameraFileSort_radioButton2 = new System.Windows.Forms.RadioButton();
            this.CameraFileSort_radioButton3 = new System.Windows.Forms.RadioButton();
            this.CameraFilesSort_groupBox = new System.Windows.Forms.GroupBox();
            this.GuessType_groupBox = new System.Windows.Forms.GroupBox();
            this.CameraFilesSort_groupBox.SuspendLayout();
            this.GuessType_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Job Directory";
            // 
            // JobDirTextBox
            // 
            this.JobDirTextBox.Location = new System.Drawing.Point(81, 2);
            this.JobDirTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.JobDirTextBox.Name = "JobDirTextBox";
            this.JobDirTextBox.Size = new System.Drawing.Size(344, 20);
            this.JobDirTextBox.TabIndex = 1;
            this.JobDirTextBox.TextChanged += new System.EventHandler(this.JobDirTextBox_TextChanged);
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(432, 4);
            this.BrowseButton.Margin = new System.Windows.Forms.Padding(2);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(34, 22);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "...";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // GetCameraButton
            // 
            this.GetCameraButton.Enabled = false;
            this.GetCameraButton.Location = new System.Drawing.Point(4, 62);
            this.GetCameraButton.Margin = new System.Windows.Forms.Padding(2);
            this.GetCameraButton.Name = "GetCameraButton";
            this.GetCameraButton.Size = new System.Drawing.Size(138, 32);
            this.GetCameraButton.TabIndex = 3;
            this.GetCameraButton.Text = "Load From Camera Files";
            this.GetCameraButton.UseVisualStyleBackColor = true;
            this.GetCameraButton.Click += new System.EventHandler(this.GetCameraButton_Click);
            // 
            // CameraTreeView
            // 
            this.CameraTreeView.Location = new System.Drawing.Point(4, 125);
            this.CameraTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.CameraTreeView.Name = "CameraTreeView";
            this.CameraTreeView.Size = new System.Drawing.Size(212, 334);
            this.CameraTreeView.TabIndex = 4;
            this.CameraTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CameraTreeView_AfterSelect);
            this.CameraTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.CameraTreeView_NodeMouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Camera Files";
            // 
            // JobTreeView
            // 
            this.JobTreeView.Location = new System.Drawing.Point(498, 120);
            this.JobTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.JobTreeView.Name = "JobTreeView";
            this.JobTreeView.Size = new System.Drawing.Size(282, 334);
            this.JobTreeView.TabIndex = 6;
            this.JobTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.JobTreeView_NodeMouseDoubleClick);
            // 
            // JobDirLabel
            // 
            this.JobDirLabel.Location = new System.Drawing.Point(496, 92);
            this.JobDirLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.JobDirLabel.Name = "JobDirLabel";
            this.JobDirLabel.Size = new System.Drawing.Size(283, 19);
            this.JobDirLabel.TabIndex = 7;
            this.JobDirLabel.Text = "Job Dir";
            // 
            // SceneListBox
            // 
            this.SceneListBox.FormattingEnabled = true;
            this.SceneListBox.Location = new System.Drawing.Point(250, 25);
            this.SceneListBox.Margin = new System.Windows.Forms.Padding(2);
            this.SceneListBox.Name = "SceneListBox";
            this.SceneListBox.Size = new System.Drawing.Size(169, 69);
            this.SceneListBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Scenes";
            // 
            // AddSceneButton
            // 
            this.AddSceneButton.Enabled = false;
            this.AddSceneButton.Location = new System.Drawing.Point(212, 47);
            this.AddSceneButton.Margin = new System.Windows.Forms.Padding(2);
            this.AddSceneButton.Name = "AddSceneButton";
            this.AddSceneButton.Size = new System.Drawing.Size(30, 30);
            this.AddSceneButton.TabIndex = 12;
            this.AddSceneButton.Text = "+";
            this.AddSceneButton.UseVisualStyleBackColor = true;
            this.AddSceneButton.Click += new System.EventHandler(this.AddSceneButton_Click);
            // 
            // AddCopyButton
            // 
            this.AddCopyButton.Enabled = false;
            this.AddCopyButton.Location = new System.Drawing.Point(221, 238);
            this.AddCopyButton.Margin = new System.Windows.Forms.Padding(2);
            this.AddCopyButton.Name = "AddCopyButton";
            this.AddCopyButton.Size = new System.Drawing.Size(29, 29);
            this.AddCopyButton.TabIndex = 13;
            this.AddCopyButton.Text = "+";
            this.AddCopyButton.UseVisualStyleBackColor = true;
            this.AddCopyButton.Click += new System.EventHandler(this.AddCopyButton_Click);
            // 
            // SourceListBox
            // 
            this.SourceListBox.FormattingEnabled = true;
            this.SourceListBox.Location = new System.Drawing.Point(254, 132);
            this.SourceListBox.Margin = new System.Windows.Forms.Padding(2);
            this.SourceListBox.Name = "SourceListBox";
            this.SourceListBox.Size = new System.Drawing.Size(108, 277);
            this.SourceListBox.TabIndex = 14;
            this.SourceListBox.SelectedIndexChanged += new System.EventHandler(this.SourceListBox_SelectedIndexChanged);
            this.SourceListBox.DoubleClick += new System.EventHandler(this.SourceListBox_DoubleClick);
            this.SourceListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceListBox_KeyDown);
            // 
            // DestListBox
            // 
            this.DestListBox.FormattingEnabled = true;
            this.DestListBox.Location = new System.Drawing.Point(395, 132);
            this.DestListBox.Margin = new System.Windows.Forms.Padding(2);
            this.DestListBox.Name = "DestListBox";
            this.DestListBox.Size = new System.Drawing.Size(99, 277);
            this.DestListBox.TabIndex = 15;
            this.DestListBox.SelectedIndexChanged += new System.EventHandler(this.DestListBox_SelectedIndexChanged);
            // 
            // ExecuteCopyButton
            // 
            this.ExecuteCopyButton.Enabled = false;
            this.ExecuteCopyButton.Location = new System.Drawing.Point(270, 413);
            this.ExecuteCopyButton.Margin = new System.Windows.Forms.Padding(2);
            this.ExecuteCopyButton.Name = "ExecuteCopyButton";
            this.ExecuteCopyButton.Size = new System.Drawing.Size(208, 39);
            this.ExecuteCopyButton.TabIndex = 16;
            this.ExecuteCopyButton.Text = "Do Copy";
            this.ExecuteCopyButton.UseVisualStyleBackColor = true;
            this.ExecuteCopyButton.Click += new System.EventHandler(this.ExecuteCopyButton_Click);
            // 
            // CameraLabel
            // 
            this.CameraLabel.AutoSize = true;
            this.CameraLabel.Location = new System.Drawing.Point(341, 102);
            this.CameraLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CameraLabel.Name = "CameraLabel";
            this.CameraLabel.Size = new System.Drawing.Size(83, 13);
            this.CameraLabel.TabIndex = 17;
            this.CameraLabel.Text = "Camera Number";
            // 
            // DeleteCopyButton
            // 
            this.DeleteCopyButton.Enabled = false;
            this.DeleteCopyButton.Location = new System.Drawing.Point(221, 271);
            this.DeleteCopyButton.Margin = new System.Windows.Forms.Padding(2);
            this.DeleteCopyButton.Name = "DeleteCopyButton";
            this.DeleteCopyButton.Size = new System.Drawing.Size(29, 29);
            this.DeleteCopyButton.TabIndex = 18;
            this.DeleteCopyButton.Text = "-";
            this.DeleteCopyButton.UseVisualStyleBackColor = true;
            // 
            // CamNumListBox
            // 
            this.CamNumListBox.FormattingEnabled = true;
            this.CamNumListBox.Location = new System.Drawing.Point(359, 132);
            this.CamNumListBox.Name = "CamNumListBox";
            this.CamNumListBox.Size = new System.Drawing.Size(38, 277);
            this.CamNumListBox.TabIndex = 19;
            this.CamNumListBox.SelectedIndexChanged += new System.EventHandler(this.CamNumListBox_SelectedIndexChanged);
            // 
            // GuessAllButton
            // 
            this.GuessAllButton.Enabled = false;
            this.GuessAllButton.Location = new System.Drawing.Point(285, 477);
            this.GuessAllButton.Name = "GuessAllButton";
            this.GuessAllButton.Size = new System.Drawing.Size(181, 31);
            this.GuessAllButton.TabIndex = 20;
            this.GuessAllButton.Text = "Guess All";
            this.GuessAllButton.UseVisualStyleBackColor = true;
            this.GuessAllButton.Click += new System.EventHandler(this.GuessAllButton_Click);
            // 
            // GuessType_radioButton1
            // 
            this.GuessType_radioButton1.AutoSize = true;
            this.GuessType_radioButton1.Checked = true;
            this.GuessType_radioButton1.Location = new System.Drawing.Point(12, 3);
            this.GuessType_radioButton1.Name = "GuessType_radioButton1";
            this.GuessType_radioButton1.Size = new System.Drawing.Size(124, 17);
            this.GuessType_radioButton1.TabIndex = 21;
            this.GuessType_radioButton1.TabStop = true;
            this.GuessType_radioButton1.Text = "Guess By Time/Date";
            this.GuessType_radioButton1.UseVisualStyleBackColor = true;
            this.GuessType_radioButton1.CheckedChanged += new System.EventHandler(this.GuessType_radioButton1_CheckedChanged);
            // 
            // GuessType_radioButton2
            // 
            this.GuessType_radioButton2.AutoSize = true;
            this.GuessType_radioButton2.Location = new System.Drawing.Point(142, 3);
            this.GuessType_radioButton2.Name = "GuessType_radioButton2";
            this.GuessType_radioButton2.Size = new System.Drawing.Size(121, 17);
            this.GuessType_radioButton2.TabIndex = 22;
            this.GuessType_radioButton2.Text = "Guess By Sort Order";
            this.GuessType_radioButton2.UseVisualStyleBackColor = true;
            this.GuessType_radioButton2.CheckedChanged += new System.EventHandler(this.GuessType_radioButton2_CheckedChanged);
            // 
            // CameraFileSort_radioButton1
            // 
            this.CameraFileSort_radioButton1.AutoSize = true;
            this.CameraFileSort_radioButton1.Checked = true;
            this.CameraFileSort_radioButton1.Location = new System.Drawing.Point(5, 11);
            this.CameraFileSort_radioButton1.Name = "CameraFileSort_radioButton1";
            this.CameraFileSort_radioButton1.Size = new System.Drawing.Size(53, 17);
            this.CameraFileSort_radioButton1.TabIndex = 23;
            this.CameraFileSort_radioButton1.TabStop = true;
            this.CameraFileSort_radioButton1.Text = "Name";
            this.CameraFileSort_radioButton1.UseVisualStyleBackColor = true;
            this.CameraFileSort_radioButton1.CheckedChanged += new System.EventHandler(this.CameraFileSort_radioButton1_CheckedChanged);
            // 
            // CameraFileSort_radioButton2
            // 
            this.CameraFileSort_radioButton2.AutoSize = true;
            this.CameraFileSort_radioButton2.Location = new System.Drawing.Point(64, 11);
            this.CameraFileSort_radioButton2.Name = "CameraFileSort_radioButton2";
            this.CameraFileSort_radioButton2.Size = new System.Drawing.Size(49, 17);
            this.CameraFileSort_radioButton2.TabIndex = 24;
            this.CameraFileSort_radioButton2.Text = "Type";
            this.CameraFileSort_radioButton2.UseVisualStyleBackColor = true;
            this.CameraFileSort_radioButton2.CheckedChanged += new System.EventHandler(this.CameraFileSort_radioButton2_CheckedChanged);
            // 
            // CameraFileSort_radioButton3
            // 
            this.CameraFileSort_radioButton3.AutoSize = true;
            this.CameraFileSort_radioButton3.Location = new System.Drawing.Point(113, 11);
            this.CameraFileSort_radioButton3.Name = "CameraFileSort_radioButton3";
            this.CameraFileSort_radioButton3.Size = new System.Drawing.Size(48, 17);
            this.CameraFileSort_radioButton3.TabIndex = 25;
            this.CameraFileSort_radioButton3.Text = "Date";
            this.CameraFileSort_radioButton3.UseVisualStyleBackColor = true;
            this.CameraFileSort_radioButton3.CheckedChanged += new System.EventHandler(this.CameraFileSort_radioButton3_CheckedChanged);
            // 
            // CameraFilesSort_groupBox
            // 
            this.CameraFilesSort_groupBox.Controls.Add(this.CameraFileSort_radioButton3);
            this.CameraFilesSort_groupBox.Controls.Add(this.CameraFileSort_radioButton2);
            this.CameraFilesSort_groupBox.Controls.Add(this.CameraFileSort_radioButton1);
            this.CameraFilesSort_groupBox.Location = new System.Drawing.Point(80, 92);
            this.CameraFilesSort_groupBox.Name = "CameraFilesSort_groupBox";
            this.CameraFilesSort_groupBox.Size = new System.Drawing.Size(174, 33);
            this.CameraFilesSort_groupBox.TabIndex = 26;
            this.CameraFilesSort_groupBox.TabStop = false;
            // 
            // GuessType_groupBox
            // 
            this.GuessType_groupBox.Controls.Add(this.GuessType_radioButton2);
            this.GuessType_groupBox.Controls.Add(this.GuessType_radioButton1);
            this.GuessType_groupBox.Location = new System.Drawing.Point(254, 457);
            this.GuessType_groupBox.Name = "GuessType_groupBox";
            this.GuessType_groupBox.Size = new System.Drawing.Size(266, 21);
            this.GuessType_groupBox.TabIndex = 27;
            this.GuessType_groupBox.TabStop = false;
            // 
            // IngressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 533);
            this.Controls.Add(this.GuessType_groupBox);
            this.Controls.Add(this.CameraFilesSort_groupBox);
            this.Controls.Add(this.GuessAllButton);
            this.Controls.Add(this.CamNumListBox);
            this.Controls.Add(this.DeleteCopyButton);
            this.Controls.Add(this.CameraLabel);
            this.Controls.Add(this.ExecuteCopyButton);
            this.Controls.Add(this.DestListBox);
            this.Controls.Add(this.SourceListBox);
            this.Controls.Add(this.AddCopyButton);
            this.Controls.Add(this.AddSceneButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SceneListBox);
            this.Controls.Add(this.JobDirLabel);
            this.Controls.Add(this.JobTreeView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CameraTreeView);
            this.Controls.Add(this.GetCameraButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.JobDirTextBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "IngressForm";
            this.Text = "360 Video Ingress";
            this.CameraFilesSort_groupBox.ResumeLayout(false);
            this.CameraFilesSort_groupBox.PerformLayout();
            this.GuessType_groupBox.ResumeLayout(false);
            this.GuessType_groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox JobDirTextBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button GetCameraButton;
        private System.Windows.Forms.TreeView CameraTreeView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView JobTreeView;
        private System.Windows.Forms.Label JobDirLabel;
        private System.Windows.Forms.ListBox SceneListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button AddSceneButton;
        private System.Windows.Forms.Button AddCopyButton;
        private System.Windows.Forms.ListBox SourceListBox;
        private System.Windows.Forms.ListBox DestListBox;
        private System.Windows.Forms.Button ExecuteCopyButton;
        private System.Windows.Forms.Label CameraLabel;
        private System.Windows.Forms.Button DeleteCopyButton;
        private System.Windows.Forms.ListBox CamNumListBox;
        private System.Windows.Forms.Button GuessAllButton;
        private System.Windows.Forms.RadioButton GuessType_radioButton1;
        private System.Windows.Forms.RadioButton GuessType_radioButton2;
        private System.Windows.Forms.RadioButton CameraFileSort_radioButton1;
        private System.Windows.Forms.RadioButton CameraFileSort_radioButton2;
        private System.Windows.Forms.RadioButton CameraFileSort_radioButton3;
        private System.Windows.Forms.GroupBox CameraFilesSort_groupBox;
        private System.Windows.Forms.GroupBox GuessType_groupBox;
    }
}

