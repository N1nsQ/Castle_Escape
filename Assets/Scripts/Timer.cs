using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour, IDataPersistence
{
    private TMP_Text _timerText;
    enum TimerType { Countdown, Stopwatch }
    [SerializeField] private TimerType timerType;

    [SerializeField] private float timeToDisplay = 900f;

    private bool _isRunning;

    private void Awake() => _timerText = GetComponent<TMP_Text>();

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
        EventManager.TimerUpdate += EventManagerOnTimerUpdate;
        EventManager.TimerToNull += EventManagerOnTimerToNull;

    }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
        EventManager.TimerUpdate -= EventManagerOnTimerUpdate;
        EventManager.TimerToNull -= EventManagerOnTimerToNull;

    }

    private void EventManagerOnTimerStart() => _isRunning = true;
    private void EventManagerOnTimerStop() => _isRunning = false;
    private void EventManagerOnTimerToNull() => timeToDisplay = 900f;
    private void EventManagerOnTimerUpdate(float value) => timeToDisplay += value;

    private void Update()
    {
        if (!_isRunning) return;
        if (timerType == TimerType.Countdown && timeToDisplay < 0.0f)
        {
            EventManager.OnTimerStop();
            return;
        }

        timeToDisplay += timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        _timerText.text = timeSpan.ToString(@"mm\:ss");
    }

    public void LoadData(GameData data)
    {
        timeToDisplay = data.timeToDisplay;
        Debug.Log("Loading: " + data.timeToDisplay);
    }

    public void SaveData(GameData data)
    {
        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 1)
        {
            data.timeToDisplay = 900f;
            Debug.Log("special time save: " + data.timeToDisplay);
        }
        else
        {
            data.timeToDisplay = timeToDisplay;
            Debug.Log("normal timesave: " + data.timeToDisplay);
        }
    }
}
