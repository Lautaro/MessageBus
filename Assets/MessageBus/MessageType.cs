using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.MessageBus
{
    public class MessageType
    {
        public MessageTopics MessageName;
        
        Dictionary<string, UnityAction<MessageCallback>> Subscribers = new Dictionary<string, UnityAction<MessageCallback>>();

        public MessageType(MessageTopics messageName, string listenerId, UnityAction<MessageCallback> listener)
        {
    
            MessageName = messageName;
            Subscribers.Add(listenerId, listener);
            Debug.Log($"Added subscriber ID {listenerId}. Total listeners: {Subscribers.Count()}");
        }

        public void AddSubscriber(string listenerId, UnityAction<MessageCallback> listener)
        {
            Subscribers.Add(listenerId, listener);
            Debug.Log($"Added subscriber ID {listenerId}. Total listeners: {Subscribers.Count()}");
        }

        public bool CheckForId(string id)
        {
            return Subscribers.ContainsKey(id);
        }

        public bool CheckForListener(UnityAction<MessageCallback> listener)
        {
            return Subscribers.ContainsValue(listener);
        }

        public void RemoveSubscriberByKey(string id)
        {
            Subscribers.Remove(id);
        }

        public void RemoveSubscriberByListener(UnityAction<MessageCallback> listener)
        {
            foreach (var item in Subscribers.Where(kvp => kvp.Value == listener).ToList())
            {
                Subscribers.Remove(item.Key);
            }
        }

        public void PublishThis<T>(T message)
        {

            var callbackMessage = new MessageCallback()
            {
                MessageTopic = MessageName,
                Message = message
            };

            Subscribers.Values.ToList().ForEach(sub =>
            {
                sub.Invoke(callbackMessage);
            });
        }
    }
}
