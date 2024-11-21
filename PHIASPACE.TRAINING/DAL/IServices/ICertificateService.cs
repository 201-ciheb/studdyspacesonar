using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.IServices
{
    public interface ICertificateService
    {
        int certifyPersons(List<TblCertificate> graduants);
        IQueryable<TblCertificate> getExpiredCerts();
        IQueryable<TblCertificate> getAllCertificates();
        IQueryable<TblCertificate> getCertificatesByAdminArea(String status, int? provinceid, int? districtid, int? facilityid);
        IQueryable<TblCertificate> getCertificatesCategory(int certId);
        IQueryable<TblCertificate> getCertificates(int certId, int fid, int did, int pid);
        IQueryable<TblCertificate> searchCertificate(string key, DateTime? From, DateTime? To);
        Object getCertificateSummary();
        Object getCertificateSummary_National();
        TblCertificate getCertificateById(int id);
    }
}
