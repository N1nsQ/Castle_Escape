using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EventManager;

public class Triggers : MonoBehaviour//, IDataPersistence
{
    /*
    private string playedScene;

    public void LoadData(GameData data)
    {
        //nothing
    }
    public void SaveData(GameData data)
    {
        if (playedScene != null)
        {
            data.scenesCompleted.Add(playedScene, true);
            data.lastFinishedScene = playedScene;
        }
    }*/

    private void OnTriggerEnter(Collider collision)
    {
        /*
        if (collision.gameObject.tag == "goal")
        {
            playedScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadSceneAsync("MovingBtwnScene");

            transform.position = new Vector3((float)-5.7, (float)0.2500001, (float)-9.93);
            transform.rotation = new Quaternion((float)0.00000, (float)0.65060, (float)0.00000, (float)0.75942);

            DataPersistenceManager.instance.SaveGame();

            
            OnTimerStop();
            OnFinishSuccess();
            

        }*/

        if (collision.gameObject.tag == "ChooseDoor")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
