using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ZarzadzanieZamowieniami.Models
{
    public class PozycjaZamowienia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być większa niż 0.")]
        public int Ilosc { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa niż 0.")]
        public decimal Cena { get; set; }

        [ForeignKey("Zamowienie")]
        public int ZamowienieId { get; set; }
        [ValidateNever]
        public Zamowienie Zamowienie { get; set; }

        [ForeignKey("Produkt")]
        public int ProduktId { get; set; } 

        [ValidateNever]
        public Produkt Produkt { get; set; } 
    }
}
