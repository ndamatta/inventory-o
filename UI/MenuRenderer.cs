using Spectre.Console;
using InventoryApp.Models;

namespace InventoryApp.UI;

public class MenuRenderer
{
    public void Render(List<Product> products, List<Product> pageProducts, MenuState state, int pageCount)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold cyan]Inventory-o[/]");

        if (!string.IsNullOrWhiteSpace(state.SearchQuery))
            AnsiConsole.MarkupLine($"[yellow]Search:[/] {state.SearchQuery}");

        AnsiConsole.MarkupLine("");

        if (!products.Any())
        {
            AnsiConsole.MarkupLine("[red]No products found[/]");
            RenderFooter(products, state, pageCount);
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("ID")
            .AddColumn("Name")
            .AddColumn("Description")
            .AddColumn("Stock");

        for (int i = 0; i < pageProducts.Count; i++)
        {
            var p = pageProducts[i];

            if (i == state.SelectedIndex)
            {
                table.AddRow(
                    $"[black on yellow]{p.Id.ToString()[..8]}[/]",
                    $"[black on yellow]{p.Name}[/]",
                    $"[black on yellow]{p.Description}[/]",
                    $"[black on yellow]{p.Stock}[/]"
                );
            }
            else
            {
                table.AddRow(p.Id.ToString()[..8], p.Name, p.Description, p.Stock.ToString());
            }
        }

        AnsiConsole.Write(table);
        RenderFooter(products, state, pageCount);
    }

private void RenderFooter(List<Product> products, MenuState state, int pageCount)
{
    var saveHint = state.HasUnsavedChanges ? "[yellow]UNSAVED CHANGES[/] | " : "";
    var pageInfo = $"Page {state.CurrentPage + 1}/{Math.Max(pageCount, 1)}";

    AnsiConsole.MarkupLine($"{saveHint}[bold cyan]{pageInfo}[/]");

    var toolsPanel = new Panel("[white]F1 Add | F2 Search | F3 Edit | + - Stock | F5 Export PDF[/]")
        .Header("[cyan]Tools[/]")
        .BorderColor(Color.Cyan);

    AnsiConsole.Write(toolsPanel);
    AnsiConsole.MarkupLine("");
    AnsiConsole.MarkupLine(
        "[grey]ENTER Save | ESC Exit | UP/DOWN Move | LEFT/RIGHT Page[/]"
    );
}

}
