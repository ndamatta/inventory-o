using InventoryApp.Data;
using InventoryApp.Models;

namespace InventoryApp.UI;

public class Menu
{
    private readonly InventoryRepository _repo;
    private readonly MenuState _state;
    private readonly MenuRenderer _renderer;
    private readonly MenuActions _actions;

    public Menu(InventoryRepository repo)
    {
        _repo = repo;
        _state = new MenuState();
        _renderer = new MenuRenderer();
        _actions = new MenuActions(repo, _state);
    }

    public void Run()
    {
        ConsoleKey key;

        do
        {
            var products = GetFilteredProducts();
            var pageProducts = GetCurrentPageProducts(products);
            var pageCount = GetPageCount(products);

            _renderer.Render(products, pageProducts, _state, pageCount);

            key = Console.ReadKey(true).Key;

            if (!products.Any())
                continue;

            if (key == ConsoleKey.UpArrow && _state.SelectedIndex > 0)
                _state.SelectedIndex--;

            if (key == ConsoleKey.DownArrow && _state.SelectedIndex < pageProducts.Count - 1)
                _state.SelectedIndex++;

            if (key == ConsoleKey.LeftArrow && _state.CurrentPage > 0)
            {
                _state.CurrentPage--;
                _state.SelectedIndex = 0;
            }

            if (key == ConsoleKey.RightArrow && _state.CurrentPage < pageCount - 1)
            {
                _state.CurrentPage++;
                _state.SelectedIndex = 0;
            }

            if (key == ConsoleKey.F1)
                _actions.AddProduct();

            if (key == ConsoleKey.F2)
                _actions.Search();

            if (key == ConsoleKey.Delete)
                _actions.DeleteProduct(pageProducts[_state.SelectedIndex]);

            if (key == ConsoleKey.F3)
                _actions.EditProduct(pageProducts[_state.SelectedIndex]);

            if (key == ConsoleKey.Add || key == ConsoleKey.OemPlus)
                _actions.IncreaseStock(pageProducts[_state.SelectedIndex]);

            if (key == ConsoleKey.Subtract || key == ConsoleKey.OemMinus)
                _actions.DecreaseStock(pageProducts[_state.SelectedIndex]);

            if (key == ConsoleKey.Enter)
                _actions.SaveChanges(pageProducts[_state.SelectedIndex]);
            if (key == ConsoleKey.F5)
                _actions.ExportPdf();

        } while (key != ConsoleKey.Escape);
    }

    private List<Product> GetFilteredProducts()
    {
        var products = _repo.GetAll();

        if (string.IsNullOrWhiteSpace(_state.SearchQuery))
            return products;

        return products
            .Where(p =>
                p.Name.Contains(_state.SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(_state.SearchQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private List<Product> GetCurrentPageProducts(List<Product> products)
    {
        return products
            .Skip(_state.CurrentPage * MenuState.PageSize)
            .Take(MenuState.PageSize)
            .ToList();
    }

    private int GetPageCount(List<Product> products)
    {
        return (int)Math.Ceiling(products.Count / (double)MenuState.PageSize);
    }
}
