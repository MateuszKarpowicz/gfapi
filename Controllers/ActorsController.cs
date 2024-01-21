using Microsoft.AspNetCore.Mvc;
using GFapi.Data;
using GFapi.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Net;


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

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Actor>>> SearchActors([FromQuery] string gender, [FromQuery] int? ageFrom, [FromQuery] int? ageTo, [FromQuery] int? heightFrom, [FromQuery] int? heightTo, [FromQuery] string skills, [FromQuery] string languages, [FromQuery] string hairColor)
        {
            var query = _context.Actors.AsQueryable();
            if (!string.IsNullOrWhiteSpace(gender))
            {
                query = query.Where(a => a.Gender == gender);
            }

            if (ageFrom.HasValue)
            {
                var dateFrom = DateTime.UtcNow.AddYears(-ageFrom.Value);
                query = query.Where(a => a.BirthDate <= dateFrom);
            }
            if (ageTo.HasValue)
            {
                var dateTo = DateTime.UtcNow.AddYears(-ageTo.Value);
                query = query.Where(a => a.BirthDate >= dateTo);
            }
            if (heightFrom.HasValue)
            {
                query = query.Where(a => a.Height >= heightFrom.Value);
            }
            if (heightTo.HasValue)
            {
                query = query.Where(a => a.Height <= heightTo.Value);
            }
            if (!string.IsNullOrWhiteSpace(skills))
            {
                var skillsArray = skills.Split(',').Select(skill => skill.Trim().ToLower());
                foreach (var skill in skillsArray)
                {
                    query = query.Where(a => a.Skills.ToLower().Contains(skill));
                }
            }

            if (!string.IsNullOrWhiteSpace(languages))
            {
                var languagesArray = languages.Split(',').Select(language => language.Trim().ToLower());
                foreach (var language in languagesArray)
                {
                    query = query.Where(a => a.Languages.ToLower().Contains(language));
                }
            }
            if (!string.IsNullOrWhiteSpace(hairColor))
            {
                query = query.Where(a => a.HairColor.Contains(hairColor));
            }
            return await query.ToListAsync();
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
