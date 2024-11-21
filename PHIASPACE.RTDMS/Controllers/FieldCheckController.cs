using Microsoft.AspNetCore.Mvc;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.Model;
using PHIASPACE.RTDMS.Models;

namespace PHIASPACE.RTDMS.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FieldCheckController : Controller
{
    private readonly IFieldCheckService _fieldCheckService;

    public FieldCheckController(IFieldCheckService fieldCheckService)
    {
        _fieldCheckService = fieldCheckService;
    }

    [HttpPost("post-field-check-table", Name = "Get Retention Data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PostFieldCheck(FieldCheckPayload payload)
    {
        try
        {
            await _fieldCheckService.SaveFieldCheckData(new FieldCheck
            {
                TableName = payload.TableName,
                TableId = payload.TableID,
                TableDetail = payload.TableDetail,
                TableHeader = payload.TableHeader,
                Graph = payload.Graph,
                TableJson = payload.TableJson,
            });
            return Ok(new { message = "Field check data received successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("homerural")]
    public IActionResult HomeRural()
    {
        return View("HomeRural");
    }

    [HttpGet("homeurban")]
    public IActionResult HomeUrban()
    {
        return View("HomeUrban");
    }

    [HttpGet("ageheaping")]
    public IActionResult AgeHeaping()
    {
        return View("AgeHeaping");
    }

    [HttpGet("eligiblewomen")]
    public IActionResult EligibleWomen()
    {
        return View("EligibleWomen");
    }

    [HttpGet("eligiblechildren")]
    public IActionResult EligibleChildren()
    {
        return View("EligibleChildren");
    }

    [HttpGet("eligiblemen")]
    public IActionResult EligibleMen()
    {
        return View("EligibleMen");
    }

    [HttpGet("agedisplacementmen")]
    public IActionResult AgeDisplacementMen()
    {
        return View("AgeDisplacementMen");
    }

    [HttpGet("agedisplacementwomen")]
    public IActionResult AgeDisplacementWomen()
    {
        return View("AgeDisplacementWomen");
    }

    [HttpGet("eligiblewomenraterural")]
    public IActionResult EligibleWomenRateRural()
    {
        return View("EligibleWomenRateRural");
    }

    [HttpGet("eligiblewomenrateurban")]
    public IActionResult EligibleWomenRateUrban()
    {
        return View("EligibleWomenRateUrban");
    }

    [HttpGet("eligiblechildrencompletion")]
    public IActionResult EligibleChildrenCompletion()
    {
        return View("EligibleChildrenCompletion");
    }

    [HttpGet("eligiblemenraterural")]
    public IActionResult EligibleMenRateRural()
    {
        return View("EligibleMenRateRural");
    }

    [HttpGet("eligiblemenrateurban")]
    public IActionResult EligibleMenRateUrban()
    {
        return View("EligibleMenRateUrban");
    }

    [HttpGet("hivbloodmen")]
    public IActionResult HivBloodMen()
    {
        return View("HivBloodMen");
    }

    [HttpGet("hivbloodwomen")]
    public IActionResult HivBloodWomen()
    {
        return View("HivBloodWomen");
    }

    [HttpGet("hivbloodchildren")]
    public IActionResult HivBloodChildren()
    {
        return View("HivBloodChildren");
    }

    [HttpGet("variablemissingdata")]
    public IActionResult VariableMissingData()
    {
        return View("VariableMissingData");
    }

    [HttpGet("lengthinterviews")]
    public IActionResult LengthInterviews()
    {
        return View("LengthInterviews");
    }
}
