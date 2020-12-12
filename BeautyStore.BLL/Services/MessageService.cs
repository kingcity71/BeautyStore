using BeautyStore.Entities;
using BeautyStore.Interfaces.Helpers;
using BeautyStore.Interfaces.Repositories;
using BeautyStore.Interfaces.Services;
using BeautyStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyStore.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<string>> GetAllInterlocutors()
            => await _messageRepository.GetAllInterLocutors();



        public async Task<IEnumerable<MessageModel>> GetConversationMessages(string owner, string interlocutor)
        {
            var entities = await _messageRepository.GetConversationMessages(owner, interlocutor);
            var models = entities.Select(msg => _mapper.Map<Message, MessageModel>(msg)).ToList();
            return models;
        }

        public async Task PutMessage(MessageModel model)
        {
            var entity = _mapper.Map<MessageModel, Message>(model);
            await _messageRepository.Create(entity);
        }
    }
}
