using AutomicObjectDesignerBack.Data;
using Microsoft.AspNetCore.Mvc;

namespace AutomicObjectDesignerBack.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly AppDatabaseContext _appDatabaseContext;

        public HomeController(AppDatabaseContext appDatabaseContext)
        {
            _appDatabaseContext = appDatabaseContext;
        }

    }
}
