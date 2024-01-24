using Microsoft.AspNetCore.Mvc;
using POS.Aplication.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeApplication _documentTypeApplication;
        public DocumentTypeController(IDocumentTypeApplication documentTypeApplication)
        {
            _documentTypeApplication = documentTypeApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListDocumentType()
        {
            var response = await _documentTypeApplication.ListDocumentType();
            return Ok(response);
        }

    }
}
