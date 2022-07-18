using FlyTonight.Application.Services;

namespace FlyTonight.Application.Interfaces
{
    public interface IEventReportGeneratorService
    {
        public void GenerateSpreadsheet(SpreadsheetData data, int week, Stream outStream);
    }
}
