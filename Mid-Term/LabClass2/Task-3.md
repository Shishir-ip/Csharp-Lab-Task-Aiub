# Question 3

Write a C# program that uses a loop to find and display all even numbers between 1 and 20.

## Code

```csharp
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            for (int i = 1; i <= 20; i++ )
            {
                if (i % 2 ==0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
