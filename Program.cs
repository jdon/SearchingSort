using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingSort
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager fm = new FileManager();
            fm.inputFiles(@"C:\Users\jdon\Downloads\CMP1124M_Weather_Data");
            WeatherData[] WeatherDataArray = fm.WeatherData1;
            QuickSort(WeatherDataArray);
            foreach (WeatherData wd in WeatherDataArray)
            {
                Console.WriteLine(wd.ToString());
            }
            Console.ReadLine(); 

        }

        public static void QuickSort(WeatherData[] WeatherDataArray)
        {
            // pre: 0 <= n <= data.length
            // post: values in data[0 ... n
            Quick_SortYears(WeatherDataArray, 0, WeatherDataArray.Length-1);
        }
        public static void Quick_SortYears(WeatherData[] WeatherDataArray, int left, int right)
        {
            // DueDate.Ticks is used to get the number of milliseconds
            int index, index2;
            double pivot;
            WeatherData temp;
            index = left;
            index2 = right;
            pivot = WeatherDataArray[(left + right) / 2].Year;
            do
            {
                while ((WeatherDataArray[index].Year > pivot) && (index < right)) index++;
                while ((pivot > WeatherDataArray[index2].Year) && (index2 > left)) index2--;
                if (index <= index2)
                {
                    temp = WeatherDataArray[index];
                    WeatherDataArray[index] = WeatherDataArray[index2];
                    WeatherDataArray[index2] = temp;
                    index++;
                    index2--;
                }
            } while (index <= index2);
            if (left < index2) Quick_SortYears(WeatherDataArray, left, index2);
            if (index < right) Quick_SortYears(WeatherDataArray, index, right);
        }
    }
}
