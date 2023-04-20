using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalogo.API.Model;
using Catalogo.Infrastructure;

namespace Catalogo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoItemsController : ControllerBase
    {
        private readonly CatalogoContext _context;

        public CatalogoItemsController(CatalogoContext context)
        {
            _context = context;
        }

        // GET: api/CatalogoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogoItem>>> GetCatalogoItems()
        {
          if (_context.CatalogoItems == null)
          {
              return NotFound();
          }
            return await _context.CatalogoItems.ToListAsync();
        }

        // GET: api/CatalogoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogoItem>> GetCatalogoItem(int id)
        {
          if (_context.CatalogoItems == null)
          {
              return NotFound();
          }
            var catalogoItem = await _context.CatalogoItems.FindAsync(id);

            if (catalogoItem == null)
            {
                return NotFound();
            }

            return catalogoItem;
        }

        // PUT: api/CatalogoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogoItem(int id, CatalogoItem catalogoItem)
        {
            if (id != catalogoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(catalogoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoItemExists(id))
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

        // POST: api/CatalogoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CatalogoItem>> PostCatalogoItem(CatalogoItem catalogoItem)
        {
          if (_context.CatalogoItems == null)
          {
              return Problem("Entity set 'CatalogoContext.CatalogoItems'  is null.");
          }
            _context.CatalogoItems.Add(catalogoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatalogoItem", new { id = catalogoItem.Id }, catalogoItem);
        }

        // DELETE: api/CatalogoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogoItem(int id)
        {
            if (_context.CatalogoItems == null)
            {
                return NotFound();
            }
            var catalogoItem = await _context.CatalogoItems.FindAsync(id);
            if (catalogoItem == null)
            {
                return NotFound();
            }

            _context.CatalogoItems.Remove(catalogoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatalogoItemExists(int id)
        {
            return (_context.CatalogoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
