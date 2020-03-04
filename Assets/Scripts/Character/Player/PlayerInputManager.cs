using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerMoveCntrl MoveCntrl;
    private PlayerStatManager StatManager;
    private InteractionManager interactionManager;
    private float HorMov;
    private float VerMov;

    public GameObject Pause;
    public List<GameObject> MonsterPool;
    public int NowScene;
    private void Start()
    {
        Caching();
    }
    private void Caching()
    {
        StatManager = GetComponent<PlayerStatManager>();
        MoveCntrl = GetComponent<PlayerMoveCntrl>();
        interactionManager = GetComponent<InteractionManager>();
    }
    private void FixedUpdate()
    {
        PCInput();
        if (StatManager.IsAlive)
        {
            PlayerMovement();
        }
    }
    private void PCInput()
    {
        HorMov = Input.GetAxisRaw("Horizontal");
        VerMov = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            interactionManager.InteractAbleCheck();
        }
    }
    private void PlayerMovement()
    {
        if (HorMov.Equals(0) && VerMov.Equals(0))
            MoveCntrl.StopMove();
        else
        {
            MoveCntrl.PlayerMove(VerMov, HorMov);
        }
    }
    private void PauseGame()
    {
        MoveCntrl.OkToMove = false;
        for (int i = 0; i < MonsterPool.Count; ++i)
            MonsterPool[i].SetActive(false);
    }
    private void ResumeGame()
    {
        MoveCntrl.OkToMove = true;
        for (int i = 0; i < MonsterPool.Count; ++i)
            MonsterPool[i].SetActive(true);
    }
#region touchInput
    public void Go_Previous()
    {
        if (NowScene.Equals(2))
            return;
        SceneManager.LoadScene(NowScene - 1);
    }
    public void Go_Select()
    {
        SceneManager.LoadScene(1);
    }
    public void RestartStage()
    {
        SceneManager.LoadScene(NowScene);
    }
    public void PauseButton()
    {
        if (Pause.activeInHierarchy)
        {
            ResumeGame();
            Pause.SetActive(false);
        }
        else
        {
            PauseGame();
            Pause.SetActive(true);
        }
    }
    public void ActionButton()
    {
        interactionManager.InteractAbleCheck();
    }
    public void LeftMove()
    {
        HorMov = -1;
    }
    public void RightMove()
    {
        HorMov = 1;
    }
    public void HorMovReset()
    {
        HorMov = 0;
    }
    public void UpMove()
    {
        VerMov = 1;
    }
    public void DownMove()
    {
        VerMov = -1;
    }
    public void VerMovReset()
    {
        VerMov = 0;
    }
#endregion
}
