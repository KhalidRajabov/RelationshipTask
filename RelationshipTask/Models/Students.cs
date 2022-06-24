using System.Text.RegularExpressions;

namespace RelationshipTask.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int GroupId { get; set; }
        public Groups Group { get; set; }
    }
}
