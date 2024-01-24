using Microsoft.AspNetCore.Mvc;
using POS.Aplication.Commons.Bases.Request;
using POS.Aplication.Dtos.Category.Request;
using POS.Aplication.Interfaces;
using POS.Utilites.Static;

namespace POS.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplication _categoryAplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;

        public CategoryController(ICategoryApplication categoryAplication, IGenerateExcelApplication generateExcelApplication)
        {
            _categoryAplication = categoryAplication;
            _generateExcelApplication = generateExcelApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListCategories([FromQuery] BaseFilterRequest filters)
        {
            var response = await _categoryAplication.ListCategories(filters);

            //Funcion para descargar en formato excel los datos
            if((bool)filters.Download!)
            {
                var columnsNames = ExcelColumnsName.GetColumnsCategories();
                var fileBytes = _generateExcelApplication.GenerateToExcel(response.Data!, columnsNames);
                return File(fileBytes, ContentType.ContentTypeExcel);
            }
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult>ListSelectCategories()
        {
            var response = await _categoryAplication.ListSelectCategories();
            return Ok(response);
        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> CategoryById(int categoryId)
        {
            var response = await _categoryAplication.CategoryById(categoryId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCategory([FromBody] CategoryRequestDto requestDto)
        {
            var response=await _categoryAplication.RegisterCategory(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{categoryId:int}")]
        public async Task<IActionResult> EditCategory(int categoryId, [FromBody] CategoryRequestDto requestDto)
        {
            var response = await _categoryAplication.EditCategory(categoryId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{categoryId:int}")]
        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            var response = await _categoryAplication.RemoveCategory(categoryId);
            return Ok(response);
        }

    }
}
