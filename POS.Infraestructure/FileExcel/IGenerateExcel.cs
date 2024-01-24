using POS.Utilites.Static;

namespace POS.Infraestructure.FileExcel
{
    public interface IGenerateExcel
    {
        MemoryStream GenerateToExcel<T>(IEnumerable<T> data,List<TableColumns> columns);
    }
}
