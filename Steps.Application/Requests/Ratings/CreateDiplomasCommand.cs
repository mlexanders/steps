using Calabonga.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        var repository = _unitOfWork.GetRepository<Rating>();
        
        var ratings = (await repository.GetAllAsync(
            predicate: r => ratingsIds.Contains(r.Id),
            include: x => x.Include(r => r.Athlete),
            trackingType: TrackingType.Tracking)).ToList();


        var view = new DiplomasViewModel();
        if (ratings.Count > 0)
        {
            foreach (var rating in ratings)
            {
                rating.IsComplete = true;
            }
        
            repository.Update(ratings);
            await _unitOfWork.SaveChangesAsync();
            view.FileName = ratings.FirstOrDefault()?.CertificateDegree.GetDisplayName() ?? string.Empty;
        }

        view.FileName = $"{view.FileName}_{DateTime.UtcNow.AddHours(3):T}.pdf";
        view.FileBytes = CreatePdf(ratings);
        
        return Result<DiplomasViewModel>.Ok(view);
    }
    
    private static byte[] CreatePdf(List<Rating> scores)
    {
        using var document = new PdfDocument();
    
        var font = new XFont("Times New Roman", 20);
        // var textPositions = new XPoint[]
        // {
        //     new XPoint(501f, 766f),
        //     new XPoint(501f, 966f),
        //     new XPoint(501f, 1166f)
        // };
        // var width = XUnit.FromPoint(1237.0);
        // var height =  XUnit.FromPoint(1750.0);
        
        foreach (var data in scores)
        {
            var page = document.AddPage();
            // page.Width = width;
            // page.Height = height;
            var gfx = XGraphics.FromPdfPage(page);

            var fio = data.Athlete.FullName.Split(' ');
            var surName = fio.Length > 1 ? fio[1] : " ";
            var name = fio.Length >= 1 ? fio[0] : " ";
            
            gfx.DrawString($"{name}", font, XBrushes.Black, new XRect(300, -460, page.Width, page.Height), XStringFormats.BottomLeft);

            gfx.DrawString($"{surName}", font, XBrushes.Black, new XRect(300, -365, page.Width, page.Height), XStringFormats.BottomLeft);

            gfx.DrawString($"{data.TotalScore}", font, XBrushes.Black, new XRect(300, -270, page.Width, page.Height), XStringFormats.BottomLeft);

            
            // gfx.DrawString(surName, font, XBrushes.Black, textPositions[0]);
            // gfx.DrawString(name, font, XBrushes.Black, textPositions[1]);
            // gfx.DrawString($"{data.TotalScore}", font, XBrushes.Black, textPositions[2]);
        }

        using var stream = new MemoryStream();
        document.Save(stream);
        return stream.ToArray();
    }

}
