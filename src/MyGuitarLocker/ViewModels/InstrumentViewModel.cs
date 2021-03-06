﻿using MyGuitarLocker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGuitarLocker.ViewModels
{
    public class InstrumentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Thumbnail { get; set; } = "http://www.clipartbest.com/cliparts/aiq/eBa/aiqeBae7T.svg";
        //public DateTime Uploaded { get; set; } = DateTime.UtcNow;
    }
}