using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{
    public GameEventName gameEventName;
    public EventTriggerTiming timing = EventTriggerTiming.OnEnter;

    public float stayTimeBeforeTrigger = 0f;
    float timer;


    public void Trigger()
    {
        GameEvent gameEvent = GameEventManager.Instance.GetGameEvent(gameEventName);
        gameEvent.eventName = gameEventName;
        gameEvent.trigger = gameObject;

        GameEventManager.Instance.ProcessGameEvent(gameEvent);

    }


    public void OnTriggerEnter(Collider other)
    {
        if (timing == EventTriggerTiming.OnEnter)
        {
            Trigger();
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (timing == EventTriggerTiming.OnEnter)
        {
            Trigger();
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (timing == EventTriggerTiming.OnExit)
        {
            Trigger();
        }

        timer = 0;
    }

    public void OnCollisionExit(Collision other)
    {
        if (timing == EventTriggerTiming.OnExit)
        {
            Trigger();
        }

        timer = 0;
    }

    public void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;

        if (timing == EventTriggerTiming.OnStay && timer >= stayTimeBeforeTrigger)
        {
            Trigger();
        } 
    }

    public void OnCollisionStay(Collision other)
    {
        timer += Time.deltaTime;

        if (timing == EventTriggerTiming.OnStay && timer >= stayTimeBeforeTrigger)
        {
            Trigger();
        }
    }

}

public enum EventTriggerTiming
{
    OnEnter = 0,
    OnExit = 1,
    OnStay = 2,
}