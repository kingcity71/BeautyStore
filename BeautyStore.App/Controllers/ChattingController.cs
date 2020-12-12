using BeautyStore.App.Models;
using BeautyStore.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.App.Controllers
{
    public class ChattingController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public ChattingController(RoleManager<IdentityRole> roleManager, IMessageService messageService, IUserService userService)
            : base(roleManager)
        {
            _messageService = messageService;
            _userService = userService;
        }

        public async Task<IActionResult> Conversations()
        {
            var allUserConversations = await _messageService.GetAllInterlocutors();
            return View(allUserConversations);
        }

        public async Task<IActionResult> Conversation(string interlocutorLogin)//логин собеседника
        {
            var conversationHistory = await _messageService.GetConversationMessages(GetCurrentUser(), interlocutorLogin);
            var currentUserName = (await _userService.GetUser(GetCurrentUser())).FullName;
            var interlocutorName = (await _userService.GetUser(interlocutorLogin)).FullName;

            var viewModel = Activator.CreateInstance<ConversationModel>();

            viewModel.CurrentUserLogin = GetCurrentUser().ToLower();
            viewModel.InterlocutorLogin = interlocutorLogin.ToLower();
            viewModel.Messages = conversationHistory;
            viewModel.UserLoginName = new Dictionary<string, string> { { GetCurrentUser().ToLower(), currentUserName }, { interlocutorLogin.ToLower(), interlocutorName } };

            return View(viewModel);
        }
    }
}