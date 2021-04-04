using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Courses.Domain;
using Courses.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly ICoursesContext _context;

        public ModulesController(ICoursesContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModules()
        {
            return await _context.Modules.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(Guid id)
        {
            var course = await _context.Modules.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(Guid id, Module course)
        {
            if (id != course.ModuleId)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(new CancellationToken());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Module>> PostProduct(Module course)
        {
            _context.Modules.Add(course);
            await _context.SaveChangesAsync(new CancellationToken());

            return CreatedAtAction("GetModule", new {id = course.ModuleId}, course);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var course = await _context.Modules.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Modules.Remove(course);
            await _context.SaveChangesAsync(new CancellationToken());

            return NoContent();
        }

        private bool ModuleExists(Guid id)
        {
            return _context.Modules.Any(p => p.ModuleId == id);
        }
    }
}