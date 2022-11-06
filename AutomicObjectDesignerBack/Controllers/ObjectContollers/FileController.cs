//using Microsoft.AspNetCore.Mvc;
//using AutomicObjectDesignerBack.Models.Objects;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.IO;



//namespace AutomicObjectDesignerBack.Controllers.ObjectContollers
//{
//    [Route("api/file")]
//    [ApiController]
//    public class FileController : ControllerBase
//    {
//        public ActionResult Post([FromForm] FileModel file)
//        {
//            try
//            {
//                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroots", file.FileName);

//                using (Stream stream = new FileStream(path, FileMode.Create))
//                {
//                    file.FormFile.CopyTo(stream);
//                }
//                return StatusCode(StatusCodes.Status201Created);
            
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }
//    }
//}
