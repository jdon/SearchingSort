using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingSort
{
    class WeatherData
    {
        Double Year { get; set; }
        Double Month { get; set; }
        Double WS1_AF { get; set; }
        Double WS1_Rain { get; set; }
        Double WS1_Sun { get; set; }
        Double WS1_TMax { get; set; }
        Double WS1_Tmin { get; set; }
        Double WS2_AF { get; set; }
        Double WS2_Rain { get; set; }
        Double WS2_Sun { get; set; }
        Double WS2_TMax { get; set; }
        Double WS2_Tmin { get; set; }

        public WeatherData(Double Year, Double Month, Double WS1_AF,Double WS1_Rain, Double WS1_Sun, Double WS1_TMax, Double WS1_Tmin, Double WS2_AF, Double WS2_Rain, Double WS2_Sun, Double WS2_TMax, Double WS2_Tmin)
        {

            this.Year = Year;
            this.Month = Month;
            this.WS1_AF = WS1_AF;
            this.WS1_Rain = WS1_Rain;
            this.WS1_Sun = WS1_Sun;
            this.WS1_TMax = WS1_TMax;
            this.WS1_Tmin = WS1_Tmin;
            this.WS2_AF = WS2_AF;
            this.WS2_Rain = WS2_Rain;
            this.WS2_Sun = WS2_Sun;
            this.WS2_TMax = WS2_TMax;
            this.WS2_Tmin = WS2_Tmin;
        }

        override public String ToString()
        {
            return "Month: " + this.Month + " Year: " + this.Year + " WS1_AF: " + this.WS1_AF +
                "WS1_Rain: " + this.WS1_Rain + " WS1_Sun: " + this.WS1_Sun +
                "WS1_TMax: " + this.WS1_TMax + " WS1_Tmin: " + this.WS1_Tmin +
                "WS2_AF: " + this.WS2_AF + " WS2_Rain: " + this.WS2_Rain +
                "WS2_Sun: " + this.WS2_Sun + " WS2_TMax: " + this.WS2_TMax +
                "WS2_Tmin: " + this.WS2_Tmin;
        }
    }
}
