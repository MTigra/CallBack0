using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Plant
    {
        //Fields
        private double growth;
        private double photosensitivity;
        private double frostresistance;

        //Properties
        public double Growth
        {
            get
            {
                return growth;
            }

            set
            {
                growth = value;
            }
        }

        public double Photosensitivity
        {
            get
            {
                return photosensitivity;
            }

            set
            {
                if(value<0 || value>100) throw  new ArgumentOutOfRangeException("value",value,"Светочувствительность может быть только от 0 до 100");
                photosensitivity = value;
            }
        }

        public double Frostresistance
        {
            get
            {
                return frostresistance;
            }

            set
            {
                if (value < 0.0 || value > 100.0) throw new ArgumentOutOfRangeException("value",value, "Морозоустойчивость может быть только от 0 до 100");
                frostresistance = value;
            }
        }

        public Plant(double growth,double photosensitivity, double frostresistance)
        {
            Growth = growth;
            Photosensitivity = photosensitivity;
            Frostresistance = frostresistance;
        }

        public override string ToString()
        {
            return String.Format($"Рост: {Growth:f3}\tСветочувствительность: {Photosensitivity:f3}\tМорозоустойчивость: {Frostresistance:f3}.");
        }


    }

    
}
