using System.Reflection.Metadata.Ecma335;

namespace SportsStore.ViewModels
{
    //Ergänzt das Datenmodell einer Ansicht um Informationen zu
    // Produktanzahl gesamt
    // Produkte pro Seite
    // aktuelle Seitennummer
    // Anzahl Seiten
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        
    }
}
