using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliances
{
    public class Vacuum : Appliance
    {
        private string grade;
        private int batteryVoltage;
        public string Grade { get => grade; set => grade = value; }
        public int BatteryVoltage { get => batteryVoltage; set => batteryVoltage = value; }
       
        public Vacuum()
        {
            
        }

        public Vacuum(long itemNumber, string brand, int quantity, double wattage, string color, double price, string grade, int batteryVoltage) : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.grade = grade;
            this.batteryVoltage = batteryVoltage;
        }

        public override string ToString()
        {
            string voltageDescription;
            if (batteryVoltage == 18)
            {
                voltageDescription = "Low";
            }
            else if (batteryVoltage == 24)
            {
                voltageDescription = "High";
            }
            else
            {
                voltageDescription = batteryVoltage + " V";
            }
            return $"{base.ToString()}\nGrade: {grade}\nBatteryVoltage: {voltageDescription}";
        }
    }
}
