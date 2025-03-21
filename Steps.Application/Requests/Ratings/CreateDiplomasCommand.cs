using Calabonga.UnitOfWork;
using MediatR;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Steps.Domain.Entities;
using Steps.Shared;
using Steps.Shared.Contracts.Ratings;
using Steps.Shared.Exceptions;
using Steps.Shared.Utils;

namespace Steps.Application.Requests.Ratings;

public record CreateDiplomasCommand(List<Rating> Ratings) : IRequest<Result<DiplomasViewModel>>;


public class CreateDiplomasCommandHandler : IRequestHandler<CreateDiplomasCommand, Result<DiplomasViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiplomasCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DiplomasViewModel>> Handle(CreateDiplomasCommand request, CancellationToken cancellationToken)
    {
        var ratingsIds = request.Ratings.Select(r => r.Id).ToList();
        if (ratingsIds.Count == 0)
        {
            throw new StepsBusinessException("Пустой список");
        }
        
        var ratings = (await _unitOfWork.GetRepository<Rating>().GetAllAsync(
            predicate: r => ratingsIds.Contains(r.Id),
          
            trackingType: TrackingType.Tracking)).ToList();

        foreach (var rating in ratings)
        {
            rating.IsComplete = true;
        }

        await _unitOfWork.SaveChangesAsync();

        var prefix = "ratings.First()?.CertificateDegree.GetDisplayName();";
        var view = new DiplomasViewModel()
        {
            FileName = $"{prefix}_{DateTime.UtcNow.AddHours(3):T}.pdf",
            FileBytes = CreatePdf(ratings)
        };
        
        return Result<DiplomasViewModel>.Ok(view);
    }
    
    public static byte[] CreatePdf(List<Rating> scores)
    {
        using var document = new PdfDocument();
    
        var font = new XFont("Times New Roman", 20);
        var textPositions = new XPoint[]
        {
            new XPoint(501f, 766f),
            new XPoint(501f, 966f),
            new XPoint(501f, 1166f)
        };
        
        foreach (var data in scores)
        {
            var page = document.AddPage();
            page.Width = XUnit.FromMillimeter(1237.0);
            page.Height = XUnit.FromMillimeter(1750.0); 
            var gfx = XGraphics.FromPdfPage(page);

            gfx.DrawString("data.Athlete", font, XBrushes.Black, textPositions[0]);
            gfx.DrawString("data.b", font, XBrushes.Black, textPositions[1]);
            gfx.DrawString($"{data.TotalScore}", font, XBrushes.Black, textPositions[2]);
        }

        using var stream = new MemoryStream();
        document.Save(stream);
        return stream.ToArray();
    }

}
