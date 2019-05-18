using System;
using System.Collections.Generic;

namespace devProj
{
    public class Receipt
    {
        // <---------- Model Properties ---------->
        public List<Item> itemList { get; set; }
        public decimal taxTotal { get; set; }
        public decimal priceTotal { get; set; }
        public void processTaxes(Item item, List<Item> addItemList, string importCheck)
        {
            // <---------- Event Handlers for Import/Tax ---------->

            // Create taxCheck object to search lists for taxable items to compare to inputs
            var taxCheck = new TaxableItems();
            
            // Tax/Import variables
            bool taxed = true;

            // Duplicate check
            if (!duplicate(item, addItemList, this))
            {
                // Tax loop
                foreach (var list in taxCheck.taxableItems)
                {
                    foreach (var taxItem in list) 
                    {
                        // Checks to see if the object is in the taxCheck list object
                        if (item.name.ToUpper().Contains(taxItem.ToUpper()))
                        {
                            // Check to see if 'Imported' exists
                            if (importCheck == "Imported")
                            {
                                // Casts addItem to ImportedItem and adds it to addItemList
                                addItemList.Add(new ImportedItem(item, this));
                                taxed = false;
                            }
                            else if (importCheck != "Imported")
                            {
                                // Adds based Item to addItemList
                                addItemList.Add(item);

                                // Updates Receipt totalPrice with the item's price
                                this.priceTotal += item.price;
                                taxed = false;
                            }
                        }
                    }
                }
                if (taxed)
                {
                    // Check to see if 'Imported' exists
                    if (importCheck == "Imported")
                    {
                        // Casts addItem to ImportedTaxedItem and adds it to addItemList
                        addItemList.Add(new ImportedTaxedItem(item, this));
                    }
                    else
                    {
                        // Add TaxedItem
                        addItemList.Add(new TaxedItem(item, this));
                    }
                }
            }
            else
            {
                return;
            }
        }

        public static bool duplicate(Item dupeItem, List<Item> itemList, Receipt receipt)
        {
            // Checks to see if there are any items in the list
            if (itemList.Count > 0)
            {
                // Loops through item list to see if there is a duplicate
                foreach (var item in itemList)
                {
                    // Updates item instance
                    if (item.name == dupeItem.name) {
                        // Update Receipt object taxTotal and priceTotal
                        receipt.taxTotal += item.tax/item.number;
                        receipt.priceTotal += item.price;

                        // Update Item in list
                        item.tax += item.tax/item.number;
                        item.number += dupeItem.number;
                        item.price += dupeItem.price+(item.tax/item.number);
                        return true;
                    }
                }
                // Not found, return false
                return false;
            }
            // No items on list, return false
            return false;
        }
    }
}