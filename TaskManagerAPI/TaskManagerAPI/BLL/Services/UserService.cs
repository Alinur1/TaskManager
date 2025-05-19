using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.BLL.Interfaces;
using TaskManagerAPI.DAL.Models;
using TaskManagerAPI.DAL.TaskManagerDataContext;

namespace TaskManagerAPI.BLL.Services
{
    public class UserService : IUser
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                throw new Exception("This user already exists");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if(existingUser == null)
            {
                return false;
            }
            _context.Remove(existingUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> UpdateUserAsync(User user)
        {            
            var existingUser = await _context.Users.FindAsync(user.Id);
            if(existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync(); ;
            }
            return existingUser ?? throw new KeyNotFoundException("User not found");
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
