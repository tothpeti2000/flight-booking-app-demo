using FlyTonight.Application.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Globalization;

namespace FlyTonight.Application.Services
{
    public class EventReportGeneratorService : IEventReportGeneratorService
    {
        private readonly Font FONT = new Font("Tahoma", 10.0f);
        private readonly Font TITLE_FONT = new Font("Tahoma", 16.0f);
        private readonly Color TITLE_BACK_COLOR = Color.FromArgb(48, 84, 150);
        private readonly Color HEADER_BACK_COLOR = Color.FromArgb(142, 169, 219);
        private readonly Color DATA_BACK_COLOR = Color.FromArgb(217, 225, 242);

        public void GenerateSpreadsheet(SpreadsheetData data, int week, Stream outStream)
        {
            using var package = new ExcelPackage();

            GeneratePlanesPage(data.Planes, week, package.Workbook.Worksheets.Add("Gépek"));

            GenerateFlightsPage(data.Flights, week, package.Workbook.Worksheets.Add("Utazások"));

            GenerateIncidentsPage(data.Incident, week, package.Workbook.Worksheets.Add("Incidensek"));

            package.SaveAs(outStream);
        }

        private void GenerateRow(
            ExcelWorksheet ws,
            int row,
            object[] items,
            Color FontColor,
            Color BackColor,
            ExcelHorizontalAlignment horizontalAlignment = ExcelHorizontalAlignment.General,
            ExcelVerticalAlignment verticalAlignment = ExcelVerticalAlignment.Center)
        {
            for (int i = 0; i < items.Length; i++)
            {
                var cell = ws.Cells[row, i + 1];

                cell.Style.Font.SetFromFont(FONT);
                cell.Style.Font.Color.SetColor(FontColor);
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(BackColor);
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                cell.Style.HorizontalAlignment = horizontalAlignment;
                cell.Style.VerticalAlignment = verticalAlignment;

                cell.Value = items[i];
            }
        }

        private void GenerateTitle(ExcelWorksheet ws, int columns, string title)
        {
            var cells = ws.Cells[1, 1, 1, columns];

            cells.Merge = true;
            cells.Value = title;
            cells.Style.Font.SetFromFont(TITLE_FONT);
            cells.Style.Font.Color.SetColor(Color.White);
            cells.Style.Font.Bold = true;
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(TITLE_BACK_COLOR);
        }

        private void GeneratePlanesPage(List<SpreadsheetData.PlaneData> planes, int week, ExcelWorksheet ws)
        {
            const int columns = 8;

            // Title
            GenerateTitle(ws, columns, $"Jegyzett repülőgép adatok {week}. hét");

            // Headers
            GenerateRow(ws, 2, new object[columns]
            {
                "Gép azonosítója",
                "Gép típusa",
                "Utazási sebesség (mach)",
                "Magasság (m)",
                "Hosszúság (m)",
                "Szárny fesztávolság (m)",
                "Utazások száma (db)",
                "Incidensek száma (db)"
            },
            Color.White, HEADER_BACK_COLOR, ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center);

            // Data
            for (int i = 0; i < planes.Count; i++)
            {
                var plane = planes[i];

                GenerateRow(ws, 3 + i, new object[columns]
                {
                     plane.PlaneId,
                     plane.PlaneType,
                     plane.MachSpeed,
                     plane.Height,
                     plane.Length,
                     plane.Wingspan,
                     plane.FlightCount,
                     plane.IncidentCount
                },
                Color.Black, DATA_BACK_COLOR, ExcelHorizontalAlignment.Justify, ExcelVerticalAlignment.Top);
            }

            ws.Cells[3, 2, 3 + planes.Count, columns].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            ResizeCells(ws, columns);
        }

        private void GenerateFlightsPage(List<SpreadsheetData.FlightData> flights, int week, ExcelWorksheet ws)
        {
            const int columns = 13;

            // Title
            GenerateTitle(ws, columns, $"Incidens adatok {week}. hét");

            // Headers
            GenerateRow(ws, 2, new object[columns]
            {
                "Utazás azonosítója",
                "Gép azonosítója",
                "Honnan",
                "hova",
                "Szállított utasok",
                "Tervezett indulás",
                "Tényleges indulás",
                "Indulás ideje (perc)",
                "Érkezés ideje",
                "Távolság (km)",
                "Menetidő (perc)",
                "Volt-e incidens?",
                "Bevétel a jegyekből"
            },
            Color.White, HEADER_BACK_COLOR, ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center);

            // Data
            for (int i = 0; i < flights.Count; i++)
            {
                var flight = flights[i];

                GenerateRow(ws, 3 + i, new object[columns]
                {
                     flight.FlightId,
                     flight.PlaneId,
                     flight.From,
                     flight.To,
                     flight.PassengerCount,
                     flight.PlannedDeparture.ToUniversalTime().ToString("yyyy.MM.dd HH:mm"),
                     flight.ActualDeparture.ToUniversalTime().ToString("yyyy.MM.dd HH:mm"),
                     flight.DelayMinutes,
                     flight.ArrivalTime.ToUniversalTime().ToString("yyyy.MM.dd HH:mm"),
                     flight.FlightDistanceKm,
                     flight.FlightTimeMinutes,
                     flight.Incident ? "igen" : "nem",
                     flight.TotalIncome.ToString("C2", CultureInfo.CreateSpecificCulture("hu-HU"))
                },
                Color.Black, DATA_BACK_COLOR, ExcelHorizontalAlignment.Justify, ExcelVerticalAlignment.Top);
            }

            // Sum
            for (int i = 0; i < flights.Count; i++)
            {
                var envEvent = flights[i];

                GenerateRow(ws, 3 + flights.Count, new object[columns]
                {
                     "Összesen:",
                     "-",
                     "-",
                     "-",
                     flights.Sum(e => e.PassengerCount),
                     "-",
                     "-",
                     flights.Sum(e => e.DelayMinutes),
                     "-",
                     flights.Sum(e => e.FlightDistanceKm),
                     flights.Sum(e => e.FlightTimeMinutes),
                     "-",
                     flights.Sum(e => e.TotalIncome).ToString("C2", CultureInfo.CreateSpecificCulture("hu-HU"))
                },
                Color.White, HEADER_BACK_COLOR, ExcelHorizontalAlignment.Justify, ExcelVerticalAlignment.Top);
            }

            // Average
            for (int i = 0; i < flights.Count; i++)
            {
                GenerateRow(ws, 4 + flights.Count, new object[columns]
                {
                     "Átlagosan:",
                     "-",
                     "-",
                     "-",
                     (int)flights.Average(e => e.PassengerCount),
                     "-",
                     "-",
                     flights.Average(e => e.DelayMinutes).ToString("0.###"),
                     "-",
                     flights.Average(e => e.FlightDistanceKm).ToString("0.###"),
                     flights.Average(e => e.FlightTimeMinutes).ToString("0.###"),
                     "-",
                     flights.Average(e => e.TotalIncome).ToString("C2", CultureInfo.CreateSpecificCulture("hu-HU"))
                },
                Color.White, HEADER_BACK_COLOR, ExcelHorizontalAlignment.Justify, ExcelVerticalAlignment.Top);
            }

            ws.Cells[3, 2, 4 + flights.Count, columns].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            ResizeCells(ws, columns);
        }

        private void GenerateIncidentsPage(List<SpreadsheetData.IncidentData> incidents, int week, ExcelWorksheet ws)
        {
            const int columns = 5;

            // Title
            GenerateTitle(ws, columns, $"Incidens adatok {week}. hét");

            // Headers
            GenerateRow(ws, 2, new object[columns]
            {
                "Incidens megnevezése",
                "Gép azonosítója",
                "Szállított utasok",
                "Érintett utazás azonosítója",
                "Késés (perc)"
            },
            Color.White, HEADER_BACK_COLOR, ExcelHorizontalAlignment.Center, ExcelVerticalAlignment.Center);

            // Data
            for (int i = 0; i < incidents.Count; i++)
            {
                var incident = incidents[i];

                GenerateRow(ws, 3 + i, new object[columns]
                {
                     incident.Name,
                     incident.PlaneId,
                     incident.PassengerCount,
                     incident.FlightId,
                     incident.DelayMinutes
                },
                Color.Black, DATA_BACK_COLOR, ExcelHorizontalAlignment.Justify, ExcelVerticalAlignment.Top);
            }

            // Sum
            for (int i = 0; i < incidents.Count; i++)
            {
                GenerateRow(ws, 3 + incidents.Count, new object[columns]
                {
                     "Összesen:",
                     "-",
                     incidents.Sum(e => e.PassengerCount),
                     "-",
                     incidents.Sum(e => e.DelayMinutes)
                },
                Color.White, HEADER_BACK_COLOR, ExcelHorizontalAlignment.Justify, ExcelVerticalAlignment.Top);
            }

            // Average
            for (int i = 0; i < incidents.Count; i++)
            {
                GenerateRow(ws, 4 + incidents.Count, new object[columns]
                {
                     "Átlagosan:",
                     "-",
                     (int)incidents.Average(e => e.PassengerCount),
                     "-",
                     incidents.Average(e => e.DelayMinutes).ToString("0.###")
                },
                Color.White, HEADER_BACK_COLOR, ExcelHorizontalAlignment.Justify, ExcelVerticalAlignment.Top);
            }

            ws.Cells[3, 2, 4 + incidents.Count, columns].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            ResizeCells(ws, columns);
        }

        private void ResizeCells(ExcelWorksheet ws, int columns)
        {
            for (int i = 1; i <= columns; i++)
            {
                ws.Column(i).AutoFit(15.0);
            }

            ws.Row(1).CustomHeight = true;
            ws.Row(1).Height = 30.0;
        }
    }
}
