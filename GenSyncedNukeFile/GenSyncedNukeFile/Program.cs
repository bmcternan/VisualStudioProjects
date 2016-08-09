using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;


namespace GenSyncedNukeFile
{
    static class Program
    {

        static void Usage(string cmdName)
        {
            Console.Out.WriteLine("Usage: {0} -xml path_to_camera_files xml_for_pluraleyes.xml = Build xml file for import into PluralEyes", cmdName);
            Console.Out.WriteLine("   or  {0} -nk PluralEyes_export_file Nuke_Template_file.nk TemplateBaseName NewNukeFile.nk NewBasename", cmdName);
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GetParamsForm frm = new GetParamsForm();
            string[] args = Environment.GetCommandLineArgs();
            
            if (args.Length < 2)
            {
                Application.Run(frm);
            }
            else
            {
                if (args[1].ToLower().StartsWith("-h") || args[1].ToLower().StartsWith("/h"))
                {
                    Usage(args[0]);
                }
                else if (args[1].ToLower().StartsWith("-xml"))
                {
                    if (args.Length != 4)
                        Usage(args[0]);
                    else
                    {
                        FinalCutProXMLBuilder bldr = frm.GetXMLBuilder();
                        bldr.GenPluralEyesXML(args[2], args[3]);
                    }
                }
                else if (args[1].ToLower().StartsWith("-nk"))
                {
                    if (args.Length != 7)
                        Usage(args[0]);
                    else
                    {
                        NukeBuilder bldr = frm.GetNukeBuilder();
                        bldr.ModifyNukeFile(args[2], args[3], args[4], args[5], args[6]);
                    }
                }
            }
        }
    }
}
