using System.Collections;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class _DevScreenSaver : MonoBehaviour
{
    public void TakeScreenAndSave()
    {
        StartCoroutine(screen());
    }
    IEnumerator screen()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        var _dateTime = DateTime.Now;
        string dT = $"{_dateTime.Day}_{_dateTime.Month}_{_dateTime.Year}_{_dateTime.Hour}_{_dateTime.Minute}_{_dateTime.Second}";

        string desP = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string filePath = Path.Combine(desP, $"ca_{dT}.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        Destroy(ss);
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(_DevScreenSaver))]
class MyButtonInInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var _parentScript = (_DevScreenSaver)target;

        if (_parentScript == null) return;
        if(GUILayout.Button("Сделать скрин"))
        {
            _parentScript.TakeScreenAndSave();
        }
    }
}
#endif