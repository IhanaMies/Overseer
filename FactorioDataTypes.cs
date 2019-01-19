using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Overseer
{
    [Serializable]
    public abstract class Base
    {
        public string itype = "";
        public string name = "";
        public string localised_name = "";
        public string category = "";
        public string subgroup = "";
        public string iconPath = "";
        public Image icon = null;
    }

    [Serializable]
    public class Intermediate : Base
    {
        public int amount = 1;
        public Intermediate() { }
    }

    [Serializable]
    public class Recipe : Base
    {
        public float energy_needed = 0.5f;
        public bool bEnabled = true;
        public bool bExpensive = false;
        public List<Intermediate>[] ingredients = new List<Intermediate>[2];

        public List<Intermediate>[] results = new List<Intermediate>[2];

        public Recipe()
        {
            ingredients[0] = new List<Intermediate>();
            ingredients[1] = new List<Intermediate>();

            results[0] = new List<Intermediate>();
            results[1] = new List<Intermediate>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inName"></param>
        /// <param name="inNormalIngredients"></param>
        /// <param name="inExpensiveIngredients"></param>
        /// <param name="inNormalResults"></param>
        /// <param name="inExpensiveResults"></param>
        public Recipe(string inName, List<Intermediate> inNormalIngredients, List<Intermediate> inExpensiveIngredients, List<Intermediate> inNormalResults, List<Intermediate> inExpensiveResults)
        {
            name = inName;
            ingredients[0] = inNormalIngredients;
            ingredients[1] = inExpensiveIngredients;
            results[0] = inNormalResults;
            results[1] = inExpensiveResults;
        }
    }

    [Serializable]
    public class Item : Base
    {
        public List<Recipe> MadeByRecipes = new List<Recipe>();
        public List<Recipe> UsedInRecipes = new List<Recipe>();
        public Item() { }
    }

    [Serializable]
    public class Fluid : Base
    {
        public List<Recipe> MadeByRecipes = new List<Recipe>();
        public List<Recipe> UsedInRecipes = new List<Recipe>();
        public Fluid() { }
    }
}
