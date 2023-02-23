using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Controllers
{
    public class SettingsController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            Settings settings = new Settings();
            if (User.Claims.Where(w => w.Type == Settings.claimDatabaseType).First() != null)
                settings.DatabaseType = (EDatabaseType)int.Parse(User.Claims.First(x => x.Type == Settings.claimDatabaseType).Value);

            settings.DatabaseType = settings.getDefaultType;

            return View(settings);
        }
    }
}
