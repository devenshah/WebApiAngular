using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository _repo = null;

        public AccountController()
        {
            _repo = new AuthRepository();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IHttpActionResult> Login(UserModel userModel)
        {
            if (string.IsNullOrWhiteSpace(userModel.UserName) || string.IsNullOrWhiteSpace(userModel.Password))
            {
                return BadRequest("User name and password are required.");
            }
            var user = await _repo.FindUser(userModel.UserName, userModel.Password);
            if (user == null)
            {
                return BadRequest("Invalid user name or password");
            }

            var context = Request.GetOwinContext();

            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Name, user.UserName) 
                //add more claims here
            };
            //set context to authenticated
            var cookiesIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);
            context.Authentication.SignIn(cookiesIdentity);

            //generate access token
            //create oAuth identity and add claims to it
            var oAuthIdentity = new ClaimsIdentity(claims, Startup.OAuthServerOptions.AuthenticationType);            

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                        "userName", user.UserName
                    }
                });

            var ticket = new AuthenticationTicket(oAuthIdentity, props);
            ticket.Properties.IssuedUtc = DateTime.UtcNow;
            ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddMinutes(20);

            return Ok(new { AccessToken = Startup.OAuthServerOptions.AccessTokenFormat.Protect(ticket) });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
