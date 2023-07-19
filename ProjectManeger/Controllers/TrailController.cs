using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProjectManeger.Context;
using ProjectManeger.DTOs;
using ProjectManeger.Models;
using Project = ProjectManeger.Models.Project;

namespace ProjectManeger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailController : ControllerBase
    {
            ProjectDBContext _context;

            public TrailController(ProjectDBContext projectDBContext)
            {
                this._context = projectDBContext;
            }

        [HttpPost]
        [Route("listproject")]

        public async Task<ActionResult<IEnumerable<Employee>>> listproject([FromBody] Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return Ok(project);
        }

        [HttpGet]
        [Route("addmanegerid")]

        public async Task<ActionResult<IEnumerable<Project>>> addmanegerid()
        {
            return Ok (_context.Projects.Include(p => p.Manager).ToList());
        }

        [HttpPost]
        [Route("emailpassword")]

        public async Task<ActionResult<IEnumerable<Employee>>> emailpassword( EmployeeDTOs EmployeeDTOs)
        {
            return Ok(_context.Employees.Where(e => e.Eamil == EmployeeDTOs.Email && e.Password == EmployeeDTOs.Password).ToList());
        }

        [HttpPost]
        [Route("projecttask")]

        public async Task<ActionResult<IEnumerable<Projecttask>>> projecttask(Projecttask projecttask)
        {
            _context.Projecttasks.Add(projecttask);
            _context.SaveChanges();
            return Ok(projecttask);
        }


        [HttpGet]
        [Route("projectandprojecttask")]

        public async Task<ActionResult<IEnumerable<Project>>> projectandprojecttask()
        {
            return Ok(_context.Projects.Include(p=> p.Projecttasks).ToList());
        }

       }
}
