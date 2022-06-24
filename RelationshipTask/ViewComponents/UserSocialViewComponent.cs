using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelationshipTask.DAL;
using RelationshipTask.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationshipTask.ViewComponents
{
    public class UserSocialViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public UserSocialViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Socials> socials = _context.Socials.Include(p => p.Users).ToList();
            return View(await Task.FromResult(socials));
        }
    }
}
