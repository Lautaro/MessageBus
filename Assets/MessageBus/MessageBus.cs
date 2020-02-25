using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Assets.MessageBus;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Assets;

public class MessageBus
{
    // 
    static List<MessageType> messageRegistrations = new List<MessageType>();

    public static void SubscribeWithId(MessageTopics messageName, UnityAction<MessageCallback> listener, string id)
    {
        // Make sure this id does not subsribe to this message alreaedy
        if (messageRegistrations.Any(s => s.MessageName == messageName && s.CheckForListener(listener)))
        {
            Debug.LogWarning($"Listener is trying to subsribe to a topic that its already subscribed to! Subscriber ID :{id} Message name :{messageName}  ");
            return;
        }

        // MAKE SURE ACTION AND T HAS SAME TYPE!

        MessageType messageType = messageRegistrations.FirstOrDefault(s => s.MessageName == messageName);

        if (messageType == null)
        {
            messageType = new MessageType(messageName, id, listener);
            messageRegistrations.Add(messageType);
            return;
        }

        messageType.AddSubscriber(id, listener);
     
    }

    public static void Subscribe(MessageTopics messageName, UnityAction<MessageCallback> listener)
    {
        // Check if action is anonymous 
        if (listener.Method.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any())
        {
            Debug.LogError("An anonymous method is trying to register a message bus subscription without providing an ID. This is not allowed since it would be impossible to cancel subscription. Get your shit together!");
        };

        SubscribeWithId(messageName, listener, "");

    }

    public static void UnSubscribe(MessageTopics messageName, UnityAction<MessageCallback> listener)
    {
        var subscription = messageRegistrations.FirstOrDefault(s => s.MessageName == messageName);
        subscription.RemoveSubscriberByListener(listener);
    }

    public static void UnSubscribeById(MessageTopics messageName, string id)
    {
        messageRegistrations.Where(mr => mr.MessageName == messageName).ToList()
            .ForEach(s => s.RemoveSubscriberByKey(id));
    }

    public static void PostMessage<T>(MessageTopics messageName, T message)
    {
        var registration = messageRegistrations.FirstOrDefault(s => s.MessageName == messageName);
        if (registration == null )
        {
            Debug.LogWarning($"Message has been posted but no one is listening! MessageName{messageName}");
        }
        registration.PublishThis<T>(message);
    }
}
