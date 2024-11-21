using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PHIASPACE.LINKAGE.Controllers;

[Authorize]
public class NotificationController : Controller {
    public async Task<IActionResult> ParticipantsReffered(){
        return View();
    }   
}
