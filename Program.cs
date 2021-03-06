﻿using System;
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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Initialize();
            Application.Run(new Form1());
        }

        static void Initialize()
        {
            Settings.FactorioDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Factorio\mods\";
            Settings.tempModPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\temp\";

            if(!File.Exists("ignored-mods.txt"))
            {
                File.WriteAllLines("ignored-mods.txt", new string[] {
                    "rso-mod",
                    "Factorissimo2"
                });
            }

            if (File.Exists("settings.cfg"))
            {
                string[] lines = File.ReadAllLines("settings.cfg");
                if (!Directory.Exists(lines[0]) || !File.Exists(lines[0] + @"\config-path.cfg"))
                {
                    Application.Run(new FactorioFolderSelectionForm());
                }
                else
                {
                    Settings.FactorioPath = lines[0];
                }
            }
            else
            {
                Application.Run(new FactorioFolderSelectionForm());
            }
        }
    }
}
