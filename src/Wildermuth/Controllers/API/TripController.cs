using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wildermuth.Models;
using Microsoft.AspNet.Mvc;
using System.Net;
using Wildermuth.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Wildermuth.Controllers.API
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> _logger;
        private IWorldRepository _repository;


        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var results = Mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllTrips());
            return Json(new { results = results });
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var NewTrip = Mapper.Map<Trip>(vm);
                    _logger.LogInformation("Attempting to save a new Trip");
                    //Save to Database
                    _repository.AddTrip(NewTrip);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(NewTrip));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new trip", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed");
        }
    }
}
