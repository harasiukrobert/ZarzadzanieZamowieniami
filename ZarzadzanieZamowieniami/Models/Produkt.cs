﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ZarzadzanieZamowieniami.Models
{
    public class Produkt
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane.")]
        public string Nazwa { get; set; }

        [Required(ErrorMessage = "Pole 'Opis' jest wymagane.")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Pole 'Kod kreskowy' jest wymagane.")]
        public string KodKreskowy { get; set; }

        [Required(ErrorMessage = "Pole 'Stan magazynowy' jest wymagane.")]
        public int StanMagazynowy { get; set; }

        [Required(ErrorMessage = "Pole 'Lokalizacja' jest wymagane.")]
        public string Lokalizacja { get; set; }

        [Required(ErrorMessage = "Pole 'Cena' jest wymagana.")]
        public decimal Cena { get; set; }

        [ValidateNever]
        public List<PozycjaZamowienia> PozycjeZamowienia { get; set; }

    }
}