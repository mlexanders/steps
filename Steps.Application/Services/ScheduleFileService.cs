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
using NPOI.SS.Util;

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
        using IWorkbook workbook = new XSSFWorkbook();
        var groupBlockRepository = _unitOfWork.GetRepository<GroupBlock>();
        int sheetNumber = 1;

        ICellStyle headerStyle = workbook.CreateCellStyle();
        IFont headerFont = workbook.CreateFont();
        headerFont.IsBold = true;
        headerStyle.SetFont(headerFont);

        ICellStyle titleStyle = workbook.CreateCellStyle();
        IFont titleFont = workbook.CreateFont();
        titleFont.IsBold = true;
        titleFont.FontHeightInPoints = 14;
        titleStyle.SetFont(titleFont);
        titleStyle.Alignment = HorizontalAlignment.Center;

        foreach (var groupBlockId in groupBlockIds)
        {
            var groupBlock = await groupBlockRepository.GetFirstOrDefaultAsync(
                predicate: g => g.Id == groupBlockId,
                include: source => source
                    .Include(x => x.PreSchedule)
                    .ThenInclude(x => x.Athlete).ThenInclude(x => x.Team)
                    .Include(x => x.Contest),
                trackingType: TrackingType.Tracking);

            if (groupBlock == null) continue;
            if (groupBlock.PreSchedule.Any(x => x.IsConfirmed))
            {
                sheetNumber++;
                continue;
            }

            string sheetName = $"Блок {sheetNumber++}";
            ISheet sheet = workbook.CreateSheet(sheetName);
            
            sheet.PrintSetup.Landscape = true;
            sheet.PrintSetup.PaperSize = (short)PaperSize.A4;
            sheet.FitToPage = true;
            sheet.PrintSetup.FitWidth = 1;
            sheet.PrintSetup.FitHeight = 0;

            IRow titleRow = sheet.CreateRow(0);
            titleRow.HeightInPoints = 30;
            ICell titleCell = titleRow.CreateCell(0);
            titleCell.SetCellValue("СТУПЕНИ МАСТЕРСТВА (предварительное)");
            titleCell.CellStyle = titleStyle;
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 7));

            IRow subtitleRow = sheet.CreateRow(1);
            ICell subtitleCell = subtitleRow.CreateCell(0);
            subtitleCell.SetCellValue($"Запуск в СК участников {sheetName}");
            sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, 7));

            IRow registrationRow = sheet.CreateRow(2);
            ICell registrationCell = registrationRow.CreateCell(0);
            registrationCell.SetCellValue($"Регистрация с {groupBlock.PreSchedule
                .OrderBy(x => x.ExitTime)
                .FirstOrDefault()?.ExitTime.ToString("HH:mm")}");
            sheet.AddMergedRegion(new CellRangeAddress(2, 2, 0, 7));

            var headers = new string[]
            {
                "№п/п", "Название Команды, клуба", "ФИ спортсмена", 
                "Дата рождения", "Номинация (ЧИР/ЧФ)", 
                "Возрастная категория", "Ступень тестирования", 
                "Время выхода на площадку (фристайл)"
            };

            IRow headerRow = sheet.CreateRow(3);
            for (int i = 0; i < headers.Length; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(headers[i]);
                cell.CellStyle = headerStyle;
            }

            int rowNum = 4;
            foreach (var preScheduleCell in groupBlock.PreSchedule.OrderBy(x => x.ExitTime))
            {
                if (preScheduleCell.IsConfirmed) continue;
                
                var athlete = preScheduleCell.Athlete;
                if (athlete == null) continue;

                IRow row = sheet.CreateRow(rowNum++);

                string ageCategory = MapAgeCategory(athlete.AgeCategory.ToString());
                string degree = MapDegree(athlete.Degree.ToString());
                string athleteType = athlete.AthleteType.ToString() == "Cheer" ? "ЧИР" : "ЧФ";

                row.CreateCell(0).SetCellValue(rowNum - 4);
                row.CreateCell(1).SetCellValue(athlete.Team?.Name ?? string.Empty);
                row.CreateCell(2).SetCellValue(athlete.FullName ?? string.Empty);
                row.CreateCell(3).SetCellValue(athlete.BirthDate.ToString("dd.MM.yyyy"));
                row.CreateCell(4).SetCellValue(athleteType);
                row.CreateCell(5).SetCellValue(ageCategory);
                row.CreateCell(6).SetCellValue(degree);
                row.CreateCell(7).SetCellValue(preScheduleCell.ExitTime.ToString("HH:mm"));
            }

            for (int i = 0; i < headers.Length; i++)
            {
                sheet.AutoSizeColumn(i);
                sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
            }
        }

        string fileName = $"Расписание_предварительное_СМ_{DateTime.Now:ddMMyyyy}.xlsx";

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
            contest.PreScheduleFileId = scheduleFile.Id;
            contest.PreScheduleFile = scheduleFile;
            var contestRepository = _unitOfWork.GetRepository<Contest>();
            contestRepository.Update(contest);
        }

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ScheduleFileViewModel>(scheduleFile);
    }
    
    public async Task<ScheduleFileViewModel> GenerateFinalScheduleFile(List<Guid> groupBlockIds)
    {
        using IWorkbook workbook = new XSSFWorkbook();
        
        ICellStyle headerStyle = workbook.CreateCellStyle();
        IFont headerFont = workbook.CreateFont();
        headerFont.IsBold = true;
        headerStyle.SetFont(headerFont);

        ICellStyle titleStyle = workbook.CreateCellStyle();
        IFont titleFont = workbook.CreateFont();
        titleFont.IsBold = true;
        titleFont.FontHeightInPoints = 14;
        titleStyle.SetFont(titleFont);
        titleStyle.Alignment = HorizontalAlignment.Center;

        ICellStyle subtitleStyle = workbook.CreateCellStyle();
        IFont subtitleFont = workbook.CreateFont();
        subtitleFont.FontHeightInPoints = 12;
        subtitleStyle.SetFont(subtitleFont);

        int sheetNumber = 1;

        foreach (var groupBlockId in groupBlockIds)
        {
            var groupBlock = await _unitOfWork.GetRepository<GroupBlock>().GetFirstOrDefaultAsync(
                predicate: g => g.Id == groupBlockId,
                include: source => source
                    .Include(x => x.FinalSchedule)
                    .ThenInclude(x => x.Athlete).ThenInclude(x => x.Team)
                    .Include(x => x.Contest),
                trackingType: TrackingType.Tracking);

            if (groupBlock == null) continue;

            string sheetName = $"Блок {sheetNumber++}";
            ISheet sheet = workbook.CreateSheet(sheetName);
            
            sheet.PrintSetup.Landscape = true;
            sheet.PrintSetup.PaperSize = (short)PaperSize.A4;
            sheet.FitToPage = true;
            sheet.PrintSetup.FitWidth = 1;
            sheet.PrintSetup.FitHeight = 0;

            IRow titleRow = sheet.CreateRow(0);
            titleRow.HeightInPoints = 30;
            ICell titleCell = titleRow.CreateCell(0);
            titleCell.SetCellValue("СТУПЕНИ МАСТЕРСТВА");
            titleCell.CellStyle = titleStyle;
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 7));

            IRow timeRow = sheet.CreateRow(1);
            ICell timeCell = timeRow.CreateCell(0);
            timeCell.SetCellValue($"Запуск в СК участников {sheetName}");
            timeCell.CellStyle = subtitleStyle;
            sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, 7));

            IRow regRow = sheet.CreateRow(2);
            ICell regCell = regRow.CreateCell(0);
            regCell.SetCellValue($"Регистрация с {groupBlock.FinalSchedule
                .OrderBy(x => x.ExitTime)
                .FirstOrDefault()?.ExitTime.ToString("HH:mm")}");
            regCell.CellStyle = subtitleStyle;
            sheet.AddMergedRegion(new CellRangeAddress(2, 2, 0, 7));

            var headers = new string[]
            {
                "№п/п", "Название Команды, клуба", "ФИ спортсмена", 
                "Дата рождения", "Номинация (ЧИР/ЧФ)", 
                "Возрастная категория", "Ступень тестирования", 
                "Время выхода на площадку (фристайл)"
            };

            IRow headerRow = sheet.CreateRow(3);
            for (int i = 0; i < headers.Length; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(headers[i]);
                cell.CellStyle = headerStyle;
            }

            int rowNum = 4;
            foreach (var finalScheduledCell in groupBlock.FinalSchedule.OrderBy(x => x.ExitTime))
            {
                var athlete = finalScheduledCell.Athlete;
                if (athlete == null) continue;

                IRow row = sheet.CreateRow(rowNum++);

                string ageCategory = MapAgeCategory(athlete.AgeCategory.ToString());
                string degree = MapDegree(athlete.Degree.ToString());
                string athleteType = athlete.AthleteType.ToString() == "Cheer" ? "ЧИР" : "ЧФ";

                row.CreateCell(0).SetCellValue(rowNum - 4);
                row.CreateCell(1).SetCellValue(athlete.Team?.Name ?? string.Empty);
                row.CreateCell(2).SetCellValue(athlete.FullName ?? string.Empty);
                row.CreateCell(3).SetCellValue(athlete.BirthDate.ToString("dd.MM.yyyy"));
                row.CreateCell(4).SetCellValue(athleteType);
                row.CreateCell(5).SetCellValue(ageCategory);
                row.CreateCell(6).SetCellValue(degree);
                row.CreateCell(7).SetCellValue(finalScheduledCell.ExitTime.ToString("HH:mm"));
            }

            for (int i = 0; i < headers.Length; i++)
            {
                sheet.AutoSizeColumn(i);
                sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024);
            }
        }

        string fileName = $"Расписание_финальное_СМ_{DateTime.Now:ddMMyyyy}.xlsx";

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

        await _unitOfWork.GetRepository<ScheduleFile>().InsertAsync(scheduleFile);

        var contest = groupBlockIds.Select(gbId =>
            _unitOfWork.GetRepository<GroupBlock>()
                .GetFirstOrDefaultAsync(predicate: g => g.Id == gbId, 
                    include: source => source.Include(x => x.Contest), 
                    trackingType: TrackingType.Tracking)
                .Result?.Contest)
            .FirstOrDefault(c => c != null);

        if (contest != null)
        {
            contest.FinalScheduleFileId = scheduleFile.Id;
            contest.FinalScheduleFile = scheduleFile;
            _unitOfWork.GetRepository<Contest>().Update(contest);
        }

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ScheduleFileViewModel>(scheduleFile);
    }
    
    private string MapAgeCategory(string category)
    {
        return category switch
        {
            "Baby" => "Б",
            "YoungerChildren" => "МД",
            "Youth" => "ЮД",
            "Juniors" => "ЮЮ",
            "BoysGirls" => "М",
            _ => category
        };
    }

    private string MapDegree(string degree)
    {
        return degree switch
        {
            "First" => "I",
            "Second" => "II",
            "Third" => "III",
            "Fourth" => "IV",
            "Higher" => "В",
            _ => degree
        };
    }
}