using System;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.MssqlDbService;

namespace PHIASPACE.RTDMS.DAL.Service
{
    public class PersonService : IPersonService
    {
        private readonly AimsMssqlDbContext _entity;

        public PersonService(AimsMssqlDbContext entity)
        {
            _entity = entity;
        }


        // public int AddPerson(AimsPerson person)
        // {
        //     person.CreatedAt = DateTime.Now;
        //     _entity.AimsPersons.Add(person);
        //     _entity.SaveChanges();
        //     return person.Id;
        // }

        // public AimsPerson GetPerson(int id)
        // {
        //     return _entity.AimsPersons.FirstOrDefault(e => e.Id == id);
        // }

        // public IQueryable<AimsPerson> GetPersonByOrganization(int organization_id)
        // {
        //     return _entity.AimsPersons.Where(e => e.Organization == organization_id);
        // }

        // public IQueryable<AimsPerson> GetPersons()
        // {
        //     return _entity.AimsPersons.AsQueryable();
        // }

        // public void AddRole(AimsPersonRole person_Role)
        // {
        //     _entity.AimsPersonRoles.Add(person_Role);
        //     _entity.SaveChanges();
        // }

        // public void RemoveRole(int person_id, string role)
        // {
        //     var role_id = _entity.AimsRoles.FirstOrDefault(e => e.Code == role).Id;
        //     var person_role = _entity.AimsPersonRoles.FirstOrDefault(e => e.PersonId == person_id && e.RoleId == role_id);
        //     if (person_role != null)
        //     {
        //         _entity.AimsPersonRoles.Remove(person_role);
        //         _entity.SaveChanges();
        //     }
        // }

        // public void Update(AimsPerson person)
        // {
        //     var old_person = _entity.AimsPersons.FirstOrDefault(e => e.Id == person.Id);
        //     _entity.Entry(old_person).CurrentValues.SetValues(person);
        //     _entity.SaveChanges();
        // }

        public AimsMssqlDbContext GetContext()
        {
            return _entity;
        }

        // public int Assessment(AimsAssessment assess)
        // {

        //     _entity.AimsAssessments.Add(assess);
        //     _entity.SaveChanges();

        //     return assess.Id;
        // }
        // public IQueryable<AimsAssessment> GetAssessment()
        // {
        //     return _entity.AimsAssessments.AsQueryable();
        // }

        // public int? GetSupIdByPerson(int? person)
        // {
        //     try
        //     {
        //         var s = _entity.AimsSupervisors.FirstOrDefault(e => e.PersonId == person).SupId;

        //         return s;
        //     }
        //     catch (Exception)
        //     {
        //         return null;
        //     }
        // }
        // public IQueryable<AimsAssessment> GetSupervisees()
        // {
        //     // Assuming you want to return assessments related to supervisees
        //     return _entity.AimsAssessments.AsQueryable();
        // }

        public string GetPersonEmail(int id)
        {
            try
            {
                return _entity.AimsPersons.FirstOrDefault(x => x.Id == id).Email;

            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}

