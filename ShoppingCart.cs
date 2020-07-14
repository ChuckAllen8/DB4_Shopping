using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using System.Threading;

namespace DB4_Shopping
{
    class ShoppingCart
    {
        //Exercise Work

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

        public int MenuItemCount { get; set; }

        public ShoppingCart()
        {
            menuItems = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
            {
                { "Iron Ore", 100.0m },
                { "Copper Ore", 50.0m },
                { "Mithril Ore", 500.0m },
                { "Silver Ore", 400.0m },
                { "Gold Ore", 600.0m },
                { "Apple Pie", 10.0m },
                { "Pumpkin Pie", 11.0m },
                { "Pound Cake", 15.0m },
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
            if(cartItems.Contains(itemName))
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
            Thread.Sleep(750);
        }

        public void AddItem(int itemNumber)
        {
            AddItem(menuItems.ElementAt(itemNumber-1).Key);
        }

        public void AddMultipleItems(int itemNumber, int quantity)
        {
            AddMultipleItems(menuItems.ElementAt(itemNumber - 1).Key, quantity);
        }

        public void AddMultipleItems(string itemName, int quantity)
        {
            for(int added = 1; added <= quantity; added++)
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
            Console.WriteLine($"Adding {quantity} {itemName} to cart at {menuItems[itemName]:c} each.");
            Console.WriteLine($"Total amount added: {menuItems[itemName]*quantity:c}");
            Thread.Sleep(1000);
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.CursorVisible = false;

            string title = " Available Menu Items ";
            int spacer = ((WIDTH * 2) + 5 - title.Length);
            string divider = $"|{new string(HEADER, WIDTH + 3)}={new string(HEADER, WIDTH+1)}|";

            Console.WriteLine(divider); 
            Console.WriteLine($"|{new string(HEADER, 1+ (int)Math.Floor((double)spacer / 2))}" +
                $"{title}{new string(HEADER, spacer / 2)}|");
            Console.WriteLine(divider);

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

            for (int index = 0; index < cartItems.Count; index++)
            {
                Console.WriteLine($"|{(index + 1),2}: {cartItems[index],WIDTH_TWO - 4}|" +
                    $"{cartQuantities[index],WIDTH_TWO}|{cartItemCosts[index],WIDTH_TWO:c}|");
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
            string divider = $"|{new string(HEADER, WIDTH_TWO)}|{new string(HEADER, WIDTH_TWO)}|" +
                $"{new string(HEADER, WIDTH_TWO)}|";

            CalculateStatistics();

            Console.CursorVisible = false;
            Console.WriteLine($"|{"Unique Items",WIDTH_TWO}|{"Total Items",WIDTH_TWO}|{"Total Cost",WIDTH_TWO}|");
            Console.WriteLine($"|{cartItems.Count,WIDTH_TWO}|{quantitiesTotal,WIDTH_TWO}|{totalPrice,WIDTH_TWO:c}|");
            Console.WriteLine(divider);
            Console.WriteLine($"|{"High Price",WIDTH_TWO}|{"Low Price",WIDTH_TWO}|{"Avg Price Each",WIDTH_TWO}|");
            Console.WriteLine($"|{highestPrice,WIDTH_TWO:c}|{lowestPrice,WIDTH_TWO:c}|{averagePrice,WIDTH_TWO:c}|");
            Console.WriteLine(divider);
            Console.CursorVisible = true;
        }
    }
}
