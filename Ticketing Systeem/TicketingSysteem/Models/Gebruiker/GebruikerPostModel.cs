﻿namespace TicketingSysteem.Models
{
    public class GebruikerPostModel : PostModelBase<Entities.Pocos.Gebruiker>
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public string Telefoonnummer { get; set; }
        public string GsmNummer { get; set; }
        public int? VerantwoordelijkeId { get; set; }
        public string Paswoord { get; set; }
        public string Rol { get; set; }
    }
}