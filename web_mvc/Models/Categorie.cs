using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_mvc.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string nom { get; set; }
        public ICollection<Figurine> figurines { get; set; }

        public Categorie() { }
        public Categorie(string nom)
        {
            this.nom = nom;
        }
    }
}
