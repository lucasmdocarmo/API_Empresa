using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrasctruture.Context;
using Microsoft.AspNetCore.Authorization;
using Domain.Contracts.IRepository;

namespace MyAPI.Controllers
{
    [Authorize]
    [Route("api/V1/[controller]")]
    [ApiController]
    public class EnterprisesController : ControllerBase
    {
        private readonly APIContext _context;

        public EnterprisesController(APIContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enterprise>>> GetAll()
        {
            return await _context.Enterprise.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Enterprise>> Get(Guid id)
        {
            var emp = await _context.Enterprise.FindAsync(id);

            if (emp == null)
            {
                return NotFound();
            }

            return emp;
        }

        [AllowAnonymous]
        [Route("enterprise_types")]
        public async Task<ActionResult<Enterprise>> GetEnterpriseIndex(Guid id, string name)
        {
            var emp = _context.Enterprise.Where(x => x.Id == id && x.Name.Contains(name)).FirstOrDefault();

            if (emp == null)
            {
                return NotFound();
            }

            return emp;
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Enterprise emp)
        {
            if (id != emp.Id)
            {
                return BadRequest();
            }

            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnterprisExists(id))
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Enterprise>> PostEnterprise(Enterprise emp)
        {
            _context.Enterprise.Add(emp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = emp.Id }, emp);
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Enterprise>> DeleteEnterprise(Guid id)
        {
            var emp = await _context.Enterprise.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }

            _context.Enterprise.Remove(emp);
            await _context.SaveChangesAsync();

            return emp;
        }

        [AllowAnonymous]
        private bool EnterprisExists(Guid id)
        {
            return _context.Enterprise.Any(e => e.Id == id);
        }
    }
}
