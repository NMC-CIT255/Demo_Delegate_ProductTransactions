using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Delegate_ProductTransactions
{
    class Program
    {
        //
        // declare the delegate
        //
        public delegate void ProductTransaction(Item item, int units);

        static void Main(string[] args)
        {
            IList<Item> inventory = new List<Item>();

            inventory = InitializeInventory();

            DisplayInventory(inventory);
            DisplayPerformTransactions(inventory);
            DisplayInventory(inventory);            
        }

        /// <summary>
        /// cycle through the list of products and sell items
        /// </summary>
        /// <param name="inventory">list of Items</param>
        private static void DisplayPerformTransactions(IList<Item> inventory)
        {
            //
            // instantiate objects
            //
            ProductTransaction sellNonPerishableProduct = new ProductTransaction(ProcessNonPerishableSale);
            ProductTransaction sellPerishableProduct = new ProductTransaction(ProcessPerishableSale);

            //
            // sell products
            //
            foreach (var item in inventory)
            {
                int unitsSold;

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Sell Products");
                Console.WriteLine();

                //
                // get number of units to sell and process based on item class
                //
                if (item is NonPerishable)
                {
                    NonPerishable nonPerishableItem = new NonPerishable();
                    nonPerishableItem = item as NonPerishable;
                    Console.Write($"Enter the number of {nonPerishableItem.ItemName} sold: ");
                    if (int.TryParse(Console.ReadLine(), out unitsSold))
                    {
                        sellNonPerishableProduct(item, unitsSold);
                    }
                }
                else if (item is Perishable)
                {
                    Perishable perishableItem = new Perishable();
                    perishableItem = item as Perishable;
                    Console.Write($"Enter the number of {perishableItem.ItemName} sold: ");
                    if (int.TryParse(Console.ReadLine(), out unitsSold))
                    {
                        sellPerishableProduct(item, unitsSold);
                    }
                }
                else
                {
                    Console.WriteLine("Item has no category.");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Inventory Complete");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// initialize the inventory with items
        /// </summary>
        /// <returns>List of Items</returns>
        public static IList<Item> InitializeInventory()
        {
            IList<Item> inventory = new List<Item>();

            Perishable perishableStockItem01 = new Perishable(Perishable.Name.Apples, 10);
            NonPerishable nonPerishableItem01 = new NonPerishable(NonPerishable.Name.Pots, 20);

            inventory.Add(perishableStockItem01);
            inventory.Add(nonPerishableItem01);

            return inventory;
        }

        /// <summary>
        /// display the inventory
        /// </summary>
        /// <param name="inventory">list of Items</param>
        public static void DisplayInventory(IList<Item> inventory)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Current Inventory");
            Console.WriteLine();

            Console.WriteLine("\tName\t\tInventory\t\tBackorder");

            foreach (var item in inventory)
            {
                if (item is Perishable)
                {
                    Perishable perishableItem = new Perishable();
                    perishableItem = item as Perishable;
                    Console.WriteLine($"\t{perishableItem.ItemName}\t\t{perishableItem.CurrentInventory}");
                }
                else if (item is NonPerishable)
                {
                    NonPerishable nonPerishableItem = new NonPerishable();
                    nonPerishableItem = item as NonPerishable;
                    Console.WriteLine($"\t{nonPerishableItem.ItemName}\t\t{nonPerishableItem.CurrentInventory}\t\t\t{nonPerishableItem.InventoryToOrder}");
                }
                else
                {
                    Console.WriteLine("Item has no category.");
                }
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// process perishable item sales
        /// </summary>
        /// <param name="item">item to sell</param>
        /// <param name="units">number to sell</param>
        public static void ProcessPerishableSale(Item item, int units)
        {
            Perishable perishableItem = new Perishable();
            perishableItem = item as Perishable;

            if (item.CurrentInventory >= units)
            {
                perishableItem.CurrentInventory -= units;
            }
            else
            {
                perishableItem.CurrentInventory = 0;
            }
        }

        /// <summary>
        /// process nonperishable item sales
        /// </summary>
        /// <param name="item">item to sell</param>
        /// <param name="units">number to sell</param>
        public static void ProcessNonPerishableSale(Item item, int units)
        {
            NonPerishable nonPerishableItem = new NonPerishable();
            nonPerishableItem = item as NonPerishable;
            if (item.CurrentInventory >= units)
            {
                nonPerishableItem.CurrentInventory -= units;
            }
            else
            {
                nonPerishableItem.CurrentInventory = 0;
                nonPerishableItem.InventoryToOrder = Math.Abs(nonPerishableItem.CurrentInventory - units);
            }
        }

        /// <summary>
        /// process all item transactions
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="item"></param>
        /// <param name="units"></param>
        public static void ProcessTransaction(ProductTransaction transaction, Item item, int units)
        {
            transaction(item, units);
        }
    }
}
