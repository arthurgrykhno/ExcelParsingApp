using ExcelParsingApp.Common;
using ExcelParsingApp.Database.Models;
using ExcelParsingApp.Repositories;
using ExcelParsingApp.Settings;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExcelParsingApp.Services
{
    public class ExcelParseService
    {
        private readonly TagRepository tagRepository = new TagRepository();
        private readonly TagParsingSettings settings;

        public ExcelParseService()
        {
            settings = new TagParsingSettings
            {
                SheetNumber = 0,
                CellValueNumber = 1,
                FirstRowNumber = 1
            };
        }

        // TODO: в будущем понадобится тянуть также mnemoId
        public IEnumerable<string> GetTagNamesFromFile()
        {
            using var stream = new FileStream(Constants.ALL_TAGS, FileMode.Open);
            var sheet = GetSheet(stream);

            var rowsCount = sheet.PhysicalNumberOfRows;

            for (int i = settings.FirstRowNumber; i < rowsCount; i++)
            {
                var row = sheet.GetRow(i);
                var cell = row.GetCell(settings.CellValueNumber);

                if (cell is null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                {
                    yield return cell.StringCellValue;
                }
            }
        }

        public IEnumerable<TagValues> GetTagValues()
        {
            using var minValuesStream = new FileStream(Constants.MIN_VALUE, FileMode.Open);
            using var maxValuesStream = new FileStream(Constants.MAX_VALUE, FileMode.Open);
            using var medValuesStream = new FileStream(Constants.MEDIAN_VALUE, FileMode.Open);

            var minSheet = GetSheet(minValuesStream);
            var maxSheet = GetSheet(maxValuesStream);
            var medSheet = GetSheet(medValuesStream);

            if (!IsTheSameRowsCount(minSheet, maxSheet, medSheet))
            {
                throw new Exception();
            }

            var rowCount = minSheet.PhysicalNumberOfRows;
            var columnCount = tagRepository.GetTagsCount();

            for (int i = 1; i <= columnCount; i++)
            {
                var tagNamesRow = minSheet.GetRow(0);
                var tagName = tagNamesRow.GetCell(i).StringCellValue;

                var tagId = tagRepository.FindByName(tagName);

                for (int j = 1; j < rowCount; j++)
                {
                    var minValue = GetValueFromCell(minSheet, j, i);
                    var maxValue = GetValueFromCell(maxSheet, j, i);
                    var medValue = GetValueFromCell(medSheet, j, i);

                    var tagValues = new TagValues
                    {
                        TagId = tagId,
                        Min = minValue,
                        Max = maxValue,
                        Median = medValue,
                        Date = DateTime.UtcNow,
                    };

                    yield return tagValues;
                }
            }
        }

        private bool IsTheSameRowsCount(params ISheet[] sheets)
        {
            var numbersOfRows = sheets.Select(sheet => sheet.PhysicalNumberOfRows).ToList();

            return numbersOfRows.All(a => a == numbersOfRows.First());
        }


        private double GetValueFromCell(ISheet sheet, int rowNumber, int cellNumber)
        {
            var row = sheet.GetRow(rowNumber);
            var cell = row.GetCell(cellNumber);

            if (cell is null || string.IsNullOrWhiteSpace(cell.ToString()))
                return 0;

            var value = cell.NumericCellValue;

            return value;
        }

        private ISheet GetSheet(FileStream stream)
        {
            var workBook = new XSSFWorkbook(stream);
            var sheet = workBook.GetSheetAt(settings.SheetNumber);

            return sheet;
        }
    }
}
