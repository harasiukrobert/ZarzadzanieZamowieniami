// Models/Zamowienie.cs
using System;
using System.ComponentModel.DataAnnotations;
using ZarzadzanieZamowieniami.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace ZarzadzanieZamowieniami.Models
{
    public class Zamowienie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole 'Data Złożenia' jest wymagane.")]
        public DateTime DataZlozenia { get; set; }

        [Required(ErrorMessage = "Pole 'Status' jest wymagane.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Pole 'KlientId' jest wymagane.")]
        public int KlientId { get; set; }

        [ValidateNever]
        public Klient Klient { get; set; }

    }
}
