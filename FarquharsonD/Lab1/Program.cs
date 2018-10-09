using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
/// <summary>
/// author: Daniel Farquharson - 000780401 19-sep-20018
/// purpose: read from a text file and create an array of up to 100 employees,
/// this array has the fields name, number, pay rate, hours, and gross pay,
/// it can be sorted by any of these fields and the console will prompt the user
/// to enter a number from 1 to 6 and the console will display the list sorted by
/// the coresponding number 1 to 5, or 6 to quit
/// I, Daniel Farquharson, 000780401 certify that this material is my original work.
/// No other person's work has been used without due acknowledgement.
/// </summary>
namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ///prompt method calls this switch case
                ///it will call sort method and print "menu" bar and sorted list
                ///it will loop back on itself until 6 is chosen
                var prompt = Prompt();
                Employee[] employees = ReadRecords();
                List<int> sortIndex;
                while (prompt != 6)
                {
                    switch (prompt)
                    {
                        case 1:
                            Console.WriteLine("Employee                  Number       Rate        Hours           Gross pay");
                            sortIndex = GetSortIndex(employees.Where(a => a != null).Select(a => (IComparable)a.Name).ToList(), true);
                            foreach (var index in sortIndex)
                                Console.WriteLine(employees[index].ToString());
                            break;

                        case 2:
                            Console.WriteLine("Employee                  Number       Rate        Hours           Gross pay");
                            sortIndex = GetSortIndex(employees.Where(a => a != null).Select(a => (IComparable)a.Number).ToList(), true);
                            foreach (var index in sortIndex)
                                Console.WriteLine(employees[index].ToString());
                            break;

                        case 3:
                            Console.WriteLine("Employee                  Number       Rate        Hours           Gross pay");
                            sortIndex = GetSortIndex(employees.Where(a => a != null).Select(a => (IComparable)a.Rate).ToList(), false);
                            foreach (var index in sortIndex)
                                Console.WriteLine(employees[index].ToString());
                            break;

                        case 4:
                            Console.WriteLine("Employee                  Number       Rate        Hours           Gross pay");
                            sortIndex = GetSortIndex(employees.Where(a => a != null).Select(a => (IComparable)a.Hours).ToList(), false);
                            foreach (var index in sortIndex)
                                Console.WriteLine(employees[index].ToString());
                            break;

                        case 5:
                            Console.WriteLine("Employee                  Number       Rate        Hours           Gross pay");
                            sortIndex = GetSortIndex(employees.Where(a => a != null).Select(a => (IComparable)a.Gross).ToList(), false);
                            foreach (var index in sortIndex)
                                Console.WriteLine(employees[index].ToString());
                            break;
                    }
                    prompt = Prompt();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.ReadLine();
            }

        }
        /// <summary>
        /// reads "employees.txt" line by line and splits it at the ','
        /// making an array of strings which is then used directly or parsed into apropriate
        /// types and used as input to call the constructor
        /// </summary>
        /// <returns>array of type Eployee</returns>
        public static Employee[] ReadRecords()
        {
            var employees = new Employee[100];

            StreamReader sr = new StreamReader("employees.txt");

            String line;
            line = sr.ReadLine();
            int count = 0;
            //Continue to read until you reach end of file or 100
            while (line != null & count < 100)
            {
                string[] dataFromString = line.Split(',');
                string name = dataFromString[0];
                int number;
                decimal rate;
                double hours;
                int.TryParse(dataFromString[1], out number);
                decimal.TryParse(dataFromString[2], out rate);
                double.TryParse(dataFromString[3], out hours);
                Employee e = new Employee(name, number, rate, hours);
                employees[count] = e;
                count++;
                line = sr.ReadLine();
            }
            sr.Close();
            return employees;
        }
        /// <summary>
        /// generates the initial console screen prompting the user to choose from 1 to 6
        /// what to do next, 1 to 5 being sort options and 6 being exit, if any other ints or any other character(s)
        /// is entered it will generate a error message and write the console again,
        /// when a number from 1 to 5 is entered it will call the apropriate sort method
        /// which will write the list to console sorted as requested, and return to this method
        /// if 6 is enetred it will exit the program
        /// </summary>
        /// <returns>int from 1 to 6 to choose sort column or exit</returns>
        public static int Prompt()
        {
            bool isGoodInput = false;
            int choice = 0;
            string typed;
            while (!isGoodInput)
            {
                Console.WriteLine("1. Sort by Employee Name");
                Console.WriteLine("2. Sort by Employee Number");
                Console.WriteLine("3. Sort by Employee Pay Rate");
                Console.WriteLine("4. Sort by Employee Hours");
                Console.WriteLine("5. Sort by Employee Gross Pay");
                Console.WriteLine("");
                Console.WriteLine("6. Exit");
                Console.WriteLine("");
                Console.Write("Enter choice:");
                typed = Console.ReadLine();
                isGoodInput = int.TryParse(typed, out choice);
                isGoodInput = choice > 0 & choice < 7;
                if (!isGoodInput)
                {
                    Console.WriteLine("");
                    Console.WriteLine("*** Invalid Choice - Try Again ***");
                    Console.WriteLine("");
                }
            }
            
            return choice;
        }
        /// <summary>
        /// makes key value pair of requested column and it's index 
        /// then sorts them by the value and saves the new order of the index
        /// and returns this
        /// </summary>
        /// <param name="columnList">chooses which column from the array to sort by</param>
        /// <param name="ascending">orders list ascending or descending, true for ascending</param>
        /// <returns>list of index sorted by value</returns>
        private static List<int> GetSortIndex(List<IComparable> columnList, bool ascending)
        {
            var indexList = new List<KeyValuePair<int, IComparable>>(columnList.Count);
            for(var i = 0; i < columnList.Count; i++)
                indexList.Add(new KeyValuePair<int, IComparable>(i, columnList[i]));
            //sorting the list
            var sortedIndexList = new List<int>();
            while(indexList.Count>0)
            {
                var topVal = indexList[0].Value;
                var topIndex = 0;
                for(var x = 1; x < indexList.Count; x++)
                {
                    if (topVal.CompareTo(indexList[x].Value) == (ascending? 1:-1))
                    {
                        topIndex = x;
                        topVal = indexList[x].Value;
                    }
                }
                sortedIndexList.Add(indexList[topIndex].Key);
                indexList.RemoveAt(topIndex);
            }
            return sortedIndexList;
        }
    }


}
