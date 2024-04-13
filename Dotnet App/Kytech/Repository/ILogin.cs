using Kytech.models;

namespace Kytech.Repository;
     public interface ILogin{
        Task<Login> AddUser(Login User);
        Task<bool> DeleteUser(string username);
        Task<Login> GetUserById(string username);
    }
