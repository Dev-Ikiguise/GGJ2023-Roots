using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shack1Event : GameEvent
{
    public GameObject player;

    public GameObject monsterPrefab;
    GameObject monster;
    public GameObject WalkPoint1;
    public GameObject WalkPoint2;

    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;


    // Start is called before the first frame update
    public override void Initialize()
    {
        eventName = GameEventName.EnterShack1;
    }

    public override IEnumerator Process()
    {
        StartEvent();
        GameEventManager.Instance.ToggleControlLock();

        Debug.Log(eventName +" triggered.");

        yield return new WaitForSeconds(1);



        GameEventManager.Instance.ToggleControlLock();

        CompleteEvent();
    }

    

}
