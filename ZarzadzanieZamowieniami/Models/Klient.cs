using System.ComponentModel.DataAnnotations;

namespace ZarzadzanieZamowieniami.Models
{
    public class Klient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole 'Imię' jest wymagane.")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Pole 'Nazwisko' jest wymagane.")]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Pole 'Adres' jest wymagane.")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Pole 'Telefon' jest wymagane.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Pole 'Email' jest wymagane.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email.")]
        public string Email { get; set; }

        public List<Zamowienie> Zamowienia { get; set; }

        public Klient()
        {
            Zamowienia = new List<Zamowienie>();
        }
    }
}