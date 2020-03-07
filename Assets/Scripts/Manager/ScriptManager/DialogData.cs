using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class ScriptData
{
    public string script;
    public string characterName;
    public string characterSprite;
    public string dialogBG;

    public ScriptData(string script, string characterName, string characterSprite, string dialogBG)
    {
        this.script = script;
        this.characterName = characterName;
        this.characterSprite = characterSprite;
        this.dialogBG = dialogBG;
    }
}
[System.Serializable]
public class DialogData
{
    public bool readOnSceneLoad;
    public string endCallback;
    public int dialogLength;
    public ScriptData[] scriptData;

    public void PrintData()
    {
        Debug.Log($"readOnSceneLoad: {readOnSceneLoad}");
        Debug.Log($"endCallback: {endCallback}");
        Debug.Log($"dialogLength: {dialogLength}");
        for (int i = 0; i < scriptData.Length; ++i)
        {
            Debug.Log($"scriptData.script : {scriptData[i].script}");
            Debug.Log($"scriptData.characterName : {scriptData[i].characterName}");
            Debug.Log($"scriptData.characterSprite : {scriptData[i].characterSprite}");
            Debug.Log($"scriptData.dialogBG : {scriptData[i].dialogBG}");
        }
    }
}
