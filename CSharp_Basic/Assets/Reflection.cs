using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Markup;

namespace CSharp_Basic.Assets
{
    public static class Reflection
    {
        public static void ReflectionMain()
        {
            Console.WriteLine("ReflectionMain");

            //code
            Player player = new Player("용사", 100, 100);
            Type type = player.GetType();
            #region EX1
            // PropertyInfo[] propertyInfos = type.GetProperties();
            // foreach (var item in propertyInfos)
            //     Console.WriteLine($"{item.PropertyType}, {item.Name}");

            // MethodInfo[] methodInfos = type.GetMethods();
            // foreach (var method in methodInfos)
            //     Console.WriteLine($"{method.ReturnType}, {method.Name}");
            #endregion
            #region EX2
            string[] strings = { "itemId", "itemName", "desc" };
            string[] values = { "3", "sword", "basic sword" };

            ItemTable itemTable = new ItemTable();

            PropertyInfo[] propertyInfos = itemTable.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Console.WriteLine($"{propertyInfo.PropertyType}, {propertyInfo.Name}");

                for (int i = 0; i < strings.Length; i++)
                {
                    if (strings[i].ToLower() == propertyInfo.Name.ToLower())
                    {
                        if (propertyInfo.PropertyType == typeof(int))
                            propertyInfo.SetValue(itemTable, int.Parse(values[i]));
                        else
                            propertyInfo.SetValue(itemTable, values[i]);
                    }
                }
            }
            Console.WriteLine($"ItemId: {itemTable.itemId}, ItemName: {itemTable.itemName}, Desc: {itemTable.desc}");
            #endregion
        }
    }

    public class ItemTable
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string desc { get; set; }

    }
}