using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/api/account")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        [HttpPost("register")]
        public ActionResult RegiserUser([FromBody] RegisterUserDTO dTO)
        {
            
        }
    }
}
