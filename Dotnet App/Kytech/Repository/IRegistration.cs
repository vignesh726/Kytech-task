using Kytech.models;

namespace Kytech.Repository;

     public interface IRegistration
    {
        Task<UserDetail> AddUser(UserDetail User);
        Task<bool> DeleteUser(int id);

        Task<UserDetail> GetUserById(int id);

        Task<IEnumerable<UserDetail>> GetAllUsers();

        Task<UserDetail> UpdateUser(UserDetail User, int id);
    }
