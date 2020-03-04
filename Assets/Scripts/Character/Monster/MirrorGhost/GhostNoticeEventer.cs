using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostNoticeEventer : MonoBehaviour
{
    private GhostMoveCntrl MoveCntrl;
    private void Start()
    {
        MoveCntrl = transform.parent.GetComponent<GhostMoveCntrl>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("PLAYER"))
        {
            MoveCntrl.RunningAI = GhostMoveCntrl.AIState.Trace;
            MoveCntrl.StopWalkAround();
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("PLAYER"))
        {
            MoveCntrl.RunningAI = GhostMoveCntrl.AIState.WalkAround;
            MoveCntrl.StartWalkAround();
        }
    }
}
