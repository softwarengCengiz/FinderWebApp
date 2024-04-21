using Application.User.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Interfaces
{
    public interface IUserService
    {
        Task<Guid> UpdateUser(UserDto userDto);
    }
}
