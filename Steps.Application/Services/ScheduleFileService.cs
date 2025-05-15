using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Steps.Domain.Entities.GroupBlocks;

namespace Steps.Application.Services;

public class ScheduleFileService
{
    private readonly IUnitOfWork _unitOfWork;

    public ScheduleFileService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task GeneratePreScheduleFile(Guid groupBlockId)
    {
        var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
        
        var groupBlock = await groupBlockRepository.GetFirstOrDefaultAsync(
            predicate: g => g.Id == groupBlockId,
            include: source => source
                .Include(x => x.PreSchedule)
                .ThenInclude(x => x.Athlete).ThenInclude(x => x.Team)
                .Include(x => x.Contest),
            trackingType: TrackingType.Tracking);

        IWorkbook workbook = new XSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("PreSchedule");

        var headers = new string[]
            {
                "№",
                "Команда",
                "Имя спортсмена",
                "Дата рождения",
                "Номинация",
                "Возрастная категория",
                "Ступень",
                "Время выхода"
            };

        IRow headerRow = sheet.CreateRow(0);
        for (int i = 0; i < headers.Length; i++)
        {
            ICell cell = headerRow.CreateCell(i);
            cell.SetCellValue(headers[i]);
        }

        int rowNum = 1;
        foreach (var preScheduleCell in groupBlock.PreSchedule)
        {
            var athlete = preScheduleCell.Athlete;
            if (athlete == null) continue;

            IRow row = sheet.CreateRow(rowNum++);

            row.CreateCell(0).SetCellValue(rowNum - 1);

            row.CreateCell(1).SetCellValue(athlete.Team.Name ?? string.Empty);

            row.CreateCell(2).SetCellValue(athlete.FullName ?? string.Empty);

            row.CreateCell(3).SetCellValue(athlete.BirthDate.ToString("dd.MM.yyyy") ?? string.Empty);

            row.CreateCell(4).SetCellValue(preScheduleCell.Athlete.AthleteType.ToString() ?? string.Empty);

            row.CreateCell(5).SetCellValue(preScheduleCell.Athlete.AgeCategory.ToString() ?? string.Empty);

            row.CreateCell(6).SetCellValue(preScheduleCell.Athlete.Degree.ToString() ?? string.Empty);

            row.CreateCell(7).SetCellValue(preScheduleCell.ExitTime.ToString("HH:mm") ?? string.Empty);
        }

        for (int i = 0; i < headers.Length; i++)
        {
            sheet.AutoSizeColumn(i);
        }

        // Сохраняем файл
        string fileName = $"PreSchedule_{groupBlock.Contest?.Name ?? "Unknown"}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        string filePath = Path.Combine(Path.GetTempPath(), fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            workbook.Write(fileStream);
        }

        // Можно вернуть путь к файлу или отправить его пользователю
        Console.WriteLine($"Файл сохранен: {filePath}");

    }
}