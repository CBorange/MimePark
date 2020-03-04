using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public GameObject TouchToStart;
    public GameObject IntroCutScene;
    private void Start()
    {
        StartCoroutine(TouchToStartTwinkle());
        Screen.SetResolution(1920, 1080, true);
    }
    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            StartCoroutine(Introduce());
    }
    private IEnumerator TouchToStartTwinkle()
    {
        TouchToStart.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        TouchToStart.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(TouchToStartTwinkle());
    }
    private IEnumerator Introduce()
    {
        IntroCutScene.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(1);
    }
}
