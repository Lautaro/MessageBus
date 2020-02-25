using Assets;
using Assets.MessageBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITestCoolScript : MonoBehaviour
{
    void Start()
    {
        MessageBus.SubscribeWithId(MessageTopics.SomethingHappened_Vector2, new UnityAction<MessageCallback>(MyCallback), this.GetInstanceID().ToString());

    }

    void MyCallback(MessageCallback myMessage)
    {
        Text myText = GetComponent<Text>();

        myText.text = myMessage.ToString();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
