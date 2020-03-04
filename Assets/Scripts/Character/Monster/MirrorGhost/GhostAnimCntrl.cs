using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimCntrl : MonoBehaviour
{
    private Animator animator;

    private int IdleAnimState;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ChangeAttackAnim(bool state)
    {
        switch(IdleAnimState)
        {
            case 0:
                animator.SetBool("Front_Attack", state);
                break;
            case 1:
                animator.SetBool("Back_Attack", state);
                break;
            case 2:
                animator.SetBool("Right_Attack", state);
                break;
            case 3:
                animator.SetBool("Left_Attack", state);
                break;
        }
    }
    public void ChangeIdleArcordingAngle(int State)
    {
        IdleAnimState = State;
        animator.SetInteger("IdleState", State);
    }
    public void ChangeIdleArcordingAngle()
    {
        animator.SetInteger("IdleState", IdleAnimState);
    }
}
