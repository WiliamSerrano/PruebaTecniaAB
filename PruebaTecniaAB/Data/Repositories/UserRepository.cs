using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using PruebaTecniaAB.Data.Interfaces;
using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Data.Repositories
{

    public class UserRepository : IUserRepository
    {

        private readonly DBVENTASContext _dbVentasContext;

        public UserRepository(DBVENTASContext context)
        {

            _dbVentasContext = context;

        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _dbVentasContext.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int idUser)
        {
            User oUser = new User();

            if (idUser != 0)
            {

                oUser = await  _dbVentasContext.Users.FindAsync(idUser);

            }

            return oUser;
        }

        public async Task<User> Add(User oUser)
        {

            await _dbVentasContext.Users.AddAsync(oUser);
            
            await _dbVentasContext.SaveChangesAsync();

            return oUser;
        }

        public async Task<User> UpdateUser(User oUserUp)
        {

            var existingUser = await _dbVentasContext.Users.FindAsync(oUserUp.IdUser);

            if (existingUser == null)
            {
                throw new Exception("User not found"); 
            }

            existingUser.FirstName = oUserUp.FirstName;
            existingUser.LastName = oUserUp.LastName;
            existingUser.Mail = oUserUp.Mail;
            existingUser.Password = oUserUp.Password;
            existingUser.Role = oUserUp.Role;

            _dbVentasContext.Users.Update(existingUser);
            _dbVentasContext.Entry(existingUser).State = EntityState.Modified;
            await _dbVentasContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User> GetUserToDelete(int idUser)
        {
            User oUser = await _dbVentasContext.Users.Where(e => e.IdUser == idUser).FirstOrDefaultAsync();

            return oUser;

        }

        public async Task<User> DeleteUser(User user)
        {
            _dbVentasContext.Remove(user);
            await _dbVentasContext.SaveChangesAsync();

            return user;
        }
    }
}
