using BeautyStore.Models.Abstract;
using System;

namespace BeautyStore.Models
{
    public class MessageModel : Model
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public string MessageBody { get; set; }

    }
}