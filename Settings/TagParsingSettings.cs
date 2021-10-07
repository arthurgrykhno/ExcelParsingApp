namespace ExcelParsingApp.Settings
{
    public class TagParsingSettings
    {
        /// <summary>
        /// Номер страницы с которой нужно парсить
        /// </summary>
        public int SheetNumber { get; set; }

        /// <summary>
        /// Номер строки, с которой начинаем парсить
        /// </summary>
        public int FirstRowNumber { get; set; }

        /// <summary>
        /// Номер столбца, с которого начинаем парсить
        /// </summary>
        public int CellValueNumber { get; set; }

    }
}
