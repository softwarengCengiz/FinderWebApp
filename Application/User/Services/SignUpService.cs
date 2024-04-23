using Application.Student.Contract;
using Application.Student.Interfaces;
using Application.User.Contract;
using Application.User.Interfaces;
using AutoMapper;
using Azure;
using Domain.Student;
using Domain.User;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public SignUpService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Guid> SignUp(UserDto user)
        {
            try
            {
                var newUser = new Domain.User.User
                {
                    Id = Guid.NewGuid(),
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Password = HashPassword(user.Password),
                    Role = user.Role
                };

                _db.Users.Add(newUser);
                _db.SaveChanges();

                return newUser.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
            
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
