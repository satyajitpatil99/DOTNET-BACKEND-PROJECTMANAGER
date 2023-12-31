﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManeger.Context;
using ProjectManeger.Models;

namespace ProjectManeger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjecttasksController : ControllerBase
    {
        private readonly ProjectDBContext _context;

        public ProjecttasksController(ProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Projecttasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projecttask>>> GetProjecttasks()
        {
            if (_context.Projecttasks == null)
            {
                return NotFound();
            }
            return await _context.Projecttasks.ToListAsync();
        }

        // GET: api/Projecttasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projecttask>> GetProjecttask(int id)
        {
            if (_context.Projecttasks == null)
            {
                return NotFound();
            }
            var projecttask = await _context.Projecttasks.FindAsync(id);

            if (projecttask == null)
            {
                return NotFound();
            }

            return projecttask;
        }

        // PUT: api/Projecttasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjecttask(int id, Projecttask projecttask)
        {
            if (id != projecttask.Id)
            {
                return BadRequest();
            }

            _context.Entry(projecttask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjecttaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(projecttask);
        }

        // POST: api/Projecttasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Projecttask>> PostProjecttask(Projecttask projecttask)
        {
            if (_context.Projecttasks == null)
            {
                return Problem("Entity set 'ProjectDBContext.Projecttasks'  is null.");
            }
            _context.Projecttasks.Add(projecttask);
            await _context.SaveChangesAsync();

            return Ok(projecttask);
        }

        // DELETE: api/Projecttasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjecttask(int id)
        {
            if (_context.Projecttasks == null)
            {
                return NotFound();
            }
            var projecttask = await _context.Projecttasks.FindAsync(id);
            if (projecttask == null)
            {
                return NotFound();
            }

            _context.Projecttasks.Remove(projecttask);
            await _context.SaveChangesAsync();

            return Ok(projecttask);
        }

        [HttpGet]
        [Route("listtasks/{id}")]
        
        public async Task<ActionResult<IEnumerable<Projecttask>>> ListTask([FromRoute]int id)
        {
            return Ok(_context.Projecttasks.Where(pt=> pt.Moduleid == id));
        }

        [HttpGet]
        [Route("mytasks/{employeid}")]

        public async Task <ActionResult<IEnumerable<Projecttask>>> MyTask([FromRoute]int employeid)
        {
            return Ok(_context.Projecttasks.Include(pt => pt.Project).Include(pt => pt.Module).Where(m => m.Employeid == employeid).ToList());
        }

        [HttpPut]
        [Route("updatestatus/{id}/{status}")]
        public async Task<ActionResult<IEnumerable<Projecttask>>> UpdateStatus([FromRoute] int id, [FromRoute] string status)
        {
            Projecttask projecttask = await _context.Projecttasks.FindAsync(id);
            if (projecttask != null)
            {
                projecttask.Status = status;
                _context.Entry(projecttask).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok(projecttask);
        }
        [HttpGet]
        [Route("reportlist")]

        public async Task<ActionResult<IEnumerable<Projecttask>>> Reportlist()
        {
           // return Ok(_context.Projecttasks.Include(pt => pt.Employe).Include(pt => pt.Project).Include(pt => pt.Module).ToList());
           var reportlist = (from e in _context.Employees
                             join p in _context.Projects on e.Id equals p.Managerid
                             join pm in _context.Projectmodules on p.Id equals pm.Projectid
                             join pt in _context.Projecttasks on pm.Id equals pt.Moduleid
                             
                             select new
                             {
                                 e,
                                 employeename = e.Name,
                                 projectname = p.Name,
                                 modulename = pm.Name,
                                 taskname = pt.Task,
                                 taskstatus = pt.Status
                             }

         
                             
                             ).ToList();
            return Ok(reportlist);
    
        }
        private bool ProjecttaskExists(int id)
        {
            return (_context.Projecttasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
