using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelationshipTask.DAL;
using RelationshipTask.Models;
using RelationshipTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RelationshipTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();

            homeVM.Groups = _context.Groups.ToList();
            homeVM.Students = _context.Students.ToList();
            homeVM.Users= _context.Users.ToList();
            homeVM.Socials= _context.Socials.ToList();
            return View(homeVM);
        }
    }
}
