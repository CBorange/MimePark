using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataManager : MonoBehaviour
{
    private static StageDataManager instance;
    private static GameObject container;
    public static StageDataManager GetInstance()
    {
        if (!instance)
        {
            container = new GameObject("StageDataManager");
            instance = container.AddComponent<StageDataManager>();
        }
        return instance;
    }
    public int HaveKey;

}
