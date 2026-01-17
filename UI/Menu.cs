using Inventory_o.Data;
using Inventory_o.Models;
using Inventory_o.UI;
using Spectre.Console;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Inventory_o.UI
{
    public class Menu
    {
        public static void Run()
        {
            ProductData.Initialize();

            while (true)
            {
                AnsiConsole.Clear();
                DisplayHeader();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Select an option:[/]")
                        .AddChoices(new[] {
                            "See all products",
                            "Add product",
                            "Delete product",
                            "Edit product",
                            "Reload database",
                            "Export products",
                            "Exit"
                        })
                );

                if (choice == "Exit") break;

                AnsiConsole.Clear();
                HandleChoice(choice);
            }
        }

        private static void DisplayHeader()
        {
            AnsiConsole.Write(
                new Panel("[cyan bold]Inventory-o[/]")
                    .BorderColor(Color.Cyan)
            );
            AnsiConsole.WriteLine();
        }

        private static void HandleChoice(string choice)
        {
            switch (choice)
            {
                case "See all products":
                    ProductOperations.ShowAll();
                    break;
                case "Add product":
                    ProductOperations.Add();
                    break;
                case "Delete product":
                    ProductOperations.Delete();
                    break;
                case "Edit product":
                    ProductOperations.Edit();
                    break;
                case "Reload database":
                    ProductOperations.ReloadDatabase();
                    break;
                case "Export products":
                    ExportOperations.Export();
                    break;
            }

            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[grey]Press any key to continue[/]");
            Console.ReadKey(true);
        }
    }
}