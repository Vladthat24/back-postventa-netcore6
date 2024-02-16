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


        public static List<(string ColumnName, string PropertyName)> GetColumnsProducts()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
            {
                ("Codigo de Producto","Code"),
                ("Nombre","Name"),
                ("Stock Mín.","StockMin"),
                ("Sock Máx.","StockMax"),
                ("Precio de Venta","UnitSalePrice"),
                ("Categoría","Category"),
                ("Fecha de Creación","AuditCreateDate"),
                ("Estado","StateProvider")
            };

            return columnsProperties;
        }

        public static List<(string ColumnName, string PropertyName)> GetColumnsPurcharse()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
            {
                ("Proveedor","Provider"),
                ("Almacén","warehouse"),
                ("Monto Total.","TotalAmount"),
                ("Fecha de Compra","DateOfPurcharse"),
 
            };

            return columnsProperties;
        }
    }
}
