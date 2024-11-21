using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PHIASPACE.LINKAGE.DAL.IService;
using PHIASPACE.LINKAGE.Models;

namespace PHIASPACE.LINKAGE.Controllers;

[Authorize]
public class LinkageController : Controller{
    private readonly ILinkageService _linkageService;

    public LinkageController(ILinkageService linkageService){
        _linkageService = linkageService;
    }

    [HttpGet]
    public async Task<IActionResult> Search(string TemporalId = null, int IsEnrolled = 0, string NupiCccNumber = "",
        int Enrolled = 0, string Reason = null)
    {
        if(!string.IsNullOrEmpty(TemporalId)){
            // TODO: store the search parameters and then store the found participant details
            // if participant is not found, update the search parameter table with the information
            var participant_linkage_detail = await _linkageService.GetParticipantLinkageDetails(TemporalId);
            if(participant_linkage_detail != null && !string.IsNullOrEmpty(participant_linkage_detail.lptid)){
                return View("LinkageDetails", participant_linkage_detail);
            }
            else{
                TempData["Message"] = new UIMessage{ Text = "Participant's PtID or Temporal ID supplied is Invalid or Not found!", SuccessValue = 0 };
                return View();
            }
        }
        return View();
    }
}
