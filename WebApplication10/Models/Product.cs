using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Models
{
    public class Product
    {
       
        public int Id { get; set; }
        [DisplayName("Naziv")]
        [Required(ErrorMessage = "Naziv je obavezan")]
        public string Naziv { get; set; }
        [DisplayName("Opis")]
        [Required(ErrorMessage = "Opis je obavezan")]
        public string Opis { get; set; }
        [DisplayName("Kategorija")]
        [Required(ErrorMessage = "Kategorija je obavezan")]
        public string Kategorija { get; set; }
        [DisplayName("Proizvodjac")]
        [Required(ErrorMessage = "Proizvodjac je obavezan")]
        public string Proizvodjac { get; set; }
        [DisplayName("Dobavljac")]
        [Required(ErrorMessage = "Dobavljac je obavezan")]
        public string Dobavljac { get; set; }
        [DisplayName("Cena")]
        [Required(ErrorMessage = "Cena je obavezna")]
        public int Cena { get; set; }
    }
}
