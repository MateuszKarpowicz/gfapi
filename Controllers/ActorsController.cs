using Microsoft.AspNetCore.Mvc;
using GFapi.Data;
using GFapi.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GFapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly DataContext _context;

        public ActorsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            return await _context.Actors.Include(a => a.Photos).ToListAsync();
        }

                // GET: api/Actors/random/8
        [HttpGet("random/8")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetRandomActors()
        {
            var allActors = await _context.Actors.ToListAsync();
            
            if (allActors.Count < 8)
            {
                return BadRequest("Not enough actors to select 8 random ones.");
            }
            
            var randomActors = allActors.OrderBy(a => Guid.NewGuid()).Take(8).ToList();

            return randomActors;
        }


        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetActor(int id)
        {
            var actor = await _context.Actors.Include(a => a.Photos).FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }











        // PUT: api/Actors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, [FromBody] ActorInputModel actorInputModel)
        {
            if (id != actorInputModel.Actor.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             var actor = await _context.Actors.FindAsync(id);
    if (actor == null)
    {
        return NotFound();
    }

    // Update actor details
    _context.Entry(actor).CurrentValues.SetValues(actorInputModel.Actor);

    if (actor.MainImageUrl != actorInputModel.MainImageUrl)
    {
        actor.MainImageUrl = actorInputModel.MainImageUrl;
    }


    var currentUrls = actor.GalleryImageUrls.ToList();
    foreach (var url in currentUrls)
    {
        if (!actorInputModel.GalleryImageUrls.Contains(url))
        {
            actor.GalleryImageUrls.Remove(url);
        }
    }

    foreach (var url in actorInputModel.GalleryImageUrls)
    {
        if (!actor.GalleryImageUrls.Contains(url))
        {
            actor.GalleryImageUrls.Add(url);
        }
    }

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!ActorExists(id))
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

       // POST: api/Actors
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor([FromBody] ActorInputModel actorInputModel)
        {
            if (actorInputModel == null || actorInputModel.Actor == null)
            {
                return BadRequest("Invalid actor data.");
            }

            if (actorInputModel.Actor.BirthDate.HasValue)
            {
                actorInputModel.Actor.BirthDate = DateTime.SpecifyKind(actorInputModel.Actor.BirthDate.Value, DateTimeKind.Utc);
            }

            actorInputModel.Actor.MainImageUrl = actorInputModel.MainImageUrl;
            actorInputModel.Actor.GalleryImageUrls = actorInputModel.GalleryImageUrls ?? new List<string>(); // Ensure there is always a list to avoid null references

            _context.Actors.Add(actorInputModel.Actor);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actorInputModel.Actor.Id }, actorInputModel.Actor);
        }


        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
public async Task<IActionResult> DeleteActor(int id)
{
    var actor = await _context.Actors.Include(a => a.Photos).FirstOrDefaultAsync(a => a.Id == id);
    if (actor == null)
    {
        return NotFound();
    }

    if (actor.Photos != null && actor.Photos.Any())
    {
        _context.Photos.RemoveRange(actor.Photos);
    }

    _context.Actors.Remove(actor);
    await _context.SaveChangesAsync();

    return NoContent();
}


        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
