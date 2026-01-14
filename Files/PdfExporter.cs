using InventoryApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InventoryApp.Files;

public static class PdfExporter
{
    public static void Export(string path, List<Product> products)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .Text("Inventory-o Report")
                    .FontSize(20)
                    .Bold()
                    .AlignCenter();

                page.Content().PaddingVertical(10).Column(col =>
                {
                    col.Item().Text($"Generated: {DateTime.Now}");
                    col.Item().Text($"Total Products: {products.Count}");
                    col.Item().Text($"Total Stock: {products.Sum(p => p.Stock)}");

                    col.Item().PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("ID").Bold();
                            header.Cell().Text("Name").Bold();
                            header.Cell().Text("Description").Bold();
                            header.Cell().Text("Stock").Bold();
                        });

                        foreach (var p in products)
                        {
                            table.Cell().Text(p.Id.ToString()[..8]);
                            table.Cell().Text(p.Name);
                            table.Cell().Text(p.Description);
                            table.Cell().Text(p.Stock.ToString());
                        }
                    });
                });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Inventory-o • ");
                        x.Span(DateTime.Now.ToShortDateString());
                    });
            });
        })
        .GeneratePdf(path);
    }
}
