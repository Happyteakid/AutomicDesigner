using Microsoft.AspNetCore.Http;

namespace AutomicObjectDesignerBack.Models.Objects
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
