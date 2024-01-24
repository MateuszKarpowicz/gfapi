using Microsoft.AspNetCore.Mvc;
using GFapi.Data;
using GFapi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;


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



    // GET: api/Actors/search
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Actor>>> SearchActors([FromQuery] string gender, [FromQuery] int? ageFrom, [FromQuery] int? ageTo, [FromQuery] int? heightFrom, [FromQuery] int? heightTo, [FromQuery] string skills, [FromQuery] string languages, [FromQuery] string hairColor)
    {
        var query = _context.Actors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(gender))
        {
            query = query.Where(a => a.Gender == gender);
        }

        if (ageFrom.HasValue || ageTo.HasValue)
        {
            var now = DateTime.UtcNow;
            if (ageFrom.HasValue)
            {
                var dateFrom = now.AddYears(-ageFrom.Value);
                query = query.Where(a => a.BirthDate.HasValue && a.BirthDate.Value <= dateFrom);
            }
            if (ageTo.HasValue)
            {
                var dateTo = now.AddYears(-ageTo.Value);
                query = query.Where(a => a.BirthDate.HasValue && a.BirthDate.Value >= dateTo);
            }
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
            query = query.Where(a => skillsArray.Any(skill => a.Skills.ToLower().Contains(skill)));
        }

        if (!string.IsNullOrWhiteSpace(languages))
        {
            var languagesArray = languages.Split(',').Select(language => language.Trim().ToLower());
            query = query.Where(a => languagesArray.Any(language => a.Languages.ToLower().Contains(language)));
        }

        if (!string.IsNullOrWhiteSpace(hairColor))
        {
            query = query.Where(a => a.HairColor.ToLower().Contains(hairColor.ToLower()));
        }

        var actors = await query.ToListAsync();
        return actors;
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


[HttpGet]
public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
{
    var actors = await _context.Actors.ToListAsync();


    return actors;
}


            [HttpGet("{id}")]
            public async Task<ActionResult<Actor>> GetActor(int id)
            {
                var actor = await _context.Actors.FindAsync(id);

                if (actor == null)
                {
                    return NotFound();
                }

               

                return actor;
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> PutActor(int id, [FromBody] ActorInputModel actorInputModel)
            {
                if (id != actorInputModel.Actor.Id)
                {
                    return BadRequest();
                }

                var actor = await _context.Actors.FindAsync(id);
                if (actor == null)
                {
                    return NotFound();
                }

                _context.Entry(actor).CurrentValues.SetValues(actorInputModel.Actor);
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
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor([FromBody] ActorInputModel actorInputModel)
        {
            if (actorInputModel == null || actorInputModel.Actor == null)
            {
                return BadRequest("Invalid actor data.");
            }

            // Konwersja listy URL-i na string
            _context.Actors.Add(actorInputModel.Actor);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetActor", new { id = actorInputModel.Actor.Id }, actorInputModel.Actor);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
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
