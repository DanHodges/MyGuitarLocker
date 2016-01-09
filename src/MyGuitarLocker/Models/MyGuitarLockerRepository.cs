using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;

namespace MyGuitarLocker.Models
{
    public class MyGuitarLockerRepository : IMyGuitarLockerRepository
    {
        public MyGuitarLockerContext _context;
        private ILogger<MyGuitarLockerRepository> _logger;

        public MyGuitarLockerRepository(MyGuitarLockerContext context, ILogger<MyGuitarLockerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddSoundClip(string InstrumentName, SoundClip newSoundClip, string userName)
        {
            var theInstrument = GetInstrumentByName(InstrumentName, userName);
            theInstrument.SoundClips.Add(newSoundClip);
            _context.SoundClips.Add(newSoundClip);
        }

        public void AddImage(string InstrumentName, Image newImage, string userName)
        {
            var theInstrument = GetInstrumentByName(InstrumentName, userName);
            theInstrument.Images.Add(newImage);
            _context.Images.Add(newImage);
        }

        public void AddInstrument(Instrument newInstrument)
        {
            _context.Add(newInstrument);
        }

        public IEnumerable<object> GetAllUsers() {
            IEnumerable<object> users = from User in _context.Users select new {user= User.UserName,pic = User.ProfilePic };
            return users;
        }

        public IEnumerable<Instrument> GetAllInstruments()
        {
            try
            {
                return _context.Instruments.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get Instruments from database", ex);
                return null;
            }
        }

        public IEnumerable<Instrument> GetAllInstrumentsWithSoundClips()
        {
            try
            {
                return _context.Instruments
                    .Include(t => t.SoundClips)
                    .OrderBy(t => t.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get Instruments with SoundClips from database", ex);
                return null;
            }
        }

        public Instrument GetInstrumentByName(string InstrumentName, string userName)
        {
            return _context.Instruments.Include(t => t.SoundClips)
                                 .Where(t => t.Name == InstrumentName && t.UserName == userName)
                                 .Include(t => t.Images).Where(t => t.Name == InstrumentName && t.UserName == userName)
                                 .FirstOrDefault();
        }

        public IEnumerable<Instrument> GetUserInstruments(string name)
        {
            try
            {
                return _context.Instruments
                    .Include(t => t.SoundClips)
                    .Include(t => t.Images)
                    .Where(t => t.UserName == name)
                    .OrderBy(t => t.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get Instruments with SoundClips from database", ex);
                return null;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
