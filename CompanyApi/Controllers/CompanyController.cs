﻿using Microsoft.AspNetCore.Mvc;

namespace CompanyApi.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private static List<Company> companies = new List<Company>();

        [HttpPost]
        public ActionResult<Company> Create(CreateCompanyRequest request)
        {
            if (companies.Exists(company => company.Name.Equals(request.Name)))
            {
                return BadRequest();
            }
            Company companyCreated = new Company(request.Name);
            companies.Add(companyCreated);
            return StatusCode(StatusCodes.Status201Created, companyCreated);
        }

        [HttpGet("{id}")]
        public ActionResult<Company> GetOne(string id)
        {
            var company = companies.FirstOrDefault(company => company.Id == id);

            return company == null ? StatusCode(StatusCodes.Status404NotFound) : StatusCode(StatusCodes.Status200OK, company);
        }

        [HttpGet]
        public ActionResult<List<Company>> GetAll()
        {
            return StatusCode(StatusCodes.Status200OK, companies);
        }

        [HttpPut("{id}")]
        public ActionResult Put(UpdateCompanyRequest company, string id)
        {
            var result = companies.FirstOrDefault(company => company.Id == id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            result.Name = company.Name;
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete]
        public void ClearData()
        { 
            companies.Clear();
        }
    }
}
