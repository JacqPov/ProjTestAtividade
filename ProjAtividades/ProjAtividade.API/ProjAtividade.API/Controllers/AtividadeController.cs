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
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {

            private readonly AtividadesContext _context;

            public AtividadeController(AtividadesContext context)
            {
                _context = context;
            }

            
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Atividade>>> GetAtividade()
            {
                return await _context.Atividade.ToListAsync();
            }

            
            [HttpGet("{id}")]
            public async Task<ActionResult<Atividade>> GetAtividade(int id)
            {
                var atividade = await _context.Atividade.FindAsync(id);

                if (atividade == null)
                {
                    return NotFound();
                }

                return atividade;
            }

            
            [HttpPut("{id}")]
            public async Task<ActionResult<Atividade>> PutAtividade(int id, Atividade atividade)
            {
                if (id != atividade.Id)
                {
                    return BadRequest();
                }

                _context.Entry(atividade).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtividadeExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                
                return await _context.Atividade.FindAsync(atividade.Id);
            }

            
            [HttpPost]
            public async Task<ActionResult<Atividade>> PostAtividade(Atividade atividade)
            {
                _context.Atividade.Add(atividade);
                await _context.SaveChangesAsync();

                
                var ativ = await _context.Atividade.FindAsync(atividade.Id);
                return ativ;
            }

           
            [HttpDelete("{id}")]
            public async Task<ActionResult<Atividade>> DeleteAtividade(int id)
            {
                var atividade = await _context.Atividade.FindAsync(id);
                if (atividade == null)
                {
                    return NotFound();
                }

                _context.Atividade.Remove(atividade);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetClient", new { id = atividade.Id }, atividade);
            }

            private bool AtividadeExists(int id)
            {
                return _context.Atividade.Any(e => e.Id == id);
            }

            private int getMaxId()
            {
                return _context.Atividade
                    .OrderByDescending(u => u.Id).FirstOrDefault().Id;
            }
    }
}
