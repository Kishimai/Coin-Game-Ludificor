using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventInfo : MonoBehaviour
{

    public GameObject eventManager;

    public Text currentEvent;

    // Start is called before the first frame update
    void Start()
    {
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
        currentEvent = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string text = eventManager.GetComponent<EventsManager>().chosenEvent;

        if (text != "")
        {
            currentEvent.text = string.Format("Current Event: {0}", eventManager.GetComponent<EventsManager>().chosenEvent);
        }
        else
        {
            currentEvent.text = "Current Event: None";
        }

    }
}
