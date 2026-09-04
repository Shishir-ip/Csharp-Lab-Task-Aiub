# Question 

Design a Smart Banking System.  Create a base class Account using private fields, properties, methods, encapsulation, access modifiers, static and instance members. Demonstrate variables/types, literals, value vs reference types, strings, operators, type casting, const, readonly, and nullable types.  Implement default, parameterized, copy, and static constructors, constructor chaining, initialization order, and a destructor (finalizer).  Create a struct Address and an enum AccountType.  Create SavingsAccount, CurrentAccount, and PremiumSavings classes to demonstrate single, hierarchical, and multilevel inheritance, including constructor flow and IS-A vs HAS-A relationships.  Declare a virtual method CalculateInterest(). Override it in SavingsAccount, use sealed override in PremiumSavings, and use method hiding (new) in CurrentAccount. Demonstrate runtime polymorphism and explain the different outputs.  Overload the Deposit() method and use ref, out, and params parameters.  Store data using a single-dimensional array, a multi-dimensional array, and a jagged array. Traverse them with foreach, and correctly use Length and GetLength().

## Code

```csharp
using System;

namespace SmartBankingSystem
{
 
    // ENUM
 
    enum AccountType
    {
        Savings,
        Current,
        PremiumSavings
    }

 
    // STRUCT (HAS-A Relationship)
 
    struct Address
    {
        public string City;
        public string Country;

        public Address(string city, string country)
        {
            City = city;
            Country = country;
        }

        public void Display()
        {
            Console.WriteLine($"Address : {City}, {Country}");
        }
    }

 
    // BASE CLASS
 
    class Account
    {
        // Private Fields (Encapsulation)
        private int accountNumber;
        private string holderName;
        private double balance;

        // Static Member
        public static string BankName;

        // Const
        public const double MinBalance = 500;

        // Readonly
        public readonly DateTime CreatedDate;

        // Nullable Type
        public string? Email;

        // HAS-A
        public Address Address;

        // Properties
        public int AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value; }
        }

        public string HolderName
        {
            get { return holderName; }
            set { holderName = value; }
        }

        public double Balance
        {
            get { return balance; }
            protected set { balance = value; }
        }

     
        // STATIC CONSTRUCTOR
     
        static Account()
        {
            BankName = "AIUB Smart Bank";
            Console.WriteLine("Static Constructor Executed");
        }

     
        // DEFAULT CONSTRUCTOR
     
        public Account()
            : this(0, "Unknown", 0)
        {
            Console.WriteLine("Default Constructor");
        }

     
        // PARAMETERIZED CONSTRUCTOR
     
        public Account(int accNo, string name, double balance)
        {
            Console.WriteLine("Parameterized Constructor");

            accountNumber = accNo;
            holderName = name;
            this.balance = balance;

            CreatedDate = DateTime.Now;
        }

     
        // COPY CONSTRUCTOR
     
        public Account(Account obj)
        {
            Console.WriteLine("Copy Constructor");

            accountNumber = obj.accountNumber;
            holderName = obj.holderName;
            balance = obj.balance;
            CreatedDate = obj.CreatedDate;
            Email = obj.Email;
            Address = obj.Address;
        }

     
        // Destructor
     
        ~Account()
        {
            Console.WriteLine("Destructor Called");
        }

     
        // METHODS
     
        public void Deposit(double amount)
        {
            Balance += amount;
        }

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Deposit(params double[] amounts)
        {
            foreach (double x in amounts)
                Balance += x;
        }

        public void Deposit(ref double amount)
        {
            Balance += amount;
            amount = 0;
        }

        public void Deposit(out double bonus)
        {
            bonus = 100;
            Balance += bonus;
        }

        public void Withdraw(double amount)
        {
            if (Balance - amount >= MinBalance)
                Balance -= amount;
        }

        public virtual double CalculateInterest()
        {
            Console.WriteLine("Account Interest = 2%");
            return Balance * .02;
        }

        public virtual void Display()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine($"Bank : {BankName}");
            Console.WriteLine($"Account : {accountNumber}");
            Console.WriteLine($"Holder : {holderName}");
            Console.WriteLine($"Balance : {Balance}");

            Address.Display();
        }
    }

 
    // SINGLE INHERITANCE
 
    class SavingsAccount : Account
    {
        public SavingsAccount()
        {
            Console.WriteLine("Savings Default Constructor");
        }

        public SavingsAccount(int a, string n, double b)
            : base(a, n, b)
        {
            Console.WriteLine("Savings Parameterized Constructor");
        }

        public override double CalculateInterest()
        {
            Console.WriteLine("Savings Interest = 5%");
            return Balance * .05;
        }
    }

 
    // HIERARCHICAL INHERITANCE
 
    class CurrentAccount : Account
    {
        public CurrentAccount(int a, string n, double b)
            : base(a, n, b)
        {
            Console.WriteLine("Current Constructor");
        }

        // Method Hiding
        public new double CalculateInterest()
        {
            Console.WriteLine("Current Account Interest = 1%");
            return Balance * .01;
        }
    }

 
    // MULTILEVEL INHERITANCE
 
    class PremiumSavings : SavingsAccount
    {
        public PremiumSavings(int a, string n, double b)
            : base(a, n, b)
        {
            Console.WriteLine("Premium Constructor");
        }

        // Sealed Override
        public sealed override double CalculateInterest()
        {
            Console.WriteLine("Premium Interest = 8%");
            return Balance * .08;
        }
    }

 
    // MAIN
 
    class Program
    {
        static void Main()
        {
 
            // Variables and Data Types
 

            int age = 25;
            double salary = 25000.75;
            char grade = 'A';
            bool status = true;

            Console.WriteLine(age);
            Console.WriteLine(salary);
            Console.WriteLine(grade);
            Console.WriteLine(status);

 
            // Strings
 

            string first = "Smart";
            string second = "Bank";

            string full = first + " " + second;

            Console.WriteLine(full);

 
            // Operators
 

            int a = 20;
            int b = 6;

            Console.WriteLine(a + b);
            Console.WriteLine(a - b);
            Console.WriteLine(a * b);
            Console.WriteLine(a / b);
            Console.WriteLine(a % b);

 
            // Type Casting
 

            double x = 20.9;

            int y = (int)x;

            Console.WriteLine(y);

 
            // Value Type vs Reference Type
 

            int num1 = 10;
            int num2 = num1;

            num2 = 50;

            Console.WriteLine(num1);
            Console.WriteLine(num2);

            Account obj1 = new Account();
            Account obj2 = obj1;

            obj2.HolderName = "AIUB";

            Console.WriteLine(obj1.HolderName);
            Console.WriteLine(obj2.HolderName);

 
            // Object Creation
 

            SavingsAccount s1 = new SavingsAccount(101, "Rahim", 10000);

            s1.Address = new Address("Dhaka", "Bangladesh");

            s1.Display();

 
            // Deposit Overloading
 

            s1.Deposit(500);

            s1.Deposit(200.5);

            s1.Deposit(100, 200, 300);

            double amount = 1000;

            s1.Deposit(ref amount);

            Console.WriteLine(amount);

            double bonus;

            s1.Deposit(out bonus);

            Console.WriteLine(bonus);

            Console.WriteLine("Balance = " + s1.Balance);

 
            // Runtime Polymorphism
 

            Account acc;

            acc = new SavingsAccount(1, "Alice", 10000);
            Console.WriteLine(acc.CalculateInterest());

            acc = new PremiumSavings(2, "Bob", 10000);
            Console.WriteLine(acc.CalculateInterest());

 
            // Method Hiding
 

            CurrentAccount c = new CurrentAccount(3, "John", 10000);

            Console.WriteLine(c.CalculateInterest());

            Account a1 = c;

            Console.WriteLine(a1.CalculateInterest());

            
            // Copy Constructor
            

            Account copy = new Account(a1);

            copy.Display();

 
            // Enum
            

            AccountType type = AccountType.Savings;

            Console.WriteLine(type);

            
            // Single Dimensional Array


            int[] accounts = { 101, 102, 103, 104 };

            foreach (int i in accounts)
                Console.Write(i + " ");

            Console.WriteLine();

            Console.WriteLine(accounts.Length);

            
            // Multi-Dimensional Array
            

            int[,] matrix =
            {
                {1,2,3},
                {4,5,6}
            };

            foreach (int i in matrix)
                Console.Write(i + " ");

            Console.WriteLine();

            Console.WriteLine(matrix.GetLength(0));

            Console.WriteLine(matrix.GetLength(1));

            // Jagged Array
            

            int[][] jagged =
            {
                new int[]{10,20},
                new int[]{30,40,50},
                new int[]{60}
            };

            foreach (int[] row in jagged)
            {
                foreach (int value in row)
                    Console.Write(value + " ");

                Console.WriteLine();
            }

            Console.WriteLine(jagged.Length);

            Console.ReadLine();
        }
    }
}
