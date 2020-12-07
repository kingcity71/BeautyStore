using BeautyStore.Entities;
using BeautyStore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BeautyStore.DAL.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) 
            : base(configuration){}

        public async Task<User> GetItem(string email)
        {
            using var ctx = GetContext();
            return await ctx.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
