using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFapi.Models
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public int ActorId { get; set; }
        public string PhotoUrl { get; set; } = "";

        public virtual Actor Actor { get; set; }

    }
}