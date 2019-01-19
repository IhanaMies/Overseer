using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Collections;

namespace Overseer
{
    public static class Settings
    {
        public static string FactorioPath = "M:\\Pelit\\Steam\\steamapps\\common\\Factorio\\data\\base";
        public static string FactorioDataPath = "";
        public static string tempModPath = "";
    }
    public static class UserFormData
    {
        public static bool bUserHasRecipeListFocus = false;
    }
    
    public static class Data
    {
        public static SortedDictionary<string, Image> Icons = new SortedDictionary<string, Image>();
        public static SortedDictionary<string, Item> Items = new SortedDictionary<string, Item>();
        public static IDictionary<string, Fluid> Fluids = new Dictionary<string, Fluid>();

        //[items, recipes, entities]
        public static IDictionary<string, string>[] Locale = new Dictionary<string, string>[5];

        public static List<Recipe> Recipes = new List<Recipe>();

        public static List<string> ItemCategories = new List<string>();
        public static List<string> ItemSubgroups = new List<string>();
        public static List<string> RecipeCategories = new List<string>();
        public static List<string> RecipeSubgroups = new List<string>();

        public static DataSerializeClass PrepareToSerialize()
        {
                    DataSerializeClass data = new DataSerializeClass();

            data.Icons = Icons;
            data.Items = Items;
            data.Fluids = Fluids;
            data.Locale = Locale;
            data.Recipes = Recipes;
            data.ItemCategories = ItemCategories;
            data.ItemSubgroups = ItemSubgroups;
            data.RecipeCategories = RecipeCategories;
            data.RecipeSubgroups = RecipeSubgroups;

            Console.WriteLine("");
            Console.WriteLine("Preparing:");
            Console.WriteLine("\tNumber of icons: {0}", data.Icons.Count);
            Console.WriteLine("\tNumber of items: {0}", data.Items.Count);
            Console.WriteLine("\tNumber of fluids: {0}", data.Fluids.Count);
            Console.WriteLine("\tNumber of recipes: {0}", data.Recipes.Count);
            Console.WriteLine("\tNumber of locale strings: {0}", data.Locale.Length);
            Console.WriteLine("\tNumber of item categories: {0}", data.ItemCategories.Count);
            Console.WriteLine("\tNumber of item subgroups: {0}", data.ItemSubgroups.Count);
            Console.WriteLine("\tNumber of recipe categories: {0}", data.RecipeCategories.Count);
            Console.WriteLine("\tNumber of recipe subgroups: {0}", data.RecipeSubgroups.Count);
            Console.WriteLine("\tTotal: {0}", data.RecipeSubgroups.Count + data.Items.Count + data.Fluids.Count + data.Recipes.Count + data.Locale.Length + data.ItemCategories.Count + data.ItemSubgroups.Count + data.RecipeCategories.Count + data.RecipeSubgroups.Count);
            Console.WriteLine("");

            return data;
        }

        public static void LoadDeserializedData(DataSerializeClass inData)
        {
            Icons = inData.Icons;
            Items = inData.Items;
            Fluids = inData.Fluids;
            Locale = inData.Locale;
            Recipes = inData.Recipes;
            ItemCategories = inData.ItemCategories;
            ItemSubgroups = inData.ItemSubgroups;
            RecipeCategories = inData.RecipeCategories;
            RecipeSubgroups = inData.RecipeSubgroups;
        }

        public static void AddRecipe(Recipe inRecipe)
        {
            Recipes.Add(inRecipe);
        }

        public static void AddItem(string inItemName, Item inItem)
        {
            if (!Items.ContainsKey(inItemName))
            {
                Items.Add(inItemName, inItem);
            }
        }
        public static void AddFluid(string inFluidName, Fluid inFluid)
        {
            if (!Fluids.ContainsKey(inFluidName))
            {
                Fluids.Add(inFluidName, inFluid);
            }
        }
        public static void PrintItems()
        {
            if (File.Exists("items.txt"))
            {
                Console.WriteLine("File exists. Delete");
                File.Delete("items.txt");
            }
            Console.WriteLine("Create new file");
            using (StreamWriter sw = new StreamWriter("items.txt"))
            {
                var es = Items.Values.GetEnumerator();
                for(int i = 0; i < Items.Values.Count; ++i)
                {
                    es.MoveNext();
                    Item item = es.Current;
                    sw.WriteLine("#{0} - {1}", i, item.name);
                    sw.WriteLine("\tMade by:");
                    foreach (Recipe rec in item.MadeByRecipes)
                    {
                        sw.WriteLine("\t\t{0}", rec.name);
                    }
                    sw.WriteLine("\tUsed in:");
                    foreach (Recipe rec in item.UsedInRecipes)
                    {
                        sw.WriteLine("\t\t{0}", rec.name);
                    }
                }
                sw.WriteLine("");
            }
        }
        public static void PrintRecipes()
        {
            if (File.Exists("recipes.txt"))
            {
                Console.WriteLine("File exists. Delete");
                File.Delete("recipes.txt");
            }
            Console.WriteLine("Create new file");
            using (StreamWriter sw = new StreamWriter("recipes.txt"))
            {
                Console.WriteLine("Start writing");
                for (int i = 0; i < Recipes.Count; ++i)
                {
                    sw.WriteLine("#{2} - {0} - E: {1}", Recipes[i].name, Recipes[i].energy_needed, i);
                    sw.WriteLine("\tIngredients:");
                    if (!Recipes[i].bExpensive)
                    {
                        IEnumerator<Intermediate> e1 = Recipes[i].ingredients[0].GetEnumerator();
                        while (e1.MoveNext())
                        {
                            sw.WriteLine("\t\t{0} - {1} - {2}", e1.Current.itype, e1.Current.name, e1.Current.amount);
                        }
                        if (Recipes[i].results[0].Count == 1)
                        {
                            sw.WriteLine("\tResult:");
                            IEnumerator<Intermediate> e2 = Recipes[i].results[0].GetEnumerator();
                            while (e2.MoveNext())
                            {
                                sw.WriteLine("\t\t{0} - {1} - {2}", e2.Current.itype, e2.Current.name, e2.Current.amount);
                            }
                        }
                        else
                        {
                            sw.WriteLine("\tResults:");
                            IEnumerator<Intermediate> e2 = Recipes[i].results[0].GetEnumerator();
                            while (e2.MoveNext())
                            {
                                sw.WriteLine("\t\t{0} - {1} - {2}", e2.Current.itype, e2.Current.name, e2.Current.amount);
                            }
                        }
                    }
                    else
                    {
                        sw.WriteLine("\t\tNormal:");
                        IEnumerator<Intermediate> e1 = Recipes[i].ingredients[0].GetEnumerator();
                        while (e1.MoveNext())
                        {
                            sw.WriteLine("\t\t\t{0} - {1} - {2}", e1.Current.itype, e1.Current.name, e1.Current.amount);
                        }

                        sw.WriteLine("\t\tExpensive:");
                        e1 = Recipes[i].ingredients[1].GetEnumerator();
                        while (e1.MoveNext())
                        {
                            sw.WriteLine("\t\t\t{0} - {1} - {2}", e1.Current.itype, e1.Current.name, e1.Current.amount);
                        }
                        sw.WriteLine("\tResults:");
                        sw.WriteLine("\t\tNormal:");
                        IEnumerator<Intermediate> e2 = Recipes[i].results[0].GetEnumerator();
                        while (e2.MoveNext())
                        {
                            sw.WriteLine("\t\t\t{0} - {1} - {2}", e2.Current.itype, e2.Current.name, e2.Current.amount);
                        }

                        sw.WriteLine("\t\tExpensive:");
                        e2 = Recipes[i].results[1].GetEnumerator();
                        while (e2.MoveNext())
                        {
                            sw.WriteLine("\t\t\t{0} - {1} - {2}", e2.Current.itype, e2.Current.name, e2.Current.amount);
                        }
                    }
                    sw.WriteLine("");
                }
                Console.WriteLine("Writing to file complete");
                sw.Close();
                Console.WriteLine("File closed successfully");
            }
        }
    }
}
