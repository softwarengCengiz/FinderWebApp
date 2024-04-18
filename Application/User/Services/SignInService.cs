using Application.User.Contract;
using Application.User.Interfaces;
using AutoMapper;
using Azure;
using Azure.Core;
using Domain.User;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Services
{
    public class SignInService : ISignInService
    {

        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public SignInService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserDto> SignIn(UserDto user)
        {
            var currentUser = _db.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == HashPassword(user.Password));
            if (currentUser != null)
            {
                var dto = new UserDto
                {
                    Id = currentUser.Id,
                    Name = currentUser.Name,
                    Surname = currentUser.Surname,
                    Email = currentUser.Email,
                    Password = currentUser.Password,
                    Role = currentUser.Role
                };
                return dto;
            }

            return new UserDto();
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Hash the input.
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string.
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
