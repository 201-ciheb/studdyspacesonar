using PHIASPACE.LINKAGE.Models;

namespace PHIASPACE.LINKAGE.DAL.IService;

public interface ILinkageService {
    Task<linkage_to_care_view_model> GetParticipantLinkageDetails(string participantId);
}
