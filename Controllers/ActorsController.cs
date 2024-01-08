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
            // Pobierz wszystkich aktorów
            var allActors = await _context.Actors.ToListAsync();
            
            // Sprawdź, czy istnieje wystarczająca liczba aktorów
            if (allActors.Count < 8)
            {
                return BadRequest("Not enough actors to select 8 random ones.");
            }
            
            // Wybierz 8 losowych aktorów
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
        // Update an existing actor
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

    // Handle MainImageUrl updates
    if (actor.MainImageUrl != actorInputModel.MainImageUrl)
    {
        actor.MainImageUrl = actorInputModel.MainImageUrl;
    }

    // Handle GalleryImageUrls updates
    // First remove URLs that are not in the new list
    var currentUrls = actor.GalleryImageUrls.ToList();
    foreach (var url in currentUrls)
    {
        if (!actorInputModel.GalleryImageUrls.Contains(url))
        {
            actor.GalleryImageUrls.Remove(url);
        }
    }

    // Then add new URLs
    foreach (var url in actorInputModel.GalleryImageUrls)
    {
        if (!actor.GalleryImageUrls.Contains(url))
        {
            actor.GalleryImageUrls.Add(url);
        }
    }

    // Save changes
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
// Create a new actor
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor([FromBody] ActorInputModel actorInputModel)
        {
            if (actorInputModel == null || actorInputModel.Actor == null)
            {
                return BadRequest("Invalid actor data.");
            }

            // Normalize the BirthDate to UTC if it exists
            if (actorInputModel.Actor.BirthDate.HasValue)
            {
                actorInputModel.Actor.BirthDate = DateTime.SpecifyKind(actorInputModel.Actor.BirthDate.Value, DateTimeKind.Utc);
            }

            // Set MainImageUrl and GalleryImageUrls from input model
            actorInputModel.Actor.MainImageUrl = actorInputModel.MainImageUrl;
            actorInputModel.Actor.GalleryImageUrls = actorInputModel.GalleryImageUrls ?? new List<string>(); // Ensure there is always a list to avoid null references

            // Add the new actor to the context
            _context.Actors.Add(actorInputModel.Actor);

            // Save the changes
            await _context.SaveChangesAsync();

            // Return the created actor
            return CreatedAtAction("GetActor", new { id = actorInputModel.Actor.Id }, actorInputModel.Actor);
        }


        // DELETE: api/Actors/5
        // Delete an actor
        [HttpDelete("{id}")]
public async Task<IActionResult> DeleteActor(int id)
{
    var actor = await _context.Actors.Include(a => a.Photos).FirstOrDefaultAsync(a => a.Id == id);
    if (actor == null)
    {
        return NotFound();
    }

    // Remove all photos associated with the actor
    if (actor.Photos != null && actor.Photos.Any())
    {
        _context.Photos.RemoveRange(actor.Photos);
    }

    // Remove the actor
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
