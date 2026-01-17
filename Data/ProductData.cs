using System.Text.Json;
using Inventory_o.Models;

namespace Inventory_o.Data
{
    public static class ProductData
    {
        private static readonly string FilePath = "products.json";
        private static List<Product> _products = new();

        public static void Initialize()
        {
            if (!File.Exists(FilePath))
            {
                _products = new List<Product>();
                Save();
            }
            else
            {
                Load();
            }
        }

        public static List<Product> GetAll()
        {
            return _products;
        }

        public static Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public static void Add(Product product)
        {
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
            Save();
        }

        public static void Update(Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Description = product.Description;
                existing.Quantity = product.Quantity;
                Save();
            }
        }

        public static void Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
                Save();
            }
        }

        public static void Reload()
        {
            Load();
        }

        private static void Load()
        {
            try
            {
                var json = File.ReadAllText(FilePath);
                _products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
            }
            catch
            {
                _products = new List<Product>();
            }
        }

        private static void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_products, options);
            File.WriteAllText(FilePath, json);
        }
    }
}