using ExcelParsingApp.Services;
using System;

namespace ExcelParsingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelParseService service = new ExcelParseService();

            service.GetTagValues();

        }
    }
}
