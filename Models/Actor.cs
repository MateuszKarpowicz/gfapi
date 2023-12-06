using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFapi.Data;

namespace GFapi.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Surname { get; set; }     
        public int Age { get; set; }           
        public float Height { get; set; }       
        public string EyeColor { get; set; }    
        public string Education { get; set; }   
        public string Languages { get; set; }   
        public string Skills { get; set; }      
    }
}