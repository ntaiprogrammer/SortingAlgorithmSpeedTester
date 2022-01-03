using System;
using System.Diagnostics;
using System.IO;

/**
 * Nathan Tai
 * P148535
 * 04/03/2021
 * Programming III, PQ3
 * Sorting Methods Program
 * 
 * Question 3 – You are required to make a list of different annual salaries 
 * for payroll in whole numbers (integers) that will then need to be sorted, 
 * you should have alternate methods of sorting so that payroll can decide on 
 * which method they would like to use.You need to create an application that 
 * creates lists of integer values between 10K and 10 million. Your application 
 * must have the ability to sort in three different styles with timers to indicate 
 * the speed at which this happens you must have at least 100k items in your 
 * list as this the future business strategy to employ at least this many staff. 
 * The current system is only able to handle 12 staff. Only 1 sorting technique may 
 * use the inbuilt sorting the rest you must write yourself. In addition, you must 
 * list the advantages and disadvantages of each algorithm, including the Big O notation. 
 * You will present this information to the class in the form of a PowerPoint slideshow 
 * showing the differences via graphs showing stakeholders business critical factors.
 */

namespace SortingMethods
{
    class Program
    {
        #region Global Variables

        //Number of times to run sort, used in preset methods
        int numOfRunsDefault = 100;

        //Variables to set sort type (see sortSelect method)
        const string Bubble = "Bubble";
        const string Comb = "Comb";
        const string Insertion = "Insertion";
        const string Selection = "Selection";

        //Array for loop
        string[] algorithms = { Comb, Insertion, Selection };

        #endregion Global Variables

        static void Main(string[] args)
        {
            //Instantiate sort class
            Program sort = new Program();

            //Boolean condition to keep program running until user exits
            Boolean exit = false;

            while (exit == false)
            {
                //User prompts
                Console.WriteLine("Type (a) to run all presets");
                Console.WriteLine("Type (c) for custom parameters");
                Console.WriteLine("Type (e) to exit");

                //User input to variable
                string input = Console.ReadLine();

                //Run with user defined parameters
                if (input.Equals("c"))
                {
                    //Prompt user for amount of sizes to be tested
                    Console.WriteLine("Enter number of array sizes to be tested: ");
                    int numOfSizes = int.Parse(Console.ReadLine());
                    int[] sizes = new int[numOfSizes];

                    //Prompt user for each array size, sizes into array
                    for (int i = 0; i < numOfSizes; i++)
                    {
                        Console.WriteLine("Enter desired array size for array " + (i + 1) + ": ");
                        sizes[i] = int.Parse(Console.ReadLine());
                    }
                    Console.WriteLine("Enter number of runs for each sort: ");
                    int numOfRuns = int.Parse(Console.ReadLine());

                    Console.WriteLine("Running custom sorts...");

                    //Run sorts at each defined array size
                    for (int i = 0; i < numOfSizes; i++)
                    {
                        sort.RunSort(sizes[i], numOfRuns);
                    }
                    Console.WriteLine("\nSorts complete.\n");
                }
                //Run all presets
                else if (input.Equals("a"))
                {
                    Console.WriteLine("Running preset sorts...");
                    sort.RunAll();
                    Console.WriteLine("\nSorts complete.\n");
                }
                //Exit program
                else if (input.Equals("e"))
                {
                    System.Environment.Exit(0);
                }
            }
        }

        #region Run

        //Base method to run a sort.
        //Receives sort type, array size, file name, and array to be sorted.
        public void Sort(string sortType, int arraySize, string fileName, int[] salariesOriginal, int numOfRuns)
        {
            //Counter for number of runs
            int count = 0;

            //Print sort type and array size at start of output
            WriteFile(sortType + " sort. " + "Array size: " + arraySize, fileName);

            //Loop sorting and timestamping for specified number of runs
            while (count < numOfRuns)
            {
                int[] salaries = new int[arraySize]; //New array each time,
                salariesOriginal.CopyTo(salaries, 0); //original unsorted array copied into new array.
                Stopwatch swatch = new Stopwatch(); //Instantiate stopwatch

                swatch.Start(); //Begin stopwatch
                SortSelect(sortType, salaries); //Select and run desired sort
                swatch.Stop(); //End stopwatch

                TimeSpan ts = swatch.Elapsed; //Record elapsed time

                string elapsedTime = ts.TotalMilliseconds.ToString(); //Elapsed time into variable
                WriteFile(elapsedTime, fileName); //Write elapsed time to file
                swatch.Reset(); //Reset stopwatch
                count++; //Increment counter
                //DisplaySortedArray(salaries, arraySize, sortType); //For testing only, leave commented
            }
        }

        /** 
         * Executes the sorts.
         * Enter desired array size and number of runs.
         * Generates array of specified size, containing random numbers between 10k and 10m.
         * Runs all sorting algorithms.
         * Each algorithm gets the same array of numbers, in the same order, for fair comparison.
         * Results written to separate files, one for each array size.
         */
        public void RunSort(int arraySize, int numOfRuns)
        {
            //Dynamic file name based on array size
            string fileName = FileNameFormat(arraySize);

            //Generate random number array of specified size
            int[] salariesOriginal = GenerateNumbers(arraySize);

            //Run sort with all algorithms.
            //Strings for selecting algorithms placed into an array, iterate with for-loop.
            for (int i = 0; i < algorithms.Length; i++)
            {
                Sort(algorithms[i], arraySize, fileName, salariesOriginal, numOfRuns);
            }
        }

        //Executes all preset Run methods
        public void RunAll()
        {
            Run5000();
            Run10000();
            Run50000();
            Run100000();
        }

        #region Presets

        //Preset methods to time each algorithm 100x at four array sizes (5000, 10000, 50000, and 100000).
        public void Run5000()
        {
            //Preset variable for array size
            int size = 5000;
            string fileName = FileNameFormat(size);
            int[] salariesOriginal = GenerateNumbers(size);
            for (int i = 0; i < algorithms.Length; i++)
            {
                Sort(algorithms[i], size, fileName, salariesOriginal, numOfRunsDefault);
            }
        }

        public void Run10000()
        {
            int size = 10000;
            string fileName = FileNameFormat(size);
            int[] salariesOriginal = GenerateNumbers(size);
            for (int i = 0; i < algorithms.Length; i++)
            {
                Sort(algorithms[i], size, fileName, salariesOriginal, numOfRunsDefault);
            }
        }

        public void Run50000()
        {
            int size = 50000;
            string fileName = FileNameFormat(size);
            int[] salariesOriginal = GenerateNumbers(size);
            for (int i = 0; i < algorithms.Length; i++)
            {
                Sort(algorithms[i], size, fileName, salariesOriginal, numOfRunsDefault);
            }
        }

        public void Run100000()
        {
            int size = 100000;
            string fileName = FileNameFormat(size);
            int[] salariesOriginal = GenerateNumbers(size);
            for (int i = 0; i < algorithms.Length; i++)
            {
                Sort(algorithms[i], size, fileName, salariesOriginal, numOfRunsDefault);
            }
        }

        #endregion Presets

        #endregion Run

        #region Sorting Methods

        //For comb sort
        static int getNextGap(int gap)
        {
            gap = (gap * 10) / 13;
            if (gap < 1)
                return 1;
            return gap;
        }

        static void CombSort(int[] a)
        {
            int n = a.Length;

            int gap = n;

            bool swapped = true;

            while (gap != 1 || swapped == true)
            {
                gap = getNextGap(gap);

                swapped = false;

                for (int i = 0; i < n - gap; i++)
                {
                    if (a[i] > a[i + gap])
                    {
                        int temp = a[i];
                        a[i] = a[i + gap];
                        a[i + gap] = temp;

                        swapped = true;
                    }
                }
            }
        }

        public void BubbleSort(int[] a)
        {
            int temp;
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[i] > a[j])
                    {
                        temp = a[i];
                        a[i] = a[j];
                        a[j] = temp;
                    }
                }
            }
        }

        public void InsertionSort(int[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                int current = a[i];
                int j = i - 1;
                while (j >= 0 && current < a[j])
                {
                    a[j + 1] = a[j];
                    j--;
                }
                a[j + 1] = current;
            }
        }

        public void SelectionSort(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                int min = a[i];
                int minId = i;
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[j] < min)
                    {
                        min = a[j];
                        minId = j;
                    }
                }
                int temp = a[i];
                a[i] = min;
                a[minId] = temp;
            }
        }

        #endregion Sorting Methods

        #region Other Methods

        //Selects and runs sort method based on given parameters
        public void SortSelect(string sortType, int[] salaries)
        {
            if (sortType.Equals(Insertion))
            {
                InsertionSort(salaries);
            }
            else if (sortType.Equals(Selection))
            {
                SelectionSort(salaries);
            }
            else if (sortType.Equals(Comb))
            {
                CombSort(salaries);
            }
        }

        //Writes durations to text file, selects which file to write to
        public void WriteFile(string data, string fileName)
        {
            try
            {
                StreamWriter sw = new StreamWriter(fileName, append: true);
                sw.WriteLine(data);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        //Generate values between 10k and 10 million, add to array. Array size set here.
        public int[] GenerateNumbers(int arraySize)
        {
            int[] salariesUnsorted = new int[arraySize];

            Random rnd = new Random();
            for (int i = 0; i < arraySize; i++)
            {
                salariesUnsorted[i] = (rnd.Next(10000, 10000000));
            }
            return salariesUnsorted;
        }

        //Dynamically creates file names based on array sizes
        public string FileNameFormat(int arraySize)
        {
            string fileName = ("results" + arraySize + ".csv");
            return fileName;
        }

        //Display method for testing
        public void DisplaySortedArray(int[] a, int arraySize, string sortType)
        {
            Console.WriteLine("Array sorted by " + sortType + " sort.");
            for (int i = 0; i < arraySize; i++)
            {
                Console.WriteLine(a[i]);
            }
            Console.WriteLine("End of " + sortType + " sort.");
        }

        #endregion Other Methods
    }
}
