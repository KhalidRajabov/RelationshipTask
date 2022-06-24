using RelationshipTask.Models;
using System.Collections.Generic;

namespace RelationshipTask.ViewModels
{
    public class HomeVM
    {
        public List<Groups> Groups { get; set; }
        public List<Students> Students { get; set; }
        public List<Users> Users { get; set; }
        public List<Socials> Socials { get; set; }
    }
}