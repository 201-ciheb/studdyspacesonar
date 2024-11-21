using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PHIASPACE.RTDMS.DAL.IService;
using PHIASPACE.RTDMS.DAL.Model;

namespace PHIASPACE.RTDMS.Controllers
{
    [Route("api/[controller]")]
    public class PersonApiController : Controller
    {
        IPersonService _personService;
        public PersonApiController(IPersonService personService)
        {
            _personService = personService;
        }

        // [HttpGet("getsearch/{id}")]
        // public List<SearchResult> GetSearch(string id)
        // {
        //     var result = new List<SearchResult>();
        //     var persons = _personService.GetPersons().Where(e => e.FirstName.Contains(id) || e.LastName.Contains(id) || e.OtherNames.Contains(id) || e.Phone.Contains(id) || e.Email.Contains(id));
        //     foreach (var x in persons)
        //     {
        //         result.Add(new SearchResult { id = x.Id.ToString(), name = $"{x.LastName} {x.FirstName} - {x.Email}" });
        //     }

        //     return result;
        // }

        [HttpPost]
        [AllowAnonymous]
        public int login(string email, string password)
        {
            // var person = _personService.GetPersons().FirstOrDefault(e => e.Phone + "@naiis.ng" == email || e.Phone + "@naiis.ng" == "0" + email || e.Email == email);
            var person = new AimsPerson();
            return person.Id;
        }

    }
}

