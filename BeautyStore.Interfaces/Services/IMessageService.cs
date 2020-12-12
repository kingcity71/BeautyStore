using BeautyStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<string>> GetAllInterlocutors();
        Task<IEnumerable<MessageModel>> GetConversationMessages(string owner, string interlocutor);
        Task PutMessage(MessageModel model);
    }
}
