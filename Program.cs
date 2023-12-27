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

            if (cmdArgs.Length < 2)
            {
                MessageBox.Show("No JSON file specified!");
                Environment.Exit(0);
            }

            var commandLine = cmdArgs[1];

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ResourceCreator());
        }
    }
}
