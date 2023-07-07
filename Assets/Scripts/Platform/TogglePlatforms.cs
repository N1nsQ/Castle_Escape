using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TogglePlatforms : MonoBehaviour
{
    [Header("Platform Toggle")]
    [SerializeField] private float hideAngle;
    [SerializeField] private float showAngle;
    [SerializeField] private GameObject hidePlatform;
    [SerializeField] private GameObject showPlatform;

    [Header("Secondary Platform Toggle")]
    [SerializeField] private float hideAngle2;
    [SerializeField] private float showAngle2;
    [SerializeField] private GameObject hidePlatform2;
    [SerializeField] private GameObject showPlatform2;

    private void Update()
    {
        if (hidePlatform != null) HideCondition(hidePlatform, hideAngle);
        if (hidePlatform2 != null) HideCondition(hidePlatform2, hideAngle2);

        if (showPlatform != null) ShowCondition(showPlatform, showAngle);
        if (showPlatform2 != null) ShowCondition(showPlatform2, showAngle2);
    }

    private void ShowCondition(GameObject showobject, float _showAngle)
    {
        if (_showAngle == 314)
        {
            if (this.transform.eulerAngles.x >= 314)
            {
                showobject.SetActive(true);
            }
        }

        if (_showAngle == 46)
        {
            if (this.transform.eulerAngles.x <= 46)
            {
                showobject.SetActive(true);
            }
        }
    }

    private void HideCondition(GameObject hideObject, float _hideAngle)
    {
        if (_hideAngle == 314)
        {
            if (this.transform.eulerAngles.x >= 314)
            {
                hideObject.SetActive(false);
            }
        }

        if (_hideAngle == 46)
        {
            if (this.transform.eulerAngles.x <= 46)
            {
                hideObject.SetActive(false);
            }
        }
    }
}
