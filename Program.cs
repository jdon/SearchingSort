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

        public static void QuickSort(WeatherData[] data)
        {
            // pre: 0 <= n <= data.length
            // post: values in data[0 ... n
            Quick_SortYears(data, 0, data.Length-1);
        }
        public static void Quick_SortYears(WeatherData[] Assignments, int left, int right)
        {
            // DueDate.Ticks is used to get the number of milliseconds
            int index, index2;
            double pivot;
            WeatherData temp;
            index = left;
            index2 = right;
            pivot = Assignments[(left + right) / 2].Year;
            do
            {
                while ((Assignments[index].Year > pivot) && (index < right)) index++;
                while ((pivot > Assignments[index2].Year) && (index2 > left)) index2--;
                if (index <= index2)
                {
                    temp = Assignments[index];
                    Assignments[index] = Assignments[index2];
                    Assignments[index2] = temp;
                    index++;
                    index2--;
                }
            } while (index <= index2);
            if (left < index2) Quick_SortYears(Assignments, left, index2);
            if (index < right) Quick_SortYears(Assignments, index, right);
        }
    }
}
