using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Delegate_ProductTransactions
{
    public class NonPerishable : Item
    {
        //
        // items to stock
        //
        public enum Name
        {
            Pots,
            Pans,
            Knives
        }

        public Name ItemName { get; set; }
        public int InventoryToOrder { get; set; }

        public NonPerishable()
        {

        }

        public NonPerishable(Name itemName, int currentInventory)
        {
            this.ItemName = itemName;
            this.CurrentInventory = currentInventory;
        }
    }
}
