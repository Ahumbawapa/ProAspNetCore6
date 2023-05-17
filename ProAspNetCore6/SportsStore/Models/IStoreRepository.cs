namespace SportsStore.Models
{
    // Anwenden des Repository-Entwurfmusters
    public interface IStoreRepository
    {
        /// <summary>
        /// IQueryable erlaubt es eine Sammlung von Objekten effektiv abzufragen
        /// Soll nur eine Teilmenge der Daten abgefragt werden, kann über 
        /// IQueryable mit Standard-LINQ darauf zugegriffen werden
        /// </summary>
        IQueryable<Product> Products { get; }
    }
}
