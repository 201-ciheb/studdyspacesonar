using Microsoft.AspNetCore.Mvc;

namespace PHIASPACE.RTDMS.Controllers;
public class ProcessMonitorController: RtdmsBaseController{
    public async Task<IActionResult> Index(){
        return View();
    }   
}