namespace Lab3;

public class ReeferContainer : Container
{
    public Dictionary<string, double> Products = new Dictionary<string, double>
    {
        { "bananas", 13.3 },
        { "chocolate", 18 },
        { "fish", 2 },
        { "meat", -15 },
        { "ice cream", -18 },
        { "frozen pizza", -30 },
        { "cheese", 7.2 },
        { "sausage", 5 },
        { "butter", 20.5 },
        { "eggs", 19 }
    };

    public string ProductType { get; set; }
    public double Temperature { get; set; }


    public ReeferContainer(double cargoWeight, double height, double containerWeight, double depth,
        double maxLoad, string product) : base(cargoWeight, height, containerWeight, depth, "C", maxLoad)
    {
        if (!Products.ContainsKey(product.ToLower()))
        {
            throw new ArgumentException($"Invalid product type: {product}");
        }

        ProductType = product.ToLower();
        Temperature = Products[product.ToLower()];
    }

    public override string ToString()
    {
        return base.ToString() + $"temperature={Temperature} \u00b0C, productType={ProductType})";
    }
}