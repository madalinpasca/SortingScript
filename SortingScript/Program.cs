using System;
using System.Collections.Generic;
using System.IO;

namespace SortingScript
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            string name = "";
            string order = "";
            string type = "";

            try
            {
                name = args[0];
                order = args[1];
                type = args[2];
            }
            catch (IndexOutOfRangeException)
            {
                Console.Write("");
            }

            try
            {
                if (name.Contains(".txt"))
                {
                    p.PathAndCall(name, type, order);
                }
                else
                {
                    p.PathAndCall(name+".txt", type, order);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("First argument is missing or text file with this name doesn't exist in folder.");
                Console.WriteLine("Please check the text file name, and write it correctly and also, ");
                Console.WriteLine("if the text file exist in same folder with the [Program.cs] class or with the executable [SortingScript.exe]");
            }
        }

        private void PathAndCall(string name, string type, string order)
        {
            string parentdirabsolutpath = Directory.GetCurrentDirectory();
            string[] directories = parentdirabsolutpath.Split(@"\");
            string parentdirname = directories[directories.Length - 1];
            string[] pathForRunWithDotNetRun = Directory.GetDirectories(parentdirabsolutpath, parentdirname);
            if (!(pathForRunWithDotNetRun.Length == 0))
            {
                string path = parentdirabsolutpath + @"\" + parentdirname + @"\";
                string text = File.ReadAllText(path + name + "");
                InitializeTheSortingProcesAndTypeOfSorting(type, order, text);
            }
            else
            {
                string path = parentdirabsolutpath + @"\";
                string text = File.ReadAllText(path + name + "");
                InitializeTheSortingProcesAndTypeOfSorting(type, order, text);
            }
        }
        private void InitializeTheSortingProcesAndTypeOfSorting(string type, string order, string text)
        {
            if (type == "numbersort")
            {
                string[] numbersasstringarray = text.Split(", ");
                if (DataContainsOthersThanNumbers(numbersasstringarray))
                {
                    int[] value = SortArray(ConvertStringArrayToIntegerArray(numbersasstringarray));
                    SelectOrder(order, value);
                    Console.WriteLine("Strings that are not numbers founded and replaced with 0 for this kind of sort!");
                    Console.WriteLine("");
                    PrintTheArray(value);
                }
                else
                {
                    int[] value = SortArray(ConvertStringArrayToIntegerArray(numbersasstringarray));
                    SelectOrder(order, value);
                    PrintTheArray(value);
                }

            }
            else if (type == "stringsort")
            {
                string[] numbersasstringarray = text.Split(", ");
                string[] value = SortArray(numbersasstringarray);
                SelectOrder(order, value);
                PrintTheArray(value);
            }
            else if (type == "hybridsort")
            {
                string[] numbersasstringarray = text.Split(", ");
                if (DataContainsOthersThanNumbers(numbersasstringarray))
                {
                    int[] value = HybridSort(ConvertStringArrayToIntegerArray(numbersasstringarray));
                    SelectOrder(order, value);
                    Console.WriteLine("Strings that are not numbers founded and replaced with 0 for this kind of sort!");
                    Console.WriteLine("");
                    PrintTheArray(value);
                }
                else
                {
                    int[] value = HybridSort(ConvertStringArrayToIntegerArray(numbersasstringarray));
                    SelectOrder(order, value);
                    PrintTheArray(value);
                }
            }
            else
            {
                Console.WriteLine("Third argument is missing or wrong. Default type of sort [numbersort] ");
                Console.WriteLine("because you're input was different than [numbersort], [stringsort] or [hybridsort]!");
                Console.WriteLine("");
                string[] numbersasstringarray = text.Split(", ");
                if (DataContainsOthersThanNumbers(numbersasstringarray))
                {
                    int[] value = SortArray(ConvertStringArrayToIntegerArray(numbersasstringarray));
                    SelectOrder(order, value);
                    Console.WriteLine("Strings that are not numbers founded and replaced with 0 for this kind of sort!");
                    Console.WriteLine("");
                    PrintTheArray(value);
                }
                else
                {
                    int[] value = SortArray(ConvertStringArrayToIntegerArray(numbersasstringarray));
                    SelectOrder(order, value);
                    PrintTheArray(value);
                }
            }
        }

        private void PrintTheArray<T>(T[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                Console.Write(list[i]);
                if (i < list.Length - 1)
                {
                    Console.Write(" < ");
                }
            }
            Console.WriteLine();
        }

        private T[] SelectOrder<T>(string order, T[] numbers)
        {
            if (order == "descending")
            {
                ReverseArrays(numbers);
            }
            else if (order == "ascending")
            {
                return numbers;
            }
            else
            {
                Console.WriteLine("Second argument is missing or wrong. Default ordered [ascending] ");
                Console.WriteLine("because you're input was different than [ascending] or [descending]!");
                Console.WriteLine("");
                return numbers;
            }
            return numbers;
        }

        private T[] ReverseArrays<T>(T[] numbers)
        {
            for (int i = 0; i < numbers.Length / 2; i++)
            {
                var tmp = numbers[i];
                numbers[i] = numbers[numbers.Length - i - 1];
                numbers[numbers.Length - i - 1] = tmp;
            }
            return numbers;
        }

        private List<List<int>> OddListAndEvenList(int[] numbers)
        {
            List<int> odd = new List<int>();
            List<int> even = new List<int>();
            List<List<int>> oddeven = new List<List<int>>();
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] % 2 == 1)
                {
                    odd.Add(numbers[i]);
                }
                else
                {
                    even.Add(numbers[i]);
                }
            }
            oddeven.Add(odd);
            oddeven.Add(even);
            return oddeven;

        }

        private bool DataContainsOthersThanNumbers(string[] data)
        {
            foreach (string element in data)
            {
                if (!isNumber(element))
                {
                    return true;
                }
            }
            return false;
        }

        private bool isNumber(string s)
        {
            for (int i = 0; i < s.Length; i++)
                if (char.IsDigit(s[i]) == false)
                    return false;

            return true;
        }


        private int[] ConvertStringArrayToIntegerArray(string[] stringarray)
        {
            for (int i = 0; i < stringarray.Length; i++)
            {
                if (!isNumber(stringarray[i]))
                {
                    string zero = "0";
                    Exchange(ref stringarray[i], ref zero);
                }
            }
            int[] numbersasintegerarray = Array.ConvertAll(stringarray, int.Parse);
            return numbersasintegerarray;
        }


        private int[] HybridSort(int[] numbers)
        {
            List<List<int>> oddevennumbers = OddListAndEvenList(numbers);
            int[] oddnumbers = SortArray(oddevennumbers[0].ToArray());
            int[] evennumbers = ReverseArrays(SortArray(oddevennumbers[1].ToArray()));
            int[] hybridsortednumbers = new int[evennumbers.Length + oddnumbers.Length];
            evennumbers.CopyTo(hybridsortednumbers, 0);
            oddnumbers.CopyTo(hybridsortednumbers, evennumbers.Length);
            return hybridsortednumbers;
        }

        private T[] SortArray<T>(T[] numbers) where T : IComparable<T>
        {
            Quicksort(numbers, 0, numbers.Length - 1);
            return numbers;
        }

        private void Quicksort<T>(T[] numbers, int left, int right) where T : IComparable<T>
        {
            int i = left;
            int j = right;

            var pivot = numbers[right];

            while (i <= j)
            {
                while (numbers[i].CompareTo(pivot) < 0)
                {
                    i++;
                }
                while (numbers[j].CompareTo(pivot) > 0)
                {
                    j--;
                }
                if (i <= j)
                {
                    Exchange(ref numbers[i], ref numbers[j]);
                    i++;
                    j--;
                }
            }
            if (left < j)
            {
                Quicksort(numbers, left, j);
            }

            if (i < right)
            {
                Quicksort(numbers, i, right);
            }
        }

        public void Exchange<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

    }
}
