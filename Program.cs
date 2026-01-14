using System.Diagnostics;
using InventoryApp.Data;
using InventoryApp.UI;
using Spectre.Console;

ShowAscii();

var repo = new InventoryRepository();
var menu = new Menu(repo);

menu.Run();

void ShowAscii()
{
    AnsiConsole.Clear();

    var logo = @"
    ____                      __                                    
   /  _/___ _   _____  ____  / /_____  _______  __             ____ 
   / // __ \ | / / _ \/ __ \/ __/ __ \/ ___/ / / /  ______    / __ \
 _/ // / / / |/ /  __/ / / / /_/ /_/ / /  / /_/ /  /_____/   / /_/ /
/___/_/ /_/|___/\___/_/ /_/\__/\____/_/   \__, /             \____/ 
                                         /____/                     
";

    AnsiConsole.MarkupLine($"[bold cyan]{logo}[/]");
    AnsiConsole.MarkupLine($"[cyan]your inventory, easier[/]");
    AnsiConsole.MarkupLine("\n[bold white]Press ENTER to start Inventory-o[/]");

    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
}
