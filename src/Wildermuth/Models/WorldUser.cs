using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Wildermuth.Models
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}