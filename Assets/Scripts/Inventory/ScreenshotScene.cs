using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
public class ScreenshotScene : MonoBehaviour
{
    private Camera _camera;
    public List<GameObject> sceneObjects;
    public List<InventoryItemData> dataObjects;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    [ContextMenu("Screenshot")]
    private void ProcessScreenshots()
    {
        //TakeScreenshot("Assets/Icons/Testi.png");
        StartCoroutine(Screenshot());
    }

    void TakeScreenshot(string fullPath)
    {
        if (_camera == null)
        {
            _camera = GetComponent<Camera>();
        }

        RenderTexture rt = new RenderTexture(256, 256, 24);
        _camera.targetTexture = rt;
        Texture2D screenshot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        _camera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        _camera.targetTexture = null;
        RenderTexture.active = null;

        if(Application.isEditor)
        {
            DestroyImmediate(rt);
        }
        else
        {
            Destroy(rt);
        }

        byte[] bytes = screenshot.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath, bytes);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    private IEnumerator Screenshot()
    {
        for (int i = 0; i< sceneObjects.Count; i++)
        {
            GameObject obj = sceneObjects[i];
            InventoryItemData data = dataObjects[i];

            obj.gameObject.SetActive(true);

            yield return null;

            TakeScreenshot($"{Application.dataPath}/Icons/{data.name_id}_Icon.png");
            
            yield return null;
            obj.gameObject.SetActive(false);
            Sprite s = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Icons/{data.name_id}_Icon.png");
            if (s != null )
            {
                data.icon = s;
                EditorUtility.SetDirty( data );
            }

            yield return null;
        }
    }
}
#endif
