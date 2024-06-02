using Application.User.Contract;
using Application.User.Interfaces;
using AutoMapper;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }

        public async Task<bool> ChangeProfilePhoto(string photoUrl, Guid userId)
        {
            try
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    user.Photo = photoUrl;
                    context.Users.Update(user);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserDto> GetUser(Guid userId)
        {
            var dto = new UserDto();
            try
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    dto.Name = user.Name;
                    dto.Surname = user.Surname;
                    dto.Email = user.Email;
                    dto.PhoneNumber = user.PhoneNumber;
                    dto.Photo = user.Photo;
                    dto.Role = user.Role;   
                }

                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid> UpdateUser(UserDto userDto)
        {
            try
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userDto.Id);

                if (user != null)
                {
                    user.Name = userDto.Name;
                    user.Surname = userDto.Surname;
                    user.PhoneNumber = userDto.PhoneNumber;
                    user.Email = userDto.Email;

                    context.Users.Update(user);
                    context.SaveChanges();
                }

                return userDto.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
