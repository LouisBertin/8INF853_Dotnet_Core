using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_mvc.Models
{
    public class Figurine
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public float prix_ttc { get; set; }
        public int quantite_magasin { get; set; }
        public int quantite_stock { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date_parution { get; set; }

        public int nb_exemplaires { get; set; }
        public float poids { get; set; }
        public float largeur { get; set; }
        public float hauteur { get; set; }
        public float longueur { get; set; }
        public String reference { get; set; }
        public String description { get; set; }

        public int MarqueId { get; set; }
        public Marque marque { get; set; }

        public int CategorieId {get; set;}
        public Categorie categorie { get; set; }

        public ICollection<Reservation> reservations { get; set; }
    }
}
