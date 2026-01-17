using Inventory_o.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text;

namespace Inventory_o.Exports
{
    public static class ProductExporter
    {
        static ProductExporter()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public static void ExportToPdf(List<Product> products, string filename)
        {
            var filepath = $"{filename}.pdf";

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Text("Inventory-o - Product List")
                        .SemiBold()
                        .FontSize(20)
                        .FontColor(Colors.Blue.Darken2);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(50);  // ID
                                columns.RelativeColumn(3);    // Name
                                columns.RelativeColumn(4);    // Description
                                columns.ConstantColumn(80);   // Quantity
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("ID").Bold();
                                header.Cell().Element(CellStyle).Text("Name").Bold();
                                header.Cell().Element(CellStyle).Text("Description").Bold();
                                header.Cell().Element(CellStyle).Text("Quantity").Bold();

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .Background(Colors.Blue.Lighten3)
                                        .Padding(5)
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten1);
                                }
                            });

                            foreach (var product in products)
                            {
                                table.Cell().Element(CellStyle).Text(product.Id.ToString());
                                table.Cell().Element(CellStyle).Text(product.Name);
                                table.Cell().Element(CellStyle).Text(product.Description);
                                table.Cell().Element(CellStyle).Text(product.Quantity.ToString());

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .Padding(5)
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten2);
                                }
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated on ");
                            x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).SemiBold();
                            x.Span(" | Page ");
                            x.CurrentPageNumber();
                            x.Span(" of ");
                            x.TotalPages();
                        });
                });
            });

            document.GeneratePdf(filepath);
        }

        public static void ExportToCsv(List<Product> products, string filename)
        {
            var filepath = $"{filename}.csv";
            var csv = new StringBuilder();

            csv.AppendLine("ID,Name,Description,Quantity");

            foreach (var product in products)
            {
                csv.AppendLine($"{product.Id},\"{EscapeCsv(product.Name)}\",\"{EscapeCsv(product.Description)}\",{product.Quantity}");
            }

            File.WriteAllText(filepath, csv.ToString());
        }

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.Replace("\"", "\"\"");
        }
    }
}