using BeautyStore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Repositories
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<IEnumerable<string>> GetAllInterLocutors();
        Task<IEnumerable<Message>> GetConversationMessages(string owner, string interpreter);
    }
}
