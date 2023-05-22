using BizLand.DAL;
using BizLand.Models;
using Microsoft.AspNetCore.Mvc;

namespace BizLand.Controllers
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
            List<TeamMember> members = _context.TeamMembers.ToList();
            return View(members);
        }
    }
}
