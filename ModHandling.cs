﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using FactorioApp;
using System.Windows.Forms;

namespace Overseer
{
    public static class ModHandling
    {
        //Ignored mods
        static List<string> IgnoredMods = new List<string>();

        //Contains full path of mods
        static IEnumerable<string> ModPaths;

        //Contains name of the mod and if it is enabled (from mod-list.json)
        static IDictionary<string, bool> Modlist = new Dictionary<string, bool>();

        //List of already extracted mods
        static List<string> ExtractedMods = new List<string>();

        //List of deserialized mods
        public static List<Mod> Mods = new List<Mod>();

        //List in which order mods should be loaded
        public static IDictionary<string, Mod> ModOrder = new Dictionary<string, Mod>();

        public static IDictionary<string, string> modz = new Dictionary<string, string>();

        private static BackgroundWorker backgroundWorker;
        private static DataLoadingProgressForm form;

        private static float progressTask = 0.0f;
        private static float progressTotal = 0.0f;
        private static int tasksCompleted = 0;
        private static string[] progressTexts = {
        "Deserializing data",
        "Getting enabled mods",
        "Checking extracted mods",
        "Extracting Mods",
        "Building mod objects",
        "Parsing mod dependencies",
        "Building load order",
        "Loading prototypes",
        "Serializing data", };

        static public void InitializeDataBackgroundWorker()
        {
            string str = Settings.FactorioPath;
            backgroundWorker = new BackgroundWorker();
            form = new DataLoadingProgressForm();
            form.TaskTextBox.Text = "Getting ignored mods";
            form.Show();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            backgroundWorker.RunWorkerAsync();
        }

        static private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (form != null)
            {
                form.Close();
            }
            Form1 form1 = (Form1)Form.ActiveForm;
            form1.PopulateItemList();
        }

        static private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (form == null)
            {
                return;
            }

            if (e.ProgressPercentage == 0)
            {
                form.TaskTextBox.Text = progressTexts[0];
            }
            else
            {
                float progressF = 100.0f / e.ProgressPercentage;
                progressTask += progressF;
                int progressI = Convert.ToInt16(progressTask);
                form.progressBarTask.Value = progressI;
                if (progressI == 100)
                {
                    progressTask = 0.0f;
                    progressTotal += (100.0f / 9);
                    form.progressBarTotal.Value = Convert.ToInt16(progressTotal);
                    tasksCompleted += 1;
                    if (tasksCompleted < progressTexts.Count())
                    {
                        form.TaskTextBox.Text = progressTexts[tasksCompleted];
                    }
                }
            }
        }

        static private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GetModList();
        }

        static public void GetModList()
        {
            if(File.Exists("data.dat"))
            {
                backgroundWorker.ReportProgress(0);
                Console.WriteLine("File data.dat exists. Start deserializing data");
                if (Serialization.Deserialize())
                {
                    Console.WriteLine("Deserialize complete");
                }
                else
                {
                    Console.WriteLine("Error occurred during deserialization");
                    File.Delete("data.dat");
                    GetModList();
                }
            }
            else
            {
                modz.Add("__base__", Settings.FactorioPath + @"\data\base");
                Data.Locale[0] = new Dictionary<string, string>(); //Items
                Data.Locale[1] = new Dictionary<string, string>(); //Fluids
                Data.Locale[2] = new Dictionary<string, string>(); //Recipes
                Data.Locale[3] = new Dictionary<string, string>(); //Entities
                Data.Locale[4] = new Dictionary<string, string>(); //Other
                
                GetIgnoredMods();
                GetEnabledMods();
                CheckExtractedMods();
                ExtractMods();
                BuildModObjects();
                ParseDependencies();
                BuildLoadingOrder();
                DataLoader dataloader = new DataLoader();
                backgroundWorker.ReportProgress(1);
                SerializeData();
            }
        }
        static void GetIgnoredMods()
        {
            if (File.Exists("ignored-mods.txt"))
            {
                string[] lines = File.ReadAllLines("ignored-mods.txt");
                foreach (string line in lines)
                {
                    IgnoredMods.Add(line);
                }
            }
            backgroundWorker.ReportProgress(1);
        }
        static void GetEnabledMods()
        {
            ModPaths = Directory.EnumerateFiles(Settings.FactorioDataPath);
            string[] lines = File.ReadAllLines(Settings.FactorioDataPath + "\\mod-list.json");
            for (int i = 3; i < lines.Count() - 4; i += 4)
            {
                string mod = lines[i].Substring(15);
                mod = mod.Replace("\",", "");
                bool bEnabled = lines[i + 1].Contains("true");
                if (!IgnoredMods.Contains(mod))
                {
                    Modlist.Add(mod, bEnabled);
                }
            }
            backgroundWorker.ReportProgress(1);
        }
        static void CheckExtractedMods()
        {
            if (Directory.Exists(Settings.tempModPath))
            {
                IEnumerable<string> ExtractedModPaths = Directory.EnumerateDirectories(Settings.tempModPath);
                foreach (string str in ExtractedModPaths)
                {
                    string modname = str.Replace(Settings.tempModPath, "");
                    //parsedModName can be used to check if existing extracted mod is up to date.
                    //
                    //TODO: Version checking
                    string[] parsedModName = modname.Split('_');
                    ExtractedMods.Add(parsedModName[0]);
                }
            }
            backgroundWorker.ReportProgress(1);
        }

        //Checks if the mod is enabled and mod file exists. Extracts if needed.
        static void ExtractMods()
        {
            int progressStep = Modlist.Keys.Count;
            foreach (string modName in Modlist.Keys)
            {
                if (modName != "base")
                {
                    for (int i = 0; i < ModPaths.Count(); ++i)
                    {
                        if (ExtractedMods != null)
                        {
                            if (!ExtractedMods.Contains(modName))
                            {
                                if (ModPaths.ElementAt(i).Contains(modName))
                                {
                                    ExtractMod(ModPaths.ElementAt(i), modName);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (ModPaths.ElementAt(i).Contains(modName))
                            {
                                ExtractMod(ModPaths.ElementAt(i), modName);
                                break;
                            }
                        }
                    }
                }
                backgroundWorker.ReportProgress(progressStep);
            }            
        }

        static void ExtractMod(string modPath, string modName)
        {
            if (!File.Exists(modPath))
            {
                return;
            }

            FastZip fz = new FastZip();
            fz.ExtractZip(modPath, Settings.tempModPath, "");
        }

        static void BuildModObjects()
        {
            ParseLocaleFile(Settings.FactorioPath + @"\data\base");
            IEnumerable<string> ExtractedModPaths = Directory.EnumerateDirectories(Settings.tempModPath);
            int progressStep = ExtractedModPaths.Count();
            foreach (string str in ExtractedModPaths)
            {
                ParseLocaleFile(str);
                if(File.Exists(str + "\\info.json"))
                {
                    string[] infos = File.ReadAllLines(str + "\\info.json");
                    string info = "";
                    foreach (string st in infos)
                    {
                        info = info + st;
                    }
                    ModInfo modInfo = JsonConvert.DeserializeObject<ModInfo>(info);
                    if(!IgnoredMods.Contains(modInfo.name))
                    {
                        Mod newMod = new Mod();
                        newMod.modInfo = modInfo;
                        newMod.Path = str;
                        Mods.Add(newMod);
                        modz.Add("__" + newMod.modInfo.name + "__", str);

                        if(modInfo.name.Contains("bobplates"))
                        {
                            if(File.Exists(str + @"\data-updates.lua"))
                            {
                                string[] lines = File.ReadAllLines(str + @"\data-updates.lua");
                                lines[107] = "";
                                lines[108] = "";
                                File.WriteAllLines(str + @"\data-updates.lua", lines);
                            }
                        }
                    }
                    backgroundWorker.ReportProgress(progressStep);
                }
            }
        }
        static void ParseDependencies()
        {
            int progressStep = Mods.Count;
            foreach (Mod mod in Mods)
            {
                if (mod.modInfo.dependencies != null)
                {
                    foreach (string dep in mod.modInfo.dependencies)
                    {
                        string dep2 = dep;
                        bool Optional = false;
                        if (dep2.StartsWith("?"))
                        {
                            Optional = true;
                            dep2 = dep2.Trim(new char[] { '?', ' ' });
                        }
                        string[] values = dep2.Split(' ');
                        string name = values[0];
                        if (!name.Equals("base"))
                        {
                            string version = "";
                            if (values.Length > 1)
                            {
                                version = values[2];
                            }

                            ModDependency modDep = new ModDependency { bOptional = Optional };
                            bool bFound = false;

                            foreach (Mod m in Mods)
                            {
                                ModInfo i = m.modInfo;
                                if (i.name == name)
                                {
                                    bFound = true;
                                    modDep.Mod = m;
                                    modDep.bFulfillsRequirements = FulfillsVersionRequirement(i.version, version);
                                    break;
                                }
                            }
                            if (!bFound)
                            {
                                if (!Optional)
                                {
                                    mod.MissingMods.Add(dep);
                                }
                            }
                            else
                            {
                                mod.Dependencies.Add(modDep);
                            }
                        }
                    }
                }
                backgroundWorker.ReportProgress(progressStep);
            }
        }
        static void SerializeData()
        {
            DataSerializeClass data = Data.PrepareToSerialize();
            Serialization.Serialize(data);
            backgroundWorker.ReportProgress(1);
        }

        static void ParseLocaleFile(string str)
        {
            if (Directory.Exists(str + @"/locale/en"))
            {
                string[] locales = Directory.GetFiles(str + @"/locale/en");
                foreach (string locale in locales)
                {
                    int cat = 0;
                    string[] lines = File.ReadAllLines(locale);
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("["))
                        {
                            switch (line)
                            {
                                case "[item-name]":
                                    cat = 0;
                                    break;
                                case "[fluid-name]":
                                    cat = 1;
                                    break;
                                case "[recipe-name]":
                                    cat = 2;
                                    break;
                                case "[entity-name]":
                                    cat = 3;
                                    break;
                                default:
                                    cat = 4;
                                    break;
                            }
                        }
                        else if (line.Count() > 2)
                        {
                            string[] vals = Regex.Split(line, @"[=]");
                            if (!Data.Locale[cat].Keys.Contains(vals[0]))
                            {
                                Data.Locale[cat].Add(vals[0], vals[1]);
                            }
                        }
                    }
                }
            }
        }
        static void BuildLoadingOrder()
        {
            foreach (Mod mod in Mods)
            {
                TryToAddModToOrderList(mod);
            }
            List<string> l = ModOrder.Keys.ToList();
            backgroundWorker.ReportProgress(1);
        }
        static void TryToAddModToOrderList(Mod inMod)
        {
            if (inMod.Dependencies.Count > 0)
            {
                foreach (ModDependency d in inMod.Dependencies)
                {
                    if (!ModOrder.ContainsKey(d.Mod.modInfo.name))
                    {
                        if (!IgnoredMods.Contains(inMod.modInfo.name))
                        {
                            TryToAddModToOrderList(d.Mod);
                        }
                        else
                        {
                            Console.WriteLine("Mod ignored: {0}", inMod.modInfo.name);
                        }
                    }
                }
            }

            if (!IgnoredMods.Contains(inMod.modInfo.name))
            {
                if (!ModOrder.ContainsKey(inMod.modInfo.name))
                {
                    ModOrder.Add(inMod.modInfo.name, inMod);
                }
            }
            else
            {
                Console.WriteLine("Mod ignored: {0}", inMod.modInfo.name);
            }
        }  

        static bool FulfillsVersionRequirement(string inA, string inB)
        {
            string[] A = inA.Split('.');
            string[] B = inA.Split('.');
            int MajorA = Convert.ToInt16(A[0]);
            int MajorB = Convert.ToInt16(B[0]);
            int MiddleA = Convert.ToInt16(A[1]);
            int MiddleB = Convert.ToInt16(B[1]);
            int MinorA = Convert.ToInt16(A[2]);
            int MinorB = Convert.ToInt16(B[2]);

            if (MajorA >= MajorB && MiddleA >= MiddleB && MinorA >= MinorB)
            {
                return true;
            }
            return false;
        }
    }

    public class ModInfo
    {
        public string name { get; set; }
        public string version { get; set; }
        public string factorio_version { get; set; }
        public string title { get; set; }
        public IList<string> dependencies { get; set; }
    }

    public class Mod
    {
        public bool bHasBeenLoaded = false;
        public bool bFailedToLoad = false;
        public bool bCanBeLoaded = true;
        public string Path = "";
        public List<string> MissingMods = new List<string>();
        public ModInfo modInfo = new ModInfo();
        public List<string> Locale = new List<string>();
        public List<ModDependency> Dependencies = new List<ModDependency>();
        
        public MemoryStream locale = new MemoryStream();

        public Mod() { }
    }
    public class ModDependency
    {
        public Mod Mod;
        public bool bFulfillsRequirements = false;
        public bool bOptional = false;
    }
    public enum Comp
    {
        LesserOrEqual,
        Equal,
        GreaterOrEqual
    }
}
