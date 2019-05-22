using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortfolioProject.Models;
using System.Net.Mail;

namespace PortfolioProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult About2()
        {
            return View();
        }

        public IActionResult Skills()
        {
            return View();
        }

        
        public IActionResult Portfolio()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Contact(ContacViewModal vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
                    var smtpPort = Environment.GetEnvironmentVariable("SMTP_PORT");
                    var smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME");
                    var smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");


                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(vm.Email);//Email which you are getting 
                                                         //from contact us page 
                    msz.To.Add("greenfeld.adam@gmail.com");//Where mail will be sent 
                    msz.Subject = vm.Subject;
                    msz.Body = vm.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = smtpHost;

                    smtp.Port = int.Parse(smtpPort);

                    smtp.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);

                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting me! I'll be with you shortly!!! ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }
            }

            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
