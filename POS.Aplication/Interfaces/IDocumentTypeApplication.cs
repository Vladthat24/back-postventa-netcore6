using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Dtos.DocumentType.Response;

namespace POS.Aplication.Interfaces
{
    public interface IDocumentTypeApplication
    {
        Task<BaseResponse<IEnumerable<DocumentTypeResponseDto>>> ListDocumentType();
    }
}
