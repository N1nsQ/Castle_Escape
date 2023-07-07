using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction TimerStart;
    public static event UnityAction TimerStop;
    public static event UnityAction TimerToNull;
    public static event UnityAction<float> TimerUpdate;
    public static event UnityAction Success;
    public static event UnityAction NewBox;
    public static event UnityAction OpenInventory;
    public static event UnityAction<string> GetInventoryItem;
    public static event UnityAction CloseInventory;
    public static event UnityAction ShowStory;
    public static event UnityAction ShowOneStory;
    public static event UnityAction ShowDiary;
    public static event UnityAction DiaryDecrypted;
    public static event UnityAction TorchInHand;
    public static event UnityAction TakeTorch;
    public static event UnityAction FirstPartSolved;
    public static event UnityAction SecondPartSolved;
    public static event UnityAction MusicIn;
    public static event UnityAction MusicOut;
    public static event UnityAction PlatformUnActivated;


    public static void OnTimerStart() => TimerStart?.Invoke();
    public static void OnTimerStop() => TimerStop?.Invoke();
    public static void OnTimerToNull() => TimerToNull?.Invoke();
    public static void OnTimerUpdate(float value) => TimerUpdate?.Invoke(value);

    public static void OnFinishSuccess() => Success?.Invoke();
    public static void OnBoxStucked() => NewBox?.Invoke();

    public static void OnOpenInventory() => OpenInventory?.Invoke();
    public static void OnCloseInventory() => CloseInventory?.Invoke();
    public static void OnGetInventoryItem(string name) => GetInventoryItem?.Invoke(name);
    public static void OnShowStory() => ShowStory?.Invoke();
    public static void OnShowOneStory() => ShowOneStory?.Invoke();

    public static void OnShowDiary() => ShowDiary?.Invoke();
    public static void OnDiaryDecrypted() => DiaryDecrypted?.Invoke();

    public static void OnTorchInHand() => TorchInHand?.Invoke();

    public static void OnTakeTorch() => TakeTorch?.Invoke();

    public static void OnFirstPartSolved() => FirstPartSolved?.Invoke();
    public static void OnSecondPartSolved() => SecondPartSolved?.Invoke();

    public static void OnMusicIn() => MusicIn?.Invoke();

    public static void OnMusicOut() => MusicOut?.Invoke();

    public static void OnPlatformUnActivated() => PlatformUnActivated?.Invoke();
}
