using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Utilites.Static
{
    public class ExcelColumnsName
    {
        public static List<TableColumns> GetColumns(IEnumerable<(string ColumnName,string PropertyName)> columnsProperties)
        {
            var columns = new List<TableColumns>();

            foreach(var (ColumnName, PropertyName) in columnsProperties)
            {
                var column = new TableColumns()
                {
                    Label = ColumnName,
                    PropertyName = PropertyName
                };

                columns.Add(column);
            }

            return columns;
        }

        //region ColumnsCategories
        public static List<(string ColumnName,string PropertyName)> GetColumnsCategories()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
            {
                ("Nombre","Name"),
                ("Descripción","Description"),
                ("Fecha de Creación","AuditCreateDate"),
                ("Estado","stateCategory")
            };

            return columnsProperties;
        }
        //endRegion

        //region ColumnsProvider
        public static List<(string ColumnName, string PropertyName)> GetColumnsProvider()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
            {
                ("Nombre","Name"),
                ("Email","Email"),
                ("Tipo de Documento","DocumentType"),
                ("N° de Documento","DocumentNumber"),
                ("Dirección","Address"),
                ("Teléfono","Phone"),
                ("Fecha de Creación","AuditCreateDate"),
                ("Estado","StateProvider")
            };

            return columnsProperties;
        }
        //endRegion

        //region Warehouses
        public static List<(string ColumnName, string PropertyName)> GetColumnsWarehouses()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
            {
                ("Nombre","Name"),
                ("Fecha de Creación","AuditCreateDate"),
                ("Estado","StateWarehouse")
            };

            return columnsProperties;
        }
        //endRegion
    }
}
