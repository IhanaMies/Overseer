using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Overseer
{
    [Serializable]
    public class DataSerializeClass
    {
        public SortedDictionary<string, Image> Icons = new SortedDictionary<string, Image>();
        public SortedDictionary<string, Item> Items = new SortedDictionary<string, Item>();
        public IDictionary<string, Fluid> Fluids = new Dictionary<string, Fluid>();

        //[items, recipes, entities]
        public IDictionary<string, string>[] Locale = new Dictionary<string, string>[5];

        public List<Recipe> Recipes = new List<Recipe>();

        public List<string> ItemCategories = new List<string>();
        public List<string> ItemSubgroups = new List<string>();
        public List<string> RecipeCategories = new List<string>();
        public List<string> RecipeSubgroups = new List<string>();
    }

    public static class Serialization
    {
        public static void Serialize(DataSerializeClass inData)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("data.dat", FileMode.CreateNew, FileAccess.Write);
            try
            {
                using (fs)
                {
                    bf.Serialize(fs, inData);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool Deserialize()
        {
            if (File.Exists("data.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream("data.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
                DataSerializeClass data = new DataSerializeClass();

                try
                {
                    using (fs)
                    {
                        data = (DataSerializeClass)bf.Deserialize(fs);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }
                Data.LoadDeserializedData(data);
                return true;
            }
            return false;
        }
    }
}
