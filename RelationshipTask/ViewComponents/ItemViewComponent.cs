using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelationshipTask.DAL;
using RelationshipTask.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationshipTask.ViewComponents
{
    public class ItemViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public ItemViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Students> students = _context.Students.Include(p => p.Group).ToList();
            return View(await Task.FromResult(students));
        }
    }
}
