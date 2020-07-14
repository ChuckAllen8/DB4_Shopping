﻿using System;
using System.Threading;

namespace DB4_Shopping
{
    class ShoppingApp
    {
        private ShoppingCart cart;

        private enum Screen
        {
            Menu,
            Cart
        }


        public ShoppingApp()
        {
            cart = new ShoppingCart();
        }

        public void Start()
        {
            string input;
            bool keepgoing = true;
            Screen display = Screen.Menu;
            Console.SetWindowSize(70, 40);

            while (keepgoing)
            {
                if(display == Screen.Menu)
                {
                    cart.ShowMenu();
                    cart.MenuOptions();
                    input = Console.ReadLine();
                    if (int.TryParse(input, out int selection) && (selection >= 1 && selection <= cart.MenuItemCount))
                    {
                        //user input a number for the item they want
                        cart.AddItem(selection);
                    }
                    else if (input.Split(" ").Length == 2 && int.TryParse(input.Split(" ")[0], out int itemNumber) && int.TryParse(input.Split(" ")[1], out int quantity) && (itemNumber >= 1 && itemNumber <= cart.MenuItemCount) && quantity > 0)
                    {
                        //user input a number for the item they want
                        cart.AddMultipleItems(itemNumber, quantity);
                    }
                    else if (input.Split(" ").Length == 3 && cart.ContainsMenuItem((input.Split(" ")[0] + " " + input.Split(" ")[1])) && int.TryParse(input.Split(" ")[2], out int amount) && amount > 0)
                    {
                        //user input a number for the item they want
                        cart.AddMultipleItems((input.Split(" ")[0] + " " + input.Split(" ")[1]), amount);
                    }
                    else if (cart.ContainsMenuItem(input))
                    {
                        //user input the item they want directly
                        cart.AddItem(input);
                    }
                    else if (input.ToUpper() == "C" || input.ToUpper() == "CHECKOUT")
                    {
                        //user wants to quit
                        keepgoing = false;
                    }
                    else if (input.ToUpper() == "S" || input.ToUpper() == "SHOW" || input.ToUpper() == "CART" || input.ToUpper() == "SHOW CART")
                    {
                        display = Screen.Cart;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that is unavailable. Please try again.");
                        Thread.Sleep(750);
                    }
                }
                else
                {
                    cart.ShowCart();
                    cart.CartOptions();
                    input = Console.ReadLine().ToUpper();
                    if (input == "M" || input == "MENU")
                    {
                        //Switch back to the menu
                        display = Screen.Menu;
                    }
                    else if (input.ToUpper() == "C" || input.ToUpper() == "CHECKOUT")
                    {
                        //user wants to quit
                        keepgoing = false;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that is not a valid choice. Please try again.");
                        Thread.Sleep(750);
                    }
                }
            }
            cart.ShowCartWithStatistics();
            Console.WriteLine("Thank you for your order!");
        }
    }
}
