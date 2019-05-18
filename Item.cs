using System;
using System.Collections.Generic;

namespace devProj
{
    public class Item
    {
        // <---------- Object Properties ---------->
        public int number { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal tax { get; set; }

        // Creates base tax of 0 when class is instantiated
        public Item()
        {
            tax = Convert.ToDecimal(0.00);
        }
    }

    public class ImportedItem : Item
    {
        
        // <---------- Object Properties ---------->
        // Applies 5% Import Tax
        public ImportedItem(Item item, Receipt receipt)
        {
            this.number = item.number;
            this.name = item.name;
            this.price = item.price;

            // Updates addItem.tax by using the MidpointRounding.AwayFromZero to round up, to two decimal places due to currency.
            this.tax = Math.Ceiling(Decimal.Multiply(item.price, Convert.ToDecimal(0.05))*20)/20;

            // Updates price
            this.price += this.tax;

            // Updates Receipt object's taxTotal and priceTotal with new tax and price
            receipt.taxTotal += this.tax;
            receipt.priceTotal += this.price;
        }
    }

    public class TaxedItem : Item
    {
        // <---------- Object Properties ---------->
        // Applies 10% Sales tax
        public TaxedItem(Item item, Receipt receipt)
        {
            this.number = item.number;
            this.name = item.name;
            this.price = item.price;

            // Updates addItem.tax by using the MidpointRounding.AwayFromZero to round up, to two decimal places due to currency.
            this.tax = Math.Ceiling(Decimal.Multiply(this.price, Convert.ToDecimal(0.10))*20)/20;

            // Updates price
            this.price += this.tax;
            
            // Updates Receipt object's taxTotal and priceTotal with new tax and price
            receipt.taxTotal += this.tax;
            receipt.priceTotal += this.price;
        }
    }

    public class ImportedTaxedItem : Item
    {
        // <---------- Object Properties ---------->
        // Applies 10% Sales Tax and 5% Import Tax
        public ImportedTaxedItem(Item item, Receipt receipt)
        {
            this.number = item.number;
            this.name = item.name;
            this.price = item.price;
            
            // Updates addItem.tax by using the MidpointRounding.AwayFromZero to round up, to two decimal places due to currency.
            this.tax = Math.Ceiling(Decimal.Multiply(this.price, Convert.ToDecimal(0.15))*20)/20;

            // Updates price
            this.price += this.tax;

            // Updates Receipt object's taxTotal and priceTotal with new tax and price
            receipt.taxTotal += this.tax;
            receipt.priceTotal += this.price;
        }
    }

    public class TaxableItems
    {
        // <---------- Object Properties ---------->
        public List<List<string>> taxableItems {get; set;}
        public TaxableItems()
        {
            // Creates non-taxed item categorys -- list can be added to at any point
            var books = new List<String>()
            {
                "Book"
            };
            var food = new List<String>()
            {
                "Chocolate"
            };
            var medical = new List<String>()
            {
                "Pills"
            };

            // List of categories
            var taxedItemList = new List<List<String>>()
            {
                books,
                food,
                medical
            };

            // Updates property
            this.taxableItems = taxedItemList;
        }
    }
}