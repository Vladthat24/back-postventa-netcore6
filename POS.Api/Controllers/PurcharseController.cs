using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Dtos.Purcharse.Request;
using POS.Aplication.Interfaces;
using POS.Utilites.Static;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurcharseController : ControllerBase
    {
        private readonly IPurcharseApplication _purcharseApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;
        public PurcharseController(IPurcharseApplication purcharseApplication, IGenerateExcelApplication generateExcelApplication)
        {
            _purcharseApplication = purcharseApplication;
            _generateExcelApplication = generateExcelApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListPurcharses([FromQuery] BaseFilterRequest filters)
        {
            var response = await _purcharseApplication.ListPurcharse(filters);
            if ((bool)filters.Download!)
            {
                var columnsNames = ExcelColumnsName.GetColumnsPurcharse();
                var filterBytes = _generateExcelApplication.GenerateToExcel(response.Data!, columnsNames);
                return File(filterBytes, ContentType.ContentTypeExcel);
            }

            return Ok(response);
        }

        [HttpGet("{purcharseId:int}")]
        public async Task<IActionResult> PurcharseById(int purcharseId)
        {
            var response = await _purcharseApplication.PurcharseById(purcharseId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPurcharse([FromBody] PurcharseRequestDto requestDto)
        {
            var response = await _purcharseApplication.RegisterPurcharse(requestDto);
            return Ok(response);
        }

        [HttpPut("Cancel/{purcharseId:int}")]
        public async Task<IActionResult> CancelPurcharse(int purcharseId)
        {
            var response = await _purcharseApplication.CancelPurcharse(purcharseId);
            return Ok(response);
        }
    }
}
