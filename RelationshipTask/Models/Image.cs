using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationshipTask.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        
        
    }
}
