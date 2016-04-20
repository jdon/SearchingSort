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
    class Program
    {
        //create filemanager instance 
        private static FileManager fm = new FileManager();
        //get a list of the months for use of converting month name to a number
        private static string[] MonthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;

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
                        Console.WriteLine("Selection not correct");
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
                Console.WriteLine("1. Search - Sequential");
                Console.WriteLine("2. Search - Binarytree");
                Console.WriteLine("3. QuickSort - Ascending");
                Console.WriteLine("4. QuickSort - Descending");
                Console.WriteLine("5. InsertionSort - Ascending");
                Console.WriteLine("6. InsertionSort - Descending");
                Console.WriteLine("7. Show min, max and median value");
                Console.WriteLine("8. Quit");
                selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        // display search menu
                        DisplaySearchMenu();
                        break;
                    case "2":
                        // display binary tree search menu
                        DisplayBinarySearchMenu();
                        break;
                    case "3":
                        // display sort menu - quicksort asc
                        DisplaySortMenu(true,true);
                        break;
                    case "4":
                        // display sort menu - quicksort dsc
                        DisplaySortMenu(false, true);
                        break;
                    case "5":
                        // display sort menu - insertionsort asc
                        DisplaySortMenu(true,false);
                        break;
                    case "6":
                        // display sort menu - insertionsort dsc
                        DisplaySortMenu(false,false);
                        break;
                    case "7":
                        // display the min and max for all fields
                        fm.OutPutToWebPageMinMaxMedian();
                        break;
                    default:
                        break;
                }
            } while (selection != "8");
        }

        private static void DisplaySortMenu(Boolean isAscending,Boolean isQuickSort)
        {
            string selection = "0";
            fm.NumberOfSteps = 0; // reset the Number of steps
            do
            {
                Console.WriteLine("Please type in which of the following fields you would like to sort\n");
                Console.WriteLine("Year, Month, WS1_AF, WS1_Rain, WS1_Sun, WS1_TMax, WS1_Tmin, WS2_AF, WS2_Rain, WS2_Sun, WS2_TMax, WS2_Tmin");
                Console.WriteLine("Type exit to return to the previous menu\n");
                selection = Console.ReadLine();
                if (selection == "exit") break;
                try
                {
                    //performs sort based on given parameters 
                    if (isQuickSort)
                    {
                        fm.QuickSort(isAscending, selection);
                    }
                    else
                    {
                        fm.insertionsort(isAscending, selection);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
                //print the output of the sort
                Console.WriteLine("Number of steps:" + fm.NumberOfSteps);
                fm.OutPutToWebPage();
            } while (selection != "exit");
        }
        private static void DisplaySearchMenu()
        {
            string selection = "";
            Double SearchKey = 0;
            fm.NumberOfSteps = 0; // reset the Number of steps
            do
            {
                Console.WriteLine("Please type in which of the following fields you would like to search\n");
                Console.WriteLine("Year, Month, WS1_AF, WS1_Rain, WS1_Sun, WS1_TMax, WS1_Tmin, WS2_AF, WS2_Rain, WS2_Sun, WS2_TMax, WS2_Tmin");
                Console.WriteLine("Type exit to return to the previous menu\n");
                selection = Console.ReadLine();
                Console.WriteLine("What you like to search for?\n");
                Console.WriteLine("Type exit to return to the previous menu\n");
                try
                {

                    String searchString = Console.ReadLine();
                    if (string.Equals(selection, "Month", StringComparison.OrdinalIgnoreCase)) //special case for searching for months
                    {
                        SearchKey = convertMonthtoNumber(searchString);
                    }
                    else
                    {
                        SearchKey = Convert.ToDouble(searchString);
                    }
                    List<WeatherData> wd = fm.SeqSearch(SearchKey, selection);
                    Console.WriteLine("Number of steps:" + fm.NumberOfSteps);
                    if (wd.Count == 0)
                    {
                        Console.WriteLine("No items were found!");
                    }
                    else
                    {
                        fm.OutPutToWebPage(wd);
                    }
                }
                catch (Exception e)
                {
                    if (selection == "exit") break;
                    Console.WriteLine(e.Message);
                }
            } while (selection != "exit");
        }
        private static void DisplayBinarySearchMenu()
        {
            string selection = "";
            Double SearchKey = 0;
            fm.NumberOfSteps = 0; // reset the Number of steps
            do
            {
                Console.WriteLine("Please type in which of the following fields you would like to search\n");
                Console.WriteLine("Year, Month, WS1_AF, WS1_Rain, WS1_Sun, WS1_TMax, WS1_Tmin, WS2_AF, WS2_Rain, WS2_Sun, WS2_TMax, WS2_Tmin");
                Console.WriteLine("Type exit to return to the previous menu\n");
                selection = Console.ReadLine();
                Console.WriteLine("What you like to search for?\n");
                Console.WriteLine("Type exit to return to the previous menu\n");
                try
                {
                    String searchString = Console.ReadLine();
                    if (string.Equals(selection, "Month", StringComparison.OrdinalIgnoreCase)) // special case for searching for months
                    {
                        SearchKey = convertMonthtoNumber(searchString);
                    }
                    else
                    {
                        SearchKey = Convert.ToDouble(searchString);
                    }

                    int lowerBound;
                    int upperBound;
                    fm.BinarySearch(SearchKey, selection ,out lowerBound, out upperBound);

                    if (lowerBound > upperBound)// no results found
                    {
                        Console.WriteLine("No results found!");
                    }
                    else
                    {
                        fm.OutPutToWebPage(lowerBound, upperBound);
                    }
                    Console.WriteLine("Number of steps:" + fm.NumberOfSteps);
                }
                catch (Exception e)
                {
                    if (selection == "exit") break;
                    Console.WriteLine(e.Message);
                }
            } while (selection != "exit");
        }

        public static double convertMonthtoNumber(String Month)
        {
            return Array.FindIndex(MonthNames, t => t.Equals(Month, StringComparison.InvariantCultureIgnoreCase))+1;
        }
        public static String convertNumbertoMonth(int Month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);
        }

    }
}
