using System;
using System.Collections.Generic;
using System.Text;

namespace DB4_Shopping
{
    class ShoppingApp
    {
        private ShoppingCart cart;

        public ShoppingApp()
        {
            cart = new ShoppingCart();
        }

        public void Start()
        {
            cart.ShowMenu();
            cart.ShowCartWithStatistics();
        }
    }
}
