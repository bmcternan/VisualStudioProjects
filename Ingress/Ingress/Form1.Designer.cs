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
            this.label3 = new System.Windows.Forms.Label();
            this.NumCamerasNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.SceneListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AddSceneButton = new System.Windows.Forms.Button();
            this.AddCopyButton = new System.Windows.Forms.Button();
            this.SourceListBox = new System.Windows.Forms.ListBox();
            this.DestListBox = new System.Windows.Forms.ListBox();
            this.ExecuteCopyButton = new System.Windows.Forms.Button();
            this.CameraLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumCamerasNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Job Directory";
            // 
            // JobDirTextBox
            // 
            this.JobDirTextBox.Location = new System.Drawing.Point(162, 4);
            this.JobDirTextBox.Name = "JobDirTextBox";
            this.JobDirTextBox.Size = new System.Drawing.Size(685, 31);
            this.JobDirTextBox.TabIndex = 1;
            this.JobDirTextBox.TextChanged += new System.EventHandler(this.JobDirTextBox_TextChanged);
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(865, 7);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(69, 42);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "...";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // GetCameraButton
            // 
            this.GetCameraButton.Enabled = false;
            this.GetCameraButton.Location = new System.Drawing.Point(8, 120);
            this.GetCameraButton.Name = "GetCameraButton";
            this.GetCameraButton.Size = new System.Drawing.Size(276, 62);
            this.GetCameraButton.TabIndex = 3;
            this.GetCameraButton.Text = "Load From Camera Files";
            this.GetCameraButton.UseVisualStyleBackColor = true;
            this.GetCameraButton.Click += new System.EventHandler(this.GetCameraButton_Click);
            // 
            // CameraTreeView
            // 
            this.CameraTreeView.Location = new System.Drawing.Point(8, 240);
            this.CameraTreeView.Name = "CameraTreeView";
            this.CameraTreeView.Size = new System.Drawing.Size(419, 639);
            this.CameraTreeView.TabIndex = 4;
            this.CameraTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CameraTreeView_AfterSelect);
            this.CameraTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.CameraTreeView_NodeMouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Camera Files";
            // 
            // JobTreeView
            // 
            this.JobTreeView.Location = new System.Drawing.Point(996, 231);
            this.JobTreeView.Name = "JobTreeView";
            this.JobTreeView.Size = new System.Drawing.Size(561, 638);
            this.JobTreeView.TabIndex = 6;
            this.JobTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.JobTreeView_NodeMouseDoubleClick);
            // 
            // JobDirLabel
            // 
            this.JobDirLabel.Location = new System.Drawing.Point(991, 177);
            this.JobDirLabel.Name = "JobDirLabel";
            this.JobDirLabel.Size = new System.Drawing.Size(566, 36);
            this.JobDirLabel.TabIndex = 7;
            this.JobDirLabel.Text = "Job Dir";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Num Cameras";
            // 
            // NumCamerasNumericUpDown
            // 
            this.NumCamerasNumericUpDown.Location = new System.Drawing.Point(184, 49);
            this.NumCamerasNumericUpDown.Name = "NumCamerasNumericUpDown";
            this.NumCamerasNumericUpDown.Size = new System.Drawing.Size(103, 31);
            this.NumCamerasNumericUpDown.TabIndex = 9;
            this.NumCamerasNumericUpDown.ValueChanged += new System.EventHandler(this.NumCamerasNumericUpDown_ValueChanged);
            // 
            // SceneListBox
            // 
            this.SceneListBox.FormattingEnabled = true;
            this.SceneListBox.ItemHeight = 25;
            this.SceneListBox.Location = new System.Drawing.Point(500, 49);
            this.SceneListBox.Name = "SceneListBox";
            this.SceneListBox.Size = new System.Drawing.Size(334, 129);
            this.SceneListBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(401, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Scenes";
            // 
            // AddSceneButton
            // 
            this.AddSceneButton.Enabled = false;
            this.AddSceneButton.Location = new System.Drawing.Point(425, 90);
            this.AddSceneButton.Name = "AddSceneButton";
            this.AddSceneButton.Size = new System.Drawing.Size(59, 57);
            this.AddSceneButton.TabIndex = 12;
            this.AddSceneButton.Text = "+";
            this.AddSceneButton.UseVisualStyleBackColor = true;
            this.AddSceneButton.Click += new System.EventHandler(this.AddSceneButton_Click);
            // 
            // AddCopyButton
            // 
            this.AddCopyButton.Location = new System.Drawing.Point(442, 457);
            this.AddCopyButton.Name = "AddCopyButton";
            this.AddCopyButton.Size = new System.Drawing.Size(58, 55);
            this.AddCopyButton.TabIndex = 13;
            this.AddCopyButton.Text = "+";
            this.AddCopyButton.UseVisualStyleBackColor = true;
            this.AddCopyButton.Click += new System.EventHandler(this.AddCopyButton_Click);
            // 
            // SourceListBox
            // 
            this.SourceListBox.FormattingEnabled = true;
            this.SourceListBox.ItemHeight = 25;
            this.SourceListBox.Location = new System.Drawing.Point(539, 251);
            this.SourceListBox.Name = "SourceListBox";
            this.SourceListBox.Size = new System.Drawing.Size(217, 529);
            this.SourceListBox.TabIndex = 14;
            this.SourceListBox.SelectedIndexChanged += new System.EventHandler(this.SourceListBox_SelectedIndexChanged);
            this.SourceListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SourceListBox_KeyDown);
            // 
            // DestListBox
            // 
            this.DestListBox.FormattingEnabled = true;
            this.DestListBox.ItemHeight = 25;
            this.DestListBox.Location = new System.Drawing.Point(762, 251);
            this.DestListBox.Name = "DestListBox";
            this.DestListBox.Size = new System.Drawing.Size(194, 529);
            this.DestListBox.TabIndex = 15;
            this.DestListBox.SelectedIndexChanged += new System.EventHandler(this.DestListBox_SelectedIndexChanged);
            // 
            // ExecuteCopyButton
            // 
            this.ExecuteCopyButton.Location = new System.Drawing.Point(539, 794);
            this.ExecuteCopyButton.Name = "ExecuteCopyButton";
            this.ExecuteCopyButton.Size = new System.Drawing.Size(416, 75);
            this.ExecuteCopyButton.TabIndex = 16;
            this.ExecuteCopyButton.Text = "Do Copy";
            this.ExecuteCopyButton.UseVisualStyleBackColor = true;
            this.ExecuteCopyButton.Click += new System.EventHandler(this.ExecuteCopyButton_Click);
            // 
            // CameraLabel
            // 
            this.CameraLabel.AutoSize = true;
            this.CameraLabel.Location = new System.Drawing.Point(682, 197);
            this.CameraLabel.Name = "CameraLabel";
            this.CameraLabel.Size = new System.Drawing.Size(168, 25);
            this.CameraLabel.TabIndex = 17;
            this.CameraLabel.Text = "Camera Number";
            // 
            // IngressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1589, 965);
            this.Controls.Add(this.CameraLabel);
            this.Controls.Add(this.ExecuteCopyButton);
            this.Controls.Add(this.DestListBox);
            this.Controls.Add(this.SourceListBox);
            this.Controls.Add(this.AddCopyButton);
            this.Controls.Add(this.AddSceneButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SceneListBox);
            this.Controls.Add(this.NumCamerasNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.JobDirLabel);
            this.Controls.Add(this.JobTreeView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CameraTreeView);
            this.Controls.Add(this.GetCameraButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.JobDirTextBox);
            this.Controls.Add(this.label1);
            this.Name = "IngressForm";
            this.Text = "360 Video Ingress";
            ((System.ComponentModel.ISupportInitialize)(this.NumCamerasNumericUpDown)).EndInit();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NumCamerasNumericUpDown;
        private System.Windows.Forms.ListBox SceneListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button AddSceneButton;
        private System.Windows.Forms.Button AddCopyButton;
        private System.Windows.Forms.ListBox SourceListBox;
        private System.Windows.Forms.ListBox DestListBox;
        private System.Windows.Forms.Button ExecuteCopyButton;
        private System.Windows.Forms.Label CameraLabel;
    }
}

