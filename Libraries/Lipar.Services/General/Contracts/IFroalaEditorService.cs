using Microsoft.AspNetCore.Http;

namespace Lipar.Services.General.Contracts
{
    public interface IFroalaEditorService
    {
        string UploadPicture(IFormFile formFile);
        void DeletePicture(string fileName);
    }
}
