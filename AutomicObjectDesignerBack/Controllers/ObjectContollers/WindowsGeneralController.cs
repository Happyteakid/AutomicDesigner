using AutomicObjectDesignerBack.Data;
using AutomicObjectDesignerBack.Models.Objects;
using AutomicObjectDesignerBack.Repository;
using AutomicObjectDesignerBack.Repository.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AutomicObjectDesigner.Models.Objects;
using Microsoft.AspNetCore.Authorization;

namespace AutomicObjectDesignerBack.Controllers.ObjectContollers
{
    // https://localhost:7017/api/WindowsGeneral
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Admin")]
    public class WindowsGeneralController : ControllerBase
    {
        private readonly IWindowsGeneralRepository _windowsGeneralRepository;
        private readonly ILogger<WindowsGeneralController> _logger;

        public WindowsGeneralController(AppDatabaseContext context, ILogger<WindowsGeneralController> logger,
            IWindowsGeneralRepository repository)
        {
            _windowsGeneralRepository = repository;
            _logger = logger;
        }

        //GET https://localhost:7017/api/WindowsGeneral
        [HttpGet]
        public async Task<ActionResult<List<WindowsGeneral>>> GetWindowsGeneral()
        {
            _logger.LogInformation("GetWindowsGeneral called...");
            var windowsGeneral = _windowsGeneralRepository.GetAll();

            return Ok(windowsGeneral);
        }


        //GET https://localhost:7017/api/WindowsGeneral/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<WindowsGeneral>> GetWindowsGeneral(int id)
        {
            _logger.LogInformation($"GetWindowsGeneral called with parameter id = {id}");
            var windowsGeneral = _windowsGeneralRepository.FindByCondition((x => x.Id == id));

            if (windowsGeneral == null)
            {
                return NotFound();
            }

            return Ok(windowsGeneral);
        }

        //DELETE https://localhost:7017/api/WindowsGeneral/{id}
        [HttpDelete("{id}", Name = "DeleteWindowsGeneral")]
        public async Task<ActionResult<List<WindowsGeneral>>> DeleteWindowsGeneral(int id)
        {
            var winObject = new WindowsGeneral() { Id = id };

            if (winObject == null)
            {
                return NotFound();
            }

            _windowsGeneralRepository.Delete(winObject);
            await _windowsGeneralRepository.Save();

            return NoContent();

        }

        //POST https://localhost:7017/api/WindowsGeneral/step1
        [HttpPost("step1", Name = "CreateWindowsGeneral_Step1")]
        public async Task<ActionResult<List<WindowsGeneral>>> CreateWindowsGeneral([FromBody] WindowsGeneralStep1Dto WindowsGeneralStep1Dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var windowsGeneral = new WindowsGeneral
            {
                WinLogin = WindowsGeneralStep1Dto.WinLogin,
                WinServer = WindowsGeneralStep1Dto.WinServer,
                RoutineJob = WindowsGeneralStep1Dto.RoutineJob,
                ProcessName = WindowsGeneralStep1Dto.ProcessName,
                NameSuffix = WindowsGeneralStep1Dto.NameSuffix,
                SapClient = WindowsGeneralStep1Dto.SapClient,
                SapSid = WindowsGeneralStep1Dto.SapSid,
                ObjectName = $"<>.<>#<{WindowsGeneralStep1Dto.RoutineJob}>" +
                             $"#<{WindowsGeneralStep1Dto.ProcessName}>#WIN_<{WindowsGeneralStep1Dto.NameSuffix}>.JOBS"

            };

            _windowsGeneralRepository.Create(windowsGeneral);
            await _windowsGeneralRepository.Save();

            return CreatedAtRoute("CreateWindowsGeneral_Step1", new { id = windowsGeneral.Id }, windowsGeneral);
        }

        //POST https://localhost:7017/api/WindowsGeneral/step2
        [HttpPost("step2", Name = "CreateWindowsGeneral_Step2")]

        public async Task<ActionResult<List<WindowsGeneral>>> CreateWindowsGeneralStep2([FromBody] WindowsGeneralStep2Dto WindowsGeneralStep2Dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var windowsObject = _windowsGeneralRepository.FindByCondition(x => x.Id == WindowsGeneralStep2Dto.Id).FirstOrDefault();

            if (windowsObject == null)
            {
                return NotFound();
            }

            windowsObject.ObjectName = WindowsGeneralStep2Dto.ObjectName;


            //throw new NotImplementedException();
            _windowsGeneralRepository.Update(windowsObject);
            await _windowsGeneralRepository.Save();

            return CreatedAtRoute("CreateWindowsGeneral_Step2", new { id = windowsObject.Id }, windowsObject);
        }

        //POST https://localhost:7017/api/WindowsGeneral/step3
        [HttpPost("step3", Name = "CreateWindowsGeneral_Step3")]

        public async Task<ActionResult<List<WindowsGeneral>>> CreateWindowsGeneralStep3([FromBody] WindowsGeneralStep3Dto WindowsGeneralStep3Dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var windowsObject = _windowsGeneralRepository.FindByCondition(x => x.Id == WindowsGeneralStep3Dto.Id).FirstOrDefault();

            if (windowsObject == null)
            {
                return NotFound();
            }

            windowsObject.Process = WindowsGeneralStep3Dto.Process;

            _windowsGeneralRepository.Update(windowsObject);
            await _windowsGeneralRepository.Save();

            return CreatedAtRoute("CreateWindowsGeneral_Step3", new { id = windowsObject.Id }, windowsObject);
        }

        //POST https://localhost:7017/api/WindowsGeneral/step4
        [HttpPost("step4", Name = "CreateWindowsGeneral_Step4")]

        public async Task<ActionResult<List<WindowsGeneral>>> CreateWindowsGeneralStep4([FromBody] WindowsGeneralStep4Dto WindowsGeneralStep4Dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var windowsObject = _windowsGeneralRepository.FindByCondition(x => x.Id == WindowsGeneralStep4Dto.Id).FirstOrDefault();
            if (windowsObject == null)
            {
                return NotFound();
            }

            windowsObject.Documentation = WindowsGeneralStep4Dto.Documentation;


            _windowsGeneralRepository.Update(windowsObject);
            await _windowsGeneralRepository.Save();

            return CreatedAtRoute("CreateWindowsGeneral_Step4", new { id = windowsObject.Id }, windowsObject);
        }

        //POST https://localhost:7017/api/WindowsGeneral/step5
        [HttpPost("step5", Name = "CreateWindowsGeneral_Step5")]
        public async Task<ActionResult<List<WindowsGeneral>>> CreateWindowsGeneralStep5([FromBody] WindowsGeneralStep5Dto WindowsGeneralStep5Dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var windowsObject = _windowsGeneralRepository.FindByCondition(x => x.Id == WindowsGeneralStep5Dto.Id).FirstOrDefault();

            if (windowsObject == null)
            {
                return NotFound();
            }

            windowsObject.Archive1 = WindowsGeneralStep5Dto.Archive1;
            windowsObject.Archive2 = WindowsGeneralStep5Dto.Archive2;
            windowsObject.Folder = WindowsGeneralStep5Dto.Folder;
            windowsObject.InternalAccount = WindowsGeneralStep5Dto.InternalAccount;
            windowsObject.Title = WindowsGeneralStep5Dto.Title;

            _windowsGeneralRepository.Update(windowsObject);
            await _windowsGeneralRepository.Save();

            return CreatedAtRoute("CreateWindowsGeneral_Step5", new { id = windowsObject.Id }, windowsObject);
        }

        //POST https://localhost:7017/api/WindowsGeneral/step6
        //Swagger https://localhost:7017/swagger
        [HttpPost("step6", Name = "CreateWindowsGeneral_Step6")]
        public async Task<ActionResult<List<WindowsGeneral>>> CreateWindowsGeneralStep6(
            [FromBody] WindowsGeneralStep6Dto[] windowsGeneralStep6Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var windowsObject = _windowsGeneralRepository.FindByCondition(x => x.Id == windowsGeneralStep6Dto[0].Id).FirstOrDefault();
            windowsObject.VariableKey1 = windowsGeneralStep6Dto[0].VariableKey;
            windowsObject.VariableValue1 = windowsGeneralStep6Dto[0].VariableValue;

            _windowsGeneralRepository.Update(windowsObject);
            await _windowsGeneralRepository.Save();

            return Ok(windowsObject);
        }

        // Function returns required Data ready for modification after step 1 was filled.
        //Get https://localhost:7017/api/WindowsGeneral/GetWindowsGeneralStep2/{id}
        [HttpGet("GetWindowsGeneralStep2/{id:int}", Name = "GetWindowsGeneralStep2")]
        public async Task<ActionResult<WindowsGeneral>> GetWindowsGeneralStep2(int id)
        {
            _logger.LogInformation($"GetWindowsGeneral called with parameter id = {id}");

            var windowsGeneral = _windowsGeneralRepository.FindByCondition((h => h.Id == id));
            WindowsGeneralStep2Dto windows = new WindowsGeneralStep2Dto

            {
                Id = id,
                ObjectName = windowsGeneral.FirstOrDefault().ObjectName,

            };

            return Ok(windows);
        }

        // Function returns required Data ready for modification after all steps were finished.
        //Get https://localhost:7017/api/WindowsGeneral/ExportSite/{id}
        [HttpGet("DownloadWindowsGeneralExportSite/{id:int}", Name = "GetWindowsGeneralStep7")]
        public async Task<ActionResult<WindowsGeneral>> GetWindowsGeneralStep7(int id)
        {
            _logger.LogInformation($"GetWindowsGeneral called with parameter id = {id}");


            var windowsGen = _windowsGeneralRepository.FindByCondition((h => h.Id == id)).FirstOrDefault();

            if (windowsGen == null)
            {
                return BadRequest();
            }

            WindowsGeneral win = new WindowsGeneral
            {
                Id = id,
                SapSid = windowsGen.SapSid,
                SapClient = windowsGen.SapClient,
                ObjectName = windowsGen.ObjectName,
                OwnerId = windowsGen.OwnerId,
                Active = windowsGen.Active,
                MaxParallelTasks = windowsGen.MaxParallelTasks,
                Process = windowsGen.Process,
                PreProcess = windowsGen.PreProcess,
                PostProcess = windowsGen.PostProcess,
                RoutineJob = windowsGen.RoutineJob,
                ProcessName = windowsGen.ProcessName,
                Documentation = windowsGen.Documentation,
                Title = windowsGen.Title,
                Archive1 = windowsGen.Archive1,
                Archive2 = windowsGen.Archive2,
                InternalAccount = windowsGen.InternalAccount,
                Folder = windowsGen.Folder,
                Queue = windowsGen.Queue,
                Agent = windowsGen.Agent,
                Login = windowsGen.Login,
                WinLogin = windowsGen.WinLogin,
                WinServer = windowsGen.WinServer,
                NameSuffix = windowsGen.NameSuffix,
                VariableKey1 = windowsGen.VariableKey1,
                VariableKey2 = windowsGen.VariableKey2,
                VariableKey3 = windowsGen.VariableKey3,
                VariableKey4 = windowsGen.VariableKey4,
                VariableKey5 = windowsGen.VariableKey5,
                VariableValue1 = windowsGen.VariableValue1,
                VariableValue2 = windowsGen.VariableValue2,
                VariableValue3 = windowsGen.VariableValue3,
                VariableValue4 = windowsGen.VariableValue4,
                VariableValue5 = windowsGen.VariableValue5
            };

            return Ok(win);
        }

        // Function returns required Data ready for modification after all steps were finished.
        //Get https://localhost:7017/api/WindowsGeneral/DownloadWindowsGeneralCsvFile/{id}
        [HttpGet("DownloadWindowsGeneralCsvFile/{id:int}", Name = "DownloadWindowsGeneralCsvFile"), AllowAnonymous]
        public IActionResult DownloadCsvFile(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var windowsGeneral = _windowsGeneralRepository.FindByCondition((h => h.Id == id)).FirstOrDefault();
            if (windowsGeneral == null)
            {
                return NotFound();
            };


            string[] columnNames = new string[]
            {
                "Id","SapSid", "SapClient", "WinServer", "WinLogin", "ObjectName", "OwnerId", "Active", "NameSuffix", "MaxParallelTasks",
                "Process", "PreProcess", "PostProcess", "RoutineJob", "ProcessName", "Documentation", "Title",
                "Archive1", "Archive2", "InternalAccount",
                "Folder", "Queue", "Agent", "Login"
            };
            var wins = _windowsGeneralRepository.FindByCondition((h => h.Id == id));
            string csv = String.Empty;

            foreach (string columnName in columnNames)
            {
                csv += columnName + ',';
            }

            csv += "\r\n";

            foreach (var win in wins)
            {
                csv += '"' + win.Id.ToString().Replace(",", ";") + "\",";
                csv += '"' + win.SapSid.ToString().Replace(",", ";") + "\",";
                csv += '"' + win.SapClient.ToString().Replace(",", ";") + "\",";
                csv += '"' + win.WinServer.Replace(",", ";") + "\",";
                csv += '"' + win.WinLogin.Replace(",", ";") + "\",";
                csv += '"' + win.ObjectName.Replace(",", ";") + "\",";
                csv += '"' + win.OwnerId.ToString().Replace(",", ";") + "\",";
                csv += '"' + win.Active.ToString().Replace(",", ";") + "\",";
                csv += '"' + win.NameSuffix.Replace(",", ";") + "\",";
                csv += '"' + win.MaxParallelTasks.ToString().Replace(",", ";") + "\",";
                csv += '"' + win.Process.Replace(",", ";") + "\",";
                csv += '"' + win.PreProcess.Replace(",", ";") + "\",";
                csv += '"' + win.PostProcess.Replace(",", ";") + "\",";
                csv += '"' + win.RoutineJob.ToString().Replace(",", ";") + "\",";
                csv += '"' + win.ProcessName.Replace(",", ";") + "\",";
                csv += '"' + win.Documentation.Replace(",", ";") + "\",";
                csv += '"' + win.Title.Replace(",", ";") + "\",";
                csv += '"' + win.Archive1.Replace(",", ";") + "\",";
                csv += '"' + win.Archive2.Replace(",", ";") + "\",";
                csv += '"' + win.InternalAccount.Replace(",", ";") + "\",";
                csv += '"' + win.Folder.Replace(",", ";") + "\",";
                csv += '"' + win.Queue?.Replace(",", ";") + "\",";
                csv += '"' + win.Agent?.Replace(",", ";") + "\",";
                csv += '"' + win.Login?.Replace(",", ";") + "\",";

                csv += "\r\n";

            }

            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "text/csv", "win.csv");

        }

        // Function returns required Data ready for modification after all steps were finished.
        //Get https://localhost:7017/api/WindowsGeneral/JsonFile/{id}
        [HttpGet("DownloadWindowsGeneralJsonFile/{id:int}", Name = "DownloadWindowsGeneralJsonFile"), AllowAnonymous]
        public IActionResult DownloadJsonFile(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var windowsGeneral = _windowsGeneralRepository.FindByCondition((h => h.Id == id)).FirstOrDefault();
            if (windowsGeneral == null)
            {
                return NotFound();
            };

            WindowsGeneral winJson = new WindowsGeneral
            {
                Id = id,
                SapClient = windowsGeneral.SapClient,
                SapSid = windowsGeneral.SapSid,
                WinServer = windowsGeneral.WinServer,
                WinLogin = windowsGeneral.WinLogin,
                ObjectName = windowsGeneral.ObjectName,
                OwnerId = windowsGeneral.OwnerId,
                Active = windowsGeneral.Active,
                NameSuffix = windowsGeneral.NameSuffix,
                MaxParallelTasks = windowsGeneral.MaxParallelTasks,
                Process = windowsGeneral.Process,
                PreProcess = windowsGeneral.PreProcess,
                PostProcess = windowsGeneral.PostProcess,
                RoutineJob = windowsGeneral.RoutineJob,
                ProcessName = windowsGeneral.ProcessName,
                Documentation = windowsGeneral.Documentation,
                Title = windowsGeneral.Title,
                Archive1 = windowsGeneral.Archive1,
                Archive2 = windowsGeneral.Archive2,
                InternalAccount = windowsGeneral.InternalAccount,
                Folder = windowsGeneral.Folder,
                Queue = windowsGeneral.Queue,
                Agent = windowsGeneral.Agent,
                Login = windowsGeneral.Login
            };

            string fileName = "win.json";
            string jsonString = JsonSerializer.Serialize(windowsGeneral, new JsonSerializerOptions { WriteIndented = true });

            byte[] bytes = Encoding.ASCII.GetBytes(jsonString);
            return File(bytes, "application/json", "win.json");

        }


    }

}
