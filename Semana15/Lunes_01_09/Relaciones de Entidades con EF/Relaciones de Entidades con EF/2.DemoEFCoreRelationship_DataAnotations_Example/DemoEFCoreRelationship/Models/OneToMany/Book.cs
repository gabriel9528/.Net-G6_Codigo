namespace DemoEFCoreRelationship.Models.OneToMany
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        // Relación: muchos a uno
        public int AuthorId { get; set; } // Clave foránea

        public Author? Author { get; set; } // Propiedad de navegación
    }
}
