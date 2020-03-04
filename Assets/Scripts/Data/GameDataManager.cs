using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameDataManager : MonoBehaviour
{
    private static GameDataManager instance;
    private static GameObject container;
    public GameSaveData ReadData;
    public static GameDataManager GetInstance()
    {
        if (!instance)
        {
            container = new GameObject("GameDataManager");
            instance = container.AddComponent<GameDataManager>();
        }
        return instance;
    }
    public void SaveData()
    {
        string strData;
        FileStream f = new FileStream("Assets/Resources/TextAssets/GameData.txt", FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(f);
        strData = JsonUtility.ToJson(ReadData);
        sw.WriteLine(strData);
        sw.Close();
    }
    public void LoadData()
    {
        TextAsset data = Resources.Load<TextAsset>("TextAssets/GameData");
        ReadData = JsonUtility.FromJson<GameSaveData>(data.text);
    }
}

