using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFapi.Data;
using System.ComponentModel.DataAnnotations;

namespace GFapi.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Gender { get; set; } = "nie podano płci";

        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = "nie podano imienia aktora";

        [StringLength(100, ErrorMessage = "Surname cannot be longer than 100 characters.")]
        public string Surname { get; set; } = "nie podano nazwiska aktora";

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BirthDate { get; set; } = null;

        [Range(0, 300, ErrorMessage = "Height must be between 0.5 and 2.5 meters.")]
        public int Height { get; set; } = 0;

        [StringLength(50, ErrorMessage = "Eye color cannot be longer than 50 characters.")]
        public string EyeColor { get; set; } = "nie podano koloru oczu";

        [StringLength(200, ErrorMessage = "Education cannot be longer than 200 characters.")]
        public string Education { get; set; } = "nie podano wykształcenia";

        [StringLength(200, ErrorMessage = "Languages cannot be longer than 200 characters.")]
        public string Languages { get; set; } = "nie podano języków";

        [StringLength(500, ErrorMessage = "Skills cannot be longer than 500 characters.")]
        public string Skills { get; set; } = "nie podano umiejętności";
        
        [StringLength(200, ErrorMessage = "Link cannot be longer than 200 characters.")]
        public string PolskieKinoUrl { get; set; } = "";

        public string MainImageUrl { get; set; } = ""; 
        public List<string> GalleryImageUrls { get; set; } = new List<string>(); 

        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

        public string HairColor { get; set; } = "nie podano koloru włosów";

        public string VideoUrl { get; set; } = "nie podano linku do wideo";
    }
}