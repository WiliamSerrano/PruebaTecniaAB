using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Data.Interfaces
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> Add(User user);
        Task<User> UpdateUser(User user);
        Task<User> GetUserToDelete(int id);
        Task<User> DeleteUser(User user);
    }
}
