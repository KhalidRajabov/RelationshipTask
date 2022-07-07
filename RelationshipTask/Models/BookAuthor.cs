namespace RelationshipTask.Models
{
    public class BookAuthor
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public Book Books { get; set; }
        public Author Authors { get; set; }
    }
}
