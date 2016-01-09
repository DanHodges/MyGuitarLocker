using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyGuitarLocker.Models
{
    public class MyGuitarLockerUser : IdentityUser
    {
        public DateTime FirstInstrument { get; set; }
        public string ProfilePic { get; set; } =
           "http://www.how-to-draw-cartoons-online.com/image-files/214x250xcartoon_guitar.gif.pagespeed.ic.HlE762aS4n.png";
    }
}