using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchingSort
{
    class Program
    {
        private static FileManager fm = new FileManager();

        static void Main(string[] args)
        {
            string selection = "0";
            do
            {
                Console.WriteLine("Select where your files are\n");
                Console.WriteLine("1. Current directory");
                Console.WriteLine("2. Enter a directory");
                Console.WriteLine("3. Quit");
                selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        FileManager fm = new FileManager();
                        if (fm.inputFiles(Directory.GetCurrentDirectory()) == false)
                        {
                            Console.WriteLine("\nFiles not found!\n");
                            break;
                        }
                        else
                        {
                            DisplaySearchorSortMenu();
                        }
                        break;
                    case "2":
                        DisplayDirectoryMenu();
                        break;
                    default:
                        //Console.WriteLine("Selection not correct");
                        break;
                }
            } while (selection != "3");
        }

        private static void DisplayDirectoryMenu()
        {
            Console.WriteLine("Please enter the directory of the files\n");
            String dir = Console.ReadLine();
            Boolean FilesCorrect = fm.inputFiles(@dir);
            while (!FilesCorrect)
            {
                //input not valid
                Console.WriteLine("Invalid input, Please enter the directory of the files");
                Console.WriteLine("Type exit to return to the previous menu\n");
                dir = Console.ReadLine();
                if (dir == "exit") return;
                FilesCorrect = fm.inputFiles(@dir);
            }
            //correct files found and are valid
            DisplaySearchorSortMenu();
        }

        private static void DisplaySearchorSortMenu()
        {
            Console.WriteLine("Select if you would like to search or sort\n");
            string selection = "0";
            do
            {
                Console.WriteLine("1. Search");
                Console.WriteLine("2. Sort - Ascending");
                Console.WriteLine("3. Sort - descending");
                Console.WriteLine("4. Show min, max and median value");
                Console.WriteLine("5. Quit");
                selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        // display search menu
                        DisplaySearchMenu();
                        //TODO
                        break;
                    case "2":
                        // display sort menu
                        DisplayMainMenu(true);
                        break;
                    case "3":
                        // display sort menu
                        DisplayMainMenu(false);
                        break;
                    case "4":
                        // display sort menu
                        DisplayMinMaxMenu();
                        break;
                    default:
                        break;
                        // etc..
                }
            } while (selection != "5");
        }

        private static void DisplayMainMenu(Boolean isAscending)
        {
            string selection = "0";
            do
            {
                Console.WriteLine("Please type in which of the following fields you would like to sort\n");
                Console.WriteLine("Year, Month, WS1_AF, WS1_Rain, WS1_Sun, WS1_TMax, WS1_Tmin, WS2_AF, WS2_Rain, WS2_Sun, WS2_TMax, WS2_Tmin");
                Console.WriteLine("Type exit to return to the previous menu\n");
                selection = Console.ReadLine();
                try
                {
                    fm.QuickSort_custom(isAscending, selection);
                }
                catch (Exception e)
                {
                    if (selection == "exit") break;
                    Console.WriteLine(e.Message);
                    break;
                }
                foreach (WeatherData wd in fm.WeatherDataArray)
                {
                    Console.WriteLine(wd.ToString());
                }
            } while (selection != "exit");
        }
        private static void DisplaySearchMenu()
        {
            string selection = "";
            Double SearchKey = 0;
            do
            {
                Console.WriteLine("Please type in which of the following fields you would like to search\n");
                Console.WriteLine("Year, Month, WS1_AF, WS1_Rain, WS1_Sun, WS1_TMax, WS1_Tmin, WS2_AF, WS2_Rain, WS2_Sun, WS2_TMax, WS2_Tmin");
                Console.WriteLine("Type exit to return to the previous menu\n");
                selection = Console.ReadLine();
                Console.WriteLine("What you like to search for?\n");
                try
                {
                    SearchKey = Convert.ToDouble(Console.ReadLine());
                    List<WeatherData> wd = fm.SeqSearch(SearchKey, selection);
                    if (wd.Count == 0)
                    {
                        Console.WriteLine("No items were found!");
                    }
                    else
                    {
                        //print the output of the search
                        foreach (WeatherData w in wd)
                        {
                            Console.WriteLine(w.ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    if (selection == "exit") break;
                    Console.WriteLine(e.Message);
                }
            } while (selection != "exit");
        }
        private static void DisplayMinMaxMenu()
        {
            string selection = "0";
            do
            {
                Console.WriteLine("Please type in which of the following fields you would like to find the min, max and median of\n");
                Console.WriteLine("Year, Month, WS1_AF, WS1_Rain, WS1_Sun, WS1_TMax, WS1_Tmin, WS2_AF, WS2_Rain, WS2_Sun, WS2_TMax, WS2_Tmin");
                Console.WriteLine("Type exit to return to the previous menu\n");
                selection = Console.ReadLine();
                try
                {
                    fm.GetMinMaxMedian(selection);
                }
                catch (Exception e)
                {
                    if (selection == "exit") break;
                    Console.WriteLine(e.Message);
                    break;
                }
            } while (selection != "exit");
        }
    }
}
