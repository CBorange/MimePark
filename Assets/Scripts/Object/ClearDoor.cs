using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearDoor : MonoBehaviour
{
    public int NextScene;
    private void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(NextScene);
    }
}
