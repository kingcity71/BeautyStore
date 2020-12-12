using BeautyStore.Entities.Abstract;
using System;

namespace BeautyStore.Entities
{
    public class Message: Entity
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public string MessageBody { get; set; }
    }
}
