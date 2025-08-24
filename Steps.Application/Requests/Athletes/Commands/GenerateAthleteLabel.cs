using MediatR;
using NPOI.HSSF.UserModel;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Steps.Shared;

namespace Steps.Application.Requests.Athletes.Commands;

public record GenerateAthleteLabel(string AthleteName, string TeamName) : IRequest<Result<byte[]>>;

public class GenerateAthleteLabelHandler : IRequestHandler<GenerateAthleteLabel, Result<byte[]>>
{
    public GenerateAthleteLabelHandler()
    {
    }

    public async Task<Result<byte[]>> Handle(GenerateAthleteLabel request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.AthleteName))
                return Result<byte[]>.Fail("INVALID_NAME", "Имя спортсмена не может быть пустым");

            using var document = new PdfDocument();
            var page = document.AddPage();

            page.Width = XUnit.FromMillimeter(50);
            page.Height = XUnit.FromMillimeter(30);

            using var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Arial", 14);

            var lines = request.AthleteName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            double startY = 5;
            double lineHeight = 14; 

            foreach (var line in lines)
            {
                gfx.DrawString(line, font, XBrushes.Black,
                    new XRect(0, startY, page.Width, lineHeight),
                    XStringFormats.Center);
                startY += lineHeight;
            }
            
            if (!string.IsNullOrWhiteSpace(request.TeamName))
            {
                double bottomY = page.Height.Point - lineHeight - 5;
                gfx.DrawString(request.TeamName, font, XBrushes.Black,
                    new XRect(0, bottomY, page.Width, lineHeight),
                    XStringFormats.Center);
            }

            using var ms = new MemoryStream();
            document.Save(ms, false);
            return Result<byte[]>.Ok(ms.ToArray()).SetMessage($"PDF для {request.AthleteName} создан");
        }
        catch (Exception ex)
        {
            return Result<byte[]>.Fail("ERROR", $"Ошибка при генерации PDF: {ex.Message}");
        }
    }
}