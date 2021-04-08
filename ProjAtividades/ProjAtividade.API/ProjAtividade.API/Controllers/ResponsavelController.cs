using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjAtividade.API.Data;
using ProjAtividade.API.Model;

namespace ProjAtividade.API.Controllers
{
    public class ResponsavelController : ControllerBase
    {
        private readonly AtividadesContext _context;

        public ResponsavelController(AtividadesContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Responsavel>>> GetResponsavel()
        {
            return await _context.Responsavel.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Responsavel>> GetResponsavel(int id)
        {
            var responsavel = await _context.Responsavel.FindAsync(id);

            if (responsavel == null)
            {
                return NotFound();
            }

            return responsavel;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Responsavel>> PutResponsavel(int id, Responsavel responsavel)
        {
            if (id != responsavel.Id)
            {
                return BadRequest();
            }

            _context.Entry(responsavel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsavelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return await _context.Responsavel.FindAsync(responsavel.Id);
        }


        [HttpPost]
        public async Task<ActionResult<Responsavel>> PostResponsavel(Responsavel responsavel)
        {
            _context.Responsavel.Add(responsavel);
            await _context.SaveChangesAsync();


            var respon = await _context.Responsavel.FindAsync(responsavel.Id);
            return respon;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Responsavel>> DeleteResponsavel(int id)
        {
            var responsavel = await _context.Responsavel.FindAsync(id);
            if (responsavel == null)
            {
                return NotFound();
            }

            _context.Responsavel.Remove(responsavel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = responsavel.Id }, responsavel);
        }

        private bool ResponsavelExists(int id)
        {
            return _context.Responsavel.Any(e => e.Id == id);
        }

        private int getMaxId()
        {
            return _context.Responsavel
                .OrderByDescending(u => u.Id).FirstOrDefault().Id;
        }
    }
}
