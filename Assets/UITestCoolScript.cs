using NewsBoardMessaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITestCoolScript : MonoBehaviour
{
    void Start()
    {
        NewsBoard.SubscribeWithId( NewsTopics.SomethingHappened_Vector2, new UnityAction<NewsEvent> (MyCallback), this.GetInstanceID().ToString());

    }

    void MyCallback( NewsEvent newsEvent )
    {
        Text myText = GetComponent<Text>();

        myText.text = newsEvent.ToString();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
