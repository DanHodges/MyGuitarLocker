using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using MyGuitarLocker.Models;
using MyGuitarLocker.ViewModels;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Net;
using MyGuitarLocker.Services;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyGuitarLocker.Controllers.API
{
    [Authorize]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private IMyGuitarLockerRepository _repository;
        private ILogger<UsersController> _logger;

        public UsersController(IMyGuitarLockerRepository repository, ILogger<UsersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            try
            {
                var results =  _repository.GetAllUsers();
                if (results == null)
                {
                    return Json(null);
                }
                return Json(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Users", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("an error occured");
            }
        }
    }
}