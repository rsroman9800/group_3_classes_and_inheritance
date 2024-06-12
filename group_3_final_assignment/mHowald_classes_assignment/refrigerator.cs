using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mHowald_classes_assignment
{
    public class refrigerator : appliance
    {
        private int doors;
        private int height;
        private int width;

        public int Doors { get => doors; set => doors = value; }
        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }

        public refrigerator()
        {
            
        }
        public refrigerator(long itemNumber, string brand, int quantity, double wattage, string color, double price,int doors, int width, int height) :base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.doors = doors;
            this.width = width;
            this.height = height;
        }

        public override string ToString()
        {
            string doorDescription; // Changes the integer to a text based on the doors of the refrigerator so that it matches the project output
            if (doors == 2)
            {
                doorDescription = "Double Doors";
            }
            else if (doors == 3)
            {
                doorDescription = "Three Doors";
            }
            else if (doors == 4)
            {
                doorDescription = "Four Doors";
            }
            else
            {
                doorDescription = doors + " Doors";
            }

            return $"{base.ToString()}\nNumber of Doors: {doorDescription}\nHeight: {height}\nWidth: {width}";
        }
    }
}
