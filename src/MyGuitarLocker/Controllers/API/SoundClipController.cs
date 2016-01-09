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
    [Route("api/Instruments/{InstrumentName}/SoundClips")]
    public class SoundClipController : Controller
    {
        private IMyGuitarLockerRepository _repository;
        private ILogger<SoundClipController> _logger;

        public SoundClipController(IMyGuitarLockerRepository repository, ILogger<SoundClipController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get(string InstrumentName)
        {
            try
            {
                var results = _repository.GetInstrumentByName(InstrumentName, User.Identity.Name);
                if (results == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<SoundClipViewModel>>(results.SoundClips));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get {InstrumentName}", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("an error occured");
            }
        }

        [Route("{user}")]
        [HttpGet("{user}")]
        public JsonResult Get(string InstrumentName, string user)
        {
            try
            {
                var results = _repository.GetInstrumentByName(InstrumentName, user);
                if (results == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<SoundClipViewModel>>(results.SoundClips));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get {InstrumentName}", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("an error occured");
            }
        }

        public JsonResult Post(string InstrumentName, [FromBody]SoundClipViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map to Entity
                    var newSoundClip = Mapper.Map<SoundClip>(vm);

                    //Save to the Database
                    _repository.AddSoundClip(InstrumentName, newSoundClip, User.Identity.Name);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<SoundClipViewModel>(newSoundClip));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get {InstrumentName}", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save new SoundClip");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Validation failed on new SoundClip");
        }
    }
}
