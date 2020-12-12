using Microsoft.AspNetCore.SignalR;

namespace BeautyStore.App
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
            => connection.User?.Identity.Name;
    }
}
