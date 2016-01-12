using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGuitarLocker.Models;

namespace MyGuitarLocker.Models
{
    public class MyGuitarLockerContextSeedData
    {
        private MyGuitarLockerContext _context;
        private UserManager<MyGuitarLockerUser> _userManager;

        public MyGuitarLockerContextSeedData(MyGuitarLockerContext context, UserManager<MyGuitarLockerUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("dano@internet.com") == null)
            {
                //add the user
                var newUser = new MyGuitarLockerUser()
                {
                    UserName = "TheDano",
                    Email = "dano@internet.com",
                    ProfilePic = "http://www.guitartonetalk.com/wp-content/uploads/2014/04/Duane-allman-guitar-tone.jpg",
                    FirstInstrument = DateTime.UtcNow
                };
                await _userManager.CreateAsync(newUser, "P@ssw0rd!");
            }
            if (await _userManager.FindByEmailAsync("bigjim@internet.com") == null)
            {
                //add the user
                var newUser = new MyGuitarLockerUser()
                {
                    UserName = "BigJim",
                    Email = "BigJim@internet.com",
                    ProfilePic = "http://a3.files.biography.com/image/upload/c_fit,cs_srgb,dpr_1.0,h_1200,q_80,w_1200/MTE5NTU2MzE2MTg0NzQxMzg3.jpg",
                    FirstInstrument = DateTime.UtcNow
                };
                await _userManager.CreateAsync(newUser, "P@ssw0rd!");
            }
            if (await _userManager.FindByEmailAsync("telefan@internet.com") == null)
            {
                //add the user
                var newUser = new MyGuitarLockerUser()
                {
                    UserName = "TeleFan",
                    Email = "telefan@internet.com",
                    ProfilePic = "https://vitaminsforpitbulls.com/wp-content/uploads/2013/06/english_bulldog_11.jpg",
                    FirstInstrument = DateTime.UtcNow
                };
                await _userManager.CreateAsync(newUser, "P@ssw0rd!");
            }

            if (!_context.Instruments.Any())
            {
                //Add New Data
                var Lester = new Instrument()
                {
                    Name = "Burst",
                    Make = "Gibson",
                    Model = "Les Paul Burst",
                    Year = 2005,
                    UserName = "TheDano",
                    Thumbnail = "http://www.themusiczoo.com/images/7-09-13/19002_Duane_Allman_59_Les_Paul_Washed_Cherry_VOS_DA59022_1.jpg",
                    SoundClips = new List<SoundClip>()
                    {
                    new SoundClip() {  Title = "The Ocean", Url = "https://s3.amazonaws.com/guitarlocker-pics/tmp/Catalinbread+Dirty+Little+Secret+MKIII.mp3", Description = "Les Paul bridge pickup a supro champamp", Recording_Gear = "iPhone Mic" },
                    new SoundClip() {  Title = "Misty Mountain", Url = "https://s3.amazonaws.com/guitarlocker-pics/tmp/Wampler+Plexi-Drive+Deluxe.mp3", Description = "Bridge pickup into a overdriven recording console", Recording_Gear = "sm57" },
                    },
                    Images = new List<Image>()
                    {
                        new Image() { Title = "Open Buckers", Url = "http://www.themusiczoo.com/images/7-09-13/19002_Duane_Allman_59_Les_Paul_Washed_Cherry_VOS_DA59022_1.jpg", Description = "I really think the lack of pup covers helps high-end clarity"},
                        new Image() { Title = "Lester better Pics", Url = "http://www.vintageandrare.com/uploads/products/36660/1223742/original.jpg", Description = "Notice the flame maple top"}
                    }
                };
                _context.Instruments.Add(Lester);
                _context.SoundClips.AddRange(Lester.SoundClips);
                _context.Images.AddRange(Lester.Images);

                var tele = new Instrument()
                {
                    Name = "Blackgaurd",
                    Make = "Fender",
                    Model = "NoCaster",
                    Year = 2012,
                    Thumbnail = "http://www.themusiczoo.com/images/2-25-13/17896_Fender_Custom_Cruz_52_Telecaster_heavy_relic_JC2073_1.jpg",
                    UserName = "TheDano",
                    SoundClips = new List<SoundClip>()
                    {
                    new SoundClip() {  Title = "Barn Burner", Url = "https://s3.amazonaws.com/guitarlocker-pics/The+Flintstones+Theme+on+Acoustic+Guitar+-+GuitarGamer+Fabio+Lima.mp4", Description = "Bridge pickup a siverface champ", Recording_Gear = "iPhone Mic" },
                    new SoundClip() {  Title = "Slow and Sweet", Url = "https://s3.amazonaws.com/guitarlocker-pics/The+Flintstones+Theme+on+Acoustic+Guitar+-+GuitarGamer+Fabio+Lima.mp4", Description = "Neck pickup into a super reverb", Recording_Gear = "sm57" },
                    },
                    Images = new List<Image>()
                    {
                        new Image() { Title = "Blackgaurd on Black", Url = "http://www.themusiczoo.com/images/2-25-13/17896_Fender_Custom_Cruz_52_Telecaster_heavy_relic_JC2073_1.jpg", Description = "My tele getting photographed in front by a pro"},
                        new Image() { Title = "Porch Picker", Url = "http://www.bryankimsey.com/instruments/52RI%20Tele/P1010024.JPG", Description = "Doing some grilling and telecastering on the porch"}
                    }
                };

                _context.Instruments.Add(tele);
                _context.SoundClips.AddRange(tele.SoundClips);
                _context.Images.AddRange(tele.Images);

                //Add New Data
                var LesterTwo = new Instrument()
                {
                    Name = "Lester",
                    Make = "Gibson",
                    Model = "Les Paul Burst",
                    Year = 2005,
                    UserName = "BigJim",
                    Thumbnail = "http://www.themusiczoo.com/images/7-09-13/19002_Duane_Allman_59_Les_Paul_Washed_Cherry_VOS_DA59022_1.jpg",
                    SoundClips = new List<SoundClip>()
                    {
                    new SoundClip() {  Title = "The Ocean", Url = "https://s3.amazonaws.com/guitarlocker-pics/tmp/Catalinbread+Dirty+Little+Secret+MKIII.mp3", Description = "Les Paul bridge pickup a supro champamp", Recording_Gear = "iPhone Mic" },
                    new SoundClip() {  Title = "Misty Mountain", Url = "https://s3.amazonaws.com/guitarlocker-pics/tmp/Wampler+Plexi-Drive+Deluxe.mp3", Description = "Bridge pickup into a overdriven recording console", Recording_Gear = "sm57" },
                    },
                    Images = new List<Image>()
                    {
                        new Image() { Title = "Open Buckers", Url = "http://www.themusiczoo.com/images/7-09-13/19002_Duane_Allman_59_Les_Paul_Washed_Cherry_VOS_DA59022_1.jpg", Description = "I really think the lack of pup covers helps high-end clarity"},
                        new Image() { Title = "Lester better Pics", Url = "http://www.vintageandrare.com/uploads/products/36660/1223742/original.jpg", Description = "Notice the flame maple top"}
                    }
                };
                _context.Instruments.Add(LesterTwo);
                _context.SoundClips.AddRange(LesterTwo.SoundClips);
                _context.Images.AddRange(LesterTwo.Images);

                var TeleTwo = new Instrument()
                {
                    Name = "Number One",
                    Make = "Fender",
                    Model = "NoCaster",
                    Year = 2012,
                    Thumbnail = "http://www.themusiczoo.com/images/2-25-13/17896_Fender_Custom_Cruz_52_Telecaster_heavy_relic_JC2073_1.jpg",
                    UserName = "BigJim",
                    SoundClips = new List<SoundClip>()
                    {
                    new SoundClip() {  Title = "Barn Burner", Url = "https://s3.amazonaws.com/guitarlocker-pics/The+Flintstones+Theme+on+Acoustic+Guitar+-+GuitarGamer+Fabio+Lima.mp4", Description = "Bridge pickup a siverface champ", Recording_Gear = "iPhone Mic" },
                    new SoundClip() {  Title = "Slow and Sweet", Url = "https://s3.amazonaws.com/guitarlocker-pics/The+Flintstones+Theme+on+Acoustic+Guitar+-+GuitarGamer+Fabio+Lima.mp4", Description = "Neck pickup into a super reverb", Recording_Gear = "sm57" },
                    },
                    Images = new List<Image>()
                    {
                        new Image() { Title = "Blackgaurd on Black", Url = "http://www.themusiczoo.com/images/2-25-13/17896_Fender_Custom_Cruz_52_Telecaster_heavy_relic_JC2073_1.jpg", Description = "My tele getting photographed in front by a pro"},
                        new Image() { Title = "Porch Picker", Url = "http://www.bryankimsey.com/instruments/52RI%20Tele/P1010024.JPG", Description = "Doing some grilling and telecastering on the porch"}
                    }
                };

                _context.Instruments.Add(TeleTwo);
                _context.SoundClips.AddRange(TeleTwo.SoundClips);
                _context.Images.AddRange(TeleTwo.Images);

                //Add New Data
                var LesterThree = new Instrument()
                {
                    Name = "Cherry on Top",
                    Make = "Gibson",
                    Model = "Les Paul",
                    Year = 2005,
                    UserName = "TeleFan",
                    Thumbnail = "http://www.themusiczoo.com/images/7-09-13/19002_Duane_Allman_59_Les_Paul_Washed_Cherry_VOS_DA59022_1.jpg",
                    SoundClips = new List<SoundClip>()
                    {
                    new SoundClip() {  Title = "The Ocean", Url = "https://s3.amazonaws.com/guitarlocker-pics/tmp/Catalinbread+Dirty+Little+Secret+MKIII.mp3", Description = "Les Paul bridge pickup a supro champamp", Recording_Gear = "iPhone Mic" },
                    new SoundClip() {  Title = "Misty Mountain", Url = "https://s3.amazonaws.com/guitarlocker-pics/tmp/Wampler+Plexi-Drive+Deluxe.mp3", Description = "Bridge pickup into a overdriven recording console", Recording_Gear = "sm57" },
                    },
                    Images = new List<Image>()
                    {
                        new Image() { Title = "Open Buckers", Url = "http://www.themusiczoo.com/images/7-09-13/19002_Duane_Allman_59_Les_Paul_Washed_Cherry_VOS_DA59022_1.jpg", Description = "I really think the lack of pup covers helps high-end clarity"},
                        new Image() { Title = "Lester better Pics", Url = "http://www.vintageandrare.com/uploads/products/36660/1223742/original.jpg", Description = "Notice the flame maple top"}
                    }
                };
                _context.Instruments.Add(LesterThree);
                _context.SoundClips.AddRange(LesterThree.SoundClips);
                _context.Images.AddRange(LesterThree.Images);

                var TeleThree = new Instrument()
                {
                    Name = "Fifty Two",
                    Make = "Fender",
                    Model = "Telecaster",
                    Year = 2012,
                    Thumbnail = "http://www.themusiczoo.com/images/2-25-13/17896_Fender_Custom_Cruz_52_Telecaster_heavy_relic_JC2073_1.jpg",
                    UserName = "TeleFan",
                    SoundClips = new List<SoundClip>()
                    {
                    new SoundClip() {  Title = "Barn Burner", Url = "https://s3.amazonaws.com/guitarlocker-pics/The+Flintstones+Theme+on+Acoustic+Guitar+-+GuitarGamer+Fabio+Lima.mp4", Description = "Bridge pickup a siverface champ", Recording_Gear = "iPhone Mic" },
                    new SoundClip() {  Title = "Slow and Sweet", Url = "https://s3.amazonaws.com/guitarlocker-pics/The+Flintstones+Theme+on+Acoustic+Guitar+-+GuitarGamer+Fabio+Lima.mp4", Description = "Neck pickup into a super reverb", Recording_Gear = "sm57" },
                    },
                    Images = new List<Image>()
                    {
                        new Image() { Title = "Blackgaurd on Black", Url = "http://www.themusiczoo.com/images/2-25-13/17896_Fender_Custom_Cruz_52_Telecaster_heavy_relic_JC2073_1.jpg", Description = "My tele getting photographed in front by a pro"},
                        new Image() { Title = "Porch Picker", Url = "http://www.bryankimsey.com/instruments/52RI%20Tele/P1010024.JPG", Description = "Doing some grilling and telecastering on the porch"}
                    }
                };

                _context.Instruments.Add(TeleThree);
                _context.SoundClips.AddRange(TeleThree.SoundClips);
                _context.Images.AddRange(TeleThree.Images);

                _context.SaveChanges();
            }
        }
    }
}