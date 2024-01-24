using ClosedXML.Excel;
using POS.Utilites.Static;

namespace POS.Infraestructure.FileExcel
{
    public class GenerateExcel : IGenerateExcel
    {
        public MemoryStream GenerateToExcel<T>(IEnumerable<T> data, List<TableColumns> columns)
        {
            var workbook = new XLWorkbook();
            var workSheet = workbook.Worksheets.Add("Listado");
            
            for(int i=0;i<columns.Count; i++)
            {
                workSheet.Cell(1, i + 1).Value = columns[i].Label;
            }
            var rowIndex = 2;

            foreach (var item in data)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    var propertyValue = typeof(T).GetProperty(columns[i].PropertyName!)?.GetValue(item)?.ToString();
                    workSheet.Cell(rowIndex, i +1).Value = propertyValue;

                }

                rowIndex++;
            }

            var stream = new MemoryStream();
            workbook.SaveAs(stream);

            stream.Position = 0;

            return stream;
        
        }
    }
}
