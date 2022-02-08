using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRandomizer : MonoBehaviour
{

    private string[] commonEvents;
    private string[] uncommonEvents;
    private string[] rareEvents;

    private string chosenEvent;

    // Start is called before the first frame update
    void Start()
    {
        commonEvents = GetComponent<EventsManager>().commonEvents;
        uncommonEvents = GetComponent<EventsManager>().uncommonEvents;
        rareEvents = GetComponent<EventsManager>().rareEvents;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string RollNewEvent()
    {
        return ChooseEvent(RandomEventRarity());
    }

    private int RandomEventRarity()
    {
        int eventRarity;

        eventRarity = Random.Range(0, 99);

        return eventRarity;
    }

    private string ChooseEvent(int rarity)
    {
        if (rarity <= 49)
        {
            chosenEvent = commonEvents[Random.Range(0, commonEvents.Length)];
        }
        else if (rarity <= 89)
        {
            chosenEvent = uncommonEvents[Random.Range(0, uncommonEvents.Length)];
        }
        else if (rarity <= 99)
        {
            chosenEvent = rareEvents[Random.Range(0, rareEvents.Length)];
        }

        return chosenEvent;
    }
}
