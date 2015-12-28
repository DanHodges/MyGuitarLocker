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

        [Required]
        [StringLength(255, MinimumLength =5)]
        public string Name { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        [Required]
        public DateTime Arrival { get; set; }
    }
}