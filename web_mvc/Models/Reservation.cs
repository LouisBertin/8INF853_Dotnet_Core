using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_mvc.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Display(Name = "Acheté")]
        [DefaultValue(false)]
        public bool achete { get; set; }

        
        [Display(Name = "Quantité")]
        public int quantite { get; set; }

        [Display(Name = "Date d'expiration")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date_expiration { get; set; }

        [Display(Name = "Montant")]
        public float montant { get; set; }

        public int FigurineId { get; set; }
        public Figurine figurine { get; set; }

        public string UserId { get; set; }
    }
}
