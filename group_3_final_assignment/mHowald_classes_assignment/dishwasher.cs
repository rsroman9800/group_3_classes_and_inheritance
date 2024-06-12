using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mHowald_classes_assignment
{
    public class dishwasher : appliance
    {
        private string feature;
        private string soundRating;

        public string Feature { get => feature; set => feature = value; }
        public string SoundRating { get => soundRating; set => soundRating = value; }

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
            string soundDescription; // Changes the text based on the sound rating in the dishwasher item so that it matches the project output
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
