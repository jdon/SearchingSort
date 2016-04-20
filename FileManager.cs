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
        Random rnd = new Random();
        long numberOfLines;
        string[] MonthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
        public int NumberOfSteps { get; set; }

    public Boolean inputFiles(String Dir) // method for inputting the text files
        {
            Boolean FilesValid = true;
            NumberOfSteps = 0;
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
                numberOfLines = CountLinesInFile(Dir + @"\Year.txt");
                WeatherDataArray = new WeatherData[numberOfLines];
                for (int x = 0; x < numberOfLines; x++)
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
                    //creat a new weather data object for each line in the text files and add it to the array
                    WeatherData wd = new WeatherData(Year, Month, WS1_AF, WS1_Rain, WS1_Sun, WS1_TMax, WS1_Tmin, WS2_AF, WS2_Rain, WS2_Sun, WS2_TMax, WS2_Tmin);
                WeatherDataArray[x] = wd;
                }
                           
            }
            catch
            {
                // any errors will be return to the method that called this which can handle the error on screen
                FilesValid = false;
            }
            return FilesValid;
        }

        static long CountLinesInFile(string f) // method for counting the amount of lines in the textfile 
        {
            long count = 0;
            using (StreamReader r = new StreamReader(f))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    count++;
                }
            }
            return count;
        }

        public List<WeatherData> SeqSearch(Double WhatToFind,String WhatToSearch)
        {
            List<WeatherData> weather = new List<WeatherData>();
            //loop throught each item in the array
           for (int i = 0; i < WeatherDataArray.Length; i++)
           {
                NumberOfSteps++;
               if (GetData(i, WhatToSearch) == WhatToFind)
               {
                   // they match so add to array;
                   weather.Add(WeatherDataArray[i]);
               }
           }
            return weather;
        }

        public void BinarySearch(Double WhatToFind, String WhatToSearch, out int lowerindex, out int upperindex)
        {
            QuickSort(true, WhatToSearch); // sort the array before performing binary searches
            NumberOfSteps = 0;
            upperindex = UpperBound(WhatToFind, 0, WeatherDataArray.Length - 1, WhatToSearch); // returns the last result from the search
            lowerindex = LowerBound(WhatToFind, 0, WeatherDataArray.Length - 1, WhatToSearch); // returns the first result from the search
        }

        public void insertionsort(Boolean ascending, String WhatToSort) // insertion sort method
        {
            for (int i = 1; i < WeatherDataArray.Length; i++)
            {
                Double x = GetData(i,WhatToSort);
                WeatherData weatherDataofX = WeatherDataArray[i];
                int j = i - 1;
                if (ascending)
                {
                    while (j >= 0 && GetData(j,WhatToSort) > x)
                    {
                        WeatherDataArray[j + 1] = WeatherDataArray[j];
                        j = j - 1;
                        NumberOfSteps++;
                    }
                }
                else
                {
                    while (j >= 0 && GetData(j, WhatToSort) < x)
                    {
                        WeatherDataArray[j + 1] = WeatherDataArray[j];
                        j = j - 1;
                        NumberOfSteps++;
                    }
                }
                WeatherDataArray[j + 1] = weatherDataofX;
                NumberOfSteps++;
            }
        }


        public void QuickSort(Boolean Ascending,String WhatToSort)
        {
            Quick_Sort(Ascending,0, WeatherDataArray.Length - 1, WhatToSort);
        }
        public void Quick_Sort(Boolean Ascending, int left, int right, String WhatToSort)
        {
            int index, index2;
            double pivot;
            WeatherData temp;
            index = left;
            index2 = right;
            //random pivot is used to reduce chance of worst case of O(n^2)
            int randomPivotNumber = rnd.Next(left, right);
            pivot = GetData(randomPivotNumber, WhatToSort);
            do
            {
                if (Ascending)
                {
                    // the movement of the sort is modififed so that it will sort it in ascending order
                    while ((GetData(index, WhatToSort) < pivot) && (index < right)) index++;
                    while ((pivot < GetData(index2, WhatToSort)) && (index2 > left)) index2--;
                }
                else
                {
                    // the movement of the sort is modififed so that it will sort it in descending order
                    while ((GetData(index, WhatToSort) > pivot) && (index < right)) index++;
                    while ((pivot > GetData(index2, WhatToSort)) && (index2 > left)) index2--;
                }
                if (index <= index2)
                {
                    //swapping is performed here
                    temp = WeatherDataArray[index];
                    WeatherDataArray[index] = WeatherDataArray[index2];
                    WeatherDataArray[index2] = temp;
                    index++;
                    index2--;
                    NumberOfSteps++;
                }
            } while (index <= index2);
            if (left < index2) Quick_Sort(Ascending,left, index2, WhatToSort);
            if (index < right)
                Quick_Sort(Ascending,index, right, WhatToSort);
        }

        int UpperBound(Double WhatToFind, int low, int high,String datatype)
        {
            NumberOfSteps++;
            if (low > high) return low - 1;
            int mid = (low + high) / 2;
            if (GetData(mid, datatype) > WhatToFind)
            {
                return UpperBound(WhatToFind,low, mid - 1, datatype);
            }
            else
            {
                return UpperBound(WhatToFind, mid + 1, high, datatype);
            }
        }
        int LowerBound(Double WhatToFind, int low, int high,String datatype)
        {
            NumberOfSteps++;
            if (low > high) return low;
            int mid = (low + high) / 2;
            if (WhatToFind <= GetData(mid, datatype))
            {
                return LowerBound(WhatToFind, low, mid - 1, datatype);
            }
            else
            {
                return LowerBound(WhatToFind, mid + 1, high, datatype);
            }
        }

        private double GetData(int index, String data) // method to get the data type from the array from a given index
        {
            // StringComparison.OrdinalIgnoreCase is used, so the user can input the data type in upper or lower case or a mixture of both
            if (string.Equals(data,"Year",StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].Year;
            }
            else if (string.Equals(data, "Month", StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].Month;
            }
            else if(string.Equals(data, "WS1_AF", StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].WS1_AF;
            }
            else if (string.Equals(data, "WS1_Rain", StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].WS1_Rain;
            }
            else if (string.Equals(data, "WS1_Sun", StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].WS1_Sun;
            }
            else if (string.Equals(data, "WS1_TMax", StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].WS1_TMax;
            }
            else if (string.Equals(data, "WS1_Tmin", StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].WS1_Tmin;
            }
            else if (string.Equals(data, "WS2_AF", StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].WS2_AF;
            }
            else if (string.Equals(data, "WS2_Rain", StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].WS2_Rain;
            }
            else if (string.Equals(data, "WS2_Sun", StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].WS2_Sun;
            }
            else if (string.Equals(data, "WS2_TMax", StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].WS2_TMax;
            }
            else if (string.Equals(data, "WS2_Tmin", StringComparison.OrdinalIgnoreCase))
            {
                return WeatherDataArray[index].WS2_Tmin;
            }
            throw new System.ArgumentException("Not a valid data type!", data);
        }
        public string createtable(String selection)
        {
            QuickSort(true, selection);
            Double min = GetData(0, selection);
            Double max = GetData(WeatherDataArray.Length - 1, selection);
            Double median = (GetData(511, selection) + GetData(512, selection)) / 2;
            List<WeatherData> WeatherArray = new List<WeatherData>();
            WeatherArray.Add(WeatherDataArray[0]);
            WeatherArray.Add(WeatherDataArray[WeatherDataArray.Length - 1]);
            WeatherArray.Add(WeatherDataArray[511]);
            WeatherArray.Add(WeatherDataArray[512]);
            String html= "<h2>"+selection+"</h2>";
            foreach (WeatherData wd in WeatherArray)
            {
                if (wd == WeatherDataArray[0])
                {
                    html += "Min is "+min;
                }
                else if (wd == WeatherDataArray[WeatherDataArray.Length - 1])
                {
                    html += "Max is "+ max;
                }
                else
                {
                    html += "Median is "+median;
                }
                html += "<table style='width: 100 %'><tr><th>Year</th> <th>Month</th> <th>Ws1_af</th> <th>Ws1_rain</th> <th>Ws1_sun</th> <th>Ws1_tmax</th> <th>Ws1_tmin</th> <th>Ws2_af</th> <th>Ws2_rain</th> <th>Ws2_sun</th> <th>Ws2_tmax</th> <th>Ws2_tmin</th>";
                html += "<tr>";
                html += "<td>" + wd.Year + "</td>";
                html += "<td>" + Program.convertNumbertoMonth(Convert.ToInt32(wd.Month)) + "</td>";
                html += "<td>" + wd.WS1_AF + "</td>";
                html += "<td>" + wd.WS1_Rain + "</td>";
                html += "<td>" + wd.WS1_Sun + "</td>";
                html += "<td>" + wd.WS1_TMax + "</td>";
                html += "<td>" + wd.WS1_Tmin + "</td>";
                html += "<td>" + wd.WS2_AF + "</td>";
                html += "<td>" + wd.WS2_Rain + "</td>";
                html += "<td>" + wd.WS2_Sun + "</td>";
                html += "<td>" + wd.WS2_TMax + "</td>";
                html += "<td>" + wd.WS2_Tmin + "</td>";
                html += "</tr>";
                html += "</table>";
            }
            return html;
        }
        public void OutPutToWebPageMinMaxMedian()
        {
            String html = "<!DOCTYPE html><html><head><title>Sorting Output</title><style>table, th, td {    border: 1px solid black;    border-collapse: collapse;}th, td {    padding: 5px;}</style></head><body> ";
            html += createtable("Year");
            html += createtable("Month");
            html += createtable("ws1_af");
            html += createtable("ws1_rain");
            html += createtable("ws1_sun");
            html += createtable("ws1_tmax");
            html += createtable("ws1_tmin");
            html += createtable("ws2_af");
            html += createtable("ws2_rain");
            html += createtable("ws2_sun");
            html += createtable("ws2_tmax");
            html += createtable("ws2_tmin");
            html += "</body></html>";
            try
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\output.html", html);
                System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\output.html");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not save or open the webpage due to the following error" + e.ToString());
            }
        }
        public void OutPutToWebPage()
        {
            String html = "<!DOCTYPE html><html><head><title>Sorting Output</title><style>table, th, td {    border: 1px solid black;    border-collapse: collapse;}th, td {    padding: 5px;}</style></head><body> <table style='width: 100 %'><tr><th>Year</th> <th>Month</th> <th>Ws1_af</th> <th>Ws1_rain</th> <th>Ws1_sun</th> <th>Ws1_tmax</th> <th>Ws1_tmin</th> <th>Ws2_af</th> <th>Ws2_rain</th> <th>Ws2_sun</th> <th>Ws2_tmax</th> <th>Ws2_tmin</th>";
            foreach (WeatherData wd in WeatherDataArray)
            {
                html += "<tr>";
                html += "<td>" + wd.Year + "</td>";
                html += "<td>" + Program.convertNumbertoMonth(Convert.ToInt32(wd.Month)) + "</td>";
                html += "<td>" + wd.WS1_AF + "</td>";
                html += "<td>" + wd.WS1_Rain + "</td>";
                html += "<td>" + wd.WS1_Sun + "</td>";
                html += "<td>" + wd.WS1_TMax + "</td>";
                html += "<td>" + wd.WS1_Tmin + "</td>";
                html += "<td>" + wd.WS2_AF + "</td>";
                html += "<td>" + wd.WS2_Rain + "</td>";
                html += "<td>" + wd.WS2_Sun + "</td>";
                html += "<td>" + wd.WS2_TMax + "</td>";
                html += "<td>" + wd.WS2_Tmin + "</td>";
                html += "</tr>";
            }
            html += "</table></body></html>";
            try
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\output.html", html);
                System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\output.html");
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not save or open the webpage due to the following error" + e.ToString());
            }
        }
        public void OutPutToWebPage(List<WeatherData> WeatherArray)
        {
            String html = "<!DOCTYPE html><html><head><title>Searchin Output</title><style>table, th, td {    border: 1px solid black;    border-collapse: collapse;}th, td {    padding: 5px;}</style></head><body> <table style='width: 100 %'><tr><th>Year</th> <th>Month</th> <th>Ws1_af</th> <th>Ws1_rain</th> <th>Ws1_sun</th> <th>Ws1_tmax</th> <th>Ws1_tmin</th> <th>Ws2_af</th> <th>Ws2_rain</th> <th>Ws2_sun</th> <th>Ws2_tmax</th> <th>Ws2_tmin</th>";
            foreach (WeatherData wd in WeatherArray)
            {
                html += "<tr>";
                html += "<td>" + wd.Year + "</td>";
                html += "<td>" + Program.convertNumbertoMonth(Convert.ToInt32(wd.Month)) + "</td>";
                html += "<td>" + wd.WS1_AF + "</td>";
                html += "<td>" + wd.WS1_Rain + "</td>";
                html += "<td>" + wd.WS1_Sun + "</td>";
                html += "<td>" + wd.WS1_TMax + "</td>";
                html += "<td>" + wd.WS1_Tmin + "</td>";
                html += "<td>" + wd.WS2_AF + "</td>";
                html += "<td>" + wd.WS2_Rain + "</td>";
                html += "<td>" + wd.WS2_Sun + "</td>";
                html += "<td>" + wd.WS2_TMax + "</td>";
                html += "<td>" + wd.WS2_Tmin + "</td>";
                html += "</tr>";
            }
            html += "</table></body></html>";
            try
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\output.html", html);
                System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\output.html");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not save or open the webpage due to the following error" + e.ToString());
            }
        }
        public void OutPutToWebPage(int lowerBound, int upperBound)
        {
            String html = "<!DOCTYPE html><html><head><title>Searching Output</title><style>table, th, td {    border: 1px solid black;    border-collapse: collapse;}th, td {    padding: 5px;}</style></head><body> <table style='width: 100 %'><tr><th>Year</th> <th>Month</th> <th>Ws1_af</th> <th>Ws1_rain</th> <th>Ws1_sun</th> <th>Ws1_tmax</th> <th>Ws1_tmin</th> <th>Ws2_af</th> <th>Ws2_rain</th> <th>Ws2_sun</th> <th>Ws2_tmax</th> <th>Ws2_tmin</th>";
            while (lowerBound <= upperBound) // results found so print them out
            {
                WeatherData wd = WeatherDataArray[lowerBound];
                html += "<tr>";
                html += "<td>" + wd.Year + "</td>";
                html += "<td>" + Program.convertNumbertoMonth(Convert.ToInt32(wd.Month)) + "</td>";
                html += "<td>" + wd.WS1_AF + "</td>";
                html += "<td>" + wd.WS1_Rain + "</td>";
                html += "<td>" + wd.WS1_Sun + "</td>";
                html += "<td>" + wd.WS1_TMax + "</td>";
                html += "<td>" + wd.WS1_Tmin + "</td>";
                html += "<td>" + wd.WS2_AF + "</td>";
                html += "<td>" + wd.WS2_Rain + "</td>";
                html += "<td>" + wd.WS2_Sun + "</td>";
                html += "<td>" + wd.WS2_TMax + "</td>";
                html += "<td>" + wd.WS2_Tmin + "</td>";
                html += "</tr>";
                lowerBound++;
            }
            html += "</table></body></html>";
            try
            {
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\output.html", html);
                System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\output.html");
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not save or open the webpage due to the following error" + e.ToString());
            }
        }
    }
}
