﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Aplication.Commons.Bases.Request;
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

    }
}