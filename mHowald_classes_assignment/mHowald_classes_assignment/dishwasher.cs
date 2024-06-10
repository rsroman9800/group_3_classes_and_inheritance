using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mHowald_classes_assignment
{
    internal class dishwasher : appliance
    {
        private string feature { get; set; }
        private string soundRating { get; set; }

        public dishwasher()
        {
            
        }
        public dishwasher(long itemNumber, string brand, int quantity, double wattage, string color, double price, string feature, string soundRating) : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.feature = feature;
            this.soundRating = soundRating;
        }

        public override string ToString()
        {
            string soundDescription;
            if (soundRating == "Qu")
            {
                soundDescription = "Quiet";
            }
            else if (soundRating == "Qr")
            {
                soundDescription = "Quieter";
            }
            else if (soundRating == "Qt")
            {
                soundDescription = "Quietest";
            }
            else if (soundRating == "M")
            {
                soundDescription = "Moderate";
            }
            else
            {
                soundDescription = soundRating;
            }
            return $"{base.ToString()}\nFeature: {feature}\nSoundRating: {soundDescription}";
        }
    }
}
