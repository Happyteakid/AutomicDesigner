using System.Text;
using AutomicObjectDesigner.Models.Objects;
using AutomicObjectDesignerBack.Controllers.Functions;
using AutomicObjectDesignerBack.Data;
using AutomicObjectDesignerBack.Models.Objects;
using AutomicObjectDesignerBack.Repository;
using AutomicObjectDesignerBack.Repository.Implementations;
using Fingers10.ExcelExport.ActionResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;

namespace AutomicObjectDesignerBack.Controllers.ObjectContollers
{
    // https://localhost:7017/api/SapJobBwChain

    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    public class SapJobBwChainController : ControllerBase
    {
        private readonly ISapJobBwChainRepository _sapJobBwChainRepository;
        private readonly ILogger<SapJobBwChainController> _logger;

        public SapJobBwChainController(ILogger<SapJobBwChainController> logger,
            ISapJobBwChainRepository repository)
        {
            _sapJobBwChainRepository = repository;
            _logger = logger;
        }

        //POST https://localhost:7017/api/SapJobBwChain/step1
        [HttpPost("step1", Name = "CreateSapJobBwChain_Step1")]
        public async Task<ActionResult<List<SapJobBwChain>>> CreateSapJobBwChain(
            [FromBody] SapJobBwChainStep1Dto SapJobBwChainStep1Dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sapJobBwChain = new SapJobBwChain
            {
                SapSid = SapJobBwChainStep1Dto.SapSid,
                SapClient = SapJobBwChainStep1Dto.SapClient,
                SapJobName = SapJobBwChainStep1Dto.SapJobName,
                DeleteSapJob = SapJobBwChainStep1Dto.DeleteSapJob,
                RoutineJob = SapJobBwChainStep1Dto.RoutineJob,
                ProcessName = SapJobBwChainStep1Dto.ProcessName,
                SapReport = Converter.TextReportConverter(SapJobBwChainStep1Dto.SapReport),
                SapVariant = Converter.TextVariantConverter(SapJobBwChainStep1Dto.SapVariant),
                Kette = Converter.TextKetteConverter(SapJobBwChainStep1Dto.Kette),
                ObjectName =
                    $"<{SapJobBwChainStep1Dto.SapSid}>.<{SapJobBwChainStep1Dto.SapClient}>#<{SapJobBwChainStep1Dto.RoutineJob}>#<{SapJobBwChainStep1Dto.ProcessName}>#<{SapJobBwChainStep1Dto.SapReport}>" +
                    $"$<{SapJobBwChainStep1Dto.SapVariant}>.JOBS"
            };
            _sapJobBwChainRepository.Create(sapJobBwChain);
            await _sapJobBwChainRepository.Save();
            int id = sapJobBwChain.Id;
            Console.WriteLine($"Id = {id}");
            return CreatedAtRoute($"CreateSapJobBwChain_Step1", new { id = sapJobBwChain.Id }, sapJobBwChain);
        }

        // Delete https://localhost:7017/api/SapJobBwChain/{id}
        [HttpDelete("{id:int}", Name = "DeleteSapJobBwChain")]
        public async Task<ActionResult<List<SapJobBwChain>>> DeleteSapJobBwChainStep1(int id)
        {
            var sapObject = new SapJobBwChain() { Id = id };

            _sapJobBwChainRepository.Delete(sapObject);

            _sapJobBwChainRepository.Save();

            return NoContent();
        }


        //Get https://localhost:7017/api/SapJobBwChain
        [HttpGet]
        public async Task<ActionResult<List<SapJobBwChain>>> GetSapJobBwChain()
        {
            _logger.LogInformation("GetSapJobBwChain called...");
            var sapJobBwChain = _sapJobBwChainRepository.GetAll();

            return Ok(sapJobBwChain);
        }

        //Get https://localhost:7017/api/SapJobBwChain/{id}
        [HttpGet("{id:int}", Name = "GetSapJobBwChain")]
        public async Task<ActionResult<SapJobBwChain>> GetSapJobBwChain(int id)
        {
            _logger.LogInformation($"GetSapSobBwChain called with parameter id = {id}");

            var sapJobBwChain = _sapJobBwChainRepository.FindByCondition((h => h.Id == id));

            return Ok(sapJobBwChain);
        }

        // Function returns required Data ready for modification after step 1 was filled.
        //Get https://localhost:7017/api/SapJobBwChain/GetSapJobBwChainStep2/{id}
        [HttpGet("GetSapJobBwChainStep2/{id:int}", Name = "GetSapJobBwChainStep2")]
        public async Task<ActionResult<SapJobBwChain>> GetSapJobBwChainStep2(int id)
        {
            _logger.LogInformation($"GetSapSobBwChain called with parameter id = {id}");

            var sapJobBwChain = _sapJobBwChainRepository.FindByCondition((h => h.Id == id));

            SapJobBwChainStep2Dto sap = new SapJobBwChainStep2Dto
            {
                Id = id,
                SapVariant = sapJobBwChain.FirstOrDefault().SapVariant,
                SapReport = sapJobBwChain.FirstOrDefault().SapReport,
                ObjectName = sapJobBwChain.FirstOrDefault().ObjectName,
                Kette = sapJobBwChain.FirstOrDefault().Kette
            };

            return Ok(sap);
        }

        // Function returns required Data ready for modification after all steps were finished.
        //Get https://localhost:7017/api/SapJobBwChain/ExportSite/{id}
        [HttpGet("DownloadSapJobBwChainExportSite/{id:int}", Name = "GetSapJobBwChainStep5")]
        public async Task<ActionResult<SapJobBwChain>> GetSapJobBwChainStep5(int id)
        {
            _logger.LogInformation($"GetSapSobBwChain called with parameter id = {id}");

            var sapJobBwChain = _sapJobBwChainRepository.FindByCondition((h => h.Id == id));

            SapJobBwChain sap = new SapJobBwChain
            {
                Id = id,
                SapVariant = sapJobBwChain.FirstOrDefault().SapVariant,
                SapReport = sapJobBwChain.FirstOrDefault().SapReport,
                ObjectName = sapJobBwChain.FirstOrDefault().ObjectName,
                Kette = sapJobBwChain.FirstOrDefault().Kette,
                OwnerId = sapJobBwChain.FirstOrDefault().OwnerId,
                Active = sapJobBwChain.FirstOrDefault().Active,
                MaxParallelTasks = sapJobBwChain.FirstOrDefault().MaxParallelTasks,
                Process = sapJobBwChain.FirstOrDefault().Process,
                PreProcess = sapJobBwChain.FirstOrDefault().PreProcess,
                PostProcess = sapJobBwChain.FirstOrDefault().PostProcess,
                SapClient = sapJobBwChain.FirstOrDefault().SapClient,
                SapSid = sapJobBwChain.FirstOrDefault().SapSid,
                RoutineJob = sapJobBwChain.FirstOrDefault().RoutineJob,
                ProcessName = sapJobBwChain.FirstOrDefault().ProcessName,
                SapJobName = sapJobBwChain.FirstOrDefault().SapJobName,
                DeleteSapJob = sapJobBwChain.FirstOrDefault().DeleteSapJob,
                Documentation = sapJobBwChain.FirstOrDefault().Documentation,
                Title = sapJobBwChain.FirstOrDefault().Title,
                Archive1 = sapJobBwChain.FirstOrDefault().Archive1,
                Archive2 = sapJobBwChain.FirstOrDefault().Archive2,
                InternalAccount = sapJobBwChain.FirstOrDefault().InternalAccount,
                Folder = sapJobBwChain.FirstOrDefault().Folder,
                Queue = sapJobBwChain.FirstOrDefault().Queue,
                Agent = sapJobBwChain.FirstOrDefault().Agent,
                Login = sapJobBwChain.FirstOrDefault().Login
            };
            return Ok(sap);
        }

        // Function returns required Data ready for modification after all steps were finished.
        //Get https://localhost:7017/api/SapJobBwChain/DownloadSapJobBwChainCsvFile/{id}
        [HttpGet("DownloadSapJobBwChainCsvFile/{id:int}", Name = "DownloadSapJobBwChainCsvFile"), AllowAnonymous]
        public IActionResult DownloadCsvFile(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var sapJobBwChain = _sapJobBwChainRepository.FindByCondition((h => h.Id == id)).FirstOrDefault();
            if (sapJobBwChain == null)
            {
                return NotFound();
            };



            string[] columnNames = new string[]
            {
                "Id", "SapVariant", "SapReport", "ObjectName", "Kette", "OwnerId", "Active", "MaxParallelTasks",
                "Process", "PreProcess", "PostProcess", "SapClient",
                "SapSid", "RoutineJob", "ProcessName", "SapJobName", "DeleteSapJob", "Documentation", "Title",
                "Archive1", "Archive2", "InternalAccount",
                "Folder", "Queue", "Agent", "Login"
            };
            var saps = _sapJobBwChainRepository.FindByCondition((h => h.Id == id));
            string csv = String.Empty;

            foreach (string columnName in columnNames)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";

            foreach (var sap in saps)
            {
                csv += '"' + sap.Id.ToString().Replace(",", ";") + "\",";
                csv += '"' + sap.SapVariant.Replace(",", ";") + "\",";
                csv += '"' + sap.SapReport.Replace(",", ";") + "\",";
                csv += '"' + sap.ObjectName.Replace(",", ";") + "\",";
                csv += '"' + sap.Kette.Replace(",", ";") + "\",";
                csv += '"' + sap.OwnerId.ToString().Replace(",", ";") + "\",";
                csv += '"' + sap.Active.ToString().Replace(",", ";") + "\",";
                csv += '"' + sap.MaxParallelTasks.ToString().Replace(",", ";") + "\",";
                csv += '"' + sap.Process.Replace(",", ";") + "\",";
                csv += '"' + sap.PreProcess.Replace(",", ";") + "\",";
                csv += '"' + sap.PostProcess.Replace(",", ";") + "\",";
                csv += '"' + sap.SapClient.Replace(",", ";") + "\",";
                csv += '"' + sap.SapSid.Replace(",", ";") + "\",";
                csv += '"' + sap.RoutineJob.ToString().Replace(",", ";") + "\",";
                csv += '"' + sap.ProcessName.Replace(",", ";") + "\",";
                csv += '"' + sap.SapJobName.Replace(",", ";") + "\",";
                csv += '"' + sap.DeleteSapJob.ToString().Replace(",", ";") + "\",";
                csv += '"' + sap.Documentation.Replace(",", ";") + "\",";
                csv += '"' + sap.Title.Replace(",", ";") + "\",";
                csv += '"' + sap.Archive1.Replace(",", ";") + "\",";
                csv += '"' + sap.Archive2.Replace(",", ";") + "\",";
                csv += '"' + sap.InternalAccount.Replace(",", ";") + "\",";
                csv += '"' + sap.Folder.Replace(",", ";") + "\",";
                csv += '"' + sap.Queue?.Replace(",", ";") + "\",";
                csv += '"' + sap.Agent?.Replace(",", ";") + "\",";
                csv += '"' + sap.Login?.Replace(",", ";") + "\",";

                csv += "\r\n";

            }

            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "text/csv", "sap.csv");
            //return new ExcelResult<SapJobBwChain>(saps, $"{id}_JOBS_SAP_JOB_BW_CHAIN", $"{id}_JOBS_SAP_JOB_BW_CHAIN_EX");
        }

        // Function returns required Data ready for modification after all steps were finished.
        //Get https://localhost:7017/api/SapJobBwChain/JsonFile/{id}
        [HttpGet("DownloadSapJobBwChainJsonFile/{id:int}", Name = "DownloadSapJobBwChainJsonFile"), AllowAnonymous]
        public IActionResult DownloadJsonFile(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var sapJobBwChain = _sapJobBwChainRepository.FindByCondition((h => h.Id == id)).FirstOrDefault();
            if (sapJobBwChain == null)
            {
                return NotFound();
            };

            SapJobBwChain sapJson = new SapJobBwChain
            {
                Id = id,
                SapVariant = sapJobBwChain.SapVariant,
                SapReport = sapJobBwChain.SapReport,
                ObjectName = sapJobBwChain.ObjectName,
                Kette = sapJobBwChain.Kette,
                OwnerId = sapJobBwChain.OwnerId,
                Active = sapJobBwChain.Active,
                MaxParallelTasks = sapJobBwChain.MaxParallelTasks,
                Process = sapJobBwChain.Process,
                PreProcess = sapJobBwChain.PreProcess,
                PostProcess = sapJobBwChain.PostProcess,
                SapClient = sapJobBwChain.SapClient,
                SapSid = sapJobBwChain.SapSid,
                RoutineJob = sapJobBwChain.RoutineJob,
                ProcessName = sapJobBwChain.ProcessName,
                SapJobName = sapJobBwChain.SapJobName,
                DeleteSapJob = sapJobBwChain.DeleteSapJob,
                Documentation = sapJobBwChain.Documentation,
                Title = sapJobBwChain.Title,
                Archive1 = sapJobBwChain.Archive1,
                Archive2 = sapJobBwChain.Archive2,
                InternalAccount = sapJobBwChain.InternalAccount,
                Folder = sapJobBwChain.Folder,
                Queue = sapJobBwChain.Queue,
                Agent = sapJobBwChain.Agent,
                Login = sapJobBwChain.Login
            };

            string fileName = "sap.json";
            string jsonString = JsonSerializer.Serialize(sapJson, new JsonSerializerOptions { WriteIndented = true });

            byte[] bytes = Encoding.ASCII.GetBytes(jsonString);
            return File(bytes, "application/json", "sap.json");

        }

        //// Function returns required Data ready for modification after all steps were finished.
        ////Get https://localhost:7017/api/SapJobBwChain/DownloadXmlFile/{id}
        //[HttpGet("DownloadXmlFile/{id:int}", Name = "DownloadXmlFile")]
        //public IActionResult DownloadXmlFile(int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    };

        //    var sapJobBwChain = _sapJobBwChainRepository.FindByCondition((h => h.Id == id)).FirstOrDefault();
        //    if (sapJobBwChain == null)
        //    {
        //        return NotFound();
        //    };

        //    SapJobBwChain sapXml = new SapJobBwChain
        //    {
        //        Id = id,
        //        SapVariant = sapJobBwChain.SapVariant,
        //        SapReport = sapJobBwChain.SapReport,
        //        ObjectName = sapJobBwChain.ObjectName,
        //        Kette = sapJobBwChain.Kette,
        //        OwnerId = sapJobBwChain.OwnerId,
        //        Active = sapJobBwChain.Active,
        //        MaxParallelTasks = sapJobBwChain.MaxParallelTasks,
        //        Process = sapJobBwChain.Process,
        //        PreProcess = sapJobBwChain.PreProcess,
        //        PostProcess = sapJobBwChain.PostProcess,
        //        SapClient = sapJobBwChain.SapClient,
        //        SapSid = sapJobBwChain.SapSid,
        //        RoutineJob = sapJobBwChain.RoutineJob,
        //        ProcessName = sapJobBwChain.ProcessName,
        //        SapJobName = sapJobBwChain.SapJobName,
        //        DeleteSapJob = sapJobBwChain.DeleteSapJob,
        //        Documentation = sapJobBwChain.Documentation,
        //        Title = sapJobBwChain.Title,
        //        Archive1 = sapJobBwChain.Archive1,
        //        Archive2 = sapJobBwChain.Archive2,
        //        InternalAccount = sapJobBwChain.InternalAccount,
        //        Folder = sapJobBwChain.Folder,
        //        Queue = sapJobBwChain.Queue,
        //        Agent = sapJobBwChain.Agent,
        //        Login = sapJobBwChain.Login
        //    };

        //    var serializer = new XmlSerializer(typeof(SapJobBwChain));
        //    using (var writer = new StreamWriter("sap.xml"))
        //    {
        //        serializer.Serialize(writer, sapXml);
        //    }

            
        //    byte[] bytes = Encoding.ASCII.GetBytes(serializer);
        //    return File(bytes, "application/xml", "sap.xml");

        //}

        
        // https://localhost:7017/api/SapJobBwChain/step2
        [HttpPost("step2", Name = "CreateSapJobBwChain_Step2")]
        public async Task<ActionResult<List<SapJobBwChain>>> CreateSapJobBwChainStep2([FromBody] SapJobBwChainStep2Dto SapJobBwChainStep2Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sapObject = _sapJobBwChainRepository.FindByCondition(x => x.Id == SapJobBwChainStep2Dto.Id).FirstOrDefault();

            if (sapObject == null)
            {
                return NotFound();
            }

            sapObject.SapReport = SapJobBwChainStep2Dto.SapReport;
            sapObject.SapVariant = SapJobBwChainStep2Dto.SapVariant;
            sapObject.Kette = SapJobBwChainStep2Dto.Kette;
            sapObject.ObjectName = SapJobBwChainStep2Dto.ObjectName;

            _sapJobBwChainRepository.Update(sapObject);
            await _sapJobBwChainRepository.Save();

            return CreatedAtRoute("CreateSapJobBwChain_Step2", new { id = sapObject.Id }, sapObject);
        }


        // https://localhost:7017/SapJobBwChain/step3
        [HttpPost("step3", Name = "CreateSapJobBwChain_Step3")]
        public async Task<ActionResult<List<SapJobBwChain>>> CreateSapJobBwChainStep3(
            [FromBody] SapJobBwChainStep3Dto SapJobBwChainStep3Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sapObject = _sapJobBwChainRepository.FindByCondition(x => x.Id == SapJobBwChainStep3Dto.Id).FirstOrDefault();

            if (sapObject == null)
            {
                return NotFound();
            }

            sapObject.Documentation = SapJobBwChainStep3Dto.Documentation;
            

            _sapJobBwChainRepository.Update(sapObject);
            await _sapJobBwChainRepository.Save();

            return CreatedAtRoute("CreateSapJobBwChain_Step3", new { id = sapObject.Id }, sapObject);
            
        }

        [HttpPost("step4", Name = "CreateSapJobBwChain_Step4")]
        public async Task<ActionResult<List<SapJobBwChain>>> CreateSapJobBwChainStep4(
            [FromBody] SapJobBwChainStep4Dto SapJobBwChainStep4Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var sapObject = _sapJobBwChainRepository.FindByCondition(x => x.Id == SapJobBwChainStep4Dto.Id).FirstOrDefault();

            if (sapObject == null)
            {
                return NotFound();
            }

            sapObject.Archive1 = SapJobBwChainStep4Dto.Archive1;
            sapObject.Archive2 = SapJobBwChainStep4Dto.Archive2;
            sapObject.Folder = SapJobBwChainStep4Dto.Folder;
            sapObject.InternalAccount = SapJobBwChainStep4Dto.InternalAccount;
            sapObject.Title = SapJobBwChainStep4Dto.Title;

            _sapJobBwChainRepository.Update(sapObject);
            await _sapJobBwChainRepository.Save();

            return CreatedAtRoute("CreateSapJobBwChain_Step4", new { id = sapObject.Id }, sapObject);
        }
    }
}