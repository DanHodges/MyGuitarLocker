using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Wildermuth.Models;
using Wildermuth.ViewModels;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Net;
using Wildermuth.Services;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Wildermuth.Controllers.API
{
    [Authorize]
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<StopController> _logger;
        private CoordService _coordService;

        public StopController(IWorldRepository repository, ILogger<StopController> logger, CoordService coordService )
        {
            _repository = repository;
            _logger = logger;
            _coordService = coordService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = _repository.GetTripByName(tripName, User.Identity.Name);
                if (results == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get {tripName}", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("an error occured");
            }
        }

        public async Task<JsonResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map to Entity
                    var newStop = Mapper.Map<Stop>(vm);
                    // Look up GeoCordinates
                    var coordResult = await _coordService.Lookup(newStop.Name);

                    if (!coordResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Json(coordResult.Message);
                    }

                    newStop.Longitude = coordResult.Longitude;
                    newStop.Latitude = coordResult.Latitude;
                    //Save to the Database
                    _repository.AddStop(tripName, newStop, User.Identity.Name);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get {tripName}", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save new stop");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed on new stop");
        }
    }
}
