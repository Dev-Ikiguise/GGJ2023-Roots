using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; set; }

    public bool isPaused = false;
    public bool areControlsLocked = false;

    bool isQuitting = false;
    public GameObject confirmationUI;
    public AudioSource pauseAudioSource;

    public bool useTimescalePause = true;
    public bool useFindObjectPause = true;
    public List<string> findObjectPauseTags;
    List<GameObject> objectsToPause;

    public List<GameEvent> gameEvents;
    public List<GameEventName> gameEventListByName;

    GameEvent currentGameEvent;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        BuildGameEventList();

        GetObjectsToPause();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Pause") && !areControlsLocked)
        {
            isPaused = !isPaused;
            if (pauseAudioSource != null)
            {
                if (isPaused)
                {
                    SoundManager.Instance.bgmMusicManager.Pause();
                    pauseAudioSource.Play();
                }
                else
                {
                    pauseAudioSource.Stop();
                    SoundManager.Instance.bgmMusicManager.UnPause();
                }
            }

            if (useTimescalePause)
            {
                Time.timeScale = (isPaused ? 0 : 1);
            }

            if (useFindObjectPause && isPaused)
            {
                //GetObjectsToPause();
                //StartCoroutine(PauseGameObjects());
            }
            
        }

        if (Input.GetButtonDown("Quit") && !areControlsLocked)
        {
            isQuitting = !isQuitting;

            if (isQuitting && confirmationUI == null)
            {
                Application.Quit();
            }
            else
            {
                confirmationUI.SetActive(isQuitting);
            }
        }

        if (isQuitting)
        {
            // Do menu navigation for buttons?
        }

    }

    void GetObjectsToPause ()
    {
        if (findObjectPauseTags.Count == 0)
        {
            useFindObjectPause = false;
        }
        else
        {
            objectsToPause = new List<GameObject>();

            foreach (string s in findObjectPauseTags)
            {
                foreach (GameObject go in GameObject.FindGameObjectsWithTag(s))
                {
                    objectsToPause.Add(go);
                }
            }
        }
    }

    IEnumerator PauseGameObjects()
    {
        // This is a bad idea, not implemented

        foreach (GameObject go in objectsToPause)
        {
            
        }

        while (isPaused)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    void BuildGameEventList()
    {
        gameEvents = new List<GameEvent>();
        foreach (GameEventName name in gameEventListByName)
        {
            GameEvent gameEvent = EventList.GetEvent(name);
            gameEvent.Initialize();
            gameEvents.Add(gameEvent);
        }
    }

    public void ToggleControlLock()
    {
        areControlsLocked = !areControlsLocked;
    }

    public void ToggleControlLock(bool val)
    {
        areControlsLocked = val;
    }

    public List<GameEvent> GetStartedEvents()
    {
        return gameEvents.Where(x => x.isStarted = true).ToList();
    }

    public List<GameEvent> GetCompletedEvents()
    {
        return gameEvents.Where(x => x.isCompleted = true).ToList();
    }

    public GameEvent GetGameEvent(GameEventName gameEventName)
    {
        GameEvent e = gameEvents.Where(x => x.eventName == gameEventName).FirstOrDefault();
        return gameEvents.Where(x => x.eventName == gameEventName).FirstOrDefault();
    }

    public void ProcessGameEvent(GameEvent gameEvent)
    {
        StartCoroutine(gameEvent.Process());
    }

    public void ProcessGameEvent(GameEventName name)
    {
        GameEvent gameEvent = GetGameEvent(name);
        StartCoroutine(gameEvent.Process());
    }
}

[System.Serializable]
public abstract class GameEvent
{
    public GameEventName eventName;
    public GameObject trigger;
    public bool isStarted = false;
    public bool isCompleted = false;
    public bool isRepeatable = false;
    public int completeCount = 0;

    public abstract IEnumerator Process();
    public abstract void Initialize();

    public void ResetGameEvent(bool resetIsStarted = true, bool resetIsCompleted = true, bool setTriggerCount = true, int newTriggerCountValue = 0)
    {
        if (resetIsStarted)
        {
            isStarted = false;
        }

        if (resetIsCompleted)
        {
            isCompleted = false;
        }

        if (setTriggerCount)
        {
            completeCount = newTriggerCountValue;
        }
    }

    public void SetRepeatable(bool isRepeatableValue)
    {
        isRepeatable = isRepeatableValue;
    }


    public void StartEvent()
    {
        if (completeCount == 0 || isRepeatable)
        {
            isStarted = true;
        }
    }

    public void CompleteEvent()
    {
        isStarted = false;
        isCompleted = true;
        completeCount += 1;
    }
}

[System.Serializable]
public static class EventList
{
    public static Dictionary<GameEventName, GameEvent> events = new Dictionary<GameEventName, GameEvent>();

    public static GameEvent GetEvent(GameEventName gameEventName)
    {
        GameEvent gameEvent;
        bool hasValue = events.TryGetValue(gameEventName, out gameEvent);
        return (hasValue ? gameEvent : null);
    }

    static EventList()
    {
        events.Add(GameEventName.EnterShack1, new Shack1Event());
        events.Add(GameEventName.EnterCarrotFarm, new EnterCarrotFarmEvent());
        events.Add(GameEventName.EngageOnionMonster, new EngageOnionEvent());

    }
}

public enum GameEventName
{
    None = 0,
    EnterShack1 = 1,
    EnterCarrotFarm = 2,
    EngageOnionMonster = 3,

}