namespace Lab3;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; set; }

    public GasContainer(double cargoWeight, double height, double containerWeight, double depth,
        double maxLoad, double pressure) : base(cargoWeight, height, containerWeight, depth, "G", maxLoad)
    {
        Pressure = pressure;
    }

    public void Notify(string msg)
    {
        Console.WriteLine($"HAZARD ALERT {Id}: {msg}");
    }

    public override void Unload()
    {
        CargoWeight = 0.05 * CargoWeight;
    }

    public override string ToString()
    {
        return base.ToString() + $"pressure={Pressure} atm)";
    }
}