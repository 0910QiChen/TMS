using ServiceLayer.DTOs;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUser(int id);
        IEnumerable<UserDTO> GetUsers();
        void Register(UserDTO userDTO);
    }
}