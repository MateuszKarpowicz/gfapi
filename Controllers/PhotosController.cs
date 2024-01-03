using Microsoft.AspNetCore.Mvc;
using GFapi.Data;
using GFapi.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GFapi.Controllers
{
    [Route("api/actors/{actorId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly DataContext _context;

        public PhotosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/actors/5/photos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetActorPhotos(int actorId)
        {
            var photos = await _context.Photos.Where(p => p.ActorId == actorId).ToListAsync();
            if (!photos.Any())
            {
                return NotFound("Photos for this actor not found.");
            }
            return Ok(photos);
        }

        // GET: api/actors/5/photos/10
        [HttpGet("{photoId}")]
        public async Task<ActionResult<Photo>> GetActorPhoto(int actorId, int photoId)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.ActorId == actorId && p.PhotoId == photoId);
            if (photo == null)
            {
                return NotFound();
            }
            return Ok(photo);
        }

        // POST: api/actors/5/photos
        [HttpPost]
        public async Task<ActionResult<Photo>> PostActorPhoto(int actorId, [FromBody] Photo photo)
        {
            if (photo == null)
            {
                return BadRequest("Invalid photo data.");
            }

            photo.ActorId = actorId; // Ensure the photo is linked to the actor
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActorPhoto", new { actorId = actorId, photoId = photo.PhotoId }, photo);
        }

        // PUT: api/actors/5/photos/10
        [HttpPut("{photoId}")]
        public async Task<IActionResult> PutActorPhoto(int actorId, int photoId, [FromBody] Photo photo)
        {
            if (photoId != photo.PhotoId || actorId != photo.ActorId)
            {
                return BadRequest();
            }

            _context.Entry(photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(photoId))
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

        // DELETE: api/actors/5/photos/10
        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeleteActorPhoto(int actorId, int photoId)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.ActorId == actorId && p.PhotoId == photoId);
            if (photo == null)
            {
                return NotFound();
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhotoExists(int photoId)
        {
            return _context.Photos.Any(e => e.PhotoId == photoId);
        }
    }
}
