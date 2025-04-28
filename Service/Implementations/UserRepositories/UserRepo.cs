using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Entities;
using Dtos.UserDtos;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using Service.Interfaces.UserInterfaces;

namespace Service.Implementations.UserRepositories
{
    public class UserRepo: IUser
    {
        #region Fields
        public readonly AppDbContext _context;
        #endregion

        #region Constructor
        public UserRepo(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<UserResponse> RegisterUser(CreateUserDto user)
        {
            var userExists = await _context.users.FirstOrDefaultAsync(x => x.Email == user.Email);
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
            await _context.users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return new UserResponse { Id = newUser.Id, Message = "User created successfully" };
        }
        #endregion
    }
}
