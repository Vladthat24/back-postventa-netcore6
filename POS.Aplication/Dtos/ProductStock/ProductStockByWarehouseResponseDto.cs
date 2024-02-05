using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Aplication.Dtos.ProductStock
{
    public class ProductStockByWarehouseResponseDto
    {
        public string? Warehouse {  get; set; }
        public int CurrentStock {  get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
