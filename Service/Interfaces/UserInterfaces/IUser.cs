using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtos.UserDtos;
using Service.Helpers;

namespace Service.Interfaces.UserInterfaces
{
    public interface IUser
    {
        public Task<UserResponse> CreateUser(CreateUserDto user);
    }
}
