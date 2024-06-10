using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mHowald_classes_assignment
{
    internal class microwave : appliance
    {
        private double capacity { get; set; }
        private string roomType { get; set; }

        public microwave()
        {
            
        }
        public microwave(long itemNumber, string brand, int quantity, double wattage, string color, double price, double capacity, string roomType) : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.capacity = capacity;
            this.roomType = roomType;
        }

        public override string ToString()
        {
            string roomDescription;
            if (roomType == "K")
            {
                roomDescription = "Kitchen";
            }
            else if (roomType == "W")
            {
                roomDescription = "Work Site";
            }
            else
            {
                roomDescription = roomType;
            }
            return $"{base.ToString()}\nCapacity: {capacity}\nRoom Type: {roomDescription}";
        }
    }
}
