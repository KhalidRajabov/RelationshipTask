using System.Collections.Generic;

namespace RelationshipTask.Models
{
    public class Groups
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Students> Students { get; set; }
    }
}
