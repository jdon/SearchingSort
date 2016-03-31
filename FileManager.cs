using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingSort
{
    public class FileManager
    {
        public WeatherData[] WeatherDataArray { get; set; }
        public FileManager()
        {
            WeatherDataArray = new WeatherData[1022];
        }
        string[] MonthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;

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
                    WeatherDataArray[counter] = wd;
                    counter++;
                }
            }
            catch
            {
                FilesValid = false;
            }
            return FilesValid;
        }

        public List<WeatherData> SeqSearch(Double WhatToFind,String WhatToSearch)
        {
            List<WeatherData> weather = new List<WeatherData>();
            for (int i = 0; i < WeatherDataArray.Length; i++)
            {
                if (getStringData(i, WhatToSearch) == WhatToFind)
                {
                    // they match so add to array;
                    weather.Add(WeatherDataArray[i]);
                }
            }
            return weather;
        }

        public void GetMinMaxMedian(String Selection)
        {
            QuickSort_custom(true, Selection);
            Double min = getStringData(0, Selection);
            Double max = getStringData(WeatherDataArray.Length-1, Selection);
            Double median = (getStringData(511, Selection) + getStringData(512, Selection))/2;
            Console.WriteLine("The min value is:" + min +"\n"+ WeatherDataArray[0].ToString()+ 
                " \nThe max value is:" + max+"\n" + WeatherDataArray[WeatherDataArray.Length - 1].ToString() + 
                " \nThe median value is " + median+"\n"+WeatherDataArray[511].ToString()+"\n" + WeatherDataArray[512].ToString());
        }

        public void QuickSort_custom(Boolean ascending, String WhatToSort)
        {
            MyQuickSort_CustomData(0, WeatherDataArray.Length, ascending, WhatToSort);
        }

        public void MyQuickSort_CustomData(int left, int right, Boolean ascending,String WhatToSort)
        {
            int pivot;
            if (right - left < 2) return;
            pivot = partition_CustomData(left, right, ascending, WhatToSort);
            MyQuickSort_CustomData(left, pivot, ascending, WhatToSort);
            MyQuickSort_CustomData(pivot, right, ascending, WhatToSort);
        }
        public int partition_CustomData(int left, int right, Boolean ascending,String WhatToSort)
        {
            int i = left - 1, j = right;
            Debug.WriteLine(left);
            Random ran = new Random();
            //random pivot is used as the list is sorted
            double pivot = getStringData(ran.Next(left, right), WhatToSort);
            WeatherData temp;
            while (true)
            {
                if (!ascending)
                {
                    //move from right to left until there is something less than the pivot
                    do j--; while (getStringData(j, WhatToSort) < pivot);
                    //move from left to right until there is something that is more that the pivot
                    do i++; while (getStringData(i, WhatToSort) > pivot);
                }
                else
                {
                    //move from right to left until there is something more than the pivot
                    do j--; while (getStringData(j, WhatToSort) > pivot);
                    //move from left to right until there is something that is less that the pivot
                    do i++; while (getStringData(i, WhatToSort) < pivot);
                }
                //if they meet then all the elements to the left are smaller than or equal to the pivot and all the elements to the right are greater than the pivot
                if (i < j)
                {
                    // left and right need to be swapped as the left one is greater than the pivot and the right is less than the pivot
                    temp = WeatherDataArray[i];
                    WeatherDataArray[i] = WeatherDataArray[j];
                    WeatherDataArray[j] = temp;
                }
                else
                    return j + 1;
            }
        }

        private double getStringData(int index, String data)
        {
            if (data == "Year")
            {
                return WeatherDataArray[index].Year;
            }
            if (data == "Month")
            {
                return WeatherDataArray[index].Month;
            }
            if (data == "WS1_AF")
            {
                return WeatherDataArray[index].WS1_AF;
            }
            if (data == "WS1_Rain")
            {
                return WeatherDataArray[index].WS1_Rain;
            }
            if (data == "WS1_Sun")
            {
                return WeatherDataArray[index].WS1_Sun;
            }
            if (data == "WS1_TMax")
            {
                return WeatherDataArray[index].WS1_TMax;
            }
            if (data == "WS1_Tmin")
            {
                return WeatherDataArray[index].WS1_Tmin;
            }
            if (data == "WS2_AF")
            {
                return WeatherDataArray[index].WS2_AF;
            }
            if (data == "WS2_Rain")
            {
                return WeatherDataArray[index].WS2_Rain;
            }
            if (data == "WS2_Sun")
            {
                return WeatherDataArray[index].WS2_Sun;
            }
            if (data == "WS2_TMax")
            {
                return WeatherDataArray[index].WS2_TMax;
            }
            if (data == "WS2_Tmin")
            {
                return WeatherDataArray[index].WS2_Tmin;
            }
            throw new System.ArgumentException("Not a valid data type!", data);
        }
    }
}
