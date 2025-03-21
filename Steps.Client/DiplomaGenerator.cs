
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Steps.Client;

public class DiplomaGenerator
{
    public byte[] CreatePdf(List<(string a, string b, string c)> imageBytes)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        return QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Content().Column(column =>
                {
                    foreach (var texts in imageBytes)
                    {
                        column.Item().Text(texts.a);
                        column.Item().Text(texts.b);
                        column.Item().Text(texts.c);
                        // column.Item().Spacing(20); // Отступ между строками
                    }
                });
            });
        }).GeneratePdf();
        // var builder = new PdfDocumentBuilder();
        // var font = builder.AddStandard14Font(Standard14Font.TimesRoman);
        // var points = new List<PdfPoint>()
        // {
        //     new(501f, 766f),
        //     new(501f, 966f),
        //     new(501f, 1166f),
        // };
        // const double width = 1237.0;
        // const double height = 1750.0;
        //
        // foreach (var texts in imageBytes)
        // {
        //     var page = builder.AddPage(width, height);
        //     page.AddText("texts.a", 70f,points[0] , font);
        //     page.AddText("texts.a", 70f, points[1], font);
        //     page.AddText("texts.a", 70f, points[2], font);
        // }
        // var documentBytes = builder.Build();
        //  return documentBytes;
    }

    // public byte[] CreateImage(string text1, string text2, string text3, int width = 1237, int height = 1750)
    // {
    //     using var skSurface = SKSurface.Create(new SKImageInfo(width, height));
    //     var canvas = skSurface.Canvas;
    //     canvas.Clear(SKColors.Empty);
    //     
    //     using var font = new SKFont() { Size = 70f };
    //     
    //     var paint = new SKPaint()
    //     {
    //         IsAntialias = true
    //     };
    //     
    //     canvas.DrawText(text1, 501f, 766f, font, paint);
    //     canvas.DrawText(text2, 501f, 966f, font, paint);
    //     canvas.DrawText(text3, 501f, 1166f, font, paint);
    //     
    //     using var skImage = skSurface.Snapshot();
    //     using var skData = skImage.Encode(SKEncodedImageFormat.Png, 100);
    //     return skData.ToArray();
    // }
}