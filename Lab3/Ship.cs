namespace Lab3;

public class Ship
{
    public List<Container> Containers { get; set; }
    public int MaxCapacity { get; set; }
    public double MaxSpeed { get; set; }
    public double MaxCargoWeight { get; set; }

    public Ship(int maxCapacity, double maxSpeed, double maxCargoWeight)
    {
        Containers = new List<Container>(MaxCapacity);
        MaxCapacity = maxCapacity;
        MaxSpeed = maxSpeed;
        MaxCargoWeight = maxCargoWeight;
    }

    public void AddContainer(Container newContainer)
    {
        double weightCheck = 0;

        foreach (var c in Containers)
        {
            weightCheck += c.ContainerWeight;
        }

        if (weightCheck + newContainer.ContainerWeight > MaxCargoWeight)
        {
            throw new OverfillException("Max cargo weight exceed");
        }

        if (Containers.Count + 1 >= MaxCapacity)
        {
            throw new OverfillException("Max cargo capacity exceed");
        }

        Containers.Add(newContainer);
    }

    public void AddContainers(List<Container> newContainers)
    {
        double weightCheck = 0;

        foreach (var c in Containers)
        {
            weightCheck += c.ContainerWeight;
        }

        foreach (var newContainer in newContainers)
        {
            weightCheck += newContainer.ContainerWeight;
        }

        if (weightCheck > MaxCargoWeight)
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


    public void PrintInfo()
    {
        Console.WriteLine($"Ship Information:");
        Console.WriteLine($"Max Capacity: {MaxCapacity} containers");
        Console.WriteLine($"Max Speed: {MaxSpeed} knots");
        Console.WriteLine($"Max Cargo Weight: {MaxCargoWeight} kg");
        Console.WriteLine($"Current Number of Containers: {Containers.Count}");
        Console.WriteLine();

        Console.WriteLine("Containers on the Ship:");
        Console.WriteLine("-----------------------");
        if (Containers.Count == 0)
        {
            Console.WriteLine("No containers on board.");
        }
        else
        {
            foreach (var container in Containers)
            {
                container.PrintInfo();
                Console.WriteLine("-----------------------");
            }
        }
    }
}