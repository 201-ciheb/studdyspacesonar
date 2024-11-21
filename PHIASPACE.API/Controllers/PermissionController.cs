using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PHIASPACE.API.IService;

namespace PHIASPACE.API.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize(Policy = "ApiScope")]
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet(Name = "GetPermission")]
    public async Task<IActionResult> GetPermission()
    {
        var response = await _permissionService.RequestIdentityTokenAsync();
        return Ok(new { Permission = "None", Response = response });
    }
}