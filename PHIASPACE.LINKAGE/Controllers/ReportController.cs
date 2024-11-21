using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PHIASPACE.LINKAGE.Controllers;

[Authorize]
public class ReportController : Controller {
    public async Task<IActionResult> ParticipantsLinked(){
        return View();
    }

    public async Task<IActionResult> TempId(){
        return View();
    }
}
