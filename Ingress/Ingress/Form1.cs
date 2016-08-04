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

        private readonly string[] _knownMovieTypes = new string[] { ".mp4", ".mov", ".avi" };

        private DirectoryOrFile _camRoot;
        IngressTreeNode _selectedCamNode = null;
        private List<DirectoryOrFile> _sources;
        private List<string> _destinationScenes;
        private int _currentCamera = 0;
        private List<int> _sourceCams;

        private int _previousCamera = 0;
        private DirectoryOrFile _previousCamRoot;
        private List<DirectoryOrFile> _previousSources;
        private List<string> _previousDestinationScenes;
        private List<int> _previousSourceCams;

        private static IngressForm _instance = null;

        public enum tSortType { NOT_SELECTED, SORT_BY_NAME, SORT_BY_TYPE, SORT_BY_DATE };
        private tSortType _cameraFileSortType = tSortType.SORT_BY_NAME;

        private enum tGuessType { NOT_SELECTED, GUESS_BY_DATE, GUESS_BY_ORDER };
        private tGuessType _guessType = tGuessType.GUESS_BY_DATE;

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
            SourceListBox.DisplayMember = "FormattedDisplayName";
            SourceListBox.ValueMember = "Identifier";

        }

        //protected void TraverseDirTree(DirectoryOrFile node, int level, ref TreeStore store, TreeIter iter)
        protected void TraverseDirTree(TreeView tv, DirectoryOrFile node, int level, ref IngressTreeNode tree_node) //ref TreeStore store, TreeIter iter)
        {
            IngressTreeNode thisNodeIter = new IngressTreeNode(node.Name(), node);

            if (level == 0)
                tv.Nodes.Add(thisNodeIter); //           store.AppendValues(node.Name());
            else
                tree_node.Nodes.Add(thisNodeIter); // store.AppendValues(iter, node.Name());

            //for (int i = 0; i < level; i++)
            //    Console.Write("   ");
            //Console.WriteLine("{0} = {1} ({2})", node.Name(), node.Path(), node.Type() == DirectoryOrFile.DOF_Type.DIR ? "D" : (node.Type() == DirectoryOrFile.DOF_Type.FILE ? "F" : "U"));

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
                        //Console.WriteLine("{0}", dir.Substring(dir.LastIndexOf("\\") + 1));

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
                        // Console.WriteLine("{0}", fname.Substring(fname.LastIndexOf("\\") + 1));
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
            this.GuessAllButton.Enabled = false;
        }

        protected void EnableExecuteCopyButton()
        {
            this.ExecuteCopyButton.Enabled = true;
            this.DeleteCopyButton.Enabled = true;
            this.GuessAllButton.Enabled = true;
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

        public void AddScene(string sceneName)
        {
            if (!_scenes.Contains(sceneName))
            {
                _scenes.Add(sceneName);
                this.SceneListBox.Items.Add(sceneName);
                this.SceneListBox.SelectedIndex = this.SceneListBox.Items.Count - 1;
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
        }

        private void TraverseLoadMovieList (ref DirectoryOrFile _df, ref List<DirectoryOrFile> _list)
        {
            if (_df.Type() == DirectoryOrFile.DOF_Type.FILE)
            {
                // is this a movie?
                int i = 0;
                for (i = 0; i < _knownMovieTypes.Length; i++)
                    if (Path.GetExtension(_df.Path()).ToLower().Contains(_knownMovieTypes[i]))
                        break;
                if (i < _knownMovieTypes.Length)
                {
                    // this is a movie
                    _list.Add(_df);
                }
            }
            else
            {
                for (int i = 0; i < _df.NumChildren(); i++)
                {
                    DirectoryOrFile childDf = _df.Child(i);
                    TraverseLoadMovieList(ref childDf, ref _list);
                }
            }
        }

        private void MakeCopyGuessBySortOrder(int camNum)
        {
            if (_previousCamRoot == null)
                return;

            if (_previousSources == null)
                return;

            // start by aligning movie from cam directory with movies in copy list
            List<DirectoryOrFile> moviesOnPrevCam = new List<DirectoryOrFile>();
            _previousCamRoot.SortBy(_cameraFileSortType);
            TraverseLoadMovieList(ref _previousCamRoot, ref moviesOnPrevCam);

            for (int i = 0; i < moviesOnPrevCam.Count; i++)
            {
                Console.WriteLine("Guess: " + i + " \"" + moviesOnPrevCam[i].Name() + "\"");
            }

            List<DirectoryOrFile> moviesOnThisCam = new List<DirectoryOrFile>();
            _camRoot.SortBy(_cameraFileSortType);
            TraverseLoadMovieList(ref _camRoot, ref moviesOnThisCam);

            for (int i = 0; i < moviesOnThisCam.Count; i++)
            {
                Console.WriteLine("New: " + i + " \"" + moviesOnThisCam[i].Name() + "\"");
            }

            // _previoiusSources is list of movies selected for copy
            // moviesOnPrevCam is sorted list of movies from prev camera directory
            // moviesOnThisCam is sorted list of movies on camera directory being querried
            // - Find selected movie in moviesOnPrevCam and select corresponding movie from moviesOnThisCam
            for (int i = 0; i < _previousSources.Count; i++)
            {
                int j = 0;
                for (j = 0; j < moviesOnPrevCam.Count; j++)
                {
                    if (moviesOnPrevCam[j].Path() == _previousSources[i].Path())
                    {
                        // look for j'th movie on new movie list
                        if (j < moviesOnThisCam.Count)
                        {
                            // found it
                            _sources.Add(moviesOnThisCam[j]);
                            IngressListBoxItem ib = new IngressListBoxItem();
                            ib.DisplayName = moviesOnThisCam[j].Name() ;
                            ib.Identifier = moviesOnThisCam[j];
                            ib.CamNum = camNum;
                            ib.SceneName = _previousDestinationScenes[i] ;
                            SourceListBox.Items.Add(ib);
                            _destinationScenes.Add(_previousDestinationScenes[i]);
                            //_sourceCams.Add(_previousSourceCams[i]);
                            //CamNumListBox.Items.Add(_previousSourceCams[i]);
                            _sourceCams.Add(camNum);
                            break;
                        }
                    }
                }
            }
        }

        private void MakeCopyGuessByDate(int camNum, int numMovieFilesToGuess)
        {
            if (_previousCamRoot == null)
                return;

            if (_previousSources == null)
                return;

            // start by aligning movie from cam directory with movies in copy list by Date

            tSortType tempSortType = _cameraFileSortType ;
            _cameraFileSortType = tSortType.SORT_BY_DATE;

            List<DirectoryOrFile> moviesOnPrevCam = new List<DirectoryOrFile>();
            _previousCamRoot.SortBy(_cameraFileSortType);
            TraverseLoadMovieList(ref _previousCamRoot, ref moviesOnPrevCam);

            for (int i = 0; i < moviesOnPrevCam.Count; i++)
            {
                Console.WriteLine("Guess: " + i + " \"" + moviesOnPrevCam[i].Name() + "\" " + File.GetLastWriteTime(moviesOnPrevCam[i].Path()).ToString());
            }

            List<DirectoryOrFile> moviesOnThisCam = new List<DirectoryOrFile>();
            _camRoot.SortBy(_cameraFileSortType);
            TraverseLoadMovieList(ref _camRoot, ref moviesOnThisCam);

            _cameraFileSortType = tempSortType;

            for (int i = 0; i < moviesOnThisCam.Count; i++)
            {
                Console.WriteLine("New: " + i + " \"" + moviesOnThisCam[i].Name() + "\" " + File.GetLastWriteTime(moviesOnThisCam[i].Path()).ToString());
            }

            // _previoiusSources is list of movies selected for copy
            // moviesOnThisCam is sorted list of movies on camera directory being querried
            // - Find selected movie timestamp and select closest movie from moviesOnThisCam
            for (int i = 0; i < numMovieFilesToGuess; i++)
            {
                int closestDateMatch = -1;
                DateTime sourceDT = File.GetLastWriteTime(_previousSources[i].Path());
                long minDelta = 0;

                int j = 0;
                for (j = 0; j < moviesOnThisCam.Count; j++)
                {
                    DateTime thisDT = File.GetLastWriteTime(moviesOnThisCam[j].Path());
                    long delta = Math.Abs(thisDT.Ticks - sourceDT.Ticks);

                    if (j == 0)
                    {
                        closestDateMatch = j;
                        minDelta = delta;
                    }
                    else if (delta < minDelta)
                    {
                        closestDateMatch = j;
                        minDelta = delta;
                    }
                }

                j = closestDateMatch;
                // found it
                _sources.Add(moviesOnThisCam[j]);
                IngressListBoxItem ib = new IngressListBoxItem();
                ib.DisplayName = moviesOnThisCam[j].Name();
                ib.Identifier = moviesOnThisCam[j];
                ib.CamNum = camNum;
                ib.SceneName = _previousDestinationScenes[i];
                SourceListBox.Items.Add(ib);
                _destinationScenes.Add(_previousDestinationScenes[i]);
                _sourceCams.Add(camNum);
            }
        }

        private void LoadCameraPath (string path, bool ClearLists, int camNum, bool doGuess, int numMovieFilesToGuess)
        {
            _previousCamRoot = _camRoot;
            _previousCamera = camNum;

            _camRoot = new DirectoryOrFile();
            LoadDirTree(ref _camRoot, path, null, DirectoryOrFile.DOF_Type.DIR);

            CameraTreeView.Nodes.Clear();
            IngressTreeNode dummyRoot = new IngressTreeNode("dummy", null);
            TraverseDirTree(CameraTreeView, _camRoot, 0, ref dummyRoot);

            CameraTreeView.ExpandAll();

            if (_sources != null)
            {
                //                        _previousSources = new List<DirectoryOrFile>();
                _previousSources = _sources;
                //                        _previousDestinationScenes = new List<string>();
                _previousDestinationScenes = _destinationScenes;
                _previousCamera = camNum;
                _previousSourceCams = _sourceCams;
            }

            if(ClearLists)
            {
                _sources = new List<DirectoryOrFile>();
                SourceListBox.Items.Clear();
                _destinationScenes = new List<string>();
                _sourceCams = new List<int>();
            }

            if(doGuess)
            {
                if(_guessType == tGuessType.GUESS_BY_ORDER)
                    MakeCopyGuessBySortOrder(camNum);
                else if (_guessType == tGuessType.GUESS_BY_DATE)
                    MakeCopyGuessByDate(camNum, numMovieFilesToGuess);
            }

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
                    LoadCameraPath(fc.SelectedPath, true, _currentCamera, false, 0);
                    ResortCameraFileList();
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
            _previousSources = _sources;
            _sources.Clear();
            SourceListBox.Items.Clear();

            _previousDestinationScenes = _destinationScenes;
            _destinationScenes.Clear();

            _previousSourceCams = _sourceCams;
            _sourceCams.Clear();

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
                IngressListBoxItem ib = new IngressListBoxItem();
                ib.DisplayName = _selectedCamNode.DorF.Name();
                ib.Identifier = _selectedCamNode.DorF;

                ////SourceListBox.Items.Add(_selectedCamNode.DorF.Name());
                //SourceListBox.Items.Add(ib);
                //_sourceCams.Add(_currentCamera);
                //CamNumListBox.Items.Add(_currentCamera);

                int sel = -1;
                IngressListDialog dlg = new IngressListDialog(SceneListBox);
                if (dlg.ShowListDialog("Select Scene", ref sel) == DialogResult.OK)
                {
                    _destinationScenes.Add(_scenes[sel]);
                    //DestListBox.SelectedIndex = SceneListBox.Items.Count - 1;

                    ib.CamNum = _currentCamera;
                    ib.SceneName = _scenes[sel];

                    SourceListBox.Items.Add(ib);
                    _sourceCams.Add(_currentCamera);

                    EnableExecuteCopyButton();
                }

            }
        }

        private void CameraTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _selectedCamNode = (IngressTreeNode)(e.Node);
            EnableAddCopyButton();
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
                _destinationScenes.RemoveAt(sel);
                _sourceCams.RemoveAt(sel);

                if (_sources.Count == 0)
                    DisableExecuteCopyButton();
            }
        }

        private void DoCopyCamFiles ()
        {
            for (int i = 0; i < _sources.Count; i++)
            {
                if (_destinationScenes[i].Length > 0)
                {
                    string cam_name = string.Format("cam_{0:00}", _sourceCams[i]);
                    string jobName = Path.GetFileName(_jobDir);
                    string sceneName = _destinationScenes[i];
                    string sceneDir = _jobDir + "\\" + _destinationScenes[i] + "\\";
                    string cpCmd = string.Format("/c xcopy \"{0}\" \"{1}\" /i", _sources[i].Path(), sceneDir);
                    string destName = sceneDir + Path.GetFileName(_sources[i].Path());
                    string mvCmd = string.Format("/c rename \"{0}\" \"{1}_{2}_{3}{4}\"", destName, jobName, sceneName, cam_name, Path.GetExtension(_sources[i].Path()));
                    try
                    {
                        System.Diagnostics.Process cpProcess = new System.Diagnostics.Process();
                        cpProcess.StartInfo.UseShellExecute = false;
                        cpProcess.StartInfo.Arguments = cpCmd;
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
        }

        private void ExecuteCopyButton_Click(object sender, EventArgs e)
        {
            DoCopyCamFiles();

            UpdateTreeView();
            ClearCopyList();
        }

        private void GuessAllButton_Click(object sender, EventArgs e)
        {
            // build list of all directories with name that follows first
            DirectoryInfo dirInfo = Directory.GetParent(_camRoot.Path());
            string parentDir = dirInfo.FullName;
            
            string dirName = Path.GetFileName(_camRoot.Path());

            Console.WriteLine("_camRoot \"" + _camRoot.Path() + "\" parentDir \"" + parentDir + "\" local dir \"" + dirName + "\"");

            int numMovieFilesToGuess = _sources.Count;

            int i = 0;
            for (i = dirName.Length -1; i > 0; i--)
            {
                if ((dirName[i] < '0') || (dirName[i] > '9'))
                    break;
            }
            if (i > 0)
            {
                string baseName = dirName.Substring(0, i+1);
                int numZeros = dirName.Length - baseName.Length;

                string format = string.Format("{0}\\{1}{{0:", parentDir, baseName);
                for (int j = 0; j < numZeros; j++)
                    format = format + "0";
                format = format + "}";

                string camPath;
                int camCount = 0;
                do
                {
                    camCount++;
                    camPath = string.Format(format, camCount);
                    Console.WriteLine("Looking for \"" + camPath + "\"");
                } while (Directory.Exists(camPath));
                camCount--;

                Console.WriteLine("Num of cam dirs = " + camCount);

                int startingCam = _currentCamera + 1;
                for (int camIndex = startingCam; camIndex < (camCount+1); camIndex++)
                {
                    _currentCamera = camIndex;
                    // 
                    camPath = string.Format(format, camIndex);
                    LoadCameraPath(camPath, false, camIndex, true, numMovieFilesToGuess); // true = do guess!
                }
            }

        }

        private void SourceListBox_DoubleClick(object sender, EventArgs e)
        {
            IngressListBoxItem ie = (IngressListBoxItem)(SourceListBox.Items[SourceListBox.SelectedIndex]);
            try
            {
                // Look for a file extension.
                if (ie.Identifier.Path().Contains("."))
                    System.Diagnostics.Process.Start(ie.Identifier.Path());
            }
            // If the file is not found, handle the exception and inform the user.
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("\"" + ie.Identifier.Path() + "\" File not found.");
            }
        }

        private void ResortCameraFileList()
        {
            _camRoot.SortBy(_cameraFileSortType);
            CameraTreeView.Nodes.Clear();
            IngressTreeNode dummyRoot = new IngressTreeNode("dummy", null);
            TraverseDirTree(CameraTreeView, _camRoot, 0, ref dummyRoot);
            CameraTreeView.ExpandAll();

        }

        private void CameraFileSort_radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _cameraFileSortType = tSortType.SORT_BY_NAME;
            ResortCameraFileList();
        }

        private void CameraFileSort_radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _cameraFileSortType = tSortType.SORT_BY_TYPE;
            ResortCameraFileList();
        }

        private void CameraFileSort_radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            _cameraFileSortType = tSortType.SORT_BY_DATE;
            ResortCameraFileList();
        }

        private void GuessType_radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _guessType = tGuessType.GUESS_BY_DATE;
        }

        private void GuessType_radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _guessType = tGuessType.GUESS_BY_ORDER;
        }
    }

    public class DirectoryOrFile:IComparable<DirectoryOrFile>
    {
        private string _name;
        private string _path;
        private DirectoryOrFile _parent;
        private List<DirectoryOrFile> _sub = null;
        private DOF_Type _dof_type;
        private int _size;
        private DateTime _modified;
        static IngressForm.tSortType _sortType = IngressForm.tSortType.SORT_BY_NAME ;

        public enum DOF_Type { UNDEF, DIR, FILE };

        public DirectoryOrFile(string name, string path, DirectoryOrFile parent, DOF_Type nodeType)
        {
            _name = name;
            _path = path;
            _parent = parent;
            _sub = new List<DirectoryOrFile>();
            _dof_type = nodeType;
        }

        public void SortBy (IngressForm.tSortType sortType)
        {
            _sortType = sortType;
            _sub.Sort();
        }

        public int CompareTo(DirectoryOrFile other)
        {
            int ret = 0;
            switch (_sortType)
            {
                case IngressForm.tSortType.SORT_BY_NAME:
                    return _path.CompareTo(other.Path()) ;
                case IngressForm.tSortType.SORT_BY_TYPE:
                    return (ret = System.IO.Path.GetExtension(_path).CompareTo(System.IO.Path.GetExtension(other.Path()))) == 0 ? _path.CompareTo(other.Path()) : ret;
                case IngressForm.tSortType.SORT_BY_DATE:
                    return (ret = File.GetLastWriteTime(_path).CompareTo(File.GetLastWriteTime(other.Path()))) == 0 ? _path.CompareTo(other.Path()) : ret;
            }
            return ret ;
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
                IngressListDialog.GetInstance().listBox.SelectedIndex = IngressListDialog.GetInstance().listBox.Items.Count - 1;

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

    class IngressListBoxItem : System.Object
    {
        public string DisplayName { get; set; }
        public DirectoryOrFile Identifier { get; set; }
        public int CamNum { get; set; }
        public string SceneName { get; set; }
        public string FormattedDisplayName
        {
            get { return String.Format("{0} {1} {2}", DisplayName.PadRight(15, ' '), CamNum.ToString().PadLeft(5, ' '), SceneName);         }
            set { string foo = value; }
        }

    }


}
