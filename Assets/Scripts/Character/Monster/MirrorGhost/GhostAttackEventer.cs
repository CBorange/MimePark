using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttackEventer : MonoBehaviour
{
    private GhostAnimCntrl AnimCntrl;
    private GhostMoveCntrl MoveCntrl;
    private PlayerStatManager PlayerStat;
    private bool WhileAttack = true;
    private void Start()
    {
        AnimCntrl = transform.parent.GetComponent<GhostAnimCntrl>();
        MoveCntrl = transform.parent.GetComponent<GhostMoveCntrl>();
        PlayerStat = GameObject.Find("Player").GetComponent<PlayerStatManager>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("PLAYER"))
        {
            MoveCntrl.RunningAI = GhostMoveCntrl.AIState.Attack;
            AnimCntrl.ChangeAttackAnim(true);
            StartCoroutine(CalcAttackCycle_Cor());
            WhileAttack = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("PLAYER"))
        {
            MoveCntrl.RunningAI = GhostMoveCntrl.AIState.Trace;
            AnimCntrl.ChangeAttackAnim(false);
            WhileAttack = false;
        }
    }
    private IEnumerator CalcAttackCycle_Cor()
    {
        yield return new WaitForSeconds(0.6f);
        if (WhileAttack)
        {
            PlayerStat.GetHit(0.5f);
            StartCoroutine(CalcAttackCycle_Cor());
        }
    }
}
