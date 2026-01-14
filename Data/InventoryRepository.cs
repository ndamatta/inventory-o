using System.Text.Json;
using InventoryApp.Models;

namespace InventoryApp.Data;

public class InventoryRepository
{
    private const string FileName = "inventory.json";
    private List<Product> _products = new();

    public InventoryRepository()
    {
        Load();
    }

    public List<Product> GetAll() => _products;

    public void Add(Product product)
    {
        _products.Add(product);
        Save();
    }

    public void Delete(Product product)
    {
        _products.Remove(product);
        Save();
    }

    public void Update(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index >= 0)
        {
            _products[index] = product;
            Save();
        }
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileName, json);
    }

    private void Load()
    {
        if (!File.Exists(FileName)) return;

        _products = JsonSerializer.Deserialize<List<Product>>(File.ReadAllText(FileName)) ?? new();
    }
}
