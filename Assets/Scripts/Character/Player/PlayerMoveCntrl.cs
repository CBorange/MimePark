using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveCntrl : MonoBehaviour
{
    public bool OkToMove;
    public int MoveForward;
    private Transform cameraTrans;
    private Rigidbody2D rigid;
    private PlayerAnimCntrl AnimCntrl;
    private float moveSpeed = 500f;
    public Rect cameraLimitRect;
    
    private void Start()
    {
        Caching();
        cameraTrans.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    private void Caching()
    {
        cameraTrans = Camera.main.transform;
        rigid = GetComponent<Rigidbody2D>();
        AnimCntrl = GetComponent<PlayerAnimCntrl>();
    }
    public void PlayerMove(float v, float h)
    {
        if (!OkToMove)
            return;
        if (v.Equals(1))
        {
            if (MoveForward.Equals(1))
                AnimCntrl.ChangeWalkAnim(1);
            else
                AnimCntrl.ChangeWalkAnim(2);
        }
        else if (v.Equals(-1))
        {
            if (MoveForward.Equals(1))
                AnimCntrl.ChangeWalkAnim(2);
            else
                AnimCntrl.ChangeWalkAnim(1);
        }
        if (h.Equals(1))
        {
            if (MoveForward.Equals(1))
                AnimCntrl.ChangeWalkAnim(3);
            else
                AnimCntrl.ChangeWalkAnim(4);
        }
        else if (h.Equals(-1))
        {
            if (MoveForward.Equals(1))
                AnimCntrl.ChangeWalkAnim(4);
            else
                AnimCntrl.ChangeWalkAnim(3);
        }
        rigid.velocity = new Vector2(h * moveSpeed * MoveForward, v * moveSpeed * MoveForward);

        if (transform.position.x > cameraLimitRect.xMin && transform.position.x < cameraLimitRect.width)
            cameraTrans.position = new Vector3(transform.position.x, cameraTrans.position.y, -10);
        if (transform.position.y > cameraLimitRect.yMin && transform.position.y < cameraLimitRect.height)
            cameraTrans.position = new Vector3(cameraTrans.position.x, transform.position.y, -10);
    }
    public void StopMove()
    {
        rigid.velocity = new Vector2(0, 0);
        AnimCntrl.StopAnim();
    }
}
