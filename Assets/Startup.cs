using NewsBoardMessaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Startup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        
        NewsBoard.Subscribe( NewsTopics.SomethingHappened_Vector2, new UnityAction<NewsEvent> (NewsSubscriber));
        
    }

    void NewsSubscriber( NewsEvent newsEvent )
    {
        print(newsEvent.Topic);
        print(newsEvent.Open<Vector2>());
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NewsBoard.PublishNews( NewsTopics.SomethingHappened_Vector2, new Vector2(99, 99));
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            NewsBoard.UnSubscribe( NewsTopics.SomethingHappened_Vector2, NewsSubscriber);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            NewsBoard.Subscribe( NewsTopics.SomethingHappened_Vector2, new UnityAction<NewsEvent> ( NewsSubscriber ) );
        }
    }
}
