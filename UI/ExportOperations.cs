using Spectre.Console;
using Inventory_o.Data;
using Inventory_o.Exports;

namespace Inventory_o.UI
{
    public static class ExportOperations
    {
        public static void Export()
        {
            AnsiConsole.Write(new Panel("[cyan]Inventory-o[/] [red]|[/] [magenta]Export Products[/]"));
            AnsiConsole.WriteLine();

            var products = ProductData.GetAll();

            if (!products.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No products to export[/]");
                return;
            }

            var format = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select export format:")
                    .AddChoices(new[] { "PDF", "CSV", "Back to main menu" })
            );

            if (format == "Back to main menu")
            {
                return;
            }

            var filename = AnsiConsole.Ask("Filename:", "products");

            AnsiConsole.Status()
                .Start($"Generating {format}...", ctx =>
                {
                    if (format == "PDF")
                    {
                        ProductExporter.ExportToPdf(products, filename);
                    }
                    else
                    {
                        ProductExporter.ExportToCsv(products, filename);
                    }
                });

            AnsiConsole.MarkupLine($"[green]Exported to {filename}.{format.ToLower()}[/]");
        }
    }
}