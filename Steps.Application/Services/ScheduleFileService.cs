using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Steps.Application.Requests.ScheduleFile.Commands;
using Steps.Domain.Entities.GroupBlocks;
using Steps.Shared.Contracts.ScheduleFile;
using Steps.Shared;
using Steps.Domain.Entities;
using Steps.Shared.Contracts.ScheduleFile.ViewModel;
using AutoMapper;

namespace Steps.Application.Services;

public class ScheduleFileService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ScheduleFileService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ScheduleFileViewModel> GeneratePreScheduleFile(List<Guid> groupBlockIds)
    {
        IWorkbook workbook = new XSSFWorkbook();

        int sheetNumber = 0;

        foreach (var groupBlockId in groupBlockIds)
        {
            var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();

            var groupBlock = await groupBlockRepository.GetFirstOrDefaultAsync(
                predicate: g => g.Id == groupBlockId,
                include: source => source
                    .Include(x => x.PreSchedule)
                    .ThenInclude(x => x.Athlete).ThenInclude(x => x.Team)
                    .Include(x => x.Contest),
                trackingType: TrackingType.Tracking);

            if (groupBlock == null) continue;

            string sheetName = $"Блок {sheetNumber++}";
            ISheet sheet = workbook.CreateSheet(sheetName);

            var headers = new string[]
            {
                "№", "Команда", "Имя спортсмена", "Дата рождения",
                "Номинация", "Возрастная категория", "Ступень", "Время выхода"
            };

            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }

            int rowNum = 1;
            foreach (var preScheduleCell in groupBlock.PreSchedule)
            {
                if (preScheduleCell.IsConfirmed) continue;
                
                var athlete = preScheduleCell.Athlete;
                if (athlete == null) continue;

                IRow row = sheet.CreateRow(rowNum++);

                row.CreateCell(0).SetCellValue(rowNum - 1);
                row.CreateCell(1).SetCellValue(athlete.Team?.Name ?? string.Empty);
                row.CreateCell(2).SetCellValue(athlete.FullName ?? string.Empty);
                row.CreateCell(3).SetCellValue(athlete.BirthDate.ToString("dd.MM.yyyy"));
                row.CreateCell(4).SetCellValue(preScheduleCell.Athlete.AthleteType.ToString() ?? string.Empty);
                row.CreateCell(5).SetCellValue(preScheduleCell.Athlete.AgeCategory.ToString() ?? string.Empty);
                row.CreateCell(6).SetCellValue(preScheduleCell.Athlete.Degree.ToString() ?? string.Empty);
                row.CreateCell(7).SetCellValue(preScheduleCell.ExitTime.ToString("HH:mm"));
            }

            for (int i = 0; i < headers.Length; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }

        string fileName = $"PreSchedule_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

        byte[] fileData;
        using (var memoryStream = new MemoryStream())
        {
            workbook.Write(memoryStream);
            fileData = memoryStream.ToArray();
        }

        var scheduleFile = new ScheduleFile
        {
            FileName = fileName,
            Data = fileData
        };

        var scheduleFileRepository = _unitOfWork.GetRepository<ScheduleFile>();
        await scheduleFileRepository.InsertAsync(scheduleFile);

        var contest = groupBlockIds.Select(gbId =>
            _unitOfWork.GetRepository<GroupBlock>()
                .GetFirstOrDefaultAsync(predicate: g => g.Id == gbId,
                    include: source => source.Include(x => x.Contest),
                    trackingType: TrackingType.Tracking)
                .Result?.Contest)
            .FirstOrDefault(c => c != null);

        if (contest != null)
        {
            contest.ScheduleFileId = scheduleFile.Id;
            contest.ScheduleFile = scheduleFile;
            var contestRepository = _unitOfWork.GetRepository<Contest>();
            contestRepository.Update(contest);
        }

        await _unitOfWork.SaveChangesAsync();

        var scheduleFileViewModel = _mapper.Map<ScheduleFileViewModel>(scheduleFile);

        return scheduleFileViewModel;
    }
    
    public async Task<ScheduleFileViewModel> GenerateFinalScheduleFile(List<Guid> groupBlockIds)
    {
        IWorkbook workbook = new XSSFWorkbook();

        int sheetNumber = 0;

        foreach (var groupBlockId in groupBlockIds)
        {
            var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();

            var groupBlock = await groupBlockRepository.GetFirstOrDefaultAsync(
                predicate: g => g.Id == groupBlockId,
                include: source => source
                    .Include(x => x.PreSchedule)
                    .ThenInclude(x => x.Athlete).ThenInclude(x => x.Team)
                    .Include(x => x.Contest),
                trackingType: TrackingType.Tracking);

            if (groupBlock == null) continue;

            string sheetName = $"Блок {sheetNumber++}";
            ISheet sheet = workbook.CreateSheet(sheetName);

            var headers = new string[]
            {
                "№", "Команда", "Имя спортсмена", "Дата рождения",
                "Номинация", "Возрастная категория", "Ступень", "Время выхода"
            };

            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }

            int rowNum = 1;
            foreach (var finalScheduledCell in groupBlock.FinalSchedule)
            {
                var athlete = finalScheduledCell.Athlete;
                if (athlete == null) continue;

                IRow row = sheet.CreateRow(rowNum++);

                row.CreateCell(0).SetCellValue(rowNum - 1);
                row.CreateCell(1).SetCellValue(athlete.Team?.Name ?? string.Empty);
                row.CreateCell(2).SetCellValue(athlete.FullName ?? string.Empty);
                row.CreateCell(3).SetCellValue(athlete.BirthDate.ToString("dd.MM.yyyy"));
                row.CreateCell(4).SetCellValue(finalScheduledCell.Athlete.AthleteType.ToString() ?? string.Empty);
                row.CreateCell(5).SetCellValue(finalScheduledCell.Athlete.AgeCategory.ToString() ?? string.Empty);
                row.CreateCell(6).SetCellValue(finalScheduledCell.Athlete.Degree.ToString() ?? string.Empty);
                row.CreateCell(7).SetCellValue(finalScheduledCell.ExitTime.ToString("HH:mm"));
            }

            for (int i = 0; i < headers.Length; i++)
            {
                sheet.AutoSizeColumn(i);
            }
        }

        string fileName = $"PreSchedule_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

        byte[] fileData;
        using (var memoryStream = new MemoryStream())
        {
            workbook.Write(memoryStream);
            fileData = memoryStream.ToArray();
        }

        var scheduleFile = new ScheduleFile
        {
            FileName = fileName,
            Data = fileData
        };

        var scheduleFileRepository = _unitOfWork.GetRepository<ScheduleFile>();
        await scheduleFileRepository.InsertAsync(scheduleFile);

        var contest = groupBlockIds.Select(gbId =>
            _unitOfWork.GetRepository<GroupBlock>()
                .GetFirstOrDefaultAsync(predicate: g => g.Id == gbId, include: source => source.Include(x => x.Contest), trackingType: TrackingType.Tracking)
                .Result?.Contest)
            .FirstOrDefault(c => c != null);

        if (contest != null)
        {
            contest.ScheduleFileId = scheduleFile.Id;
            contest.ScheduleFile = scheduleFile;
            var contestRepository = _unitOfWork.GetRepository<Contest>();
            contestRepository.Update(contest);
        }

        await _unitOfWork.SaveChangesAsync();

        var scheduleFileViewModel = _mapper.Map<ScheduleFileViewModel>(scheduleFile);

        return scheduleFileViewModel;
    }
}