using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mHowald_classes_assignment
{
    abstract class appliance
    {
        private long itemNumber { get; set; }
        private string brand { get; set; }
        private int quantity { get; set; }
        private double wattage { get; set; }
        private string color { get; set; }
        private double price { get; set; }


        protected appliance()
        {
            
        }
        public appliance(long itemNumber, string brand, int quantity, double wattage, string color, double price)
        {
            this.itemNumber = itemNumber;
            this.brand = brand;
            this.quantity = quantity;
            this.wattage = wattage;
            this.color = color;
            this.price = price;
        }

        public override string ToString()
        {
            return $"ItemNumber: {itemNumber}\nBrand: {brand}\nQuantity: {quantity}\nWattage: {wattage}\nColor: {color}\nPrice: {price}";
        }
    }
}
