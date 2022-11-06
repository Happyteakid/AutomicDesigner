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
    // https://localhost:7017/api/SapSimple
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin")]
    public class SapSimpleController : ControllerBase
    {
        private readonly ISapSimpleRepository _sapSimpleRepository;
        private readonly ILogger<SapSimpleController> _logger;


        public SapSimpleController(AppDatabaseContext context, ISapSimpleRepository sapSimpleRepository,
            ILogger<SapSimpleController> logger)
        {
            _sapSimpleRepository = sapSimpleRepository;
            _logger = logger;
        }

        //GET https://localhost:7017/api/SapSimple
        [HttpGet]
        public async Task<ActionResult<List<SapSimple>>> GetSapSimple()
        {
            _logger.LogInformation("GetSapSimple called...");
            var sapSimple = _sapSimpleRepository.GetAll();



            return Ok(sapSimple);
        }

        //GET https://localhost:7017/api/SapSimple/{id}
        [HttpGet("{id:int}")]

        public async Task<ActionResult<SapSimple>> GetSapSimple(int id)
        {
            var sapSimple = _sapSimpleRepository.FindByCondition((x => x.Id == id));
            _logger.LogInformation($"GetSapSimple called with parameter id = {id}");

            if (sapSimple == null)
            {
                return NotFound();
            }

            return Ok(sapSimple);
        }

        //POST https://localhost:7017/api/SapSimple/step1
        [HttpPost("step1", Name = "CreateSapSimple_Step1")]
        
        public async Task<ActionResult<List<SapSimple>>> CreateSapSimple([FromBody] SapSimpleStep1Dto SapSimpleStep1Dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sapSimple = new SapSimple
            {
                SapSid = SapSimpleStep1Dto.SapSid,
                SapClient = SapSimpleStep1Dto.SapClient,
                SapJobName = SapSimpleStep1Dto.SapJobName,
                SapReport = Converter.TextReportConverter(SapSimpleStep1Dto.SapReport),
                SapVariant = Converter.TextVariantConverter(SapSimpleStep1Dto.SapVariant),
                RoutineJob = SapSimpleStep1Dto.RoutineJob,
                DeleteSapJob = SapSimpleStep1Dto.DeleteSapJob,
                ProcessName = SapSimpleStep1Dto.ProcessName,
                ObjectName = $"<{SapSimpleStep1Dto.SapSid}>.<{SapSimpleStep1Dto.SapClient}>#<{SapSimpleStep1Dto.RoutineJob}>#<{SapSimpleStep1Dto.ProcessName}>#<{SapSimpleStep1Dto.SapReport}>" +
                $"$<{SapSimpleStep1Dto.SapVariant}>.JOBS"

            };

            _sapSimpleRepository.Create(sapSimple);
            await _sapSimpleRepository.Save();

            return CreatedAtRoute("CreateSapSimple_Step1", new { id = sapSimple.Id }, sapSimple);
        }

        //POST https://localhost:7017/api/SapSimple/step2
        [HttpPost("step2", Name = "CreateSapSimple_Step2")]

        public async Task<ActionResult<List<SapSimple>>> CreateSapSimple([FromBody] SapSimpleStep2Dto SapSimpleStep2Dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sapObject = _sapSimpleRepository.FindByCondition(x => x.Id == SapSimpleStep2Dto.Id).FirstOrDefault();

            if (sapObject == null)
            {
                return NotFound();
            }

            sapObject.SapReport = SapSimpleStep2Dto.SapReport;
            sapObject.SapVariant = SapSimpleStep2Dto.SapVariant;
            sapObject.ObjectName = SapSimpleStep2Dto.ObjectName;

            _sapSimpleRepository.Update(sapObject);
            await _sapSimpleRepository.Save();

            return CreatedAtRoute("CreateSapSimple_Step2", new { id = sapObject.Id }, sapObject);
        }

        //POST https://localhost:7017/api/SapSimple/step3
        [HttpPost("step3", Name = "CreateSapSimple_Step3")]

        public async Task<ActionResult<List<SapSimple>>> CreateSapSimple([FromBody] SapSimpleStep3Dto SapSimpleStep3Dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sapObject = _sapSimpleRepository.FindByCondition(x => x.Id == SapSimpleStep3Dto.Id).FirstOrDefault();

            if (sapObject == null)
            {
                return NotFound();
            }

            sapObject.Documentation = SapSimpleStep3Dto.Documentation;

            _sapSimpleRepository.Update(sapObject);
            await _sapSimpleRepository.Save();

            return CreatedAtRoute("CreateSapSimple_Step3", new { id = sapObject.Id }, sapObject);
        }

        //POST https://localhost:7017/api/SapSimple/step4
        [HttpPost("step4", Name = "CreateSapSimple_Step4")]

        public async Task<ActionResult<List<SapSimple>>> CreateSapSimple([FromBody] SapSimpleStep4Dto SapSimpleStep4Dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sapObject = _sapSimpleRepository.FindByCondition(x => x.Id == SapSimpleStep4Dto.Id).FirstOrDefault();

            if (sapObject == null)
            {
                return NotFound();
            }

            sapObject.Archive1 = SapSimpleStep4Dto.Archive1;
            sapObject.Archive2 = SapSimpleStep4Dto.Archive2;
            sapObject.Folder = SapSimpleStep4Dto.Folder;
            sapObject.InternalAccount = SapSimpleStep4Dto.InternalAccount;
            sapObject.Title = SapSimpleStep4Dto.Title;

            _sapSimpleRepository.Update(sapObject);
            await _sapSimpleRepository.Save();

            return CreatedAtRoute("CreateSapSimple_Step4", new { id = sapObject.Id }, sapObject);
        }

        // Function returns required Data ready for modification after all steps were finished.
        //Get https://localhost:7017/api/SapSimple/ExportSite/{id}
        [HttpGet("DownloadSapSimpleExportSite/{id:int}", Name = "GetSapSimpleStep5")]
        public async Task<ActionResult<SapSimple>> GetSapSimpleStep5(int id)
        {
            _logger.LogInformation($"GetSapSimple called with parameter id = {id}");

            var sapSimple = _sapSimpleRepository.FindByCondition((h => h.Id == id));

            SapSimple sap = new SapSimple
            {
                Id = id,
                SapVariant = sapSimple.FirstOrDefault().SapVariant,
                SapReport = sapSimple.FirstOrDefault().SapReport,
                ObjectName = sapSimple.FirstOrDefault().ObjectName,
                OwnerId = sapSimple.FirstOrDefault().OwnerId,
                Active = sapSimple.FirstOrDefault().Active,
                MaxParallelTasks = sapSimple.FirstOrDefault().MaxParallelTasks,
                Process = sapSimple.FirstOrDefault().Process,
                PreProcess = sapSimple.FirstOrDefault().PreProcess,
                PostProcess = sapSimple.FirstOrDefault().PostProcess,
                SapClient = sapSimple.FirstOrDefault().SapClient,
                SapSid = sapSimple.FirstOrDefault().SapSid,
                RoutineJob = sapSimple.FirstOrDefault().RoutineJob,
                ProcessName = sapSimple.FirstOrDefault().ProcessName,
                SapJobName = sapSimple.FirstOrDefault().SapJobName,
                DeleteSapJob = sapSimple.FirstOrDefault().DeleteSapJob,
                Documentation = sapSimple.FirstOrDefault().Documentation,
                Title = sapSimple.FirstOrDefault().Title,
                Archive1 = sapSimple.FirstOrDefault().Archive1,
                Archive2 = sapSimple.FirstOrDefault().Archive2,
                InternalAccount = sapSimple.FirstOrDefault().InternalAccount,
                Folder = sapSimple.FirstOrDefault().Folder,
                Queue = sapSimple.FirstOrDefault().Queue,
                Agent = sapSimple.FirstOrDefault().Agent,
                Login = sapSimple.FirstOrDefault().Login
            };

            return Ok(sap);
        }

        // Function returns required Data ready for modification after step 1 was filled.
        //Get https://localhost:7017/api/SapSimple/GetSapSimpleStep2/{id}
        [HttpGet("GetSapSimpleStep2/{id:int}", Name = "GetSapSimpleStep2")]
        public async Task<ActionResult<SapSimple>> GetSapSimpleStep2(int id)
        {
            _logger.LogInformation($"GetSapSimple called with parameter id = {id}");

            var sapSimple = _sapSimpleRepository.FindByCondition((h => h.Id == id));
            SapSimpleStep2Dto sap = new SapSimpleStep2Dto

            {
                Id = id,
                SapVariant = sapSimple.FirstOrDefault().SapVariant,
                SapReport = sapSimple.FirstOrDefault().SapReport,
                ObjectName = sapSimple.FirstOrDefault().ObjectName,
                
            };

            return Ok(sap);
        }

        // Function returns required Data ready for modification after all steps were finished.
        //Get https://localhost:7017/api/SapSimple/DownloadCsvFile/{id}
        [HttpGet("DownloadSapSimpleCsvFile/{id:int}", Name = "DownloadSapSimpleCsvFile"), AllowAnonymous]
        public IActionResult DownloadCsvFile(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var sapSimple = _sapSimpleRepository.FindByCondition((h => h.Id == id)).FirstOrDefault();
            if (sapSimple == null)
            {
                return NotFound();
            };



            string[] columnNames = new string[]
            {
                "Id", "SapVariant", "SapReport", "ObjectName", "OwnerId", "Active", "MaxParallelTasks",
                "Process", "PreProcess", "PostProcess", "SapClient",
                "SapSid", "RoutineJob", "ProcessName", "SapJobName", "DeleteSapJob", "Documentation", "Title",
                "Archive1", "Archive2", "InternalAccount",
                "Folder", "Queue", "Agent", "Login"
            };
            var saps = _sapSimpleRepository.FindByCondition((h => h.Id == id));
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

        }

        // Function returns required Data ready for modification after all steps were finished.
        //Get https://localhost:7017/api/SapSimple/DownloadJsonFile/{id}
        [HttpGet("DownloadSapSimpleJsonFile/{id:int}", Name = "DownloadSapSimpleJsonFile"), AllowAnonymous]
        public IActionResult DownloadJsonFile(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var sapSimple = _sapSimpleRepository.FindByCondition((h => h.Id == id)).FirstOrDefault();
            if (sapSimple == null)
            {
                return NotFound();
            };

            SapSimple sapJson = new SapSimple
            {
                Id = id,
                SapVariant = sapSimple.SapVariant,
                SapReport = sapSimple.SapReport,
                ObjectName = sapSimple.ObjectName,
                OwnerId = sapSimple.OwnerId,
                Active = sapSimple.Active,
                MaxParallelTasks = sapSimple.MaxParallelTasks,
                Process = sapSimple.Process,
                PreProcess = sapSimple.PreProcess,
                PostProcess = sapSimple.PostProcess,
                SapClient = sapSimple.SapClient,
                SapSid = sapSimple.SapSid,
                RoutineJob = sapSimple.RoutineJob,
                ProcessName = sapSimple.ProcessName,
                SapJobName = sapSimple.SapJobName,
                DeleteSapJob = sapSimple.DeleteSapJob,
                Documentation = sapSimple.Documentation,
                Title = sapSimple.Title,
                Archive1 = sapSimple.Archive1,
                Archive2 = sapSimple.Archive2,
                InternalAccount = sapSimple.InternalAccount,
                Folder = sapSimple.Folder,
                Queue = sapSimple.Queue,
                Agent = sapSimple.Agent,
                Login = sapSimple.Login
            };

            string fileName = "sap.json";
            string jsonString = JsonSerializer.Serialize(sapJson, new JsonSerializerOptions { WriteIndented = true });

            byte[] bytes = Encoding.ASCII.GetBytes(jsonString);
            return File(bytes, "application/json", "sap.json");

        }


        // DELETE https://localhost:7017/api/SapSimple/{id}
        [HttpDelete("{id:int}", Name = "DeleteSapSimple")]
        public async Task<ActionResult<List<SapSimple>>> DeleteSapSimple(int id)
        {
            var sapObject = new SapSimple() { Id = id };

            if (sapObject == null)
            {
                return NotFound();
            }

            _sapSimpleRepository.Delete(sapObject);
            _sapSimpleRepository.Save();

            return NoContent();

        }
        
    }
}
