using DataAccess.Entities;
using Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.AuthToken;
using Service.Interfaces.TokenInterfaces;
using Service.Interfaces.UserInterfaces;

namespace Warehouse.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields
        public readonly IUser _UserMethods;
        public readonly IToken _tokenLogic;
        #endregion

        #region Constructor
        public UserController(IUser UserMethods,IToken tokenLogic)
        {
            _UserMethods = UserMethods;
            _tokenLogic = tokenLogic;
        }
        #endregion

        #region POST Endpoints
        [HttpPost("Register-User")]
        public async Task<IActionResult> RegisterUser(CreateUserDto user)
        {
            if(!ModelState.IsValid)
            {
                throw new Exception("Invalid model state");
            }
            var result = await _UserMethods.RegisterUser(user);
            return Ok(result);
        }

        [HttpPost("LogIn-User")]
        public async Task<IActionResult> LogInUser(LogInUserDto user)
        {
            try
            {
                var result = await _UserMethods.LogInUser(user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("refresh-access-token")]
        public async Task<IActionResult> RefreshToken(string token)
        {
                var result = await _tokenLogic.RefreshAccessTokenAsync(token);
                return Ok(result);
        }
        #endregion
    }
}
