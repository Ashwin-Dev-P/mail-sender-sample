using MailSender.Interfaces;
using MailSender.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MailSender.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender emailSender;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            this.emailSender = emailSender;
            
        }

        [HttpGet]
        public  IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string recepientMail, string subject, string body)
        {
            try
            {
                await emailSender.SendEmailAsync(recepientMail, subject, body);
                ViewBag.successMessage = "Mail sent successfully";
                ViewBag.errorMessage = null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.successMessage = null;
                ViewBag.errorMessage = ex.Message;
            }
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}