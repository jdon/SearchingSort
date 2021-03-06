﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingSort
{
    public class WeatherData
    {
        //getters and setters for all the data
        public Double Year { get; set; }
        public Double Month { get; set; }
        public Double WS1_AF { get; set; }
        public Double WS1_Rain { get; set; }
        public Double WS1_Sun { get; set; }
        public Double WS1_TMax { get; set; }
        public Double WS1_Tmin { get; set; }
        public Double WS2_AF { get; set; }
        public Double WS2_Rain { get; set; }
        public Double WS2_Sun { get; set; }
        public Double WS2_TMax { get; set; }
        public Double WS2_Tmin { get; set; }

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

        override public String ToString() // overide the tostring method, so that this is the output from this object
        {
            return "Month: " + Program.convertNumbertoMonth(Convert.ToInt32(this.Month)) + " Year: " + this.Year + " WS1_AF: " + roundDouble(this.WS1_AF) +
                " WS1_Rain: " + roundDouble(this.WS1_Rain) + " WS1_Sun: " + roundDouble(this.WS1_Sun) +
                " WS1_TMax: " + roundDouble(this.WS1_TMax) + " WS1_Tmin: " + roundDouble(this.WS1_Tmin) +
                " WS2_AF: " + roundDouble(this.WS2_AF) + " WS2_Rain: " + roundDouble(this.WS2_Rain) +
                " WS2_Sun: " + roundDouble(this.WS2_Sun) + " WS2_TMax: " + roundDouble(this.WS2_TMax) +
                " WS2_Tmin: " + roundDouble(this.WS2_Tmin);
        }
        private String roundDouble(Double input)
        {
            return string.Format("{0:000.00}", input);
        }

    }
}
