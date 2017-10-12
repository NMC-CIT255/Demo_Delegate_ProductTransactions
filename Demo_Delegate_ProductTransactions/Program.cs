using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Delegate_ProductTransactions
{
    class Program
    {
        public delegate void SellProduct(Item item, int units);

        static void Main(string[] args)
        {
            List<Item> inventory = new List<Item>();

            inventory = InitializeInventory();

            SellProduct sellPerishableItem = new SellProduct(ProcessPerishableSale);

            Console.ReadKey();
        }

        public List<Item> InitializeInventory()
        {
            List<Item> inventory = new List<Item>();

            Perishable perishableStockItem01 = new Perishable(Perishable.Name.Apples, 10);
            NonPerishable nonPerishableItem01 = new NonPerishable(NonPerishable.Name.Pots, 20);

            inventory.Add(perishableStockItem01);
            inventory.Add(nonPerishableItem01);

            return inventory;
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

            nonPerishableItem.CurrentInventory -= units;
            nonPerishableItem.InventoryToOrder += units;
        }

        public static void ProcessTransaction(SellProduct transaction, int units)
        {
            transaction(item, units);
        }
    }
}
