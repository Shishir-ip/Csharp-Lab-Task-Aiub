# Question 4

Write a C# program that takes an integer N from the user and calculates the sum of all numbers from 1 to N using a while loop.
## Code

```csharp
using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            Console.WriteLine("Enter N: ");
            n = Convert.ToInt32(Console.ReadLine());
            int i = 1;
            int sum = 0;
            while (i <= n)
            {
                sum = sum + i;
                i++;
            }
            Console.WriteLine("Sum of numbers from 1 to " + n + " : " + sum);
        }
    }
}
