﻿using Lab3;

internal class Program
{
    public static void Main(string[] args)
    {
        Ship ship1 = new Ship(6, 20, 12000);
        Ship ship2 = new Ship(2, 30, 4000);

        // Stworzenie kontenerów różnych typów
        LiquidContainer milkContainer = new LiquidContainer(500, 250, 1000, 1200, 1000, false);
        ReeferContainer bananasContainer = new ReeferContainer(200, 250, 1000, 1000, 1000, "bananas");
        GasContainer heliumContainer = new GasContainer(600, 250, 1000, 1200, 800, 1.45);

        // Wypisanie informacji o kontenerach
        Console.WriteLine("=== Informacje o kontenerach ===");
        milkContainer.PrintInfo();
        Console.WriteLine();
        bananasContainer.PrintInfo();
        Console.WriteLine();
        heliumContainer.PrintInfo();

        // Załadowanie ładunku do kontenerów
        Console.WriteLine("\n=== Ładowanie kontenerów ===");
        milkContainer.Load(400);
        bananasContainer.Load(800);
        heliumContainer.Load(200);

        // Załadowanie kontenerów na statek
        Console.WriteLine("\n=== Dodawanie kontenerów do statku ===");
        ship1.AddContainer(milkContainer);
        ship1.AddContainer(bananasContainer);
        ship1.AddContainer(heliumContainer);
        ship1.PrintInfo();

        // Rozładowanie kontenerów
        Console.WriteLine("\n=== Rozładowanie kontenerów ===");
        milkContainer.Unload();
        bananasContainer.Unload();
        heliumContainer.Unload();
        ship1.PrintInfo();

        // Załadowanie listy kontenerów
        Console.WriteLine("\n=== Dodawanie wielu kontenerów na statek ===");
        List<Container> containers = new List<Container>
        {
            new LiquidContainer(500, 250, 1000, 1200, 1500, true),
            new ReeferContainer(300, 250, 1000, 1000, 1000, "chocolate"),
            new GasContainer(450, 250, 1000, 1200, 800, 1.75)
        };

        ship1.AddContainers(containers);
        ship1.PrintInfo();

        // Usunięcie kontenera ze statku
        Console.WriteLine("\n=== Usunięcie kontenera ze statku ===");
        ship1.RemoveContainer("KON-C-2");

        // Zastąpienie kontenera na statku
        Console.WriteLine("\n=== Zastępowanie kontenera na statku ===");
        ship1.ReplaceContainer("KON-L-1", new LiquidContainer(300, 250, 1000, 1200, 2000, false));

        // Przenoszenie kontenera między statkami
        Console.WriteLine("\n=== Przenoszenie kontenera między statkami ===");
        Ship.TransferContainer(ship1, ship2, "KON-L-7");

        // Wypisanie informacji o statkach
        Console.WriteLine("\n=== Informacje o statkach ===");
        ship1.PrintInfo();
        Console.WriteLine();
        ship2.PrintInfo();
    }
}