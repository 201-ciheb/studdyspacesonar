using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.IServices
{
    public interface IThematicAreaService
    {
        TblThematicArea AddThematicArea(TblThematicArea thematicArea);
        TblThematicArea GetThematicArea(int id);
        List<TblThematicArea> GetActiveThematicAreas();
        List<TblThematicArea> GetThematicAreas();
        int UpdateThematicArea(TblThematicArea thematicArea);
        bool DeleteThematicArea(int id);
    }
}