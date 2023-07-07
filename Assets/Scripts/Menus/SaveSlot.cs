using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI currentSceneText;
    [SerializeField] private TextMeshProUGUI savingDateText;

    private Button saveSlotButton;

    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
    }

    public void SetData(GameData data)
    {
        // There's no data for this profileId
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        // There is data for this profileID
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            int profileNum;
            Int32.TryParse(profileId, out profileNum);

            currentSceneText.text = $"Player {profileNum+1}";
            savingDateText.text = System.DateTime.Now.ToString();
        }
    }

    public string GetProfileId()
    {
        return this.profileId;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
    }
}
