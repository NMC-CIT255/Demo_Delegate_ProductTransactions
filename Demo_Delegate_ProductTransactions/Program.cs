using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Delegate_ProductTransactions
{
    class Program
    {
        public delegate void ProductTransaction(Item item, int units);

        static void Main(string[] args)
        {
            IList<Item> inventory = new List<Item>();

            inventory = InitializeInventory();

            DisplayInventory(inventory);
            DisplayPerformTransactions(inventory);
            DisplayInventory(inventory);

            Console.ReadKey();
        }

        private static void DisplayPerformTransactions(IList<Item> inventory)
        {
            ProductTransaction sellNonPerishableProduct = new ProductTransaction(ProcessNonPerishableSale);
            ProductTransaction sellPerishableProduct = new ProductTransaction(ProcessPerishableSale);

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Sell Products");
            Console.WriteLine();

            //
            // sell products
            //
            foreach (var item in inventory)
            {
                if (item is NonPerishable)
                {
                    NonPerishable nonPerishableItem = new NonPerishable();
                    nonPerishableItem = item as NonPerishable;
                    sellNonPerishableProduct(item, 2);
                }
                else if (item is Perishable)
                {
                    NonPerishable nonPerishableItem = new NonPerishable();
                    nonPerishableItem = item as NonPerishable;
                    sellPerishableProduct(item, 5);
                }
                else
                {
                    Console.WriteLine("Item has no category.");
                }
            }
            
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        public static IList<Item> InitializeInventory()
        {
            IList<Item> inventory = new List<Item>();

            Perishable perishableStockItem01 = new Perishable(Perishable.Name.Apples, 10);
            NonPerishable nonPerishableItem01 = new NonPerishable(NonPerishable.Name.Pots, 20);

            inventory.Add(perishableStockItem01);
            inventory.Add(nonPerishableItem01);

            return inventory;
        }

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

        public static void ProcessPerishableSale(Item item, int units)
        {
            Perishable perishableItem = new Perishable();
            perishableItem = item as Perishable;

            perishableItem.CurrentInventory -= units;
        }

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

        public static void ProcessTransaction(ProductTransaction transaction, Item item, int units)
        {
            transaction(item, units);
        }
    }
}
