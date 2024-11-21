using PHIASPACE.LINKAGE.DAL.IService;
using PHIASPACE.LINKAGE.Models;

namespace PHIASPACE.LINKAGE.DAL.Service;

public class LinkageService: ILinkageService {
    private readonly IDbProvider _dbProvider;

    public LinkageService(IDbProvider dbProvider) {
        _dbProvider = dbProvider;
    }
    
    public async Task<linkage_to_care_view_model> GetParticipantLinkageDetails(string participantId){
        var sqlQuery = $"Exec [linkage].[sp_get_participant_linkage_information] @ParticipantId='{participantId}'";
        var linkage_to_care_detail = await _dbProvider.ExecuteSingleQueryAsync<linkage_to_care_view_model>(sqlQuery, _dbProvider.GetConnectionString());
        return linkage_to_care_detail;
    }
}
