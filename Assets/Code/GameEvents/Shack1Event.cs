using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shack1Event : GameEvent
{
    public GameObject player;


    // Start is called before the first frame update
    public override void Initialize()
    {
        eventName = GameEventName.EnterShack1;
    }

    public override IEnumerator Process()
    {
        StartEvent();
        GameEventManager.Instance.ToggleControlLock();

        player = GameObject.FindGameObjectWithTag("Player");

        yield return new WaitForSeconds(1);


        GameEventManager.Instance.ToggleControlLock();

        CompleteEvent();
    }

    

}
