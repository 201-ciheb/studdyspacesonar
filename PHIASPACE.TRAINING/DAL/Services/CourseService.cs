using PHIASPACE.TRAINING.DAL.Model;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.IServices;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PHIASPACE.TRAINING.DAL.Services
{
    public class CourseService : ICourseService
    {
        private readonly PhiaSpaceTrainingContext _context;

        public CourseService(PhiaSpaceTrainingContext context)
        {
            _context = context;
        }

        public TblCourse AddCourse(TblCourse course)
        {
            _context.Add(course);
            _context.SaveChanges();
            return course;
        }

        public TblCourse GetCourse(int id)
        {
            var model = _context.Tbl_Course.FirstOrDefault(m => m.CourseId == id);
            return model;
        }

        public async Task<List<TblCourse>> GetCourses()
        {
            return await _context.Tbl_Course.ToListAsync();
        }

        public async Task<List<TblCourse>> GetActiveCourses()
        {
            return await _context.Tbl_Course.Where(m => m.Deleted != 1).ToListAsync();
        }
        
        public async Task<List<TblCourse>> GetActiveCoursesByThematicArea(List<int?> thematic_areas)
        {
            var list = await _context.Tbl_Course.Where(m => thematic_areas.Contains(m.ThematicAreaId)).ToListAsync();
            return list;
        }

        public void RemoveCourse(TblCourse course)
        {
            throw new NotImplementedException();
        }

        public IList<TblCourse> SearchCourses(string term, int courseid)
        {
            throw new NotImplementedException();
        }

        public int UpdateCourse(TblCourse course)
        {
            try
            {
                var existing = _context.Tbl_Course.FirstOrDefault(m => m.CourseId == course.CourseId);
                if(existing != null){
                    course.CreatedBy = existing.CreatedBy;
                    course.DateCreated = existing.DateCreated;
                    _context.Entry(existing).CurrentValues.SetValues(course);
                    _context.Entry(existing).State = EntityState.Modified;
                    _context.SaveChanges();
                    return course.CourseId;
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
