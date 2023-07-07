using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VipuSceneScript : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject switch1;
    [SerializeField] private GameObject switch2;
    [SerializeField] private GameObject switch3;
    [SerializeField] private GameObject switch4;
    [SerializeField] private GameObject switch5;
    [SerializeField] private GameObject switch6;

    [SerializeField] private GameObject fence1;
    [SerializeField] private GameObject fence2;
    [SerializeField] private GameObject fence3;
    [SerializeField] private GameObject fence4;
    [SerializeField] private GameObject goalDoor;

    private bool vipuPuzzle;
    public void LoadData (GameData data)
    {
        vipuPuzzle = data.vipuPuzzle;
    }

    public void SaveData (GameData data)
    {
        data.vipuPuzzle = vipuPuzzle;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (vipuPuzzle)
        {
            GameObject.Destroy(fence1);
            GameObject.Destroy(fence2);
            GameObject.Destroy(fence3);
            GameObject.Destroy(fence4);
            goalDoor.gameObject.tag = "goal";
        }
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCheck();
    }

    // Up: 315, Down: 45
    private void SwitchCheck()
    {
        if (switch1.transform.eulerAngles.x >= 314 &&
            switch2.transform.eulerAngles.x <= 46 &&
            switch3.transform.eulerAngles.x >= 314 &&  
            switch4.transform.eulerAngles.x >= 314 &&
            switch5.transform.eulerAngles.x <= 46 &&
            switch6.transform.eulerAngles.x <= 46)
        {
            Debug.Log("switches right");
            vipuPuzzle = true;

            GameObject.Destroy(fence1);
            GameObject.Destroy(fence2);
            GameObject.Destroy(fence3);
            GameObject.Destroy(fence4);
            goalDoor.gameObject.tag = "goal";
        }
        else
        {
            Debug.Log("switches wrong");
            /*Debug.Log(switch1.transform.eulerAngles.x);
            Debug.Log(switch2.transform.eulerAngles.x);
            Debug.Log(switch3.transform.eulerAngles.x);
            Debug.Log(switch4.transform.eulerAngles.x);
            Debug.Log(switch5.transform.eulerAngles.x);
            Debug.Log(switch6.transform.eulerAngles.x);*/
        }
    }
}
