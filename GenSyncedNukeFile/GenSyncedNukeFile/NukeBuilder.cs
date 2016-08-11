using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;


namespace GenSyncedNukeFile
{

    public class CameraInput : IComparable<CameraInput>
    {
        private int _start;
        private int _duration;
        private string _path;

        public CameraInput(int start, int duration, string path)
        {
            _start = start;
            _duration = duration;
            _path = path;
        }

        public int CompareTo(CameraInput other)
        {
            return _path.CompareTo(other.Path());
        }

        public void Set(int start, int duration, string path)
        {
            _start = start;
            _duration = duration;
            _path = path;
        }
        public CameraInput()
        {
            _start = 0;
            _duration = 0;
            _path = "";
        }

        public int Start() { return _start; }
        public int Duration() { return _duration; }
        public string Path() { return _path; }
    };

    public class NukeBuilder
    {
        //List<int> _starts;
        //List<int> _durations;
        //List<string> _paths;
        List<CameraInput> _cameras;
        public NukeBuilder()
        {
            _cameras = new List<CameraInput>();
            //_starts = new List<int>();
            //_durations = new List<int>();
            //_paths = new List<string>();
        }

        // 0 - path #write_info Write1 file:\"Z:/CreativeStudioJobs/VR_Flex360/04_EDIT/media/footage/080316_JPEGS/Shot00/Shot00_quick_stitch.%04d.jpg\" format:\"1024 512 1\" chans:\":rgba.red:rgba.green:rgba.blue:\" framerange:\"1 1410\" fps:\"59.9401\" colorspace:\"rec709\" datatype:\"8 bit\" transfer:\"unknown\" views:\"main\" timecode:\"00:00:00:01\" colorManagement:\"Nuke\"\n" +
        // 1 - duration
        string NUKEFILE_Header =
"#! C:/Program Files/Nuke10.0v3/nuke-10.0.3.dll -nx\n" +
"#write_info Write1 file:\"{0}\" format:\"1024 512 1\" chans:\":rgba.red:rgba.green:rgba.blue:\" framerange:\"1 {1}\" fps:\"59.9401\" colorspace:\"rec709\" datatype:\"8 bit\" transfer:\"unknown\" views:\"main\" timecode:\"00:00:00:01\" colorManagement:\"Nuke\"\n";
        string NUKEFILE_Header2 =
"version 10.0 v3\n" +
"define_window_layout_xml {<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
"<layout version=\"1.0\">\n" +
"    <window x=\"-1920\" y=\"0\" w=\"1920\" h=\"1080\" screen=\"0\">\n" +
"        <splitter orientation=\"1\">\n" +
"            <split size=\"40\"/>\n" +
"            <dock id=\"\" hideTitles=\"1\" activePageId=\"Toolbar.1\">\n" +
"                <page id=\"Toolbar.1\"/>\n" +
"            </dock>\n" +
"            <split size=\"1241\" stretch=\"1\"/>\n" +
"            <splitter orientation=\"2\">\n" +
"                <split size=\"595\"/>\n" +
"                <dock id=\"\" activePageId=\"Viewer.1\">\n" +
"                    <page id=\"Viewer.1\"/>\n" +
"                </dock>\n" +
"                <split size=\"419\"/>\n" +
"                <dock id=\"\" activePageId=\"DAG.1\" focus=\"true\">\n" +
"                    <page id=\"DAG.1\"/>\n" +
"                    <page id=\"Curve Editor.1\"/>\n" +
"                    <page id=\"DopeSheet.1\"/>\n" +
"                </dock>\n" +
"            </splitter>\n" +
"            <split size=\"615\"/>\n" +
"            <dock id=\"\" activePageId=\"Properties.1\">\n" +
"                <page id=\"Properties.1\"/>\n" +
"            </dock>\n" +
"        </splitter>\n" +
"    </window>\n" +
"</layout>\n" +
"}\n";

        // 0 - name Z:/CreativeStudioJobs/VR_Flex360/08_TRANSFER/EXTERNAL/SHOOT/Flexi_20160728/SortedCameras/Shot00/Shot00.nk
        // 1 - duration
        string NUKEFILE_Root =
"Root {{\n" +
" inputs 0\n" +
" name {0}\n" +
" frame 1\n" +
" last_frame {1}\n" +
" lock_range true\n" +
" format \"4096 2048 0 0 4096 2048 1 4K_LatLong\"\n" +
" proxy_type scale\n" +
" proxy_format \"1024 778 0 0 1024 778 1 1K_Super_35(full-ap)\"\n" +
" colorManagement Nuke\n" +
" views {{\n" +
"  {{main #ffffff}}\n";

        // 0 - " file Z:/CreativeStudioJobs/VR_Flex360/08_TRANSFER/EXTERNAL/SHOOT/Flexi_20160728/SortedCameras/Shot00/Shot00_cam_05.MP4\n" +
        // 1 - duraton
        // 2 - offset
        // 3 - duration
        // 4 - name Readn
        // 5 - node xpos
        // 6 - node ypos
        string NUKEFILE_Read =
"Read {{\n" +
" inputs 0\n" +
" file {0}\n" +
" format \"1920 1080 0 0 1920 1080 1 HD_1080\"\n" +
" last {1}\n" +
" frame_mode \"start at\"\n" +
" frame {2}\n" +
" origlast {3}\n" +
" origset true\n" +
" colorspace SLog3\n" +
" mov32_codec {{{{0}}}}\n" +
" mov32_pixel_format {{{{0}}}}\n" +
" name {4}\n" +
" xpos {5}\n" +
" ypos {6}\n" +
"}}\n";

        // 0 - number of cameras
        // 1 - number of cameras
        string NUKEFILE_CamSolver =
"C_CameraSolver1_0 {{\n" +
" inputs {0}\n" +
" controlPoints \"\n" +
"version 2\n" +
"frames 0\n" +
"\"\n" +
" rigSensorSize {{23.5 15.60000038}}\n" +
" rigFocalLength 7.5\n" +
" analysisKeyframe {{{{curve x1 1}}}}\n" +
" isSolved true\n" +
" cameraRig \"\n" +
"version 6\n" +
"cameras {1}\n" +
"size 0.3\n" +
"keyframes 0\n";

        // 0 - cam num (starting at 1)
        // 1 - name ie: camera1
        string NUKEFILE_CamDefault =
"CameraDefault {0}\n" +
"  name {1}\n" +
"  enabled 1\n" +
"  view 0\n" +
"  lens 1\n" +
"  focal_length 7.5\n" +
"  field_of_view 179.527\n" +
"  sensor_size 23.5 15.6\n" +
"  center_shift 0 0\n" +
"  distortion 0 0 0\n" +
"  position 0 0 0\n" +
"  rotation -144 0 0\n" +
"  links 4 1 2 3 4\n" +
"  crop_shape 0\n" +
"  crop_size 1 1\n" +
"  crop_feather 0.4\n";

        // 0 - cam num (starting at 1)
        // 1 - name ie: camera1
        string NUKEFILE_Camera =
"Camera {0}\n" +
"  name {1}\n" +
"  enabled 1\n" +
"  view 0\n" +
"  lens 1\n" +
"  focal_length 7.5\n" +
"  field_of_view 179.527\n" +
"  sensor_size 23.5 15.6\n" +
"  center_shift 0 0\n" +
"  distortion 0 0 0\n" +
"  position 0 0 0\n" +
"  rotation -144 0 0\n" +
"  links 4 1 2 3 4\n" +
"  crop_shape 0\n" +
"  crop_size 1 1\n" +
"  crop_feather 0.4\n";

        // 0 - number of cameras
        // 1 - camera solver node xpos
        // 2 - camera solver node ypos
        // 3 - reformat node xpos
        // 4 - reformat node ypos
        // 5 - write file format: " file Z:/CreativeStudioJobs/VR_Flex360/04_EDIT/media/footage/080316_JPEGS/Shot00/Shot00_quick_stitch.####.jpg\n" +
        string NUKEFILE_Tail =
"\"\n" +
" numCameras {0}\n" +
" cameraRotationEvent \"\"\n" +
" rotate {{0 0 -90}}\n" +
" name C_CameraSolver1\n" +
" xpos {1}\n" +
" ypos {2}\n" +
"}}\n" +
"Reformat {{\n" +
" type scale\n" +
" format \"2048 1024 0 0 2048 1024 1 2K_LatLong\"\n" +
" scale 0.25\n" +
" name Reformat1\n" +
" xpos {3}\n" +
" ypos {4}\n" +
"}}\n" +
"Write {{\n" +
" file {5}\n";

        // 0 - write node xpos
        // 1 - write node ypos
        string NUKEFILE_Tail2 =
" colorspace rec709\n" +
" views {{main}}\n" +
" file_type jpeg\n" +
" checkHashOnRead false\n" +
" version 10\n" +
" name Write1\n" +
" xpos {0}\n" +
" ypos {1}\n";
        string NUKEFILE_Tail3 =
" addUserKnob {20 caravr l CaraVR}\n" +
" addUserKnob {4 viewPresets l \"View Presets\" M {main stereo cams all}}\n" +
" addUserKnob {22 set l Set -STARTLINE T \"w = nuke.thisNode()\\nscriptViews = nuke.views()\\nviewPreset = w\\['viewPresets'].getValue()\\nselectedViews = None\\nif viewPreset == 0:\\n  selectedViews = \\['main']\\nif viewPreset == 1:\\n  stereoViews = \\['left', 'right']\\n  if set(stereoViews).issubset(set(scriptViews)):\\n    selectedViews = stereoViews\\n  else:\\n    selectedViews = None\\nelif viewPreset == 2:\\n  selectedViews = \\[v for v in scriptViews if 'cam' in v.lower()]\\nelif viewPreset == 3:\\n  selectedViews = scriptViews\\nif selectedViews:\\n  w\\['views'].fromScript(' '.join(selectedViews))\\n\"}\n" +
"}\n";

        private void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private readonly string[] _knownMovieTypes = new string[] { ".mp4", ".mov", ".avi" };


        public enum tNUKEFILE_ERROR { NUKE_OK, NUKE_BAD_PLURALEYES, NUKE_BAD_SOURCE, NUKE_BAD_DEST } ;
        public tNUKEFILE_ERROR ModifyNukeFile(string pluralEyesXMLPath, string sourceNukeFilePath, string sourceBasename, string nukeFilePath, string destBaseName)
        {
            // read pluraleyes xml
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(pluralEyesXMLPath);
            }
            catch (Exception ex) when (ex is XmlException || ex is SystemException)
            {
                MessageBox.Show(string.Format("Could not find xml \"{0}\"", pluralEyesXMLPath), "Generate Nuke File ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return tNUKEFILE_ERROR.NUKE_BAD_PLURALEYES;
            }


            XmlNodeList trackNodes = doc.DocumentElement.SelectNodes("/xmeml/project/children/sequence/media/video/track");
            foreach (XmlNode trackNode in trackNodes)
            {
                XmlNode startNode = trackNode.SelectSingleNode("clipitem/start");
                int start = int.Parse(startNode.InnerText);
                XmlNode durationNode = trackNode.SelectSingleNode("clipitem/duration");
                int duration = int.Parse(durationNode.InnerText);
                XmlNode pathNode = trackNode.SelectSingleNode("clipitem/file/pathurl");
                Uri pathUri = new Uri(pathNode.InnerText);
                string path = pathUri.LocalPath.Substring(12); // skip over \\localhost\

                CameraInput camInp = new CameraInput(start, duration, path);
                _cameras.Add(camInp);
            }
            if (_cameras.Count < 1)
                return tNUKEFILE_ERROR.NUKE_BAD_PLURALEYES;

            _cameras.Sort();

            // find min duration
            int minDuration = _cameras[0].Duration() - (_cameras[0].Start() < 0 ? 0 : _cameras[0].Start());
            for (int i = 1; i < _cameras.Count; i++)
            {
                int duration = _cameras[i].Duration() - (_cameras[i].Start() < 0 ? 0 : _cameras[i].Start());
                minDuration = duration < minDuration ? duration : minDuration;
            }

            // read source file
            string source = "";
            try
            {

                using (FileStream fsSource = new FileStream(sourceNukeFilePath,
                    FileMode.Open, FileAccess.Read))
                {

                    // Read the source file into a byte array.
                    byte[] bytes = new byte[fsSource.Length];
                    int numBytesToRead = (int)fsSource.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        // Read may return anything from 0 to numBytesToRead.
                        int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                        // Break when the end of the file is reached.
                        if (n == 0)
                            break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                    numBytesToRead = bytes.Length;

                    source = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                }
            }
            catch (FileNotFoundException ioEx)
            {
                Console.WriteLine(ioEx.Message);
                return tNUKEFILE_ERROR.NUKE_BAD_SOURCE;
            }

            // replace basename
            source = source.Replace(sourceBasename, destBaseName);

            string[] lines = source.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            List<string> linesOut = new List<string>();
            for (int i = 0; i < lines.Length; i++)
                linesOut.Add(lines[i]);

            // replace duration in second line
            if (lines[1].Trim().StartsWith("#write_info"))
            {
                int beginCut = lines[1].IndexOf(" framerange");
                int endCut = lines[1].IndexOf(" fps");
                linesOut[1] = lines[1].Substring(0, beginCut) + string.Format(" framegange:\"1 {0}\"", minDuration) + lines[1].Substring(endCut);
            }


            List<int> readStarts = new List<int>();
            bool bInReadBlock = false;
            bool bCameraFound = false;
            int cameraIndex = 0;
            bool bInRoot = false;
            int linesOutIndex = 2;
            for (int i = 2; i < lines.Length ; i++, linesOutIndex++)
            {
                if (bInReadBlock)
                {
                    if (lines[i][0] == '}')
                    {
                        bInReadBlock = false;
                        bCameraFound = false;
                    }

                    if (lines[i].Trim().StartsWith("file "))
                    {
                        string path = lines[i].Trim().Substring(6).Trim('"');
                        string ext = Path.GetExtension(path);
                        if (_knownMovieTypes.Contains(ext.ToLower()))
                        {
                            // this is a movie - see if there's a cam_## in it
                            int camIndex = lines[i].IndexOf("cam_");
                            if (camIndex > -1)
                            {
                                bCameraFound = true;
                                cameraIndex = int.Parse(lines[i].Substring(camIndex + 4, 2));
                            }
                        }
                    }
                    if (bCameraFound)
                    {
                        if (lines[i].Trim().StartsWith("last "))
                        {
                            linesOut[linesOutIndex] = string.Format(" last {0}", _cameras[cameraIndex - 1].Duration());

                            if (!lines[i+1].Trim().StartsWith("frame_mode "))
                            {
                                linesOut.Insert(linesOutIndex + 1, " frame_mode \"start at\"");
                                linesOutIndex++;
                                linesOut.Insert(linesOutIndex + 1, string.Format(" frame {0}", _cameras[cameraIndex-1].Start()));
                                linesOutIndex++;
                            }
                            else
                            {
                                linesOut[linesOutIndex + 1] = " frame_mode \"start at\"" ;
                                linesOut[linesOutIndex + 2] = string.Format(" frame {0}", _cameras[cameraIndex - 1].Start()) ;
                            }
                        }
                    }
                }
                else if (bInRoot)
                {
                    if (lines[i].Trim().StartsWith("last_frame "))
                    {
                        linesOut[linesOutIndex] = string.Format(" last_frame {0}", minDuration);
                    }
                    if (lines[i][0] == '}')
                    {
                        bInRoot = false;
                    }
                }

                if (lines[i].Trim().StartsWith("Read {"))
                {
                    bInReadBlock = true;
                }
                if (lines[i].Trim().StartsWith("Root {"))
                {
                    bInRoot = true;
                }
            }
            // write nuke file
            if (File.Exists(nukeFilePath))
            {
                File.Delete(nukeFilePath);
            }
            using (FileStream fs = File.Create(nukeFilePath))
            {
                for (int i = 0; i < linesOut.Count; i++)    
                    AddText(fs, linesOut[i] + "\n");
            }

            return tNUKEFILE_ERROR.NUKE_OK;

        }

        public void GenerateNukeFile(string pluralEyesXMLPath, string nukeFilePath, string nukeWriteNodePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(pluralEyesXMLPath);

            XmlNodeList trackNodes = doc.DocumentElement.SelectNodes("/xmeml/project/children/sequence/media/video/track");
            foreach (XmlNode trackNode in trackNodes)
            {
                Console.WriteLine("node is \"" + trackNode.ToString() + "\" type is \"" + trackNode.NodeType.ToString() + "\"" + " name \"" + trackNode.Name + "\"");
                XmlNode startNode = trackNode.SelectSingleNode("clipitem/start");
                Console.WriteLine("node start is \"" + startNode.InnerText + "\"");
                int start = int.Parse(startNode.InnerText) ;
                XmlNode durationNode = trackNode.SelectSingleNode("clipitem/duration");
                Console.WriteLine("node duration is \"" + durationNode.InnerText + "\"");
                int duration = int.Parse(durationNode.InnerText) ;
                XmlNode pathNode = trackNode.SelectSingleNode("clipitem/file/pathurl");
                Console.WriteLine("node path is \"" + pathNode.InnerText + "\"");
                Uri pathUri = new Uri(pathNode.InnerText);
                string path = pathUri.LocalPath.Substring(12); // skip over \\localhost\
                Console.WriteLine("Local path is \"" + path + "\"");

                CameraInput camInp = new CameraInput(start, duration, path);
                _cameras.Add(camInp);
            }

            Console.WriteLine("presorted:");
            for (int i = 0; i < _cameras.Count; i++)
                Console.WriteLine("{0}: {1} {2} \"{3}\"", i, _cameras[i].Start(), _cameras[i].Duration(), _cameras[i].Path());
            _cameras.Sort();
            Console.WriteLine("postsorted:");
            for (int i = 0; i < _cameras.Count; i++)
                Console.WriteLine("{0}: {1} {2} \"{3}\"", i, _cameras[i].Start(), _cameras[i].Duration(), _cameras[i].Path());

            string baseName = Path.GetFileName(nukeWriteNodePath);
            // get num cameras
            int numCams = _cameras.Count;
            // get start nums and lengths
            // find min duration
            int minDuration = _cameras[0].Duration() - (_cameras[0].Start() < 0 ? 0 : _cameras[0].Start());
            for (int i = 1; i < _cameras.Count; i++)
            {
                int duration = _cameras[i].Duration() - (_cameras[i].Start() < 0 ? 0 : _cameras[i].Start());
                minDuration = duration < minDuration ? duration : minDuration;
            }

            if (File.Exists(nukeFilePath))
            {
                File.Delete(nukeFilePath);
            }
            using (FileStream fs = File.Create(nukeFilePath))
            {
                // write NUKEFILE_Header , write_path, duration
                string jpgPath = nukeWriteNodePath + "\\" + baseName + ".%04d.jpg";
                jpgPath = jpgPath.Replace('\\', '/');

                Console.WriteLine(NUKEFILE_Header);
                string fmtedText = string.Format(NUKEFILE_Header,
                    jpgPath,
                    minDuration.ToString());
                AddText(fs, fmtedText);

                AddText(fs, NUKEFILE_Header2);

                string flippedNukeFilePath = nukeFilePath.Replace('\\', '/');
                // write NUKEFILE_Root, nuke file name
                // 0 - name Z:/CreativeStudioJobs/VR_Flex360/08_TRANSFER/EXTERNAL/SHOOT/Flexi_20160728/SortedCameras/Shot00/Shot00.nk
                // 1 - duration
                fmtedText = string.Format(NUKEFILE_Root,
                    flippedNukeFilePath,
                    minDuration.ToString());
                AddText(fs, fmtedText);

                // for each camera write {camn ""}
                for (int i = 0; i < numCams; i++)
                {
                    fmtedText = string.Format("{{cam{0} \"\"}}\n", i + 1);
                    AddText(fs, fmtedText);
                }

                //  write closing } and }
                //  
                AddText(fs, " }\n}\n");

                // for each camera write {camn ""}
                for (int i = numCams - 1; i >= 0; i--)
                {
                    int duration = _cameras[i].Duration() - (_cameras[i].Start() < 0 ? 0 : _cameras[i].Start());

                    string flippedPath = _cameras[i].Path().Replace('\\', '/');
                    // 0 - " file Z:/CreativeStudioJobs/VR_Flex360/08_TRANSFER/EXTERNAL/SHOOT/Flexi_20160728/SortedCameras/Shot00/Shot00_cam_05.MP4\n" +
                    // 1 - duraton
                    // 2 - offset
                    // 3 - duration
                    // 4 - node xpos
                    // 5 - node ypos
                    fmtedText = string.Format(NUKEFILE_Read,
                        flippedPath,
                        duration.ToString(),
                        _cameras[i].Start().ToString(),
                        duration.ToString(),
                        "Read" + (i+1).ToString(),
                        (150 * i).ToString(),
                        150);
                    AddText(fs, fmtedText);
                }
                // write NUKEFILE_CamSolver
                // 0 - number of cameras
                // 1 - number of cameras
                fmtedText = string.Format(NUKEFILE_CamSolver,
                        numCams.ToString(),
                        numCams.ToString());
                AddText(fs, fmtedText);

                // for each camera 
                //       write NUKEFILE_CamDefault =
                for (int i = 0; i < numCams; i++)
                {

                    // 0 - cam num (starting at 1)
                    // 1 - name ie: camera1
                    fmtedText = string.Format(NUKEFILE_CamDefault,
                        (i + 1).ToString(),
                        "camera" + (i + 1).ToString());
                    AddText(fs, fmtedText);
                }

                // for each camera 
                //       write NUKEFILE_Camera =
                for (int i = 0; i < numCams; i++)
                {
                    // 0 - cam num (starting at 1)
                    // 1 - name ie: camera1
                    fmtedText = string.Format(NUKEFILE_Camera,
                        (i + 1).ToString(),
                        "camera" + (i + 1).ToString());
                    AddText(fs, fmtedText);
                }

                // write NUKEFILE_Tail

                string writeFmt = nukeWriteNodePath + "\\" + baseName + "_quick_stitch.####.jpg";
                writeFmt = writeFmt.Replace('\\', '/');
                fmtedText = string.Format(NUKEFILE_Tail,
                    // 0 - number of cameras
                    numCams.ToString(),
                    // 1 - Camera solver node xpos
                    ((150 * (numCams - 1)) / 2).ToString(),
                    // 4 - camera solver node ypos
                    300,
                    // 3 - reformat node xpos
                    ((150 * (numCams - 1)) / 2).ToString(),
                    // 4 - reformat node ypos
                    350,
                    // 5 - write file format: " file Z:/CreativeStudioJobs/VR_Flex360/04_EDIT/media/footage/080316_JPEGS/Shot00/Shot00_quick_stitch.####.jpg\n" +
                    writeFmt);
                AddText(fs, fmtedText);

                fmtedText = string.Format(NUKEFILE_Tail2,
                    ((150 * (numCams - 1)) / 2).ToString(),
                    400);
                AddText(fs, fmtedText);

                AddText(fs, NUKEFILE_Tail3);

                fs.Close();
            }
        }   
    }
}
