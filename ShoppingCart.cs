using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;

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
        private const char HEADER = '=';
        private decimal totalPrice;
        private decimal averagePrice;
        private decimal lowestPrice;
        private decimal highestPrice;
        private int quantitiesTotal;

        public ShoppingCart()
        {
            menuItems = new Dictionary<string, decimal>
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
        }

        public bool AddItem(string itemName)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
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

        public void ShowOptions()
        {
        }

        public int GetOption()
        {
            return 0;
        }

        public void CalculateStatistics()
        {
            quantitiesTotal = 0;
            totalPrice = 0.0m;
            averagePrice = 0.0m;
            lowestPrice = decimal.MaxValue;
            highestPrice = decimal.MinValue;

            for (int index = 0; index < cartItems.Count; index++)
            {
                totalPrice += (decimal)cartItemCosts[index] * (int)cartQuantities[index];
                averagePrice += (decimal)cartItemCosts[index];

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
                averagePrice /= cartItems.Count;
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
            string divider = $"|{new string(HEADER, WIDTH)}|{new string(HEADER, WIDTH)}|" +
                $"{new string(HEADER, WIDTH)}|";

            //Console.Clear();
            Console.CursorVisible = false;

            Console.WriteLine(divider);
            Console.WriteLine($"|{"Item",WIDTH}|{"Quantity",WIDTH}|{"Cost Per Item",WIDTH}|");
            Console.WriteLine(divider);

            for (int index = 0; index < cartItems.Count; index++)
            {
                Console.WriteLine($"|{(index + 1) + ":" + cartItems[index],WIDTH}|" +
                    $"{cartQuantities[index],WIDTH}|{cartItemCosts[index],WIDTH:c}|");
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
            string divider = $"|{new string(HEADER, WIDTH)}|{new string(HEADER, WIDTH)}|" +
                $"{new string(HEADER, WIDTH)}|";

            CalculateStatistics();

            Console.CursorVisible = false;
            Console.WriteLine($"|{"Number of Items",WIDTH}|{"Total Quantity",WIDTH}|{"Total Cost",WIDTH}|");
            Console.WriteLine($"|{cartItems.Count,WIDTH}|{quantitiesTotal,WIDTH}|{totalPrice,WIDTH:c}|");
            Console.WriteLine(divider);
            Console.WriteLine($"|{"Highest Price",WIDTH}|{"Lowest Price",WIDTH}|{"Average Price",WIDTH}|");
            Console.WriteLine($"|{highestPrice,WIDTH:c}|{lowestPrice,WIDTH:c}|{averagePrice,WIDTH:c}|");
            Console.WriteLine(divider);
            Console.CursorVisible = true;
        }
    }
}
