using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Entities.SearchPocos
{
    public class GebruikerSearch : SearchBase<Gebruiker>
    {
        public string Email { get; set; }
        public string Rol { get; set; }
        public int? MinionOfId { get; set; }
    }
}