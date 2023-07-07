using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueFromCustom : MonoBehaviour
{
    public void OnContinueClicked()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Tutorial");
    }
}
