using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGuitarLocker.ViewModels;
using MyGuitarLocker.Services;
using MyGuitarLocker.Models;
using Microsoft.AspNet.Authorization;

namespace MyGuitarLocker.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IMyGuitarLockerRepository _repository;

        public AppController(IMailService service, IMyGuitarLockerRepository repository)
        {
            _mailService = service;
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult App()
        {
            var Instruments = _repository.GetAllInstruments();
            return View(Instruments);
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
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings: SiteEmailAddress"];
                if (string.IsNullOrWhiteSpace(email)) { ModelState.AddModelError("", "err"); }

                if(_mailService.SendMail(email,
                    email,
                    $"Contact Page from {model.Name} ({model.Email})",
                    model.Message))
                {
                    ModelState.Clear();
                    ViewBag.Message = "Mail Sent";
                }
            }
            return View();
        }
    }
}
