using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public void Load_Stage1()
    {
        SceneManager.LoadScene(2);
    }
    public void Load_Stage2()
    {
        SceneManager.LoadScene(3);
    }
    public void Load_Stage3()
    {
        SceneManager.LoadScene(4);
    }
    public void Load_Stage4()
    {
        SceneManager.LoadScene(5);
    }
}
