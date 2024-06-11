using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
     class Refrigerator : Appliance
    {
        private int doors;
        private int height;
        private int width;

        public int Doors { get => doors; set => doors = value; }
        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }

        public Refrigerator()
        {

        }

        public Refrigerator(long itemNumber, string brand, int quantity, double wattage, string color, double price, int doors, int height, int width) : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.doors = doors;
            this.height = height;
            this.width = width;

        }

        public override string ToString()
        {
            string doorNum;
            if (doors == 2)
            {
                doorNum = "Double Doors";
            }
            else if (doors == 3)
            {
                doorNum = "Three Doors";
            }
            else if (doors == 4)
            {
                doorNum = "Four Doors";
            }
            else
            {
                doorNum = doors + "Doors";
            }

            return $"{base.ToString()}\nNumber of Doors: {doorNum}\nHeight: {height}\nWidth: {width}";



        }
    }
}
