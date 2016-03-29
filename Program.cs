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
                            DisplayMainMenu(fm);
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
            FileManager fm = new FileManager();
            Boolean FilesCorrect = fm.inputFiles(@dir);
            while (!FilesCorrect)
            {
                //input not valid
                Console.WriteLine("Invalid input, Please enter the directory of the files");
                Console.WriteLine("Enter 3 to quit\n");
                dir = Console.ReadLine();
                if (dir == "3") return;
                FilesCorrect = fm.inputFiles(@dir);
            }
            //correct files found and are valid
            DisplayMainMenu(fm);
        }

        private static void DisplayMainMenu(FileManager fm)
        {
            Console.WriteLine("Main Menu\n");
            string selection = "0";
            do
            {
                Console.WriteLine("1. Sort by year ascending");
                Console.WriteLine("2. Sort by year decending");
                Console.WriteLine("3. Quit");
                selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        // sort and display year by ascending
                        fm.sort(false);
                        foreach (WeatherData wd in fm.WeatherData1)
                        {
                            Console.WriteLine(wd.ToString());
                        }
                        break;
                    case "2":
                        // sort and display year by ascending

                        break;
                    default:
                        break;
                        // etc..
                }
            } while (selection != "3");
        }
    }
}
