using System.Collections.Generic;

namespace RelationshipTask.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookGenre> BookGenres { get; set; }
    }
}
