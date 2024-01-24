using AutoMapper;
using POS.Aplication.Comnons.Bases.Response;
using POS.Aplication.Dtos.DocumentType.Response;
using POS.Aplication.Interfaces;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilites.Static;
using WatchDog;

namespace POS.Aplication.Services
{
    public class DocumentTypeApplication : IDocumentTypeApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DocumentTypeApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse<IEnumerable<DocumentTypeResponseDto>>> ListDocumentType()
        {
            var response=new BaseResponse<IEnumerable<DocumentTypeResponseDto>>();

            try
            {
                var documentTypes = await _unitOfWork.DocumentType.GetSelectAsync();

                if(documentTypes is not null)
                {
                    response.Data=_mapper.Map<IEnumerable<DocumentTypeResponseDto>>(documentTypes);
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }

            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }
    }
}
