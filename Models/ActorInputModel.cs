using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFapi.Models
{
    public class ActorInputModel
    {
        public Actor Actor { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> GalleryImageUrls { get; set; }
    }
}