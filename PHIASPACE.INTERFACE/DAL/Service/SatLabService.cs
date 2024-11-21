using Microsoft.EntityFrameworkCore;
using PHIASPACE.INTERFACE.DAL.IService;
using PHIASPACE.INTERFACE.DAL.Model.Custom;
using PHIASPACE.INTERFACE.DAL.Model.Data;
using PHIASPACE.INTERFACE.Model.Custom;
using PHIASPACE.INTERFACE.Utils;

namespace PHIASPACE.INTERFACE.DAL.Service;

public class SatLabService : ISatLabService
{
    private readonly InterfaceDbContext _context;
    public SatLabService(InterfaceDbContext context)
    {
        _context = context;
    }

    public async Task SaveSatLabRecord(List<SatLabAssessment> assessments)
    {
        _context.SatLabAssessments.AddRange(assessments);
        await _context.SaveChangesAsync();
    }

    public async Task SaveSatLabRecordSp(List<SatLabAssessment> assessments)
    {
        await DbUtil.DoBulkInsertUpdate(assessments, "[dbo].[SpBulkInsertSatLabAssessment]", _context.Database.GetConnectionString());
    }

    public async Task<List<SatLabAssessment>> GetSatLabRecordSpAsync()
    {
        var sqlQuery = "[GetSatelliteAssessments]";
        var satLabs = await DbUtil.ExecuteSpAsync<SatLabAssessment>(sqlQuery, _context.Database.GetConnectionString());

        return satLabs;
    }

    public async Task<ProgressCount> GetSattelliteProgressAsync()
    {
        var sqlQuery = "[report].[GetSatelliteAssessmentProgress]";
        var satLabAss = await DbUtil.ExecuteSingleSpAsync<ProgressCount>(sqlQuery, _context.Database.GetConnectionString());
        return satLabAss;
    }

    public async Task<List<TargetLabs>> GetTargetLabsAsync()
    {
        var sqlQuery = "[GetTargetLabs]";
        var satLabs = await DbUtil.ExecuteSpAsync<TargetLabs>(sqlQuery, _context.Database.GetConnectionString());

        return satLabs;
    }

    public async Task<List<TeamPerformance>> GetTeamPerformancesAsync()
    {
        var sqlQuery = "[report].[GetTeamPerformance]";
        var teamPerformance = await DbUtil.ExecuteSpAsync<TeamPerformance>(sqlQuery, _context.Database.GetConnectionString());
        return teamPerformance;
    }

    public async Task<List<CountyProgress>> GetCountyProgressesAsync(){
        var sqlQuery = "[report].[GetCountyProgress]";
        var county = await DbUtil.ExecuteSpAsync<CountyProgress>(sqlQuery, _context.Database.GetConnectionString());
        return county;
    }

    public async Task<List<GeoProgress>> GetSatLabGeoInfo() {
        var sqlQuery = "[report].[GetGeoProgress]";
        var county = await DbUtil.ExecuteSpAsync<GeoProgress>(sqlQuery, _context.Database.GetConnectionString());
        return county;
        // var target_labs = await GetTargetLabsAsync();
        // var visited_labs = await GetSatLabRecordSpAsync();

        // var kmfls = visited_labs.Select(x => x.Kmfl).ToList();

        // foreach (var lab in target_labs) {
        //     if (kmfls.Contains(lab.kmfl))
        //     {
        //         lab.AssessmentStatus = 1;
        //     }
        //     else {
        //         lab.AssessmentStatus = 0;
        //     }
        // }

        // return target_labs;
    }

    public async Task<List<LabScore>> GetLabScoreAsync(){
        var sqlQuery = "[report].[GetLabAssessmentScore]";
        var scores = await DbUtil.ExecuteSpAsync<LabScore>(sqlQuery, _context.Database.GetConnectionString());
        return scores;
    }

    public async Task<List<ScoreMap>> GetScoreMappingAsync(){
        var sqlQuery = "[dbo].[GetScoreMapping]";        
        var scores = await DbUtil.ExecuteSpAsync<ScoreMap>(sqlQuery, _context.Database.GetConnectionString());
        return scores;
    }

    public async Task SaveScoreDetails(Dictionary<string,double> scores, int _id, string kmfl){
        string columns = string.Join(", ", scores.Keys) + ",formId, kmfl";
        string values = string.Join(", ", scores.Values) + "," + _id + ", '" + kmfl + "'";
        string sqlCommand = $"INSERT INTO ScoringDetails ({columns}) VALUES ({values})";
        await DbUtil.ExecuteSingleQueryAsync(sqlCommand, _context.Database.GetConnectionString());
    }

    public async Task DeleteScoreDetails(){
        string sqlCommand = $"TRUNCATE Table ScoringDetails";
        await DbUtil.ExecuteSingleQueryAsync(sqlCommand, _context.Database.GetConnectionString());
    }

    public async Task SaveSpecialScoreDetails(Dictionary<string,string> scores, int _id, string kmfl){
        string columns = string.Join(", ", scores.Keys) + ",formId, kmfl";
        string values = "'" +  string.Join("', '", scores.Values) + "'," + _id + ", '" + kmfl + "'";
        string sqlCommand = $"INSERT INTO ScoringSpecific ({columns}) VALUES ({values})";
        await DbUtil.ExecuteSingleQueryAsync(sqlCommand, _context.Database.GetConnectionString());
    }

    public async Task DeleteSpecialScore(){
        string sqlCommand = $"TRUNCATE Table ScoringSpecific";
        await DbUtil.ExecuteSingleQueryAsync(sqlCommand, _context.Database.GetConnectionString());
    }
    
}