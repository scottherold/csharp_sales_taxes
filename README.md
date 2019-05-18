# csharp_sales_taxes
A simple console application using C# that takes in a list of items, and logs the items in receipt format.

The console application is built on the method processReceipt(), which takes in an array of strings of any length. The strings follow a
certain designated item syntax of [{itemNumber} {itemName} at {itemPrice}]. Within the method, a new Receipt object and a List object of
Item objects are instatiated. The Receipt object has itemList, taxTotal and priceTotal properties. The method then iterates through the 
array of strings passed as arguments.

Within the loop, the method instansiates a new Item object during each iteration. The parent item object has number, name, price and tax 
properties. The number property is assigned, by using the string.Split() method and casting the string as an integer. The name
property is assigned by using the custom global method parseInput, which takes in three strings as parameters -- a string to be spliced,
a starting string and an ending string; the function then converts the argument passed as the string to be spliced into an array of
substrings, splicing the index between the start and end index variables. The price property is assigned by using the String.Split()
and Enumerable.Last() methods on the current string of the current iteration. The tax property is more complex, as it requires the string 
to be spliced and passed as an argument to the processTaxes() method of the Receipt along with the current item and the List object of 
Item objects instantiated in the processReceipt() method.

The processTaxes() method instantiates a TaxableItems object -- this object is a dynamic List object of Lists of String objects. Each
of these seperate lists is broken down into categories that are not taxed (Books, Food and Medical Items). The Taxable Items object
can be updated at any time to include new items that fall under the parent categories. The processTaxes() method then creates a Boolean
varaible to determine the taxed status. 

Next, processTaxes() uses the duplicte() method, which takes the Item object and the List object of Item objects passed as arguments to 
the processTaxes() method, as well as the Receipt object, and determines if there is an Item object with the same name property as the 
passed Item object. If there is, the Item object within the Receipt object passed is updated by adding the passed Item's tax, number and 
price; the Receipt object's total taxTotal and priceTotal properties are also updated, and the method returns true. Otherwise, the method 
returns false, to which the processTaxes() method continues.

Next, the processTaxes() method checks the passed Item object's name property against the strings within the TaxableItems class using the 
String.Contains() and String.ToUpper() methods to encompass all potential cases. If the passed Item object's name property does not 
contain one of the strings in TaxableItems, the processTaxes() method uses a boolean to determine if the string passed as an argument 
for the importCheck parameter is "Imported"; if the strings match, the Item object is cast as the child Item object ImportedItem, which 
takes in the Item object paassed into processTaxes() and the current Receipt object. The ImportedItem Object polymorphs the tax property 
to the sales price multiplied by 0.05 and rounded up using the Math.Ceiling() method, and aupdates the passed Receipt object taxTotal and
priceTotal properties accordingly and adds the item to the List object passed as an argument to processTaxes(), and finally updates the
taxed variable to false. Otherwise, the Item object passed into the processTaxes() method is added to the List object passed as an
argument, and the parent Receipt object's priceTotal property is updated with the passed Item object's price property.

If the Item object passed into processTaxes() has a name property that does not contain one of the strings from the TaxableItems object,
processTaxes() will search the Item object's name property for the string "Imported" as detailed above. If the Item object's name
property contains "Imported", the Item object is cast as a ImportedTaxedItem child of the Item object and added to the passed List of
Item objects as detailed with the ImportedItem object above; the difference is that Item Objects prices is polymorphed using 0.15
instead of 0.05 -- the passed Receipt object's taxTotal and priceTotal are also updated similarly to that of when an Item object is
cast as an ImportedItem child. Otherwise, the Item object is cast as a TaxedItem child of the Item object and added to the passed List 
of Item objects, as detailed with ImportedItem and ImportedTaxedItem children; the parent Receipt object is again updated in the same
manner.

Finally, if no criteria are met, or the item is a duplicate, the function is returned.

In the processReceipt() method, the instansiated List of Item objects is added to the instantiated Receipt object as the propery
itemList. The method ends by iterating through each object in the instantiated Receipt object's itemList and prints them using
Console.WriteLine() depending on the item's count. The method ends by printing and formatting the Receipt object's taxTotal and 
priceTotal properties.
