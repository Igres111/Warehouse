using Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers;
using Service.Interfaces.UserInterfaces;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields
        public readonly IUser _UserMethods;
        #endregion

        #region Constructor
        public UserController(IUser UserMethods)
        {
            _UserMethods = UserMethods;
        }
        #endregion

        #region POST Endpoints
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {
            if(!ModelState.IsValid)
            {
                throw new Exception("Invalid model state");
            }
            var result = await _UserMethods.CreateUser(user);
            return Ok(result);
        }
        #endregion
    }
}
