using System;
using PHIASPACE.RTDMS.DAL.IService;

namespace PHIASPACE.RTDMS.DAL.Service
{

    public class CAPIService : ICAPIService
    {


        // public IQueryable<CAPI_AHSECOVER> GetHouseholds()
        // {
        //     return _entity.CAPI_AHSECOVER.AsQueryable();
        // }


        // public CAPI_AHSECOVER GetHousehold(int ea_id, int hh_id)
        // {
        //     return _entity.CAPI_AHSECOVER.FirstOrDefault(e => e.A1QCLUST == ea_id && e.A1QNUMBER == hh_id);
        // }

        // public IQueryable<CAPI_IACOVER> GetIndividuals()
        // {
        //     return _entity.CAPI_IACOVER.AsQueryable();
        // }

        // public CAPI_IACOVER GetIndividual(int ea_id, int hh_id, int line_id)
        // {
        //     return _entity.CAPI_IACOVER.FirstOrDefault(e => e.IACLUSTER == ea_id && e.IANUMBER == hh_id && e.IALINE == line_id);

        // }

        // public IQueryable<CAPI_A1SEC01X> GetRoaster(int ea_id, int hh_id)
        // {
        //     return _entity.CAPI_A1SEC01X.Where(e => e.A1QCLUST == ea_id && e.A1QNUMBER == hh_id);

        // }

        // public void MarkCompleted(int ea_id, int hh_id, string user)
        // {
        //     var monitor = _entity.AimsHhMonitors.FirstOrDefault(e => e.EaId == ea_id && e.HhId == hh_id);
        //     if (monitor != null)
        //     {
        //         var old = monitor;
        //         monitor.Status = 15;
        //         monitor.UpdatedAt = DateTime.Now;
        //         _entity.Entry(monitor).CurrentValues.SetValues(monitor);
        //         _entity.SaveChanges();
        //     }
        //     else
        //     {
        //         _entity.AimsHhMonitors.Add(new AimsHhMonitor { Status = 15, EaId = ea_id, HhId = hh_id, UpdatedAt = DateTime.Now, Username = user });
        //         _entity.SaveChanges();
        //     }
        // }
    }
}

