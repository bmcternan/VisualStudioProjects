using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ingress
{
    public partial class IngressForm : Form
    {
        private bool _bTreeViewInited = false;

        private int _numCameras = 0;

        private string _jobDir;
        private DirectoryOrFile _jobRoot;
        private List<string> _scenes;

        private const string _invalidDirChars = "\\<>:\"/|?*\n\t";

        private DirectoryOrFile _camRoot;
        IngressTreeNode _selectedCamNode = null;
        private List<DirectoryOrFile> _sources;
        private List<string> _destinationScenes;
        private int _currentCamera = 0;

        private static IngressForm _instance = null;

        public static IngressForm GetInstance()
        {
            return _instance;
        }

        public IngressForm()
        {
            InitializeComponent();
            _instance = this;
        }
        
        public void Init()
        {
            if (!_bTreeViewInited)
            {
                _bTreeViewInited = true;
            }
            _scenes = new List<string>();
        }

        //protected void TraverseDirTree(DirectoryOrFile node, int level, ref TreeStore store, TreeIter iter)
        protected void TraverseDirTree(TreeView tv, DirectoryOrFile node, int level, ref IngressTreeNode tree_node) //ref TreeStore store, TreeIter iter)
        {
            IngressTreeNode thisNodeIter = new IngressTreeNode(node.Name(), node);

            if (level == 0)
                tv.Nodes.Add(thisNodeIter); //           store.AppendValues(node.Name());
            else
                tree_node.Nodes.Add(thisNodeIter); // store.AppendValues(iter, node.Name());

            for (int i = 0; i < level; i++)
                Console.Write("   ");
            Console.WriteLine("{0} = {1} ({2})", node.Name(), node.Path(), node.Type() == DirectoryOrFile.DOF_Type.DIR ? "D" : (node.Type() == DirectoryOrFile.DOF_Type.FILE ? "F" : "U"));

            for (int i = 0; i < node.NumChildren(); i++)
            {
                //TraverseDirTree(node.Child(i), level + 1, ref store, thisNodeIter);
                TraverseDirTree(tv, node.Child(i), level + 1, ref thisNodeIter);
            }
        }



        protected void LoadDirTree(ref DirectoryOrFile node, string path, DirectoryOrFile parent, DirectoryOrFile.DOF_Type dofType)
        {
            if (node == null)
                return;

            string nodeName = path.Substring(path.LastIndexOf("\\") + 1);

            node.Set(nodeName, path, parent, dofType);

            if (dofType == DirectoryOrFile.DOF_Type.DIR)
            {
                //scan for directories
                try
                {
                    List<string> dirList = new List<string>(Directory.EnumerateDirectories(path));
                    foreach (var dir in dirList)
                    {
                        string newDirName = dir.Substring(dir.LastIndexOf("\\") + 1);
                        Console.WriteLine("{0}", dir.Substring(dir.LastIndexOf("\\") + 1));

                        DirectoryOrFile sub = new DirectoryOrFile();
                        LoadDirTree(ref sub, dir, node, DirectoryOrFile.DOF_Type.DIR);

                        node.AddChild(sub);

                        if(node == _jobRoot)
                        {
                            if (!_scenes.Contains(newDirName))
                            {
                                _scenes.Add(newDirName);
                                SceneListBox.Items.Add(newDirName);
                            }
                        }
                    }
                    int num = dirList.Count;
                    Console.WriteLine("{0} directories found.", dirList.Count);
                }
                catch (UnauthorizedAccessException UAEx)
                {
                    Console.WriteLine(UAEx.Message);
                }
                catch (PathTooLongException PathEx)
                {
                    Console.WriteLine(PathEx.Message);
                }

                //scan for files
                try
                {
                    List<string> fileList = new List<string>(Directory.EnumerateFiles(path));
                    foreach (var fname in fileList)
                    {
                        string foo = fname.Substring(fname.LastIndexOf("\\") + 1);
                        Console.WriteLine("{0}", fname.Substring(fname.LastIndexOf("\\") + 1));
                        DirectoryOrFile sub = new DirectoryOrFile();
                        LoadDirTree(ref sub, fname, node, DirectoryOrFile.DOF_Type.FILE);

                        node.AddChild(sub);
                    }
                    int num = fileList.Count;
                    Console.WriteLine("{0} directories found.", fileList.Count);
                }
                catch (UnauthorizedAccessException UAEx)
                {
                    Console.WriteLine(UAEx.Message);
                }
                catch (PathTooLongException PathEx)
                {
                    Console.WriteLine(PathEx.Message);
                }
            }
        }

        protected void DisableButtons()
        {
            this.GetCameraButton.Enabled = false;
            this.AddSceneButton.Enabled = false;
        }

        protected void EnableButtons()
        {
            this.GetCameraButton.Enabled = true;
            this.AddSceneButton.Enabled = true;
        }

        protected void DisableAddCopyButton()
        {
            this.AddCopyButton.Enabled = false;
        }

        protected void EnableAddCopyButton()
        {
            this.AddCopyButton.Enabled = true;
        }

        protected void DisableExecuteCopyButton()
        {
            this.ExecuteCopyButton.Enabled = false;
            this.DeleteCopyButton.Enabled = false;
        }

        protected void EnableExecuteCopyButton()
        {
            this.ExecuteCopyButton.Enabled = true;
            this.DeleteCopyButton.Enabled = true;
        }

        private void JobDirTextBox_TextChanged(object sender, EventArgs e)
        {
            string sendertext = sender.ToString();
            string thisString = this.JobDirTextBox.Text;

            bool bDirExists = false;
            try
            {
                if (Directory.Exists(this.JobDirTextBox.Text))
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
                //_jobDir = this.JobDirTextBox.Text;
                //_jobRoot = new DirectoryOrFile();
                //LoadDirTree(ref _jobRoot, _jobDir, null, DirectoryOrFile.DOF_Type.DIR);
                //EnableButtons();

                //JobTreeView.Nodes.Clear();
                //IngressTreeNode dummyRoot = new IngressTreeNode("dummy", null);
                //TraverseDirTree(JobTreeView, _jobRoot, 0, ref dummyRoot);

                //JobTreeView.ExpandAll();
                UpdateTreeView();
            }
            else
                DisableButtons();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fc = new FolderBrowserDialog();

            // Set the help text description for the FolderBrowserDialog.
            fc.Description = "Select the Job directory.";

            // Do not allow the user to create new files via the FolderBrowserDialog.
            fc.ShowNewFolderButton = true;

            // Default to the My Documents folder.
            //fc.RootFolder = Environment.SpecialFolder.Personal;


            // Show the FolderBrowserDialog.
            DialogResult result = fc.ShowDialog();
            if (result == DialogResult.OK)
            {
                JobDirTextBox.Text = fc.SelectedPath;
            }
        }

        protected void UpdateTreeView()
        {
            _jobDir = this.JobDirTextBox.Text;
            _jobRoot = new DirectoryOrFile();
            LoadDirTree(ref _jobRoot, _jobDir, null, DirectoryOrFile.DOF_Type.DIR);
            EnableButtons();

            JobTreeView.Nodes.Clear();
            IngressTreeNode dummyRoot = new IngressTreeNode("dummy", null);
            TraverseDirTree(JobTreeView, _jobRoot, 0, ref dummyRoot);

            JobTreeView.ExpandAll();
        }

        private void JobTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            IngressTreeNode ie = (IngressTreeNode)(e.Node);
            try
            {
                // Look for a file extension.
                if (ie.DorF.Path().Contains("."))
                    System.Diagnostics.Process.Start(ie.DorF.Path());
            }
            // If the file is not found, handle the exception and inform the user.
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("\"" + ie.DorF.Path() + "\" File not found.");
            }
        }

        public static DialogResult ShowInputDialog(string title, ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(300, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = title;

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private static DialogResult ShowNumberDialog(string title, ref int input)
        {
            System.Drawing.Size size = new System.Drawing.Size(300, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = title;

            System.Windows.Forms.NumericUpDown numBox = new NumericUpDown();
            numBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            numBox.Location = new System.Drawing.Point(5, 5);
            numBox.Value = input;
            numBox.Select(0, numBox.Text.Length);
            inputBox.Controls.Add(numBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = (int)(numBox.Value);
            return result;
        }

        //private static DialogResult ShowListDialog(string title, ref int selection, ListBox lb)
        //{
        //    System.Drawing.Size size = new System.Drawing.Size(300, 200);
        //    Form inputBox = new Form();
        //    inputBox.Name = "ListDialog";

        //    inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        //    inputBox.ClientSize = size;
        //    inputBox.Text = title;

        //    System.Windows.Forms.ListBox listBox = new ListBox();
        //    //textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
        //    listBox.Location = new System.Drawing.Point(5, 5);
        //    //textBox.Text = input;
        //    for (int i = 0; i < lb.Items.Count; i++)
        //        listBox.Items.Add(lb.Items[i]);
        //    if (listBox.Items.Count > 0)
        //        listBox.SelectedIndex = 0;

        //    inputBox.Controls.Add(listBox);

        //    Button addSceneButton = new Button();
        //    //addSceneButton.DialogResult = System.Windows.Forms.DialogResult.OK;
        //    addSceneButton.Name = "addSceneButton";
        //    addSceneButton.Size = new System.Drawing.Size(75, 23);
        //    addSceneButton.Text = "&+";
        //    addSceneButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 79);
        //    addSceneButton.Click += new System.EventHandler(staticAddSceneButton_Click);
        //    inputBox.Controls.Add(addSceneButton);

        //    Button okButton = new Button();
        //    okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
        //    okButton.Name = "okButton";
        //    okButton.Size = new System.Drawing.Size(75, 23);
        //    okButton.Text = "&OK";
        //    okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
        //    inputBox.Controls.Add(okButton);

        //    Button cancelButton = new Button();
        //    cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        //    cancelButton.Name = "cancelButton";
        //    cancelButton.Size = new System.Drawing.Size(75, 23);
        //    cancelButton.Text = "&Cancel";
        //    cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
        //    inputBox.Controls.Add(cancelButton);

        //    inputBox.AcceptButton = okButton;
        //    inputBox.CancelButton = cancelButton;

        //    DialogResult result = inputBox.ShowDialog();
        //    selection = listBox.SelectedIndex;
        //    return result;
        //}

        public void AddScene(string sceneName)
        {
            if (!_scenes.Contains(sceneName))
            {
                _scenes.Add(sceneName);
                this.SceneListBox.Items.Add(sceneName);
            }
        }

        private void AddSceneButton_Click(object sender, EventArgs e)
        {
            string input = "";
            if (ShowInputDialog("Enter Scene Name", ref input) == DialogResult.OK)
            {
                AddScene(input);
                //if (!_scenes.Contains(input))
                //{
                //    _scenes.Add(input);
                //    this.SceneListBox.Items.Add(input);
                //}
            }
        }

        private void SourceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = SourceListBox.SelectedIndex;
            DestListBox.SelectedIndex = sel;
        }

        private void DestListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = DestListBox.SelectedIndex;
            SourceListBox.SelectedIndex = sel;
        }

        private void GetCameraButton_Click(object sender, EventArgs e)
        {
            int camNum = _currentCamera+1;
            if (ShowNumberDialog("Enter Camera Number", ref camNum) == DialogResult.OK)
            { 
                _currentCamera = camNum;
                CameraLabel.Text = "Camera " + camNum.ToString();

                FolderBrowserDialog fc = new FolderBrowserDialog();

                // Set the help text description for the FolderBrowserDialog.
                fc.Description = "Select Camera directory.";

                // Do not allow the user to create new files via the FolderBrowserDialog.
                fc.ShowNewFolderButton = false ;

                // Show the FolderBrowserDialog.
                DialogResult result = fc.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _camRoot = new DirectoryOrFile();
                    LoadDirTree(ref _camRoot, fc.SelectedPath, null, DirectoryOrFile.DOF_Type.DIR);

                    CameraTreeView.Nodes.Clear();
                    IngressTreeNode dummyRoot = new IngressTreeNode("dummy", null);
                    TraverseDirTree(CameraTreeView, _camRoot, 0, ref dummyRoot);

                    CameraTreeView.ExpandAll();

                    _sources = new List<DirectoryOrFile>();
                    SourceListBox.Items.Clear();
                    _destinationScenes = new List<string>();
                    DestListBox.Items.Clear();

                    EnableAddCopyButton();
                }
            }
        }

        private void CameraTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            IngressTreeNode ie = (IngressTreeNode)(e.Node);
            try
            {
                // Look for a file extension.
                if (ie.DorF.Path().Contains("."))
                    System.Diagnostics.Process.Start(ie.DorF.Path());
            }
            // If the file is not found, handle the exception and inform the user.
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("\"" + ie.DorF.Path() + "\" File not found.");
            }

        }

        private void ClearCopyList()
        {
            _sources.Clear();
            SourceListBox.Items.Clear();
            _destinationScenes.Clear();
            DestListBox.Items.Clear();
            DisableExecuteCopyButton();
        }

        private void AddCopyButton_Click(object sender, EventArgs e)
        {
            if (_selectedCamNode == null)
                Console.WriteLine("No Selected Node");
            else
                Console.WriteLine("Copy using \"" + _selectedCamNode.DorF.Name() +"\"");
            if (!_sources.Contains(_selectedCamNode.DorF))
            {
                _sources.Add(_selectedCamNode.DorF);
                SourceListBox.Items.Add(_selectedCamNode.DorF.Name());

                int sel = -1;
                IngressListDialog dlg = new IngressListDialog(SceneListBox);
                if (dlg.ShowListDialog("Select Scene", ref sel) == DialogResult.OK)
                {
                    DestListBox.Items.Add(SceneListBox.Items[sel]);
                    _destinationScenes.Add(_scenes[sel]);

                    EnableExecuteCopyButton();
                }

            }
        }

        private void CameraTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _selectedCamNode = (IngressTreeNode)(e.Node);

        }

        private void SourceListBox_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Key pressed " + (char)e.KeyData);
            if (e.KeyData == Keys.Delete)
            {
                Console.WriteLine("would delete " + SourceListBox.SelectedIndex);
                int sel = SourceListBox.SelectedIndex;
                SourceListBox.Items.RemoveAt(sel);
                _sources.RemoveAt(sel);
                DestListBox.Items.RemoveAt(sel);
                _destinationScenes.RemoveAt(sel);

                if (_sources.Count == 0)
                    DisableExecuteCopyButton();
            }
        }

        private void ExecuteCopyButton_Click(object sender, EventArgs e)
        { 
            string cam_name = string.Format("cam_{0:00}", _currentCamera);
            for (int i = 0; i < _sources.Count; i++)
            {
                if (_destinationScenes[i].Length > 0)
                {
                    string sceneDir = _jobDir + "\\" + _destinationScenes[i] + "\\" ;
                    string cpCmd = string.Format("/c xcopy \"{0}\" \"{1}\" /i", _sources[i].Path(), sceneDir);
                    string destName = sceneDir + Path.GetFileName(_sources[i].Path());
                    string mvCmd = string.Format("/c rename \"{0}\" \"{1}{2}\"", destName, cam_name, Path.GetExtension(_sources[i].Path()));
                    try
                    {
                        System.Diagnostics.Process cpProcess = new System.Diagnostics.Process();
                        cpProcess.StartInfo.UseShellExecute = false;
                        cpProcess.StartInfo.Arguments = cpCmd ;
                        cpProcess.StartInfo.FileName = "cmd.exe";
                        cpProcess.Start();
                        cpProcess.WaitForExit();
                        cpProcess.Dispose();
                        cpProcess.Close();

                        System.Diagnostics.Process mvProcess = new System.Diagnostics.Process();
                        mvProcess.StartInfo.UseShellExecute = false;
                        mvProcess.StartInfo.Arguments = mvCmd;
                        mvProcess.StartInfo.FileName = "cmd.exe";
                        mvProcess.Start();
                        mvProcess.WaitForExit();
                        mvProcess.Dispose();
                        mvProcess.Close();
                    }
                    // If the file is not found, handle the exception and inform the user.
                    catch (System.ComponentModel.Win32Exception err)
                    {
                        Console.WriteLine(cpCmd);
                        MessageBox.Show("\"" + cpCmd + "\" " + err.Message);
                    }
                }
            }

            UpdateTreeView();
            ClearCopyList();
        }
    }

    public class DirectoryOrFile
    {
        private string _name;
        private string _path;
        private DirectoryOrFile _parent;
        private List<DirectoryOrFile> _sub = null;
        private DOF_Type _dof_type;
        private int _size;
        private DateTime _modified;

        public enum DOF_Type { UNDEF, DIR, FILE };

        public DirectoryOrFile(string name, string path, DirectoryOrFile parent, DOF_Type nodeType)
        {
            _name = name;
            _path = path;
            _parent = parent;
            _sub = new List<DirectoryOrFile>();
            _dof_type = nodeType;
        }
        public void Set(string name, string path, DirectoryOrFile parent, DOF_Type nodeType)
        {
            _name = name;
            _path = path;
            _parent = parent;
            _sub = new List<DirectoryOrFile>();
            _dof_type = nodeType;
        }
        public DirectoryOrFile()
        {
            _name = "";
            _path = "";
            _parent = null;
            _sub = new List<DirectoryOrFile>();
            _dof_type = DOF_Type.UNDEF;
        }

        public string Name() { return _name; }
        public string Path() { return _path; }
        public DirectoryOrFile Parent() { return _parent; }
        public int NumChildren() { return _sub.Count; }
        public DirectoryOrFile Child(int index) { return ((index < 0) || (index >= _sub.Count)) ? null : _sub[index]; }
        public void AddChild(DirectoryOrFile sub) { _sub.Add(sub); }
        public DOF_Type Type() { return _dof_type; }
    };

    public class IngressListDialog
    {
        private Form inputBox ;
        System.Windows.Forms.ListBox listBox;
        Button addSceneButton;
        Button okButton;
        Button cancelButton;
        ListBox parentListBox;

        private static IngressListDialog _instance = null;

        public static IngressListDialog GetInstance()
        {
            return _instance;
        }

        public void updateListBox()
        {
            listBox.Items.Clear();
            listBox.Location = new System.Drawing.Point(5, 5);
            for (int i = 0; i < parentListBox.Items.Count; i++)
                listBox.Items.Add(parentListBox.Items[i]);
            if (listBox.Items.Count > 0)
                listBox.SelectedIndex = 0;
        }

        public IngressListDialog(ListBox lb)
        {
            _instance = this;

            System.Drawing.Size size = new System.Drawing.Size(300, 200);
            //Form inputBox = new Form();
            inputBox = new Form();
            inputBox.Name = "ListDialog";

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;

            parentListBox = lb;
            listBox = new ListBox();
            //textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            listBox.Location = new System.Drawing.Point(5, 5);
            //textBox.Text = input;
            for (int i = 0; i < lb.Items.Count; i++)
                listBox.Items.Add(lb.Items[i]);
            if (listBox.Items.Count > 0)
                listBox.SelectedIndex = 0;

            inputBox.Controls.Add(listBox);

            addSceneButton = new Button();
            //addSceneButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            addSceneButton.Name = "addSceneButton";
            addSceneButton.Size = new System.Drawing.Size(75, 23);
            addSceneButton.Text = "&+";
            addSceneButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 79);
            addSceneButton.Click += new System.EventHandler(staticAddSceneButton_Click);
            inputBox.Controls.Add(addSceneButton);

            okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

        }

        public DialogResult ShowListDialog(string title, ref int selection)
        {
            inputBox.Text = title;
            DialogResult result = inputBox.ShowDialog();
            selection = listBox.SelectedIndex;
            return result;
        }

        private static void staticAddSceneButton_Click(object sender, EventArgs e)
        {
            string input = "";
            if (IngressForm.ShowInputDialog("Enter Scene Name", ref input) == DialogResult.OK)
            {
                IngressForm.GetInstance().AddScene(input);
                IngressListDialog.GetInstance().updateListBox();
                //if (!_scenes.Contains(input))
                //{
                //    _scenes.Add(input);
                //    sender.SceneListBox.Items.Add(input);
                //}
            }
        }


    }


    public class IngressTreeNode : TreeNode
    {
        public DirectoryOrFile DorF;

        public IngressTreeNode(string text, DirectoryOrFile df)
        {
            DorF = df;
            this.Text = text;
        }
    }

}
