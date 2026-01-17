using Spectre.Console;
using Inventory_o.Models;
using Inventory_o.Data;

namespace Inventory_o.UI
{
    public static class ProductOperations
    {
        public static void ShowAll()
        {
            AnsiConsole.Write(new Panel("[cyan]Inventory-o[/] [red]|[/] [white]All Products[/]"));
            AnsiConsole.WriteLine();

            var products = ProductData.GetAll();

            if (!products.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No products found[/]");
                return;
            }

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Description");
            table.AddColumn("Quantity");

            foreach (var product in products)
            {
                table.AddRow(
                    product.Id.ToString(),
                    product.Name,
                    product.Description,
                    product.Quantity.ToString()
                );
            }

            AnsiConsole.Write(table);
        }

        public static void Add()
        {
            AnsiConsole.Write(new Panel("[cyan]Inventory-o[/] [red]|[/] [green]Add Product[/]"));
            AnsiConsole.WriteLine();

            var product = new Product
            {
                Name = AnsiConsole.Ask<string>("Product name:"),
                Description = AnsiConsole.Ask<string>("Description:"),
                Quantity = AnsiConsole.Ask<int>("Quantity:")
            };

            ProductData.Add(product);

            AnsiConsole.MarkupLine("[green]Product added[/]");
        }

        public static void Delete()
        {
            AnsiConsole.Write(new Panel("[cyan]Inventory-o[/] [red]|[/] [red]Delete Product[/]"));
            AnsiConsole.WriteLine();

            var products = ProductData.GetAll();
            if (!products.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No products to delete[/]");
                return;
            }

            var id = AnsiConsole.Ask<int>("Product ID:");
            var product = ProductData.GetById(id);

            if (product == null)
            {
                AnsiConsole.MarkupLine("[red]Product not found[/]");
                return;
            }

            AnsiConsole.MarkupLine($"[yellow]Deleting: {product.Name}[/]");

            if (!AnsiConsole.Confirm("Are you sure?"))
            {
                AnsiConsole.MarkupLine("[yellow]Cancelled[/]");
                return;
            }

            ProductData.Delete(id);
            AnsiConsole.MarkupLine("[green]Product deleted[/]");
        }

        public static void Edit()
        {
            AnsiConsole.Write(new Panel("[cyan]Inventory-o[/] [red]|[/] [yellow]Edit Product[/]"));
            AnsiConsole.WriteLine();

            var products = ProductData.GetAll();
            if (!products.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No products to edit[/]");
                return;
            }

            var id = AnsiConsole.Ask<int>("Product ID:");
            var product = ProductData.GetById(id);

            if (product == null)
            {
                AnsiConsole.MarkupLine("[red]Product not found[/]");
                return;
            }

            AnsiConsole.MarkupLine($"[yellow]Current: {product.Name}[/]");
            AnsiConsole.WriteLine();

            product.Name = AnsiConsole.Ask("New name:", product.Name);
            product.Description = AnsiConsole.Ask("New description:", product.Description);
            product.Quantity = AnsiConsole.Ask("New quantity:", product.Quantity);

            ProductData.Update(product);

            AnsiConsole.MarkupLine("[green]Product updated[/]");
        }

        public static void ReloadDatabase()
        {
            AnsiConsole.Status()
                .Start("Reloading", ctx =>
                {
                    ProductData.Reload();
                    Thread.Sleep(500);
                });

            AnsiConsole.MarkupLine("[green]Database reloaded[/]");
        }
    }
}