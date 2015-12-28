using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGuitarLocker.Models;
using Microsoft.AspNet.Mvc;
using System.Net;
using MyGuitarLocker.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Authorization;

namespace MyGuitarLocker.Controllers.API
{
    [Authorize]
    [Route("api/Instruments")]
    public class InstrumentController : Controller
    {
        private ILogger<InstrumentController> _logger;
        private IMyGuitarLockerRepository _repository;


        public InstrumentController(IMyGuitarLockerRepository repository, ILogger<InstrumentController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var Instruments = _repository.GetUserInstruments(User.Identity.Name);
            var results = Mapper.Map<IEnumerable<InstrumentViewModel>>(Instruments);
            return Json(new { results = results });
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]InstrumentViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var NewInstrument = Mapper.Map<Instrument>(vm);

                    NewInstrument.UserName = User.Identity.Name;

                    //Save to Database
                    _logger.LogInformation("Attempting to save a new Instrument");
                    _repository.AddInstrument(NewInstrument);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        return Json(Mapper.Map<InstrumentViewModel>(NewInstrument));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new Instrument", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed");
        }
    }
}
