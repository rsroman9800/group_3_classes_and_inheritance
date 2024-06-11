using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
     class Vaccum : Appliance
    {
        private string grade;
        private int batteryVoltage;

        public string Grade { get => grade; set => grade = value; }
        public int BatteryVoltage { get => batteryVoltage; set => batteryVoltage = value; }

        public Vaccum()
        {

        }

        public Vaccum(long itemNumber, string brand, int quantity, double wattage, string color, double price, string grade, int batteryVoltage) : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.grade = grade;
            this.batteryVoltage = batteryVoltage;

        }

        public override string ToString()
        {
            string voltVal;
            if (batteryVoltage == 18)
            {
                voltVal = "Low";
            }
            else if (batteryVoltage == 24)
            {
                voltVal = "High";
            }
            else
            {
                voltVal = batteryVoltage + "V";
            }

            return $"{base.ToString()}\nGrade: {grade}\nBatteryVoltage: {voltVal}";
        }
    }
}
