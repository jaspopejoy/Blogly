using Blogly.Data;
using Blogly.Enums;
using Blogly.Models;
using Blogly.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using X.PagedList;

namespace Blogly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IBlogEmailSender emailSender, ApplicationDbContext context)
        {
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;

            var blogs = _context.Blogs
                 .OrderByDescending(b => b.Created)
                 .ToPagedListAsync(pageNumber, pageSize);

            ViewData["HeaderImage"] = "/images/home-bg.jpg";
            ViewData["MainText"] = "Welcome To Blogly";
            ViewData["SubText"] = "By Jason Popejoy";

            return View(await blogs);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMe model)
        {
            //This is where emailing takes place
            model.Message = $"{model.Message} <hr/> Phone: {model.Phone}";
            await _emailSender.SendContactEmailAsync(model.Email, model.Name, model.Subject, model.Message);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}