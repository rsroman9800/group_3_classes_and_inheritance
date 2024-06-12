using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mHowald_classes_assignment
{
    public class vacuum : appliance
    {
        private string grade;
        private int voltage;
        public string Grade { get => grade; set => grade = value; }
        public int Voltage { get => voltage; set => voltage = value; }



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
            string voltageDescription; // Changes the number to the text rating based on the voltage of the vacuum so that it matches the project output
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
