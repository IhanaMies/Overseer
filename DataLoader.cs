using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLua;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.ComponentModel;

namespace Overseer
{
    public class DataLoader
    {
        static Lua lua = new Lua();
        public DataLoader()
        {
            lua["package.path"] = Settings.FactorioPath + @"\data\base\?";
            lua["package.path"] = lua["package.path"] + ";" + Settings.FactorioPath + @"\data\base\?.lua";
            lua["package.path"] = lua["package.path"] + ";" + Settings.FactorioPath + @"\data\core\?";
            lua["package.path"] = lua["package.path"] + ";" + Settings.FactorioPath + @"\data\core\?.lua";
            lua["package.path"] = lua["package.path"] + ";" + Settings.FactorioPath + @"\data\core\lualib\?";
            lua["package.path"] = lua["package.path"] + ";" + Settings.FactorioPath + @"\data\core\lualib\?.lua";
            string luascript = (@"
                    require ('util')

                    function log(...)
                    end

                    serpent = {}
                    serpent.block = function(n) return '' end
                    
                    settings = {}    
                    settings.startup = {}
                    defines = {}
                    defines.difficulty_settings = {}
                    defines.difficulty_settings.recipe_difficulty = {}
                    defines.difficulty_settings.technology_difficulty = {}
                    defines.difficulty_settings.recipe_difficulty.normal = 1
                    defines.difficulty_settings.technology_difficulty.normal = 1
                    defines.direction = {}
                    defines.direction.north = 1
                    defines.direction.east = 2
                    defines.direction.south = 3
                    defines.direction.west = 4
                    require('data')
                    require('data-updates')
                ");
            lua.DoFile(Settings.FactorioPath + @"\data\core\lualib\dataloader.lua");
            lua.DoFile(Settings.FactorioPath + @"\data\core\data.lua");
            lua.DoString(luascript);

            lua.DoString(@"
                        function electrolyserpictures() end
                        data.raw['item-subgroup']['bob-greenhouse-items'] = {}
                        data.raw['item-subgroup']['bob-greenhouse-items'].group = 'bob-intermediate-products'
                        mods = {}
                        ");

            LoadMods();            

            LuaTable rawRecipes = lua.GetTable("data.raw")["recipe"] as LuaTable;
            LuaTable rawFluids = lua.GetTable("data.raw")["fluid"] as LuaTable;
            LuaTable rawItems = lua.GetTable("data.raw")["item"] as LuaTable;
            LuaTable rawAssemblingMachines = lua.GetTable("data.raw")["assembling-machine"] as LuaTable;
            LuaTable rawFurnaces = lua.GetTable("data.raw")["furnace"] as LuaTable;
            LuaTable rawMiningDrills = lua.GetTable("data.raw")["mining-drill"] as LuaTable;
            LuaTable rawResources = lua.GetTable("data.raw")["resource"] as LuaTable;
            LuaTable rawModules = lua.GetTable("data.raw")["module"] as LuaTable;

            var fluidEnumerator = rawFluids.GetEnumerator();
            while (fluidEnumerator.MoveNext())
            {
                AddIconToCollection(fluidEnumerator.Value as LuaTable);
                Fluid newFluid = InterpretFluid(fluidEnumerator.Key as string, fluidEnumerator.Value as LuaTable);
                if (newFluid != null)
                {
                    Data.AddFluid(newFluid.name, newFluid);
                }
            }

            var itemEnumerator = rawItems.GetEnumerator();
            while(itemEnumerator.MoveNext())
            {
                AddIconToCollection(itemEnumerator.Value as LuaTable);
                Item newItem = InterpretItem(itemEnumerator.Key as string, itemEnumerator.Value as LuaTable);
                if (newItem != null)
                {
                    Data.AddItem(newItem.name, newItem);
                    if(newItem.category != "" && !Data.ItemCategories.Contains(newItem.category))
                    {
                        Data.ItemCategories.Add(newItem.category);
                    }
                    if (newItem.subgroup != "" && !Data.ItemSubgroups.Contains(newItem.subgroup))
                    {
                        Data.ItemSubgroups.Add(newItem.subgroup);
                    }
                }
            }

            var recipeEnumerator = rawRecipes.GetEnumerator();
            while (recipeEnumerator.MoveNext())
            {
                AddIconToCollection(recipeEnumerator.Value as LuaTable);
                Recipe newRecipe = InterpretRecipe(recipeEnumerator.Key as string, recipeEnumerator.Value as LuaTable);
                if (newRecipe != null)
                {
                    Data.AddRecipe(newRecipe);
                    if (newRecipe.category != "" && !Data.RecipeCategories.Contains(newRecipe.category))
                    {
                        Data.RecipeCategories.Add(newRecipe.category);
                    }
                    if (newRecipe.subgroup != "" && !Data.RecipeSubgroups.Contains(newRecipe.subgroup))
                    {
                        Data.RecipeSubgroups.Add(newRecipe.subgroup);
                    }
                }
            }

            Data.Locale.Count();
        }
        Fluid InterpretFluid(string inName, LuaTable inTable)
        {
            if (inTable["type"].ToString() == "fluid")
            {
                Fluid newFluid = new Fluid();
                newFluid.name = inTable["name"] as string;
                newFluid.category = inTable["category"] as string;
                newFluid.itype = inTable["type"] as string;
                newFluid.subgroup = inTable["subgroup"] as string;

                string mod = Regex.Match((inTable["icon"] as string), @"^[^/]*").Value;
                string iconPath = inTable["icon"] as string;
                ModHandling.modz.TryGetValue(mod, out string Path);
                newFluid.iconPath = iconPath.Replace(mod, Path);
                return newFluid;
            }
            return null;
        }
        Item InterpretItem(string inName, LuaTable inTable)
        {
            if (inTable["type"].ToString() == "item" && inTable["subgroup"].ToString() != "factorissimo2")
            {
                Item newItem = new Item();
                if (inTable["localised_name"] != null)
                {
                    newItem.localised_name = inTable["localised_name"] as string;
                }
                else
                {
                    foreach(IDictionary<string, string> dic in Data.Locale)
                    {
                        dic.TryGetValue(inName, out newItem.localised_name);
                        if(newItem.localised_name != null)
                        {
                            break;
                        }
                    }
                    if(newItem.localised_name == null)
                    {
                        Console.WriteLine("{0} missing localized name", inName);
                    }
                }
                newItem.name = inTable["name"] as string;
                newItem.category = inTable["category"] as string;
                newItem.itype = inTable["type"] as string;
                newItem.subgroup = inTable["subgroup"] as string;
                newItem.iconPath = inTable["icon"] as string;
                return newItem;
            }
            return null;
        }
        bool AddIconToCollection(LuaTable inItem)
        {
            string itemName = inItem["name"] as string;
            string iconpath = "";
            if (!(inItem["icons"] is LuaTable icons))
            {
                if ((iconpath = inItem["icon"] as string) != null)
                {
                    string str = Regex.Match(iconpath, @"^[^/]*").Value;
                    ModHandling.modz.TryGetValue(str, out string fold);
                    iconpath = iconpath.Replace(str, fold);
                }
            }
            else
            {
                var enumr = icons.GetEnumerator();
                while (enumr.MoveNext())
                {
                    LuaTable icon2 = enumr.Value as LuaTable;
                    if (icon2.Keys.Count > 1)
                    {
                        break;
                    }
                    iconpath = icon2["icon"] as string;
                    string str = Regex.Match(iconpath, @"^[^/]*").Value;
                    ModHandling.modz.TryGetValue(str, out string fold);
                    iconpath = iconpath.Replace(str, fold);
                }
            }
            if (!(iconpath == "" || iconpath == null))
            {
                if (!Data.Icons.ContainsKey(itemName))
                {
                    //BitmapSource source = PngDecoder.GetIconFromPath(iconpath);
                    //Data.Icons.Add(itemName, source);
                    Data.Icons.Add(itemName, Image.FromFile(iconpath));
                    return true;
                }
            }
            return false;
        }
        void LoadMods()
        {
            //Parse settings file for each mod
            foreach (Mod mod in ModHandling.ModOrder.Values)
            {
                EnableSettings(mod.Path);
            }

            //Build prototypes
            foreach (string str in new[] { "data", "data-updates", "data-final-fixes" })
            {
                foreach (Mod mod in ModHandling.ModOrder.Values)
                {
                    SetPackagePath(mod.Path);
                    RunLuaFile(mod.Path, str);
                }
            }
        }
        void SetPackagePath(string modFolderFullPath)
        {
            lua["package.path"] = Settings.FactorioPath + @"\data\core\lualib\" + @"\?.lua";
            lua["package.path"] = lua["package.path"] + ";" + modFolderFullPath + "\\?.lua";
            lua["package.path"] = lua["package.path"] + ";" + modFolderFullPath + "\\prototypes\\?";
            lua["package.path"] = lua["package.path"] + ";" + modFolderFullPath + "\\prototypes\\?.lua";
            lua["package.path"] = lua["package.path"] + ";" + modFolderFullPath + "\\prototypes\\recipes\\?";
            lua["package.path"] = lua["package.path"] + ";" + modFolderFullPath + "\\prototypes\\recipes\\?.lua";
        }
        void EnableSettings(string modFolderFullPath)
        {
            if (File.Exists(modFolderFullPath + "\\settings.lua"))
            {
                string[] lines = File.ReadAllLines(modFolderFullPath + "\\settings.lua");
                Lua lua2 = new Lua();
                lua2.DoString(@"data = {}
                                    data.raw = {}
                                    function data.extend(self, otherdata)
                                      if type(otherdata) ~= table_string or #otherdata == 0 then
                                      end

                                      for _, e in ipairs(otherdata) do
                                        if not e.type or not e.name then
                                        end
                                        local t = self.raw[e.type]
                                        if t == nil then
                                          t = { }
                                          self.raw[e.type] = t
                                        end
                                        t[e.name] = e
                                      end
                                    end");

                lua2.DoFile(modFolderFullPath + "\\settings.lua");
                if (lua2.GetTable("data.raw.bool-setting") is LuaTable tbl)
                {
                    var set = tbl.GetEnumerator();
                    while (set.MoveNext())
                    {
                        string name = set.Key as string;
                        LuaTable val = set.Value as LuaTable;
                        if (val["type"] as string == "bool-setting")
                        {
                            var vals = val.GetEnumerator();
                            while (vals.MoveNext())
                            {
                                if (vals.Key as string == "default_value")
                                {
                                    string luastr1 = @"settings.startup[""" + name + @"""] = {}";
                                    lua.DoString(luastr1);
                                    string luastr2 = "";
                                    if (vals.Value.ToString() == "True")
                                    {
                                        luastr2 = @"settings.startup[""" + name + @"""].value = true";
                                    }
                                    else
                                    {
                                        luastr2 = @"settings.startup[""" + name + @"""].value = false";
                                    }
                                    lua.DoString(luastr2);
                                }
                            }
                        }
                    }
                }

                tbl = lua2.GetTable("data.raw.int-setting") as LuaTable;
                if (tbl != null)
                {
                    var set = tbl.GetEnumerator();
                    set = tbl.GetEnumerator();
                    while (set.MoveNext())
                    {
                        string name = set.Key as string;
                        LuaTable val = set.Value as LuaTable;
                        if (val["type"] as string == "int-setting")
                        {
                            int val1 = Convert.ToInt16(val["default_value"]);
                            int val2 = Convert.ToInt16(val["minimum_value"]);
                            int val3 = Convert.ToInt16(val["maximum_value"]);
                            string luastr1 = "settings.startup['" + name + "'] = {}";
                            string luastr2 = "settings.startup['" + name + "'] = {{'default_value', " + val1 + "},{'minimum_value', " + val2 + "},{'maximum_value', " + val3 + "}}";
                            string luastr3 = "settings.startup['" + name + "'].value = " + val1;
                            lua.DoString(luastr1);
                            lua.DoString(luastr2);
                            lua.DoString(luastr3);
                        }
                    }
                }

                tbl = lua2.GetTable("data.raw.double-setting") as LuaTable;
                if (tbl != null)
                {
                    var set = tbl.GetEnumerator();
                    set = tbl.GetEnumerator();
                    while (set.MoveNext())
                    {
                        string name = set.Key as string;
                        LuaTable val = set.Value as LuaTable;
                        if (val["type"] as string == "double-setting")
                        {
                            int val1 = Convert.ToInt16(val["default_value"]);
                            int val2 = Convert.ToInt16(val["minimum_value"]);
                            int val3 = Convert.ToInt16(val["maximum_value"]);
                            string luastr1 = "settings.startup['" + name + "'] = {}";
                            string luastr2 = "settings.startup['" + name + "'] = {{'default_value', " + val1 + "},{'minimum_value', " + val2 + "},{'maximum_value', " + val3 + "}}";
                            string luastr3 = "settings.startup['" + name + "'].value = " + val1;
                            lua.DoString(luastr1);
                            lua.DoString(luastr2);
                            lua.DoString(luastr3);
                        }
                    }
                }
            }
        }
        void ResetLoadedPackages()
        {
             lua.DoString(@"
 							for k, v in pairs(package.loaded) do
 								package.loaded[k] = false
 							end");
        }
        void RunLuaFile(string modFolderFullPath, string inLuaFileType)
        {
            if (File.Exists(modFolderFullPath + "\\" + inLuaFileType + ".lua"))
            {
                ResetLoadedPackages();
                lua.DoFile(modFolderFullPath + "\\" + inLuaFileType + ".lua");
            }
        }
        Recipe InterpretRecipe(string inName, LuaTable inTable)
        {
            if (inTable["type"].ToString() != "recipe")
            {
                return null;
            }
            bool bSingleResult = false;
            Recipe newRecipe = new Recipe();
            newRecipe.iconPath = inTable["icon"] as string;
            Intermediate newResult = new Intermediate();
            newRecipe.name = inName;
            List<Intermediate>[] Ingredients = new List<Intermediate>[2];
            Ingredients[0] = new List<Intermediate>();
            Ingredients[1] = new List<Intermediate>();

            List<Intermediate>[] Results = new List<Intermediate>[2];
            Results[0] = new List<Intermediate>();
            Results[1] = new List<Intermediate>();

            var valEnumerator = inTable.GetEnumerator();
            while(valEnumerator.MoveNext())
            {
                string key = valEnumerator.Key as string;
                if (key == "type" || key == "name" || key == "result" || key == "category" || key == "sub-group")
                {
                    string value = valEnumerator.Value as string;
                    switch(key)
                    {
                        case "type":
                            newRecipe.itype = "item";
                            break;
                        case "name":
                            newRecipe.name = value;

                            if (inTable["localised_name"] != null)
                                newRecipe.localised_name = inTable["localised_name"] as string;
                            else
                                Data.Locale[1].TryGetValue(value, out newRecipe.localised_name);
                                if (newRecipe.localised_name == null)
                                    Data.Locale[0].TryGetValue(value, out newRecipe.localised_name);
                            break;
                        case "result":
                            bSingleResult = true;
                            newResult.name = value;
                            newResult.itype = "item";
                            break;
                        case "category":
                            newRecipe.category = value;
                            break;
                        case "sub-group":
                            newRecipe.subgroup = value;
                            break;
                    }
                }
                else if (key == "energy_needed")
                {
                    newRecipe.energy_needed = Convert.ToSingle(valEnumerator.Value);
                }
                else if (key == "result_count")
                {
                    newResult.amount = Convert.ToInt16(valEnumerator.Value);
                }
                else if (key == "ingredients")
                {
                    Ingredients[0] = Ingredients[1] = InterpretIngredients(valEnumerator.Value as LuaTable);
                    newResult.amount = 1;
                }
                else if (key == "results")
                {
                    bSingleResult = false;
                    Results[0] = Results[1] = InterpretResults(valEnumerator.Value as LuaTable);
                    newResult.amount = 1;
                }
                else if (key == "normal")
                {
                    newRecipe.bExpensive = true;
                    Intermediate MonoResult = new Intermediate();
                    List<Intermediate> NormalIngredients = new List<Intermediate>();
                    List<Intermediate> NormalResults = new List<Intermediate>();
                    LuaTable normal = valEnumerator.Value as LuaTable;
                    var ingEnumerator = normal.GetEnumerator();
                    while(ingEnumerator.MoveNext())
                    {
                        string key1 = ingEnumerator.Key as string;
                        switch (key1)
                        {
                            case "energy_required":
                                newRecipe.energy_needed = Convert.ToSingle(ingEnumerator.Value);
                                break;
                            case "ingredients":
                                NormalIngredients = InterpretIngredients(ingEnumerator.Value as LuaTable);
                                break;
                            case "result":
                                MonoResult.name = ingEnumerator.Value as string;
                                MonoResult.itype = "item";
                                NormalResults.Add(MonoResult);
                                break;
                            case "result_count":
                                MonoResult.amount = Convert.ToInt16(ingEnumerator.Value);
                                break;
                            case "results":
                                NormalResults = InterpretResults(ingEnumerator.Value as LuaTable);
                                break;
                        }
                    }
                    Results[0] = NormalResults;
                    Ingredients[0] = NormalIngredients;
                }
                else if (key == "expensive")
                {
                    Intermediate MonoResult = new Intermediate();
                    List<Intermediate> ExpensiveIngredients = new List<Intermediate>();
                    List<Intermediate> ExpensiveResults = new List<Intermediate>();
                    LuaTable expensive = valEnumerator.Value as LuaTable;
                    var ingEnumerator = expensive.GetEnumerator();
                    while (ingEnumerator.MoveNext())
                    {
                        string key1 = ingEnumerator.Key as string;
                        switch (key1)
                        {
                            case "energy_required":
                                newRecipe.energy_needed = Convert.ToSingle(ingEnumerator.Value);
                                break;
                            case "ingredients":
                                ExpensiveIngredients = InterpretIngredients(ingEnumerator.Value as LuaTable);
                                break;
                            case "result":
                                MonoResult.name = ingEnumerator.Value as string;
                                MonoResult.itype = "item";
                                ExpensiveResults.Add(MonoResult);
                                break;
                            case "result_count":
                                MonoResult.amount = Convert.ToInt16(ingEnumerator.Value);
                                break;
                            case "results":
                                ExpensiveResults = InterpretResults(ingEnumerator.Value as LuaTable);
                                break;
                        }
                    }
                    Results[1] = ExpensiveResults;
                    Ingredients[1] = ExpensiveIngredients;
                }
            }
            newRecipe.ingredients = Ingredients;
            if(bSingleResult)
            {
                newRecipe.results[0].Add(newResult);
                newRecipe.results[1] = newRecipe.results[0];
            }
            else
            {
                newRecipe.results = Results;
            }
            foreach(Intermediate r in newRecipe.results[0])
            {
                if (r.itype == "fluid")
                {
                    Data.Fluids.TryGetValue(r.name, out Fluid i);
                    if (i != null)
                    {
                        i.MadeByRecipes.Add(newRecipe);
                    }
                }
                else if(r.itype == "item")
                {
                    Data.Items.TryGetValue(r.name, out Item i);
                    if (i != null)
                    {
                        i.MadeByRecipes.Add(newRecipe);
                    }
                }
            }
            foreach (Intermediate r in newRecipe.ingredients[0])
            {
                if (r.itype == "fluid")
                {
                    Data.Fluids.TryGetValue(r.name, out Fluid i);
                    if (i != null)
                    {
                        i.UsedInRecipes.Add(newRecipe);
                    }
                }
                else if (r.itype == "item")
                {
                    Data.Items.TryGetValue(r.name, out Item i);
                    if (i != null)
                    {
                        i.UsedInRecipes.Add(newRecipe);
                    }
                }
            }
            return newRecipe;
        }
        List<Intermediate> InterpretIngredients(LuaTable ingredients)
        {
            List<Intermediate> ingrs = new List<Intermediate>();
            var ingEnumerator = ingredients.GetEnumerator();
            while (ingEnumerator.MoveNext())
            {
                Intermediate newIngredient = new Intermediate();
                LuaTable temp = ingEnumerator.Value as LuaTable;
                if (temp.Keys.Count == 2)
                {
                    var ingEnumerator2 = temp.GetEnumerator();
                    while (ingEnumerator2.MoveNext())
                        if (ingEnumerator2.Value.GetType() == typeof(string))
                        {
                            newIngredient.name = ingEnumerator2.Value as string;
                        }
                        else
                        {
                            newIngredient.amount = Convert.ToInt16(ingEnumerator2.Value);
                            newIngredient.itype = "item";
                        }
                }
                else
                {
                    var ingEnumerator2 = temp.GetEnumerator();
                    while (ingEnumerator2.MoveNext())
                    {
                        if(ingEnumerator2.Key as string == "type")
                        {
                            newIngredient.itype = ingEnumerator2.Value as string;
                        }
                        else if (ingEnumerator2.Key as string == "name")
                        {
                            newIngredient.name = ingEnumerator2.Value as string;
                            Data.Locale[0].TryGetValue(newIngredient.name, out newIngredient.localised_name);
                        }
                        else
                        {
                            newIngredient.amount = Convert.ToInt16(ingEnumerator2.Value);
                        }
                    }
                }
                ingrs.Add(newIngredient);
            }
            return ingrs;
        }
        List<Intermediate> InterpretResults(LuaTable ingredients)
        {
            List<Intermediate> results = new List<Intermediate>();
            var ingEnumerator = ingredients.GetEnumerator();
            while (ingEnumerator.MoveNext())
            {
                Intermediate newResult = new Intermediate();
                LuaTable temp = ingEnumerator.Value as LuaTable;
                if (temp.Keys.Count == 2)
                {
                    var ingEnumerator2 = temp.GetEnumerator();
                    while (ingEnumerator2.MoveNext())
                        if (ingEnumerator2.Value.GetType() == typeof(string))
                        {
                            newResult.name = ingEnumerator2.Value as string;
                            Data.Locale[0].TryGetValue(newResult.name, out newResult.localised_name);
                        }
                        else
                        {
                            newResult.amount = Convert.ToInt16(ingEnumerator2.Value);
                            newResult.itype = "item";
                        }
                }
                else
                {
                    var ingEnumerator2 = temp.GetEnumerator();
                    while (ingEnumerator2.MoveNext())
                    {
                        if (ingEnumerator2.Key as string == "type")
                        {
                            newResult.itype = ingEnumerator2.Value as string;
                        }
                        else if (ingEnumerator2.Key as string == "name")
                        {
                            newResult.name = ingEnumerator2.Value as string;
                            Data.Locale[0].TryGetValue(newResult.name, out newResult.localised_name);
                        }
                        else
                        {
                            if(ingEnumerator2.Key.GetType() == typeof(double))
                            {
                                newResult.amount = Convert.ToInt16(ingEnumerator2.Key);
                            }
                            else
                            {
                                newResult.amount = Convert.ToInt16(ingEnumerator2.Value);
                            }
                        }
                    }
                }
                results.Add(newResult);
            }
            return results;
        }
    }
}
