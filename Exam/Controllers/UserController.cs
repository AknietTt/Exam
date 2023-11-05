using Exam.Infrastructure.DTOs;
using Exam.Infrastructure.Service;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase {
        private readonly UserService _userService;
       // private readonly IContextService _contextService;
        public UserController(UserService userService) {
            _userService = userService;
           
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUser() {
            return Ok("Good");
        }

        [HttpPost("auth")]
        public async Task<string> Authorize([FromBody] AuthDto authorizeDto) => await _userService.Authorize(authorizeDto);

        [HttpPost("registration")]
        public async Task Registration([FromBody] RegisterDto registration) => await _userService.Registration(registration);
    }

}
