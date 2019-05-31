using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectionSort
{
    class Program
    {

        static int[] Numbers;
        const int Length = 120000000;
        const bool Display = false;

        static void Main(string[] args)
        {
            Numbers = new int[Length];
            Random random = new Random();
            for (int i = 0; i < Numbers.Length; i++)
                Numbers[i] = random.Next(0, Numbers.Length + 1);

            //Console.WriteLine("Orignal Array:");

            //for (int i = 0; i < Numbers.Length; i++)
            //Console.Write($"{Numbers[i]} ");

            Stopwatch sw = new Stopwatch();

            //sw.Restart();
            //int[] selectionSort = SelectionSort();
            //sw.Stop();
            //Console.WriteLine($"Selection Sort: {sw.ElapsedMilliseconds} ms and {sw.ElapsedTicks} ticks");

            //sw.Restart();
            //int[] standardBubbleSort = StandardBubbleSort();
            //sw.Stop();
            //Console.WriteLine($"Standard Bubble Sort: {sw.ElapsedMilliseconds} ms and {sw.ElapsedTicks} ticks");

            //sw.Restart();
            //int[] insertionSort = InsertionSort();
            //sw.Stop();
            //Console.WriteLine($"Insertion Sort: {sw.ElapsedMilliseconds} ms and {sw.ElapsedTicks} ticks");

            sw.Restart();
            int[] mergeSort = MergeSort(CopyNumbers());
            sw.Stop();
            Console.WriteLine($"Merge Sort: {sw.ElapsedMilliseconds} ms and {sw.ElapsedTicks} ticks");

            sw.Restart();
            int[] quickSort = CopyNumbers();
            QuickSort(quickSort, 0, Numbers.Length-1);
            sw.Stop();
            Console.WriteLine($"Quick Sort: {sw.ElapsedMilliseconds} ms and {sw.ElapsedTicks} ticks");

            //if (Display)
            //{
            //    Console.WriteLine("\n\nSelection Sort: ");
            //    for (int i = 0; i < selectionSort.Length; i++)
            //        Console.Write($"{selectionSort[i]} ");

            //    Console.WriteLine("\n\nStandard Bubble Sort: ");
            //    for (int i = 0; i < standardBubbleSort.Length; i++)
            //        Console.Write($"{standardBubbleSort[i]} ");

            //    Console.WriteLine("\n\nInsertion Sort: ");
            //    for (int i = 0; i < insertionSort.Length; i++)
            //        Console.Write($"{insertionSort[i]} ");

            //    Console.WriteLine("\n\nMerge Sort: ");
            //    for (int i = 0; i < mergeSort.Length; i++)
            //        Console.Write($"{mergeSort[i]} ");

            //    Console.WriteLine("\n\nQuick Sort: ");
            //    for (int i = 0; i < quickSort.Length; i++)
            //        Console.Write($"{quickSort[i]} ");
            //}

            Console.ReadLine();
        }

        static void QuickSort(int[] arr, int left, int right)
        {
            //Condition to prevent out of bounds exceptions
            if (left < right)
            {
                int pivot = Partition(arr, left, right);
                if (pivot > 1)
                    QuickSort(arr, left, pivot - 1);

                if (pivot + 1 < right)
                    QuickSort(arr, pivot + 1, right);
            }
        }

        static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];
            //Infinit loop
            while (true)
            {
                //find the left and right markers
                while (arr[left] < pivot)
                    left++;

                while (arr[right] > pivot)
                    right--;

                //Conditions allowing the exit of the loop
                if (left < right)
                {
                    if (arr[left] == arr[right])
                        return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
                else
                    return right;
            }
        }

        public static int[] MergeSort(int[] array)
        {
            int[] left;
            int[] right;
            int[] result = new int[array.Length];

            //This is a recursive algorithm, we need to set a base case to avoid an infinite loop which will result in a stckOverflow error
            if (array.Length <= 1)
                return array;

            // Find the midpoint of our array  
            int midPoint = array.Length / 2;

            //This will hold our 'left' array
            left = new int[midPoint];

            //if array has an even number of elements, split it so the left and right have the same number of elements
            if (array.Length % 2 == 0)
                right = new int[midPoint];

            //if array has an odd number of elements, the right array will have one more element than left
            else
                right = new int[midPoint + 1];

            //populate left array
            for (int i = 0; i < midPoint; i++)
                left[i] = array[i];

            //populate right array   
            int x = 0;
            //We start our index from the midpoint, as we have already populated the left array from 0 to midpont
            for (int i = midPoint; i < array.Length; i++)
            {
                right[x] = array[i];
                x++;
            }

            //Recursively sort the left array
            left = MergeSort(left);
            //Recursively sort the right array
            right = MergeSort(right);

            //Merge our two sorted arrays
            result = Merge(left, right);
            return result;
        }

        //This method will be responsible for combining our two sorted arrays into one giant array
        public static int[] Merge(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];
            int indexLeft = 0, indexRight = 0, indexResult = 0;
            //while either array still has an element
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                //if both arrays have elements  
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    //If item on left array is less than item on right array, add that item to the result array 
                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    // else the item in the right array wll be added to the results array
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                //if only the left array still has elements, add all its items to the results array
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                //if only the right array still has elements, add all its items to the results array
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
            return result;
        }

        static int[] SelectionSort()
        {
            int[] numbers = CopyNumbers();
            int min, temp;
            for (int i = 0; i < numbers.Length; i++)
            {
                min = i;
                for (int x = i + 1; x < numbers.Length; x++)
                    if (numbers[x] < numbers[min]) min = x;
                temp = numbers[i];
                numbers[i] = numbers[min];
                numbers[min] = temp;
            }
            return numbers;
        }

        static int[] StandardBubbleSort()
        {
            int[] numbers = CopyNumbers();
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        int temp = numbers[j + 1];
                        numbers[j + 1] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
            return numbers;
        }

        static int[] InsertionSort()
        {
            int[] numbers = CopyNumbers();
            int temp, j;
            for (int i = 1; i < numbers.Length; i++)
            {
                j = i;
                temp = numbers[i];
                while (j > 0 && numbers[j - 1] >= temp)
                {
                    numbers[j] = numbers[j - 1];
                    j--;
                }
                numbers[j] = temp;
            }

            return numbers;
        }

        static int[] CopyNumbers()
        {
            int[] copy = new int[Length];
            Array.Copy(Numbers, copy, Numbers.Length);
            return copy;
        }
    }
}