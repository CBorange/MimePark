using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimeAnimCntrl : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ChangeIdleAnim(int idleState)
    {
        animator.SetInteger("IdleState", idleState);
    }
    public void ChangeAttackAnim(bool state)
    {
    }
}
