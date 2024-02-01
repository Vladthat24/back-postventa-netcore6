using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Dtos.Product.Request;
using POS.Aplication.Interfaces;
using POS.Utilites.Static;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;


        public ProductController(IProductApplication productApplication, IGenerateExcelApplication generateExcelApplication)
        {
            _productApplication = productApplication;
            _generateExcelApplication = generateExcelApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListProducts([FromQuery] BaseFilterRequest filters)
        {
            var response = await _productApplication.ListProducts(filters);
            if ((bool)filters.Download!)
            {
                var columnsNames = ExcelColumnsName.GetColumnsProducts();
                var fileBytes = _generateExcelApplication.GenerateToExcel(response.Data!, columnsNames);
                return File(fileBytes, ContentType.ContentTypeExcel);
            }
            return Ok(response);
        }


        [HttpGet("{productId:int}")]
        public async Task<IActionResult>ProductById(int productId)
        {
            var response= await _productApplication.ProductById(productId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterProduct([FromForm] ProductRequestDto requestDto)
        {
            var response= await _productApplication.RegisterProduct(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{productId:int}")]
        public async Task<IActionResult> EditProduct(int productId, [FromForm] ProductRequestDto requestDto)
        {
            var response = await _productApplication.EditProduct(productId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{productId:int}")]
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            var response = await _productApplication.RemoveProduct(productId);
            return Ok(response);
        }
    }
}
