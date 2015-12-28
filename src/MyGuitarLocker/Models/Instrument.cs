using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGuitarLocker.Models
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
        public ICollection<SoundClip> SoundClips { get; set; }
    }
}
