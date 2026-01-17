using Inventory_o.UI;
using Spectre.Console;

ShowAscii();
Menu.Run();

void ShowAscii()
{
    AnsiConsole.Clear();

    var logo = new FigletText("Inventory-o")
        .Centered()
        .Color(Color.Cyan);

    AnsiConsole.Write(logo);

    AnsiConsole.Write(
        new Markup("[dim cyan]your inventory, easier[/]")
            .Centered()
    );

    AnsiConsole.WriteLine();
    AnsiConsole.WriteLine();

    AnsiConsole.Write(
        new Markup("[bold white]Press ENTER to start[/]")
            .Centered()
    );

    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
}