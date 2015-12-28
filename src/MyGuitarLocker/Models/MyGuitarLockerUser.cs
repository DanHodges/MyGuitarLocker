using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyGuitarLocker.Models
{
    public class MyGuitarLockerUser : IdentityUser
    {
        public DateTime FirstInstrument { get; set; }
    }
}