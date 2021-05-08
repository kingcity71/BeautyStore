namespace BeautyStore.Helpers
{
    public interface IMailHelper
    {
        void Send(string to, string title, string msg);
    }
}
