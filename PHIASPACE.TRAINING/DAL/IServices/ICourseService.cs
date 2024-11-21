using PHIASPACE.TRAINING.DAL.Model.Db;

namespace PHIASPACE.TRAINING.DAL.IServices
{
    public interface ICourseService
    {
        TblCourse AddCourse(TblCourse course);
        TblCourse GetCourse(int id);
        Task<List<TblCourse>> GetCourses();
        Task<List<TblCourse>> GetActiveCourses();
        Task<List<TblCourse>> GetActiveCoursesByThematicArea(List<int?> thematic_areas);
        IList<TblCourse> SearchCourses(string term, int courseid);
        int UpdateCourse(TblCourse course);
        void RemoveCourse(TblCourse course);
    }
}
