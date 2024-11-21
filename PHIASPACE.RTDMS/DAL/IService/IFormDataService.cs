using System.Data;
using System.Threading.Tasks;

namespace PHIASPACE.RTDMS.DAL.IService
{
    public interface IFormDataService
    {
        Task<DataSet> GetFormDataAsync(int? formId, int? clusterNo, int? hh, int? line);
    }
}


