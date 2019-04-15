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

        [Display(Name = "Bought")]
        [DefaultValue(false)]
        public bool achete { get; set; }

        
        [Display(Name = "Quantity")]
        public int quantite { get; set; }

        [Display(Name = "Expiration date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date_expiration { get; set; }

        [Display(Name = "Amount")]
        public float montant { get; set; }

        [Display(Name = "Figurine")]
        public int FigurineId { get; set; }
        public Figurine figurine { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }
    }
}
