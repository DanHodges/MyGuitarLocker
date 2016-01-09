using System;
using Microsoft.AspNet.Mvc;
using System.Web.Http;
using Microsoft.AspNet.Authorization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.Mvc.WebApiCompatShim;

namespace Wildermuth.Controllers.API
{
    [Authorize]
    [Route("api/Sign")]
    public class SignController : ApiController
    {
        [HttpGet("")]
        public ResponseMessageResult Get([FromUri] string to_sign)
        {
            var content = new StringContent(SignData(to_sign));
            //content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Text.Plain);
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
            return ResponseMessage(response);
        }

        private static string SignData(string to_sign)
        {
            using (var signature = new HMACSHA1(key: Encoding.UTF8.GetBytes("secret")))
            {
                var bytes = Encoding.UTF8.GetBytes(to_sign);
                var hash = signature.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
