﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliances
{
    public class Microwave : Appliance
    {
        private double capacity;
        private string roomType;

        public double Capacity { get => capacity; set => capacity = value; }
        public string RoomType { get => roomType; set => roomType = value; }

        public Microwave()
        {
            
        }

        public Microwave(long itemNumber, string brand, int quantity, double wattage, string color, double price, double capacity, string roomType) : base(itemNumber, brand, quantity, wattage, color, price)
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
