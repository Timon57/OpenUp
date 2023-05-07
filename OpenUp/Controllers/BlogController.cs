using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenUp.DataAccess.Data;
using OpenUp.Models;
using System.Security.Claims;

namespace OpenUp.Controllers
{
    
    public class BlogController : Controller
    {

        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            string user = User.Identity.Name;
            IEnumerable<Blog> objBlogList = _db.Blogs.Where(b => b.Author == user);
            Console.WriteLine("hi"+user);
            return View(objBlogList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            var authorName = User.Identity.Name;
            blog.Author = authorName;
            if (ModelState.IsValid)
            {                
                _db.Blogs.Add(blog);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var blog = _db.Blogs.FirstOrDefault(b=> b.Id == id);
            if(blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog blog)
        {
            var authorName = User.Identity.Name;
            blog.Author = authorName;
            if (ModelState.IsValid)
            {
                _db.Blogs.Update(blog);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var blog = _db.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);

        }
        //POST
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var blog = _db.Blogs.FirstOrDefault(u => u.Id == id);
            if(blog == null)
            {
                return NotFound();
            }
            _db.Blogs.Remove(blog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
