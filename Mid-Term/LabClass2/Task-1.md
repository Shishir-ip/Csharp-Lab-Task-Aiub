# Question 1

Write a C# program that inputs two integers from the user and displays their addition, subtraction, multiplication, and division results.

## Code

```csharp
using System;

namespace Labtask1
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            int b;

            Console.WriteLine("Enter 1st number: ");
            a = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter second number: ");
            b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Addition = " + (a + b));
            Console.WriteLine("Subtraction = " + (a - b));
            Console.WriteLine("Multiplication = " + (a * b));
            Console.WriteLine("Division = " + (a / b));
        }
    }
}
