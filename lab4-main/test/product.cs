namespace _2025_KN_23_Lab3;

public struct Product
{
    public int Id;
    public int Price;
    public string Name = "";
    public string Desription;
    public int Quantity;

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public bool IsEmpty()
    {
        return String.IsNullOrWhiteSpace(Name);
    }
}