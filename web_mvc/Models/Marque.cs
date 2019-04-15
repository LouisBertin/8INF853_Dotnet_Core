using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_mvc.Models
{
    public class Marque
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Nom { get; set; }
        public ICollection<Figurine> figurines { get; set; }

        public Marque() { }
        public Marque(string nom)
        {
            this.Nom = nom;
        }
    }
}
