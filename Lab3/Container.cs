namespace Lab3;

public abstract class Container
{
    public double CargoWeight { get; set; }
    public double Height { get; set; }
    public double ContainerWeight { get; set; }
    public double Depth { get; set; }
    public string Id { get; set; }
    public double MaxLoad { get; set; }

    private static int IdCounter = 1;


    public Container(double cargoWeight, double height, double containerWeight, double depth, string type,
        double maxLoad)
    {
        CargoWeight = cargoWeight;
        Height = height;
        ContainerWeight = containerWeight;
        Depth = depth;
        Id = $"KON-{type}-{IdCounter++}";
        MaxLoad = maxLoad;
    }

    public virtual void Unload()
    {
        CargoWeight = 0;
    }

    public virtual void Load(double load)
    {
        if (load + CargoWeight > MaxLoad)
        {
            throw new OverfillException("Cargo Weight + New Load is too heavy.");
        }

        CargoWeight += load;
    }
    
    public virtual void PrintInfo()
    {
        Console.WriteLine($"Container ID: {Id}");
        Console.WriteLine($"Cargo Weight: {CargoWeight} kg");
        Console.WriteLine($"Container Weight: {ContainerWeight} kg");
        Console.WriteLine($"Height: {Height} cm");
        Console.WriteLine($"Depth: {Depth} cm");
        Console.WriteLine($"Max Load: {MaxLoad} kg");
    }
}