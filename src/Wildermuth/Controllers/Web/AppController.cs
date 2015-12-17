﻿using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wildermuth.ViewModels;
using Wildermuth.Services;
using Wildermuth.Models;

namespace Wildermuth.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IWorldRepository _repository;

        public AppController(IMailService service, IWorldRepository repository)
        {
            _mailService = service;
            _repository = repository;
        }
        public IActionResult Index()
        {
            var trips = _repository.GetAllTrips();
            return View(trips);
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