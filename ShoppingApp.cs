using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            int sleepTimer = 750;
            Screen display = Screen.Menu;
            Console.SetWindowSize(70, 40);

            List<string> quitCommands = new List<string>
            { "C", "CHECKOUT", "QUIT", "CHECK" };

            List<string> cartCommands = new List<string>
            { "S", "SHOW", "CART", "SHOW CART" };

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
                        //user input a number for the item they want, and a quantity of that item
                        cart.AddMultipleItems(itemNumber, quantity);
                    }
                    else if (input.Split(" ").Length == 3 && cart.ContainsMenuItem((input.Split(" ")[0] + " " + input.Split(" ")[1])) && int.TryParse(input.Split(" ")[2], out int amount) && amount > 0)
                    {
                        //user input a name for the item they want, and a quantity.
                        cart.AddMultipleItems((input.Split(" ")[0] + " " + input.Split(" ")[1]), amount);
                    }
                    else if (cart.ContainsMenuItem(input))
                    {
                        //user input a name for the item they want.
                        cart.AddItem(input);
                    }
                    else if (quitCommands.Contains(input.ToUpper()))
                    {
                        //user wants to quit
                        keepgoing = false;
                    }
                    else if (cartCommands.Contains(input.ToUpper()))
                    {
                        //user wants to see their cart
                        display = Screen.Cart;
                    }
                    else
                    {
                        //invalid input was entered
                        Console.WriteLine("Sorry, that is unavailable. Please try again.");
                        Thread.Sleep(sleepTimer);
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
                    else if (quitCommands.Contains(input))
                    {
                        //user wants to quit
                        keepgoing = false;
                    }
                    else
                    {
                        //invalid input received.
                        Console.WriteLine("Sorry, that is not a valid choice. Please try again.");
                        Thread.Sleep(sleepTimer);
                    }
                }
            }
            //now that the user is done with their shopping, display their cart with statistics
            cart.ShowCartWithStatistics();
            Console.WriteLine("Thank you for your order!");
        }
    }
}
