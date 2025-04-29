using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Entities;
using Dtos.UserDtos;
using Microsoft.EntityFrameworkCore;
using Service.AuthToken;
using Service.Interfaces.TokenInterfaces;
using Service.Interfaces.UserInterfaces;

namespace Service.Implementations.UserRepositories
{
    public class UserRepo: IUser
    {
        #region Fields
        public readonly AppDbContext _context;
        public readonly IToken _tokenLogic;
        #endregion

        #region Constructor
        public UserRepo(AppDbContext context, IToken tokenLogic)
        {
            _context = context;
            _tokenLogic = tokenLogic;
        }
        #endregion

        #region Public Methods
        public async Task<UserResponse> RegisterUser(CreateUserDto user)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (userExists != null)
            {
                throw new Exception("User already exists");
            }
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                CreatedAt = DateTime.UtcNow,
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return new UserResponse { Id = newUser.Id, Message = "User created successfully" };
        }

        public async Task<string> LogInUser(LogInUserDto user)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (userExists == null)
            {
                throw new Exception("User does not exist");
            }
            var passwordMatch = BCrypt.Net.BCrypt.Verify(user.Password, userExists.Password);
            if (!passwordMatch)
            {
                throw new Exception("Password is incorrect");
            }
            var refreshToken = await _tokenLogic.CreateRefreshTokenAsync(userExists);
            var accessToken = _tokenLogic.CreateAccessToken(userExists);
            await _context.SaveChangesAsync();
            return accessToken;
        }
        #endregion
    }
}
