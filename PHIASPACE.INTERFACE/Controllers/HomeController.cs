
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PHIASPACE.INTERFACE.DAL.IService;
using PHIASPACE.INTERFACE.DAL.Model.Custom;
using PHIASPACE.INTERFACE.DAL.Model.Data;
using PHIASPACE.INTERFACE.Models;
using PHIASPACE.INTERFACE.Model.Custom;
using PHIASPACE.INTERFACE.Utils;

namespace PHIASPACE.INTERFACE.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISatLabService _satLabService;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory,
        ISatLabService satLabService)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _satLabService = satLabService;
    }

    public async Task<IActionResult> Index()
    {
        var sat = await _satLabService.GetSattelliteProgressAsync();
        return View(sat);
    }

    public async Task<IActionResult> TeamPerformance()
    {
        return View();
    }

    public async Task<IActionResult> LabProgress()
    {
        return View();
    }

    public async Task<IActionResult> CountyProgress()
    {
        return View();
    }

    public async Task<IActionResult> PullData()
    {
        HttpUtils utils = new HttpUtils(_httpClientFactory, _configuration, _satLabService);
        await utils.GetDataFromKoboCollectAPI("1454953");
        return View();
    }
    public async Task<IActionResult> GeographicStats() {
        var model = await _satLabService.GetTargetLabsAsync();
        return View(model);
       
    }
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Reports()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<List<TeamPerformance>> GetTeamPerformance(){
        var teamPerformance = await _satLabService.GetTeamPerformancesAsync();
        return teamPerformance;
    }

    public async Task<ProgressCount> GetCountyPerformance(){
        var sat = await _satLabService.GetSattelliteProgressAsync();
        return sat;
    }

    public async Task<List<CountyProgress>> GetCountyProgress(){
        var county = await _satLabService.GetCountyProgressesAsync();
        return county;
    }
    
    public async Task<List<GeoProgress>> GetLabsGeoInfo()
    {
        //var sat = await _satLabService.GetTargetLabsAsync();
        var sat = await _satLabService.GetSatLabGeoInfo();
        return sat;
    }
        
    public async Task<List<LabScore>> GetLabsScores()
    {
        var sat = await _satLabService.GetLabScoreAsync();
        return sat;
    }
}

