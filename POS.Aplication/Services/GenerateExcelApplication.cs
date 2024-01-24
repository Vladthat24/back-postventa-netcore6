using POS.Aplication.Interfaces;
using POS.Infraestructure.FileExcel;
using POS.Utilites.Static;

namespace POS.Aplication.Services
{
    public class GenerateExcelApplication : IGenerateExcelApplication
    {
        private readonly IGenerateExcel _generateExcel;

        public GenerateExcelApplication(IGenerateExcel generateExcel)
        {
            _generateExcel = generateExcel;
        }

        public byte[] GenerateToExcel<T>(IEnumerable<T> data, List<(string ColumnName, string PropertyName)> columns)
        {
            var excelColumns = ExcelColumnsName.GetColumns(columns);
            var memoryStreamExcel= _generateExcel.GenerateToExcel(data, excelColumns);
            var fileBytes = memoryStreamExcel.ToArray();
            return fileBytes;

        }
    }
}
