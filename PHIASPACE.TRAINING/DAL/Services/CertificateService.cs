using PHIASPACE.TRAINING.DAL.Model;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.IServices;

namespace PHIASPACE.TRAINING.DAL.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly PhiaSpaceTrainingContext _context;

        public CertificateService(PhiaSpaceTrainingContext context)
        {
            _context = context;
        }

        public int certifyPersons(List<TblCertificate> graduants)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblCertificate> getAllCertificates()
        {
            throw new NotImplementedException();
        }

        public TblCertificate getCertificateById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblCertificate> getCertificates(int certId, int fid, int did, int pid)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblCertificate> getCertificatesByAdminArea(string status, int? provinceid, int? districtid, int? facilityid)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblCertificate> getCertificatesCategory(int certId)
        {
            throw new NotImplementedException();
        }

        public object getCertificateSummary()
        {
            throw new NotImplementedException();
        }

        public object getCertificateSummary_National()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblCertificate> getExpiredCerts()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TblCertificate> searchCertificate(string key, DateTime? From, DateTime? To)
        {
            throw new NotImplementedException();
        }
    }
}
