using System.Collections.Generic;

namespace RelationshipTask.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public List<BookGenre> BookGenres { get; set; }
    }
}
