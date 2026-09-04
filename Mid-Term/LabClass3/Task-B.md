# Question B

Write a C# program to input and display a 3X3 matrix using a two-dimensional array and calculate the sum of all elements.

## Code

```csharp
using System;

namespace LabTaskB
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[3, 3];
            Console.WriteLine("Enter the elements of 3X3 matrix: ");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }

            Console.WriteLine("\nThe 3X3 matrix: ");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {                    
                    Console.Write(matrix[i, j] + " ");
                }                
                Console.WriteLine();
            }

            int sum = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {                    
                    sum = sum + matrix[i, j];
                }
            }
            Console.WriteLine("\nThe sum of all elements: " + sum);
        }
    }
}
