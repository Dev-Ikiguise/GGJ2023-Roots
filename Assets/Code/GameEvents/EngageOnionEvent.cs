using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageOnionEvent : GameEvent
{

    public override void Initialize()
    {
        eventName = GameEventName.EnterShack1;
    }

    public override IEnumerator Process()
    {



        yield return null;
    }
}
