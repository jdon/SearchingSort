using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingSort
{
    public class FileManager
    {
        public FileManager()
        {

        }
        WeatherData[] WeatherData = new WeatherData[1022];
        string[] MonthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        int counter = 0;

        internal WeatherData[] WeatherData1
        {
            get
            {
                return WeatherData;
            }

            set
            {
                WeatherData = value;
            }
        }

        public void inputFiles(String Dir)
        {
            StreamReader YearFile = new StreamReader(Dir+@"\Year.txt");
            StreamReader MonthFile = new StreamReader(Dir+@"\Month.txt");
            StreamReader WS1_AFFile = new StreamReader(Dir+@"\WS1_AF.txt");
            StreamReader WS1_RainFile = new StreamReader(Dir+@"\WS1_Rain.txt");
            StreamReader WS1_SunFile = new StreamReader(Dir+@"\WS1_Sun.txt");
            StreamReader WS1_TMaxFile = new StreamReader(Dir+@"\WS1_TMax.txt");
            StreamReader WS1_TMinFile = new StreamReader(Dir+@"\WS1_TMin.txt");
            StreamReader WS2_AFFile = new StreamReader(Dir+@"\WS2_AF.txt");
            StreamReader WS2_RainFile = new StreamReader(Dir+@"\WS2_Rain.txt");
            StreamReader WS2_SunFile = new StreamReader(Dir+@"\WS2_Sun.txt");
            StreamReader WS2_TMaxFile = new StreamReader(Dir+@"\WS2_TMax.txt");
            StreamReader WS2_TMinFile = new StreamReader(Dir+@"\WS2_TMin.txt");
            // all files have the same amount of lines
            for(Double x = 0; x<= 1021; x++)
            {
                Double Year = Convert.ToDouble(YearFile.ReadLine());
                Double Month = Array.IndexOf(MonthNames, MonthFile.ReadLine()) + 1;

                Double WS1_AF = Convert.ToDouble(WS1_AFFile.ReadLine());
                Double WS1_Rain = Convert.ToDouble(WS1_RainFile.ReadLine());
                Double WS1_Sun = Convert.ToDouble(WS1_SunFile.ReadLine());
                Double WS1_TMax = Convert.ToDouble(WS1_TMaxFile.ReadLine());
                Double WS1_Tmin = Convert.ToDouble(WS1_TMinFile.ReadLine());

                Double WS2_AF = Convert.ToDouble(WS2_AFFile.ReadLine());
                Double WS2_Rain = Convert.ToDouble(WS2_RainFile.ReadLine());
                Double WS2_Sun = Convert.ToDouble(WS2_SunFile.ReadLine());
                Double WS2_TMax = Convert.ToDouble(WS2_TMaxFile.ReadLine());
                Double WS2_Tmin = Convert.ToDouble(WS2_TMinFile.ReadLine());

                WeatherData wd = new WeatherData(Year, Month, WS1_AF, WS1_Rain, WS1_Sun, WS1_TMax, WS1_Tmin, WS2_AF, WS2_Rain, WS2_Sun, WS2_TMax, WS2_Tmin);
                WeatherData1[counter] = wd;
                counter++;
            }
        }
    }
}
