using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class LightToggle : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject lamps;
    [SerializeField] private GameObject candle1;
    [SerializeField] private GameObject candle2;

    private bool lightsOn;
    public void LoadData(GameData data)
    {
        lightsOn = data.lightsOn;
    }

    public void SaveData(GameData data)
    {
        data.lightsOn = lightsOn;
    }
    private void OnEnable()
    {
        EventManager.TorchInHand += LightsOn;
    }
    private void OnDisable()
    {
        EventManager.TorchInHand -= LightsOn;
    }

    private void Start()
    {
        if (lightsOn)
        {
            LightsOn();
        }
    }
    private void LightsOn()
    {
        lamps.SetActive(true);
        candle1.SetActive(true);
        candle2.SetActive(true);
        lightsOn = true;
        EventManager.OnFirstPartSolved();
    }
}