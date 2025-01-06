namespace ZarzadzanieZamowieniami.Models
{
    public class Produkt
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public string KodKreskowy { get; set; }
        public int StanMagazynowy { get; set; }
        public string Lokalizacja { get; set; }
        public decimal Cena { get; set; }

        public List<PozycjaZamowienia> PozycjeZamowien { get; set; }
    }
}
