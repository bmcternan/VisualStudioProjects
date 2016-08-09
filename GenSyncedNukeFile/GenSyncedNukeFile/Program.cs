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
                FinalCutProXMLBuilder bldr = frm.GetXMLBuilder();
                bldr.GenPluralEyesXML(args[1], args[2]);
            }
        }
    }
}
