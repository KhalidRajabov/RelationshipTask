namespace RelationshipTask.Models
{
    public class BookGenre
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int GenreId { get; set; }
        public Book Books { get; set; }
        public Genre Genres { get; set; }
    }
}
