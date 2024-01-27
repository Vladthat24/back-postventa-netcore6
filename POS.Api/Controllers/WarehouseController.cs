using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Dtos.Warehouse.Request;
using POS.Aplication.Interfaces;
using POS.Utilites.Static;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseApplication _warehouseApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;

        public WarehouseController(IGenerateExcelApplication generateExcelApplication, IWarehouseApplication warehouseApplication)
        {
            _generateExcelApplication = generateExcelApplication;
            _warehouseApplication = warehouseApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListWarehouses([FromQuery] BaseFilterRequest filters)
        {
            var response = await _warehouseApplication.ListWarehouses(filters);

            if((bool)filters.Download!)
            {
                var columnsNames = ExcelColumnsName.GetColumnsWarehouses();
                var fileBytes = _generateExcelApplication.GenerateToExcel(response.Data!, columnsNames);
                return File(fileBytes, ContentType.ContentTypeExcel);
            }

            return Ok(response);
        }

        [HttpGet("{warehouseId:int}")]
        public async Task<IActionResult>WarehouseId(int warehouseId)
        {
            var response = await _warehouseApplication.WarehouseById(warehouseId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterWarehouse([FromBody] WarehouseRequestDto requestDto)
        {
            var response = await _warehouseApplication.RegisterWareHouse(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{warehouseId:int}")]
        public async Task<IActionResult> EditWarehouse(int warehouseId, [FromBody] WarehouseRequestDto requestDto)
        {
            var response= await _warehouseApplication.EditWarehouse(warehouseId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{warehouseId:int}")]
        public async Task<IActionResult> RemoveWarehouse(int warehouseId)
        {
            var response= await _warehouseApplication.RemoveWarehouse(warehouseId);
            return Ok(response);
        }
    }
}
