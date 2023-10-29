using System;
using System.Collections.Generic;

// Абстрактний клас Vehicle
public abstract class Vehicle
{
    public int Speed { get; set; }
    public int Capacity { get; set; }

    public abstract void Move();
}

// Клас Human
public class Human
{
    public int Speed { get; set; }

    public Human(int speed)
    {
        Speed = speed;
    }

    public void Move()
    {
        Console.WriteLine("Human is moving");
    }
}

// Спадкоємці класу Vehicle: Car, Bus, Train
public class Car : Vehicle
{
    public string FuelType { get; set; }

    public Car(int speed, int capacity, string fuelType)
    {
        Speed = speed;
        Capacity = capacity;
        FuelType = fuelType;
    }

    public override void Move()
    {
        Console.WriteLine($"Car is moving at {Speed} km/h with a capacity of {Capacity} passengers.");
    }
}

public class Bus : Vehicle
{
    public int RouteNumber { get; set; }

    public Bus(int speed, int capacity, int routeNumber)
    {
        Speed = speed;
        Capacity = capacity;
        RouteNumber = routeNumber;
    }

    public override void Move()
    {
        Console.WriteLine($"Bus is moving at {Speed} km/h on route {RouteNumber} with a capacity of {Capacity} passengers.");
    }
}

public class Train : Vehicle
{
    public string TrainType { get; set; }

    public Train(int speed, int capacity, string trainType)
    {
        Speed = speed;
        Capacity = capacity;
        TrainType = trainType;
    }

    public override void Move()
    {
        Console.WriteLine($"Train is moving at {Speed} km/h with a capacity of {Capacity} passengers in a {TrainType} train.");
    }
}

// Клас TransportNetwork
public class TransportNetwork
{
    private List<Vehicle> vehicles = new List<Vehicle>();

    public void AddVehicle(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
    }

    public void ControlTraffic()
    {
        foreach (var vehicle in vehicles)
        {
            vehicle.Move();
        }
    }
}

class Program
{
    static void Main()
    {
        // Створення об'єктів і робота з ними
        // ...
    }
}
