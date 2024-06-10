using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mHowald_classes_assignment
{
    internal class vacuum : appliance
    {
        private string grade { get; set; }
        private int voltage { get; set; }


        public vacuum()
        {
            
        }

        public vacuum(long itemNumber, string brand, int quantity, double wattage, string color, double price, string grade, int voltage) : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.grade = grade;
            this.voltage = voltage;
        }

        public override string ToString()
        {
            string voltageDescription;
            if (voltage == 18)
            {
                voltageDescription = "Low";
            }
            else if (voltage == 24)
            {
                voltageDescription = "High";
            }
            else
            {
                voltageDescription = voltage + " V";
            }
            return $"{base.ToString()}\nGrade: {grade}\nBatteryVoltage: {voltageDescription}";
        }
    }
}
