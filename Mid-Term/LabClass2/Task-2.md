# Question 2

Write a C# program that takes an integer input from the user and checks whether it is positive, negative, or zero .

## Code

```csharp
using System;
namespace LabTask2
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            Console.WriteLine("Enter a number: ");
            a = Convert.ToInt32(Console.ReadLine());
            if (a > 0)
            {

                Console.WriteLine("The number is positive");
            }
            else if (a < 0)
            {

                Console.WriteLine("The number is negative");
            }
            else
            {
                Console.WriteLine("The number is zero");
            }

        }
    }
}
