using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecryptCanvas : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Button submitBtn;
    [SerializeField] private Button QuitBtn;
    [SerializeField] private GameObject resolve;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private GameObject goalDoors;

    private bool diaryDecrypted;

    public void LoadData(GameData data)
    {
        diaryDecrypted = data.diaryDecryped;
    }

    public void SaveData(GameData data)
    {
        //
    }
    private void Start()
    {
        Debug.Log("Diary state: " + diaryDecrypted);
        if (diaryDecrypted)
        {
            foreach (Transform t in goalDoors.transform)
            {
                t.gameObject.tag = "goal";
            }
        }

        submitBtn.onClick.AddListener(GetInputOnClick);
        QuitBtn.onClick.AddListener(Close);
    }

    private void Close()
    {
        resolve.SetActive(false);
    }

    public void GetInputOnClick()
    {
        Debug.Log(input.text);
        if (input.text.ToLower() == "king")
        {
            EventManager.OnDiaryDecrypted();
            foreach (Transform t in goalDoors.transform)
            {
                t.gameObject.tag = "goal";
            }
            Close();
        }
        else
        {
            message.text = $"{input.text} is incorrect. Please try again or try to look for clues!";
            foreach (Transform t in goalDoors.transform)
            {
                t.gameObject.tag = "dectypt";
            }
        }
    }

}
