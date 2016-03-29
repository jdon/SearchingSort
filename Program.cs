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
            foreach (WeatherData wd in fm.WeatherData1)
            {
                Console.WriteLine(wd.ToString());
            }
            Console.ReadLine(); 

        }
    }
}
