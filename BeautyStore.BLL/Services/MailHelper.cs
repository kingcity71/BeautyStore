using BeautyStore.Helpers;
using System;
using System.Net;
using System.Net.Mail;

namespace BeautyStore.Services
{
    public class MailHelper : IMailHelper
    {
        public void Send(string to, string title, string msg)
        {
            var from = new MailAddress("beautystorenotifications@gmail.com", "BeautyStore");
            var toObj = new MailAddress(to);
            var m = new MailMessage(from, toObj);
            m.Subject = title;
            m.Body = msg;

            var smtp = new SmtpClient("smtp.gmail.com", 587);
            
            smtp.Credentials = new NetworkCredential("beautystorenotifications@gmail.com", "P@ssw0rd71");

            smtp.EnableSsl = true;
            
            try
            {
                //smtp.SendAsync(m, null);
                smtp.Send(m);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
