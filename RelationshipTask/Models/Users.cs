using System.Collections.Generic;

namespace RelationshipTask.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Socials> Socials { get; set; }
    }
}
