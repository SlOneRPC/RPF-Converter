using ResourceCreatorv2.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResourceCreatorv2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var cmdArgs = Environment.GetCommandLineArgs();

            if (cmdArgs.Length < 1) // run winforms app
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ResourceCreator());
            }
            else
            {
                if (cmdArgs[1] == "CHECK_MODELS")
                {
                    if (cmdArgs.Length >= 2)
                    {
                        ModelChecker.Start(cmdArgs[2]);
                    }
                }
            }
        }
    }
}
