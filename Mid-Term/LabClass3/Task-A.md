# Question A

Write a program to find the largest element in a 1D array. Take array size and elements as input from the user

## Code

```csharp
using System;
namespace LabTaskA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the size of the array: ");
            int size = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[size];
            Console.WriteLine("Enter the elements of the array: ");
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }
                int largest = arr[0];
                    for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > largest)
                {
                    largest = arr[i];
                }
            }
            Console.WriteLine("The largest number in the array is: " + largest);
        }
    }
} 

