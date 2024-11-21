using Microsoft.AspNetCore.Mvc;
using PHIASPACE.TRAINING.DAL.Model.Db;
using PHIASPACE.TRAINING.DAL.IServices;
using Microsoft.AspNetCore.Authorization;

namespace PHIASPACE.TRAINING.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private readonly IPersonService _prservice;

        public PersonController(IPersonService prservice) {
            _prservice = prservice;
        }

        public IActionResult Index()
        {
            var persons = _prservice.GetAllPersons();
            return View(persons);
        }
        public IEnumerable<TblPersons> Search(string? term)
        {
            var persons = _prservice.SearchPerson(term);
            return persons;
        }
    }
}
