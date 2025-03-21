namespace Lab3;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; set; }

    public LiquidContainer(double cargoWeight, double height, double containerWeight, double depth,
        double maxLoad, bool isHazardous) : base(cargoWeight, height, containerWeight, depth, "L",
        maxLoad)
    {
        IsHazardous = isHazardous;
    }


    public void Notify(string msg)
    {
        Console.WriteLine($"HAZARD ALERT {Id}: {msg}");
    }

    public override void Load(double load)
    {
        if (load + CargoWeight > (IsHazardous ? MaxLoad * 0.5 : MaxLoad * 0.9))
        {
            Notify("Load is too heavy.");
            throw new OverfillException("Cargo Weight + New Load is too heavy.");
        }

        CargoWeight += load;
    }

    public override string ToString()
    {
        return base.ToString() + $"isHazardous={IsHazardous})";
    }
}