using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Delegate_ProductTransactions
{
    public class Perishable : Item
    {
        //
        // items to stock
        //
        public enum Name
        {
            Apples,
            Bananas,
            Strawberries
        }

        public Name ItemName { get; set; }

        public Perishable()
        {

        }

        public Perishable(Name itemName, int currentInventory)
        {
            this.ItemName = itemName;
            this.CurrentInventory = currentInventory;
        }
    }
}
