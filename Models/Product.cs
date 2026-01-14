namespace InventoryApp.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int Stock { get; set; }
}
