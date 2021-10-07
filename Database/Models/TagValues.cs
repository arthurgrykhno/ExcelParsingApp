using System;

namespace ExcelParsingApp.Database.Models
{
    public class TagValues
    {
        public int Id { get; set; }
        public int? TagId { get; set; }
        public int Mode { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Median { get; set; }
        public DateTime Date { get; set; }
    }
}
