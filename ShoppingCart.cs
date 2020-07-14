using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Globalization;

namespace DB4_Shopping
{
    class ShoppingCart
    {
        private Dictionary<string, decimal> menuItems; //decimal for money items
        private ArrayList cartItems;
        private ArrayList cartQuantities;
        private ArrayList cartItemCosts; //decimal for money items
        private const int WIDTH = 16;
        private const int WIDTH_TWO = 20;
        private const char HEADER = '=';
        private decimal totalPrice;
        private decimal averagePrice;
        private decimal lowestPrice;
        private decimal highestPrice;
        private int quantitiesTotal;
        private int sleepTimerOne = 750;
        private int sleepTimerTwo = 1000;

        public int MenuItemCount { get; set; }

        public ShoppingCart()
        {
            //add menu items to the dictionary, and initialize the ArrayLists.
            menuItems = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
            {
                { "Iron Ore", 100.0m },
                { "Copper Ore", 49.50m },
                { "Mithril Ore", 500.0m },
                { "Silver Ore", 400.99m },
                { "Gold Ore", 675.0m },
                { "Apple Pie", 11.25m },
                { "Pumpkin Pie", 11.0m },
                { "Pound Cake", 11.50m },
                { "Warlock Hat", 1000.0m },
                { "Toilet Paper", 1999.99m },
                { "Unobtanium Ore", 1999999.99m }
            };
            cartItems = new ArrayList();
            cartQuantities = new ArrayList();
            cartItemCosts = new ArrayList();
            MenuItemCount = menuItems.Count;
        }

        public void AddItem(string itemName)
        {
            //check to see if the item is in the cart already
            //if so add 1 to the quantity for it, otherwise add it.
            //also ensures that if the user entered the name in a different case
            //that it is converted to the same case the dictionary uses.
            itemName = (new CultureInfo("en-Us", false)).TextInfo.ToTitleCase(itemName);

            if (cartItems.Contains(itemName))
            {
                cartQuantities[cartItems.IndexOf(itemName)] = (int)cartQuantities[cartItems.IndexOf(itemName)] + 1;
            }
            else
            {
                cartItems.Add(itemName);
                cartQuantities.Add(1);
                cartItemCosts.Add(menuItems[itemName]);
            }
            Console.WriteLine($"Adding {itemName} to cart at {menuItems[itemName]:c}");
            Thread.Sleep(sleepTimerOne);
        }

        public void AddItem(int itemNumber)
        {
            //add an item based on the number selection for the item.
            AddItem(menuItems.ElementAt(itemNumber-1).Key);
        }

        public void AddMultipleItems(int itemNumber, int quantity)
        {
            //add multiple items based on the number selection and quantity given.
            AddMultipleItems(menuItems.ElementAt(itemNumber - 1).Key, quantity);
        }

        public void AddMultipleItems(string itemName, int quantity)
        {
            //add multiple items, making sure to properly change quantity.
            //or add the item if it isn't already in the cart.
            //also ensures that if the user entered the name in a different case
            //that it is converted to the same case the dictionary uses.
            itemName = (new CultureInfo("en-Us", false)).TextInfo.ToTitleCase(itemName);

            for (int added = 1; added <= quantity; added++)
            {
                if (cartItems.Contains(itemName))
                {
                    cartQuantities[cartItems.IndexOf(itemName)] = (int)cartQuantities[cartItems.IndexOf(itemName)] + 1;
                }
                else
                {
                    cartItems.Add(itemName);
                    cartQuantities.Add(1);
                    cartItemCosts.Add(menuItems[itemName]);
                }
            }
            Console.WriteLine($"Adding {quantity:#,#.#} {itemName} to cart at {menuItems[itemName]:c} each.");
            Console.WriteLine($"Total amount added: {menuItems[itemName]*quantity:c}");
            Thread.Sleep(sleepTimerTwo);
        }

        public void ShowMenu()
        {

            //clear the console and remove the cursor to improve performance.
            Console.Clear();
            Console.CursorVisible = false;

            string title = " Available Menu Items ";
            int spacer = ((WIDTH * 2) + 5 - title.Length);
            string divider = $"|{new string(HEADER, WIDTH + 3)}={new string(HEADER, WIDTH+1)}|";

            Console.WriteLine(divider); 
            Console.WriteLine($"|{new string(HEADER, 1+ (int)Math.Floor((double)spacer / 2))}" +
                $"{title}{new string(HEADER, spacer / 2)}|");
            Console.WriteLine(divider);


            //iterate over the items in the dictionary and write them out, formatted.
            for (int index = 0; index < menuItems.Count; index++)
            {
                KeyValuePair<string, decimal> item = menuItems.ElementAt(index);
                Console.WriteLine($"|{index + 1,2}. {item.Key,WIDTH}|{item.Value,WIDTH:C}|");
            }

            Console.WriteLine(divider);
            Console.CursorVisible = true;
        }

        public bool ContainsMenuItem(string itemName)
        {
            return menuItems.ContainsKey(itemName);
        }

        public void MenuOptions()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("Enter an item, it's number, (S)how cart, or (C)heckout");
            Console.Write("> ");
        }

        public void CartOptions()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("(M)enu or (C)heckout");
            Console.Write("> ");
        }

        public void CalculateStatistics()
        {
            quantitiesTotal = 0;
            totalPrice = 0.0m;
            lowestPrice = decimal.MaxValue;
            highestPrice = decimal.MinValue;

            for (int index = 0; index < cartItems.Count; index++)
            {
                totalPrice += (decimal)cartItemCosts[index] * (int)cartQuantities[index];

                quantitiesTotal += (int)cartQuantities[index];
                if ((decimal)cartItemCosts[index] <= lowestPrice)
                {
                    lowestPrice = (decimal)cartItemCosts[index];
                }
                if ((decimal)cartItemCosts[index] >= highestPrice)
                {
                    highestPrice = (decimal)cartItemCosts[index];
                }
            }

            //if there are items in the ArrayLists compute the final statistics
            //otherwise set them to 0.
            if (cartItems.Count > 0)
            {
                averagePrice = totalPrice/quantitiesTotal;
            }
            else
            {
                averagePrice = 0.0m;
                highestPrice = 0.0m;
                lowestPrice = 0.0m;
            }
        }

        public void ShowCart()
        {
            string divider = $"|{new string(HEADER, WIDTH_TWO)}|{new string(HEADER, WIDTH_TWO)}|" +
                $"{new string(HEADER, WIDTH_TWO)}|";

            Console.Clear();
            Console.CursorVisible = false;

            Console.WriteLine(divider);
            Console.WriteLine($"|{"Item",WIDTH_TWO}|{"Quantity",WIDTH_TWO}|{"Cost Per Each",WIDTH_TWO}|");
            Console.WriteLine(divider);

            //iterate over all items in the cart and display the information, formatted.
            for (int index = 0; index < cartItems.Count; index++)
            {
                Console.WriteLine($"|{(index + 1),2}: {cartItems[index],WIDTH_TWO - 4}|" +
                    $"{cartQuantities[index],WIDTH_TWO:#,#.#}|{cartItemCosts[index],WIDTH_TWO:c}|");
            }

            Console.WriteLine(divider);
            Console.CursorVisible = true;
        }

        public void ShowCartWithStatistics()
        {
            ShowCart();
            ShowCartStatistics();
        }

        private void ShowCartStatistics()
        {
            //should only be called after show cart, and is thus private.
            //Adds the statistics sections to the bottom of the cart.
            string divider = $"|{new string(HEADER, WIDTH_TWO)}|{new string(HEADER, WIDTH_TWO)}|" +
                $"{new string(HEADER, WIDTH_TWO)}|";

            CalculateStatistics();

            Console.CursorVisible = false;
            Console.WriteLine($"|{"Unique Items",WIDTH_TWO}|{"Total Items",WIDTH_TWO}|{"Total Cost",WIDTH_TWO}|");
            Console.WriteLine($"|{cartItems.Count,WIDTH_TWO:#,#.#}|{quantitiesTotal,WIDTH_TWO:#,#.#}|{totalPrice,WIDTH_TWO:c}|");
            Console.WriteLine(divider);
            Console.WriteLine($"|{"High Price",WIDTH_TWO}|{"Low Price",WIDTH_TWO}|{"Avg Price Each",WIDTH_TWO}|");
            Console.WriteLine($"|{highestPrice,WIDTH_TWO:c}|{lowestPrice,WIDTH_TWO:c}|{averagePrice,WIDTH_TWO:c}|");
            Console.WriteLine(divider);
            Console.CursorVisible = true;
        }
    }
}
