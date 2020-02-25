using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MessageBus
{
    public class MessageCallback
    {
        public MessageTopics MessageTopic;
        public object Message;

        public T Open<T>() 
        {
            return (T)Message ;
        }

        public override string ToString()
        {
            return MessageTopic.ToString();

        }
    }
}
