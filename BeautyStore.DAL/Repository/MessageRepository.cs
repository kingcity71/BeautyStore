using BeautyStore.Entities;
using BeautyStore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyStore.DAL.Repository
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        private const string ADMIN_NAME = "Admin";
        public MessageRepository(IConfiguration configuration) : base(configuration){}

        public async Task<IEnumerable<string>> GetAllInterLocutors()
        {
            using var ctx = GetContext();
            var fromCollection = await ctx.Messages.AsNoTracking()
                .Select(x => x.From)
                .Distinct()
                .ToListAsync();
            var toCollection = await ctx.Messages.AsNoTracking()
                .Select(x => x.To)
                .Distinct()
                .ToListAsync();
            return fromCollection.Union(toCollection).Where(x => !x.IsEqual(ADMIN_NAME)).ToList();
        }

        public async Task<IEnumerable<Message>> GetConversationMessages(string owner, string interpreter)
        {
            using var ctx = GetContext();
            return await ctx.Messages
                .AsNoTracking()
                .Where(x => (x.To.ToLower() == owner.ToLower() && x.From.ToLower() == interpreter.ToLower())
            || (x.To.ToLower() == interpreter.ToLower() && x.From.ToLower() == owner.ToLower()))
                .OrderByDescending(x=>x.Date)
                .ToListAsync();
        }
    }
}
