using Spectre.Console;
using InventoryApp.Data;
using InventoryApp.Models;
using InventoryApp.Files;

namespace InventoryApp.UI;

public class MenuActions
{
    private readonly InventoryRepository _repo;
    private readonly MenuState _state;

    public MenuActions(InventoryRepository repo, MenuState state)
    {
        _repo = repo;
        _state = state;
    }

    public void AddProduct()
    {
        AnsiConsole.Clear();

        var name = AnsiConsole.Ask<string>("Name:");
        var desc = AnsiConsole.Ask<string>("Description:");
        var stock = AnsiConsole.Ask<int>("Stock:");

        _repo.Add(new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = desc,
            Stock = stock
        });
    }

    public void Search()
    {
        AnsiConsole.Clear();
        var query = AnsiConsole.Ask<string>("Search (leave empty to clear):");

        _state.SearchQuery = string.IsNullOrWhiteSpace(query) ? null : query;
        _state.CurrentPage = 0;
        _state.SelectedIndex = 0;
    }

    public void DeleteProduct(Product product)
    {
        if (!AnsiConsole.Confirm($"Delete [red]{product.Name}[/]?"))
            return;

        _repo.Delete(product);
        _state.SelectedIndex = 0;
    }

    public void EditProduct(Product product)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"Editing [bold]{product.Name}[/]\n");

        var name = AnsiConsole.Ask<string>($"Name ({product.Name}):");
        var desc = AnsiConsole.Ask<string>($"Description ({product.Description}):");
        var stockText = AnsiConsole.Ask<string>($"Stock ({product.Stock}):");

        if (!string.IsNullOrWhiteSpace(name)) product.Name = name;
        if (!string.IsNullOrWhiteSpace(desc)) product.Description = desc;
        if (int.TryParse(stockText, out var stock)) product.Stock = stock;

        _repo.Update(product);
    }

    public void IncreaseStock(Product product)
    {
        product.Stock++;
        _state.HasUnsavedChanges = true;
    }

    public void DecreaseStock(Product product)
    {
        if (product.Stock > 0)
        {
            product.Stock--;
            _state.HasUnsavedChanges = true;
        }
    }

    public void SaveChanges(Product product)
    {
        if (!_state.HasUnsavedChanges)
            return;

        _repo.Update(product);
        _state.HasUnsavedChanges = false;
    }

    public void ExportPdf()
    {
        var path = $"inventory_report_{DateTime.Now:yyyyMMdd_HHmm}.pdf";

        PdfExporter.Export(path, _repo.GetAll());

        AnsiConsole.MarkupLine($"[green]PDF exported: {path}[/]");
        Console.ReadKey(true);
    }
}
