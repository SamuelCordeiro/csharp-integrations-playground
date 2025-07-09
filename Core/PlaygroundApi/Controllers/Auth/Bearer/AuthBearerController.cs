using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playground.Core.Auth.Bearer;
using Playground.Core.Models;
using Playground.Core.Repositories;

namespace Playground.Api.Controllers.Auth.Bearer
{
    [ApiController]
    [Route("Auth/Bearer/[controller]")]
    public class AuthBearerController : Controller
    {
        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Login([FromBody] UserLogin model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
            {
                return NotFound();
            }

            var token = new TokenService().Generate(user, 5);

            var result = new
            {
                username = model.Username,
                token,
            };

            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(result), "application/json");
        }
    }
}
