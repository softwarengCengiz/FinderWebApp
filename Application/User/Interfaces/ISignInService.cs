using Application.User.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Interfaces
{
    public interface ISignInService
    {
        Task<UserDto> SignIn(UserDto user);
    }
}
