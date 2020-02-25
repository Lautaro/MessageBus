using Assets;
using Assets.MessageBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Startup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        MessageBus.Subscribe(MessageTopics.SomethingHappened_Vector2, new UnityAction<MessageCallback>(MyCallback));
        
    }

    void MyCallback(MessageCallback myMessage)
    {
        print(myMessage.MessageTopic);
        print(myMessage.Open<Vector2>());
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MessageBus.PostMessage(MessageTopics.SomethingHappened_Vector2, new Vector2(99, 99));
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            MessageBus.UnSubscribe(MessageTopics.SomethingHappened_Vector2, MyCallback);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            MessageBus.Subscribe(MessageTopics.SomethingHappened_Vector2, new UnityAction<MessageCallback>(MyCallback));
        }
    }
}
