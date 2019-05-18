using System;
using System.Collections.Generic;
using System.Linq;

namespace devProj
{
    class Program
    {
        public static string parseInput(string strSource, string strStart, string strEnd)
        {
            // Creates indeces
            int start = 0;
            int end = 0;

            // Checks for proper values to run function
            // Both strStart & strEnd
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                // Sets indeces for substring
                start = strSource.IndexOf(strStart, 0) + strStart.Length;
                end = strSource.IndexOf(strEnd, start);

                // Splices string for requested substring
                return strSource.Substring(start, end - start);
            }
            else
            {
                return "";
            }
        }
        static Receipt processReceipt(params string[] items)
        {
            // create variables
            var newReceipt = new Receipt();
            var addItemList = new List<Item>();
            for(int i = 0; i < items.Length; i++)
            {
                // Creates a new item to add to the list
                Item addItem = new Item();

                // Splice num of items
                string[] itemNum = items[i].Split(' ');
                addItem.number = Convert.ToInt32(itemNum[0]);

                // Splices item name
                addItem.name = Convert.ToString(parseInput(items[i], itemNum[0], " at")).TrimStart(' ');

                // Splice price
                addItem.price = Convert.ToDecimal(items[i].Split(' ').Last());

                // // <---------- Event Handlers for Import/Tax ---------->
                string[] importCheck = items[i].Split(' ');
                newReceipt.processTaxes(addItem, addItemList, importCheck[1]);

            }
            // Console.WriteLine values as requested
            newReceipt.itemList = addItemList;
            foreach (var i in newReceipt.itemList) 
            {
                // Displays individual prices for items with multiple purchases
                if (i.number > 1)
                {
                    Console.WriteLine($"{i.name}: {i.price} ({i.number} @ {i.price/i.number})");
                }
                else
                {
                    Console.WriteLine($"{i.name}: {i.price}");
                }
            }
            Console.WriteLine(new string('-', 10));
            Console.WriteLine($"Sales Taxes: {newReceipt.taxTotal.ToString("F")}");
            Console.WriteLine($"Total: {newReceipt.priceTotal.ToString("F")}");
            Console.WriteLine("");

            return newReceipt;
        }
        static void Main(string[] args)
        {
            processReceipt("1 Book at 12.49", "1 Book at 12.49", "1 Music CD at 14.99", "1 Chocolate Bar at 0.85");
            processReceipt("1 Imported box of chocolates at 10.00", "1 Imported bottle of perfume at 47.50");
            processReceipt("1 Imported bottle of perfume at 27.99", "1 Bottle of perfume at 18.99", "1 Packet of headache pills at 9.75", "1 Imported box of chocolates at 11.25", "1 Imported box of chocolates at 11.25");
        }
    }
}
