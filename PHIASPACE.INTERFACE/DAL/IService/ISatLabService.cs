using PHIASPACE.INTERFACE.DAL.Model.Custom;
using PHIASPACE.INTERFACE.DAL.Model.Data;
using PHIASPACE.INTERFACE.Model.Custom;

namespace PHIASPACE.INTERFACE.DAL.IService;

public interface ISatLabService {
    Task SaveSatLabRecord(List<SatLabAssessment> assessments);
    Task SaveSatLabRecordSp(List<SatLabAssessment> assessments);
    Task<ProgressCount> GetSattelliteProgressAsync();
    Task<List<TargetLabs>> GetTargetLabsAsync();
    Task<List<TeamPerformance>> GetTeamPerformancesAsync();
    Task<List<CountyProgress>> GetCountyProgressesAsync();
    Task<List<SatLabAssessment>> GetSatLabRecordSpAsync();
    //Task<List<TargetLabs>> GetSatLabGeoInfo();
    Task<List<GeoProgress>> GetSatLabGeoInfo();
    Task<List<LabScore>> GetLabScoreAsync();
    Task<List<ScoreMap>> GetScoreMappingAsync();
    Task SaveScoreDetails(Dictionary<string,double> scores, int _id, string kmfl);
    Task DeleteScoreDetails();
    Task SaveSpecialScoreDetails(Dictionary<string,string> scores, int _id, string kmfl);
    Task DeleteSpecialScore();
}