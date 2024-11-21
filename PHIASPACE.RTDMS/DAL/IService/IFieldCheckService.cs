using PHIASPACE.RTDMS.DAL.Model;

namespace PHIASPACE.RTDMS.DAL.IService;

public interface IFieldCheckService{
    Task SaveFieldCheckData(FieldCheck fieldCheck);
}
