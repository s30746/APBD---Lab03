namespace Lab3;

public class Ship
{
    public List<Container> Containers { get; set; }
    public int MaxCapacity { get; set; }
    public double MaxSpeed { get; set; }
    public double MaxCargoWeight { get; set; }

    public int Id { get; set; }

    private static int IdCounter = 1;

    public Ship(int maxCapacity, double maxSpeed, double maxCargoWeight)
    {
        Containers = new List<Container>(MaxCapacity);
        MaxCapacity = maxCapacity;
        MaxSpeed = maxSpeed;
        MaxCargoWeight = maxCargoWeight;
        Id = IdCounter++;
    }

    public void AddContainer(Container newContainer)
    {
        double totalWeight = Containers.Sum(c => c.ContainerWeight + c.CargoWeight); 

        if (totalWeight + newContainer.ContainerWeight + newContainer.CargoWeight > MaxCargoWeight * 1000)
        {
            throw new OverfillException("Max cargo weight exceeded");
        }

        if (Containers.Count >= MaxCapacity) 
        {
            throw new OverfillException("Max cargo capacity exceeded");
        }

        Containers.Add(newContainer);
    }

    public void AddContainers(List<Container> newContainers)
    {
        double totalWeight = Containers.Sum(c => c.ContainerWeight + c.CargoWeight);
        double newContainersWeight = newContainers.Sum(c => c.ContainerWeight + c.CargoWeight);

        if (totalWeight + newContainersWeight > MaxCargoWeight * 1000)
        {
            throw new OverfillException("Max cargo weight exceeded");
        }

        if (Containers.Count + newContainers.Count > MaxCapacity)
        {
            throw new OverfillException("Max cargo capacity exceeded");
        }

        Containers.AddRange(newContainers);
    }


    public void RemoveContainer(string containerId)
    {
        Container containerToRemove = Containers.Find(c => c.Id == containerId);

        if (containerToRemove == null)
        {
            Console.WriteLine("Container not found on the ship.");
        }
        else
        {
            Containers.Remove(containerToRemove);
            Console.WriteLine($"Container {containerId} has been removed from the ship.");
        }
    }

    public void ReplaceContainer(string containerId, Container newContainer)
    {
        int index = Containers.FindIndex(c => c.Id == containerId);

        if (index != -1)
        {
            Containers[index] = newContainer;
            Console.WriteLine($"Container {containerId} has been replaced from the ship.");
        }
        else
        {
            Console.WriteLine("Container not found on the ship.");
        }
    }

    public static void TransferContainer(Ship from, Ship to, string containerId)
    {
        Container transferContainer = from.Containers.Find(c => c.Id == containerId);

        if (transferContainer == null)
        {
            Console.WriteLine("Container not found on the ship.");
        }
        else
        {
            to.AddContainer(transferContainer);
            from.RemoveContainer(containerId);
            Console.WriteLine($"Container {containerId} has been transferred from Source Ship to Destination Ship.");
        }
    }


    public override string ToString()
    {
        return $"Ship {Id} (speed={MaxSpeed} knots, maxContainerNum={MaxCapacity}, maxWeight={MaxCargoWeight} t)";
    }

    public void PrintContainers()
    {
        Console.WriteLine(
            $"Ship {Id} (speed={MaxSpeed} knots, maxContainerNum={MaxCapacity}, maxWeight={MaxCargoWeight} t)");
        Console.WriteLine("-----------------------");
        Console.WriteLine("Containers on the Ship:");
        Console.WriteLine("-----------------------");

        foreach (var c in Containers)
        {
            Console.WriteLine(c);
        }
    }
}