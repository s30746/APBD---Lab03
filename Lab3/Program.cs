using Lab3;

internal class Program
{
    public static List<Ship> Ships = new List<Ship>();
    public static List<Container> Containers = new List<Container>();

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("List of container ships:");
            if (Ships.Count == 0) Console.WriteLine("None");
            else Ships.ForEach(s => Console.WriteLine(s));

            Console.WriteLine("\nList of containers:");
            if (Containers.Count == 0) Console.WriteLine("None");
            else Containers.ForEach(c => Console.WriteLine(c));

            Console.WriteLine("\nActions: ");
            Console.WriteLine("1. Add a container ship");
            Console.WriteLine("2. Add a container");
            Console.WriteLine("3. Remove a container ship");
            Console.WriteLine("4. Remove a container");
            Console.WriteLine("5. Load a container");
            Console.WriteLine("6. Unload a container");
            Console.WriteLine("7. Load a container onto a ship");
            Console.WriteLine("8. Remove a container from a ship");
            Console.WriteLine("9. Transfer a container between ships");
            Console.WriteLine("10. Display ship details");
            Console.WriteLine("11. Exit");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddShip();
                    break;
                case "2":
                    AddContainer();
                    break;
                case "3":
                    RemoveShip();
                    break;
                case "4":
                    RemoveContainer();
                    break;
                case "5":
                    LoadContainer();
                    break;
                case "6":
                    UnloadContainer();
                    break;
                case "7":
                    LoadContainerOntoShips();
                    break;
                case "8":
                    RemoveContainerFromShip();
                    break;
                case "9":
                    TransferContainerBetweenShips();
                    break;
                case "10":
                    ShowShipDetails();
                    break;
                case "11":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private static void AddShip()
    {
        Console.Write("Enter the maximum number of containers: ");
        int maxContainers = int.Parse(Console.ReadLine());
        Console.Write("Enter the maximum speed (knots): ");
        int speed = int.Parse(Console.ReadLine());
        Console.Write("Enter the maximum cargo weight (t): ");
        int maxWeight = int.Parse(Console.ReadLine());

        Ships.Add(new Ship(maxContainers, speed, maxWeight));

        Console.WriteLine("Ship added!");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void AddContainer()
    {
        Console.WriteLine("Choose container type: \n1) Liquid \n2) Reefer \n3) Gas");
        string type = Console.ReadLine();

        Console.Write("Enter cargo weight (kg): ");
        double cargoWeight = int.Parse(Console.ReadLine());
        Console.Write("Enter height (cm): ");
        double height = int.Parse(Console.ReadLine());
        Console.Write("Enter self-weight (kg): ");
        double selfWeight = int.Parse(Console.ReadLine());
        Console.Write("Enter depth (cm): ");
        double depth = int.Parse(Console.ReadLine());
        Console.Write("Enter maximum load capacity (kg): ");
        double maxLoad = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                Console.Write("Is the cargo hazardous?(true/false): ");
                bool isHazardous = bool.Parse(Console.ReadLine());
                Containers.Add(new LiquidContainer(cargoWeight, height, selfWeight, depth, maxLoad, isHazardous));
                Console.WriteLine("Container added!");
                break;
            case "2":
                Console.Write("Enter product type: ");
                string product = Console.ReadLine();
                try
                {
                    Containers.Add(new ReeferContainer(cargoWeight, height, selfWeight, depth, maxLoad, product));
                    Console.WriteLine("Container added!");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
                break;
            case "3":
                Console.Write("Enter pressure (atm): ");
                double pressure = double.Parse(Console.ReadLine());
                Containers.Add(new GasContainer(cargoWeight, height, selfWeight, depth, maxLoad, pressure));
                Console.WriteLine("Container added!");
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
    
    private static void RemoveContainer()
    {
        Console.Write("Enter container ID: ");
        string id = Console.ReadLine();

        Container container = Containers.Find(c => c.Id == id);
        if (container == null)
        {
            Console.WriteLine("Container not found!");
        }
        else
        {
            Containers.Remove(container);
            Console.WriteLine($"Container {id} removed!");
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void RemoveShip()
    {
        Console.Write("Enter ship ID to remove: ");
        int id = int.Parse(Console.ReadLine());

        Ship ship = Ships.Find(s => s.Id == id);
        if (ship == null)
        {
            Console.WriteLine("Ship not found!");
        }
        else if (ship.Containers.Count > 0)
        {
            Console.WriteLine("Cannot remove ship with containers on board. Unload them first.");
        }
        else
        {
            Ships.Remove(ship);
            Console.WriteLine($"Ship {id} removed!");
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }


    private static void LoadContainer()
    {
        Console.Write("Enter ID of container: ");
        string id = Console.ReadLine();
        Console.Write("Enter amount of cargo weight: ");
        double cargoWeight = double.Parse(Console.ReadLine());

        var c = Containers.Find(c => c.Id == id);
        if (c == null)
        {
            Console.WriteLine("Container not found!");
        }
        else
        {
            try
            {
                c.Load(cargoWeight);
                Console.WriteLine("Container loaded successfully!");
            }
            catch (OverfillException e)
            {
                Console.WriteLine($"Error while loading container: {e.Message}");
            }
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void UnloadContainer()
    {
        Console.Write("Enter ID of container: ");
        string id = Console.ReadLine();

        var c = Containers.Find(c => c.Id == id);
        if (c == null)
        {
            Console.WriteLine("Container not found!");
        }
        else
        {
            c.Unload();
            Console.WriteLine("Container unloaded!");
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void LoadContainerOntoShips()
    {
        Console.Write("Enter ship number: ");
        int shipId = int.Parse(Console.ReadLine());
        Console.Write("Enter container ID: ");
        string containerId = Console.ReadLine();

        Ship ship = Ships.Find(s => s.Id == shipId);
        Container container = Containers.Find(c => c.Id == containerId);

        if (ship == null)
        {
            Console.WriteLine("Ship not found!");
        }
        else if (container == null)
        {
            Console.WriteLine("Container not found!");
        }
        else
        {
            try
            {
                ship.AddContainer(container);
                Console.WriteLine("Container loaded onto the ship successfully!");
            }
            catch (OverfillException e)
            {
                Console.WriteLine($"Error while loading container onto ship: {e.Message}");
            }
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void RemoveContainerFromShip()
    {
        Console.Write("Enter ship number: ");
        int shipId = int.Parse(Console.ReadLine());
        Console.Write("Enter container ID: ");
        string containerId = Console.ReadLine();

        Ship ship = Ships.Find(s => s.Id == shipId);

        if (ship == null)
        {
            Console.WriteLine("Ship not found!");
        }
        else
        {
            ship.RemoveContainer(containerId);
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void TransferContainerBetweenShips()
    {
        Console.Write("Enter source ship ID: ");
        int fromShip = int.Parse(Console.ReadLine());
        Console.Write("Enter destination ship ID: ");
        int toShip = int.Parse(Console.ReadLine());
        Console.Write("Enter container ID: ");
        string containerID = Console.ReadLine();


        Ship from = Ships.Find(s => s.Id == fromShip);
        Ship to = Ships.Find(s => s.Id == toShip);

        if (from == null || to == null)
        {
            Console.WriteLine("Ship not found!");
        }
        else
        {
            try
            {
                Ship.TransferContainer(from, to, containerID);
            }
            catch (OverfillException e)
            {
                Console.WriteLine($"Error while transferring container: {e.Message}");
            }
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void ShowShipDetails()
    {
        Console.Write("Enter ship ID: ");
        int shipIndex = int.Parse(Console.ReadLine());


        Ship ship = Ships.Find(s => s.Id == shipIndex);
        if (ship == null)
        {
            Console.WriteLine("Ship not found!");
        }
        else
        {
            ship.PrintContainers();
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}