// Copyright (c) Ethereal. All rights reserved.
//

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.IO;

namespace Ethereal.Excel.Infrastructure
{
    /// <summary>
    /// ExcelRender
    /// </summary>
    public class ExcelRender : IExcelRender
    {
        /// <summary>
        /// RenderToExcel
        /// </summary>
        public Stream? RenderToExcel<T>(IEnumerable<T> source)
        {
            var fileInfo = new FileInfo("test1.xlsx");
            var spreadsheetDocument = SpreadsheetDocument.Create("test1.xlsx", SpreadsheetDocumentType.Workbook);

            var workbookpart = spreadsheetDocument.AddWorkbookPart();
            var workbook = new Workbook();
            var sheets = new Sheets();

            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            var worksheet = new Worksheet();
            var sheetData = new SheetData();

            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = UInt32Value.FromUInt32(1u),
                Name = "SS"
            };
            sheets.Append(sheet);

            var row = new Row() { RowIndex = UInt32Value.FromUInt32(1u) };
            sheetData.Append(row);
            var lists = new List<Cell>();
            for (var i = 0; i < 100; i++)
            {
                lists.Add(new Cell
                {
                    CellValue = new CellValue("12321323"),
                    DataType = new EnumValue<CellValues>(CellValues.String)
                });
            }
            foreach (var item in lists)
            {
                row.Append(item);
            }

            worksheet.Append(sheetData);
            worksheetPart.Worksheet = worksheet;
            worksheetPart.Worksheet.Save();

            workbook.Append(sheets);
            workbookpart.Workbook = workbook;

            workbookpart.Workbook.Save();
            spreadsheetDocument.Close();
            return null;
        }

        /// <summary>
        /// RenderFromExcel
        /// </summary>
        public IEnumerable<T>? RenderFromExcel<T>(Stream stream) => null;

        /// <summary>
        /// RenderFromExcel
        /// </summary>
        public IEnumerable<T>? RenderFromExcel<T>(FileInfo fileInfo) => null;
    }
}
