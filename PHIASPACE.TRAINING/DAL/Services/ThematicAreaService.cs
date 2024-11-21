using PHIASPACE.TRAINING.DAL.Model;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.IServices;
using Microsoft.EntityFrameworkCore;

namespace PHIASPACE.TRAINING.DAL.Services
{
    public class ThematicAreaService : IThematicAreaService
    {
        private readonly PhiaSpaceTrainingContext _context;

        public ThematicAreaService(PhiaSpaceTrainingContext context)
        {
            _context = context;
        }

        public TblThematicArea AddThematicArea(TblThematicArea thematicArea)
        {
            _context.Tbl_ThematicArea.Add(thematicArea);
            _context.SaveChanges();
            return thematicArea;
        }

        public TblThematicArea GetThematicArea(int id){
            return _context.Tbl_ThematicArea.First(m => m.Id == id);
        }

        public List<TblThematicArea> GetActiveThematicAreas(){
            return _context.Tbl_ThematicArea.Where(m => m.Deleted == 0).ToList();
        }

        public List<TblThematicArea> GetThematicAreas(){
            return _context.Tbl_ThematicArea.ToList();
        }

        public bool DeleteThematicArea(int id){
            try
            {
                var existing = _context.Tbl_ThematicArea.FirstOrDefault(m => m.Id == id);
                if(existing != null){
                    existing.Deleted = 1;
                    _context.Entry(existing).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public int UpdateThematicArea(TblThematicArea thematicArea){
            try
            {
                var existing = _context.Tbl_ThematicArea.FirstOrDefault(m => m.Id == thematicArea.Id);
                if(existing != null){
                    existing.UpdatedBy = thematicArea.UpdatedBy;
                    existing.DateUpdated = thematicArea.DateUpdated;
                    existing.Name = thematicArea.Name;
                    _context.Entry(existing).State = EntityState.Modified;
                    _context.SaveChanges();
                    return thematicArea.Id;
                }
                return 0;
            }
            catch (System.Exception)
            {
                return 0;
            }
        }
    }
}