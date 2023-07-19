using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using ProjectManeger.Context;
using ProjectManeger.DTOs;
using ProjectManeger.Models;

namespace ProjectManeger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        ProjectDBContext _context;

        public AuthenticationController(ProjectDBContext projectDBContext)
        {
          this._context = projectDBContext;
        }

       
        [HttpPost]
        [Route("login")]

        public async Task<ActionResult<IEnumerable<Employee>>> Login([FromBody] EmployeeDTOs employee)
        {
            IList<Employee> employees = _context.Employees.Where(e=>e.Eamil == employee.Email
            && e.Password == employee.Password).ToList();
            return Ok(employees);
        }

    }
}
