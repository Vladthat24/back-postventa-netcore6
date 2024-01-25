using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Aplication.Dtos.Warehouse.Response
{
    public class WarehouseResponseDto
    {
        public int WarehouseId { get; set; }
        public string? Name { get; set; }
        public DateTime AuditCreateDate { get; set; }
        public int State { get; set; }
        public string? StateWarehouse {  get; set; }
    }
}
