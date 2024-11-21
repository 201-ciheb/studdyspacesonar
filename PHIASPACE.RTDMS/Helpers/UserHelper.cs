using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PHIASPACE.RTDMS.DAL.Model;
using PHIASPACE.RTDMS.Utility;

namespace PHIASPACE.RTDMS.Helpers
{
    public class UserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var userClaims = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            return userClaims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string GetUserGrps()
        {
            var userClaims = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            return userClaims?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string GetUserEmail()
        {
            var emailClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            return emailClaim?.Value;
        }

        public int GetPersonId()
        {
            var person = GetPerson();
            return person?.Id ?? 0;
        }

        public AimsPerson GetPersonDetails(string email)
        {
            // Log all claims
            var claims = _httpContextAccessor.HttpContext?.User.Claims;

            // Attempt to find the person by email
            var person = new AimsPerson();//_dbContext.AimsPersons.FirstOrDefault(p => p.Email == email);

            if (person == null)
            {
                return new AimsPerson
                {
                    Id = 0,
                    Email = "unknown@example.com",
                    FirstName = "Unknown",
                    LastName = "Person"
                };
            }

            return person;
        }

        public bool UserInRole(string[] role)
        {
            var username = _httpContextAccessor.HttpContext?.User.Identity.Name;
            var roles =new List<dynamic>();// Utils.PersonRoles(username);
            return roles != null && role.Intersect(roles).Any();
        }

        public List<Claim> GetClaims(string email, string fullName)
        {
            var splits = fullName?.Split(',');
            if (splits?.Length == 2)
            {
                return GetClaims(email, splits[1].Trim(), splits[0].Trim(), fullName.Trim());
            }

            splits = fullName?.Split(' ');
            if (splits?.Length == 2)
            {
                return GetClaims(email, splits[0].Trim(), splits[1].Trim(), fullName.Trim());
            }

            return GetClaims(email, null, null, fullName);
        }

        public List<Claim> GetClaims(string email, string givenName, string surname, string fullName)
        {
            email = email?.Trim();
            givenName = givenName?.Trim();
            surname = surname?.Trim();
            fullName = fullName?.Trim();

            var claims = new List<Claim>();

            if (!string.IsNullOrWhiteSpace(email))
            {
                claims.Add(new Claim(ClaimTypes.Email, email));
                claims.Add(new Claim(ClaimTypes.Upn, email));
                claims.Add(new Claim(ClaimTypes.Name, email));
            }

            if (!string.IsNullOrWhiteSpace(surname))
            {
                claims.Add(new Claim(ClaimTypes.Surname, surname));
            }

            if (!string.IsNullOrWhiteSpace(givenName))
            {
                claims.Add(new Claim(ClaimTypes.GivenName, givenName));
            }

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                claims.Add(new Claim("name", fullName));
            }

            return claims;
        }

        public AimsPerson GetPerson()
        {
            return GetPerson(out _, out _);
        }

        public AimsPerson GetPerson(out ClaimsIdentity identity, out AimsPerson derivativePerson)
        {
            identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            return FindPerson(identity?.Claims?.ToList() ?? new List<Claim>(), out derivativePerson);
        }

        public AimsPerson GetPersonOrDerivative(out ClaimsIdentity identity)
        {
            var person = GetPerson(out identity, out var derivative);
            return person ?? derivative;
        }

        public AimsPerson FindPerson(List<Claim> claims, out AimsPerson derivativePerson)
        {
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var mobileNumber = claims.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone)?.Value;
            var otherPhone = claims.FirstOrDefault(c => c.Type == ClaimTypes.OtherPhone)?.Value;
            var identityName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var nameId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var givenName = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var surname = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
            var fullName = claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var objectId = claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            var userPrincipalName = _httpContextAccessor.HttpContext?.User.Identity.Name;

            var personQuery = new List<dynamic>();// _dbContext.AimsPersons.AsQueryable();
            AimsPerson person = null;

            derivativePerson = new AimsPerson
            {
                Email = email ?? userPrincipalName ?? identityName,
                FirstName = givenName,
                LastName = surname,
                Phone = mobileNumber ?? otherPhone,
                UserId = nameId ?? objectId
            };

            // Test email
            if (!string.IsNullOrEmpty(email))
            {
                person = personQuery.FirstOrDefault(e => e.Email.Trim().ToLower() == email.Trim().ToLower());
            }

            // Test mobile
            if (person == null && !string.IsNullOrEmpty(mobileNumber))
            {
                person = personQuery.FirstOrDefault(e => e.Phone.Trim() == mobileNumber.Trim());
            }

            // Test user_id
            if (person == null && !string.IsNullOrEmpty(nameId))
            {
                person = personQuery.FirstOrDefault(e => e.UserId == nameId);
            }

            // Test literal name (weaker test)
            if (person == null && !string.IsNullOrEmpty(givenName) && !string.IsNullOrEmpty(surname))
            {
                var lowerGivenName = givenName.ToLower();
                var lowerSurname = surname.ToLower();

                person = personQuery.FirstOrDefault(e => e.LastName.ToLower() == lowerSurname && (e.FirstName.ToLower() == lowerGivenName || e.OtherNames.ToLower() == lowerGivenName));
            }

            if (person != null)
            {
                // Ensure person data is up-to-date
                person.Email = email ?? person.Email;
                person.LastName = surname ?? person.LastName;
                person.FirstName = givenName ?? person.FirstName;
                person.UserId = nameId ?? person.UserId;
                person.Phone = mobileNumber ?? person.Phone;

                //_dbContext.SaveChanges();
            }

            return person;
        }
    }

    public class PermissionFilter : ActionFilterAttribute
    {
        public string[] Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContextAccessor = context.HttpContext.RequestServices.GetService<IHttpContextAccessor>();
            var username = httpContextAccessor.HttpContext?.User.Identity.Name;
            var roles = new{};// Utils.PersonRoles(username);
            // if (roles == null || !Role.Intersect(roles).Any())
            // {
            //     context.Result = new RedirectToActionResult("Index", "Home", null);
            // }

            base.OnActionExecuting(context);
        }
    }

    public class PermissionApiFilter : ActionFilterAttribute
    {
        public string[] Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContextAccessor = context.HttpContext.RequestServices.GetService<IHttpContextAccessor>();
            var username = httpContextAccessor.HttpContext?.User.Identity.Name;
            // var roles = Utils.PersonRoles(username);
            // if (roles == null || !Role.Intersect(roles).Any())
            // {
            //     context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
            // }

            base.OnActionExecuting(context);
        }
    }
}
