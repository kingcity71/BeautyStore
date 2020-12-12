using BeautyStore.Interfaces.Services;
using BeautyStore.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace BeautyStore.App.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
            =>_messageService = messageService;
        
        public async Task Send(string msg, string to)
        {
            await _messageService.PutMessage(new MessageModel { Date = DateTime.Now, MessageBody = msg, From = Context.User.Identity.Name, To = to });
            await Clients.User(to).SendAsync("Send", msg);
        }
    }
}
