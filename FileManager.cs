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

        public Boolean inputFiles(String Dir)
        {
            int counter = 0;
            Boolean FilesValid = true;
            try
            {
                StreamReader YearFile = new StreamReader(Dir + @"\Year.txt");
                StreamReader MonthFile = new StreamReader(Dir + @"\Month.txt");
                StreamReader WS1_AFFile = new StreamReader(Dir + @"\WS1_AF.txt");
                StreamReader WS1_RainFile = new StreamReader(Dir + @"\WS1_Rain.txt");
                StreamReader WS1_SunFile = new StreamReader(Dir + @"\WS1_Sun.txt");
                StreamReader WS1_TMaxFile = new StreamReader(Dir + @"\WS1_TMax.txt");
                StreamReader WS1_TMinFile = new StreamReader(Dir + @"\WS1_TMin.txt");
                StreamReader WS2_AFFile = new StreamReader(Dir + @"\WS2_AF.txt");
                StreamReader WS2_RainFile = new StreamReader(Dir + @"\WS2_Rain.txt");
                StreamReader WS2_SunFile = new StreamReader(Dir + @"\WS2_Sun.txt");
                StreamReader WS2_TMaxFile = new StreamReader(Dir + @"\WS2_TMax.txt");
                StreamReader WS2_TMinFile = new StreamReader(Dir + @"\WS2_TMin.txt");
                // all files have the same amount of lines
                for (Double x = 0; x <= 1021; x++)
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
            catch
            {
                FilesValid = false;
            }
            return FilesValid;
        }

        public void sort()
        {
            MyQuick_SortYears(WeatherData, 0, WeatherData.Length-1);
        }

        public int partition(WeatherData[] WeatherDataArray, int left, int right)
        {
            //Hoare partition scheme
            // pivot is the left most element
            double pivot = WeatherDataArray[left].Year;
            int i = left;
            int j = right;
            WeatherData temp;
            while (true) {
                //move from left to right until there is something that is less that the pivot
                while (WeatherDataArray[i].Year < pivot) i++;
                //move from right to left until there is something more than the pivot
                while (WeatherDataArray[j].Year > pivot) j--;
                //if they meet then all the elements to the left are smaller than or equal to the pivot and all the elements to the right are greater than the pivot
                if (i >= j)
                {
                    return j;
                }
                // left and right need to be swapped as the left one is greater than the pivot and the right is less than the pivot
                temp = WeatherDataArray[i];
                WeatherDataArray[i] = WeatherDataArray[j];
                WeatherDataArray[j] = WeatherDataArray[i];
            }
        }

        public void MyQuick_SortYears(WeatherData[] WeatherDataArray, int left, int right)
        {
            if (left < right )
            {
                int p = partition(WeatherData, left, right);
                MyQuick_SortYears(WeatherData, right, p);
                MyQuick_SortYears(WeatherData, p + 1, left);
            }
        }
    }
}
