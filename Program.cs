using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overseer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Settings.FactorioDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Factorio\mods\";            
            Settings.tempModPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\temp\";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(File.Exists("settings.cfg"))
            {
                string[] lines = File.ReadAllLines("settings.cfg");
                if(Directory.Exists(lines[0]))
                {
                    if(!File.Exists(lines[0] + @"\config-path.cfg"))
                    {
                        Application.Run(new FactorioFolderSelectionForm());
                    }
                }
            }
            else
            {
                Application.Run(new FactorioFolderSelectionForm());
            }
            Application.Run(new Form1());
        }
    }
}
