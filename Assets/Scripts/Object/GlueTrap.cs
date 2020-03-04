using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueTrap : MonoBehaviour
{
    private bool AlreadyActive = false;
    private Collider2D ColidObj;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (AlreadyActive)
            return;
        ColidObj = col;
        int Collayer = col.gameObject.layer;

        if (Collayer.Equals(10))
            StartCoroutine(ActiveForMonster());
    }
    private IEnumerator ActiveForMonster()
    {
        AlreadyActive = true;
        yield return new WaitForSeconds(0.3f);
        ColidObj.GetComponent<GhostMoveCntrl>().RunningAI = GhostMoveCntrl.AIState.Idle;
        yield return new WaitForSeconds(5f);
        ColidObj.GetComponent<GhostMoveCntrl>().RunningAI = GhostMoveCntrl.AIState.WalkAround;
        Destroy(this.gameObject);
    }
}
