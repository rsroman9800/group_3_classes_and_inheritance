using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
     class Dishwasher : Appliance
    {
        private string feature;
        private string soundRating;

        public string Feature { get => feature; set => feature = value; }
        public string SoundRating { get => soundRating; set => soundRating = value; }

        public Dishwasher()
        {

        }

        public Dishwasher(long itemNumber, string brand, int quantity, double wattage, string color, double price, string feature, string soundRating) : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.feature = feature;
            this.soundRating = soundRating;
        }

        public override string ToString()
        {
            string sndDesc;
            if (soundRating == "Qu")
            {
                sndDesc = "Quiet";
            }

            else if (soundRating == "Qr")
            {
                sndDesc = "Quieter";
            }
            else if (soundRating == "Qt")
            {
                sndDesc = "Quietest";
            }
            else if (soundRating == "M")
            {
                sndDesc = "Moderate";
            }

            else
            {
                sndDesc = soundRating;
            }

            return $"{base.ToString()}\nFeature: {feature}\nSoundRating: {sndDesc}";
        }
    }
}
