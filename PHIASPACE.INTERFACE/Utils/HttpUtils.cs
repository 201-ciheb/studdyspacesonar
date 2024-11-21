using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using PHIASPACE.INTERFACE.DAL.IService;
using PHIASPACE.INTERFACE.Models;

namespace PHIASPACE.INTERFACE.Utils;

public class HttpUtils{
    private readonly HttpClient _koboDataServiceClient;
    private readonly IConfiguration _configuration;    
    private readonly ISatLabService _satLabService;

    public HttpUtils(IHttpClientFactory httpClientFactory, IConfiguration configuration, ISatLabService satLabService)
    {
        _koboDataServiceClient = httpClientFactory.CreateClient("KoboDataService");
        _configuration = configuration;
        _satLabService = satLabService;
    }

    public async Task GetDataFromKoboCollectAPI(string formId)
    {
        try
        {
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_configuration["API:KoboDataService:Username"]}:{_configuration["API:KoboDataService:Password"]}"));
            _koboDataServiceClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            //_koboDataServiceClient.DefaultRequestHeaders.Add("Authorization", "Token YOUR_API_TOKEN");

            var apiUrl = $"/api/v1/data/{formId}";
            var response = await _koboDataServiceClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                await _satLabService.DeleteScoreDetails();
                await _satLabService.DeleteSpecialScore();
                var content = await response.Content.ReadAsStringAsync();
                var koboObj = JsonConvert.DeserializeObject<List<SatLabApiResponse>>(content);
                if(koboObj != null && koboObj.Count() > 0){
                    var mapper = new PhiaSpaceMapper();
                    //calculate the score
                    var scoreMapping = await _satLabService.GetScoreMappingAsync();
                    foreach (var item in koboObj)
                    {
                        var sc = item.CalculateScore(scoreMapping);
                        item.TotalScore = sc.TotalScore;
                        
                        await _satLabService.SaveScoreDetails(sc.Details, item._id, item.Page1kmfl);

                        List<string> specificPages = new List<string>
                        {
                            "Page1c/accessibility",
                            "Page2/room",
                            "Page2/generator",
                            "Page3/geneXpert",
                            "Page3a/func_pima",
                            "Page5/add_room",
                            "Page6/dispose_sharps",
                            "Page6/dispose_solids",
                            "Page9/mobile_network",
                            "Page9/network4G"
                        };

                        var bs = item.GetVariableResult(specificPages);
                        await _satLabService.SaveSpecialScoreDetails(bs, item._id, item.Page1kmfl);
                        //Console.WriteLine(item.Page1kmfl);
                    }

                    var satObj = mapper.MapSatLabObj(koboObj);  
                    var dataChunks = satObj.SplitList(1000);
                    foreach (var item in dataChunks)
                    {
                        await _satLabService.SaveSatLabRecordSp(item);
                    }  
                }
            }
            else
            {
                throw new HttpRequestException($"Failed to retrieve data. Status Code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving data from Kobo API.", ex);
        }
    }
}