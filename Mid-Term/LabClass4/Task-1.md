# Question 1

A software company is developing a Vehicle Management System to simulate how different vehicles start. Design the system using inheritance and runtime polymorphism.  Create a base class named Vehicle with a virtual method Start(). Create two derived classes, Car and Bike, that override the Start() method to display their own starting messages. Demonstrate runtime polymorphism by creating a base class reference, assigning it first to a Car object and then to a Bike object, and calling the Start() method each time.  Next, create another derived class named Truck that hides the Start() method using the new keyword instead of override, and demonstrate the difference by calling the method through both a Vehicle reference and a Truck reference.  Finally, create a class SportsCar that inherits from Car. Use the sealed override keyword in the Car class to prevent further overriding of the Start() method, then attempt to override it in SportsCar and observe the compiler error.  Add appropriate comments explaining which statements demonstrate runtime polymorphism, method hiding, compile-time behavior, and the purpose of the virtual, override, new, and sealed override keywords.

Clo

## Code

```csharp
using System;
namespace LabTask4 {
class vehicle
    {
        public virtual void start()
        {
            Console.WriteLine("Vehicle is starting.");
        }
    }
    class car : vehicle
    {
        public sealed override void start()
        {
            Console.WriteLine("Car is starting.");
        }
    }
    class bike : vehicle
    {
        public override void start()
        {
            Console.WriteLine("Bike is starting vroom vroom.");
        }
    }
    class Truck : vehicle
    {
        public new void start()
        {
            Console.WriteLine("Truck is starting.");
        }
    }
    class sportscar : car
    {
   
        // public override void start()
        // {
        //     Console.WriteLine("Sports car is starting.");
        // }
    }
    class progrom
    {
        static void Main(string[] args)
        {
            vehicle v1 = new vehicle();
            v1= new car();
            v1.start();
            v1= new bike();
            v1.start();
            vehicle v2 = new Truck();
            v2.start();
            Truck t1 = new Truck();
            t1.start();
            sportscar s1 = new sportscar();
            s1.start();
        }
    }
}

