using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimCntrl : MonoBehaviour
{
    public GameObject GameOverObj;
    private Animator animator;
    private int LookDir = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ChangeWalkAnim(int state)
    {
        LookDir = state;
        animator.speed = 0.8f;
        animator.SetInteger("WalkState", state);
    }
    public void StopAnim()
    {
        animator.speed = 0f;
    }
    public void DeadAnim()
    {
        animator.speed = 0.5f;
        Debug.Log("Dead");
        if (LookDir.Equals(1))
        {
            animator.SetTrigger("Dead_Front");
            Debug.Log("Front");
        }
        else if (LookDir.Equals(2))
        {
            animator.SetTrigger("Dead_Back");
            Debug.Log("Back");
        }
        else if (LookDir.Equals(3))
        {
            animator.SetTrigger("Dead_Right");
            Debug.Log("Right");
        }
        else if (LookDir.Equals(4))
        {
            animator.SetTrigger("Dead_Left");
            Debug.Log("Left");
        }
        StartCoroutine(ProcessDeadScene());
    }
    private IEnumerator ProcessDeadScene()
    {
        GameOverObj.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(1);
    }
}
