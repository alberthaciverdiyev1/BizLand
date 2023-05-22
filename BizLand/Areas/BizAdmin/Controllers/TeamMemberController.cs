using BizLand.DAL;
using BizLand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizLand.Areas.BizAdmin.Controllers
{
    [Area("BizAdmin")]
    public class TeamMemberController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamMemberController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<TeamMember> member = await _context.TeamMembers.ToListAsync();

            return View(member);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeamMember member)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            string guid=Guid.NewGuid().ToString()+ member.Photo.FileName; 
            string path = Path.Combine(_env.WebRootPath, "assets/img/team",guid);
            FileStream stream = new FileStream(path, FileMode.Create);
            await member.Photo.CopyToAsync(stream);
            member.ImageUrl = guid;
            await _context.AddAsync(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            
            TeamMember team =  _context.TeamMembers.FirstOrDefault(x => x.Id == id);
            string path = Path.Combine(_env.WebRootPath, "assets/img/team", team.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
              
            }   _context.Remove(team);
           _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            TeamMember member=await _context.TeamMembers.FirstOrDefaultAsync(x => x.Id == id);
            if(member == null) return NotFound();
            return View(member);
        }
        [HttpPost]
        public async Task<IActionResult>Update(int id,TeamMember member)
        {
            if (id == null) return BadRequest();
                TeamMember existed= await _context.TeamMembers.FirstOrDefaultAsync(x=>x.Id==id);
            if (existed == null) return NotFound();
            if(member.Photo != null)
            {
                string path = Path.Combine(_env.WebRootPath, "assets/img/team", existed.ImageUrl);
                System.IO.File.Delete(path);
                string newpath = Path.Combine(_env.WebRootPath, "assets/img/team", Guid.NewGuid().ToString() + member.Photo.FileName);
                FileStream stream = new FileStream(path, FileMode.Create);
                await member.Photo.CopyToAsync(stream);
                member.ImageUrl=newpath;
              
            }
          await  _context.AddAsync(member);
          await  _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
