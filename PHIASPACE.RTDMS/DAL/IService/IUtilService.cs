using PHIASPACE.RTDMS.DAL.Model.Custom;

namespace PHIASPACE.RTDMS.DAL.IService;

public interface IUtilService{
    Task<List<DuplicatePtId>> ListDuplicatePtidsEA(int ea_id);
}
