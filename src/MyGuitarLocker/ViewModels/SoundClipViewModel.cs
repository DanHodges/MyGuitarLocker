﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGuitarLocker.ViewModels
{
    public class SoundClipViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        //public DateTime Uploaded { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
        public string Recording_Gear { get; set; }
    }
}
