# Question C

Write a program that stores marks of several students using a jagged array. Each student may take a different number of subjects.

## Code

```csharp
using System;

namespace LabTaskC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of students for the jagged array:");
            int rows = Convert.ToInt32(Console.ReadLine());
            
            int[][] jaggedArray = new int[rows][];
            int[] totalMarks = new int[rows];

            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine("Enter the number of subjects for student " + (i + 1) + ":");
                int cols = Convert.ToInt32(Console.ReadLine());
                jaggedArray[i] = new int[cols];
                
                int currentStudentSum = 0;
                for (int j = 0; j < cols; j++)
                {
                    Console.WriteLine("Enter the marks for subject " + (j + 1) + " of student " + (i + 1) + ":");
                    jaggedArray[i][j] = Convert.ToInt32(Console.ReadLine());
                    currentStudentSum += jaggedArray[i][j];
                }
                
                totalMarks[i] = currentStudentSum;
            }

            Console.WriteLine();
            Console.WriteLine("Student Marks Summary:");
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                Console.Write("Student " + (i + 1) + " Marks: ");
                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    Console.Write(jaggedArray[i][j] + " ");
                }
                Console.WriteLine("Total Marks = " + totalMarks[i]);
            }

            int highestMarks = totalMarks[0];
            int topStudentIndex = 0;

            for (int i = 1; i < totalMarks.Length; i++)
            {
                if (totalMarks[i] > highestMarks)
                {
                    highestMarks = totalMarks[i];
                    topStudentIndex = i;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Student " + (topStudentIndex + 1) + " achieved the highest total marks with a score of: " + highestMarks);
        }
    }
}
