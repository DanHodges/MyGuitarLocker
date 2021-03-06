﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wildermuth.ViewModels
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Url { get; set; }
        public DateTime Uploaded { get; set; } = DateTime.UtcNow;
        public string Description { get; set; }
    }
}
