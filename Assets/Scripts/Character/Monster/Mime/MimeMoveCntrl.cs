using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimeMoveCntrl : MonoBehaviour
{
    public enum AIState
    {
        Idle,
        WalkAround
    };
    private MimeAnimCntrl AnimCntrl;
    public AIState RunningAI;

    private const float MoveSpeed = 300f;
    #region WalkAround
    public Vector2[] WalkDirGroup;
    public Vector2 WalkDir;
    public float WalkDis;
    public float WalkTime;
    public float AroundRateTime = 0;
    #endregion
    private void Start()
    {
        AnimCntrl = GetComponent<MimeAnimCntrl>();

        WalkDirGroup = new Vector2[4];
        WalkDirGroup[0] = new Vector2(0, 1);
        WalkDirGroup[1] = new Vector2(1, 0);
        WalkDirGroup[2] = new Vector2(0, -1);
        WalkDirGroup[3] = new Vector2(-1, 0);

        RunningAI = AIState.WalkAround;
        WalkAroundRoutine();
    }
    private void Update()
    {
        if (RunningAI == AIState.WalkAround)
        {
            AroundRateTime += Time.deltaTime;
            if (AroundRateTime > WalkTime)
            {
                WalkDir = Vector2.zero;
                RunningAI = AIState.Idle;
                Invoke("WalkAroundRoutine", 1.0f);
                AroundRateTime = 0f;
            }
            transform.Translate(WalkDir * MoveSpeed * Time.deltaTime, Space.World);
        }
    }
    #region WalkAround
    public void StopWalkAround()
    {
        AroundRateTime = 0f;
    }
    public void StartWalkAround()
    {
        RunningAI = AIState.WalkAround;
        AroundRateTime = 0f;
        SetWalkDir();
        RotateToWalkDir();
        CalcWalkTime();
    }
    private void WalkAroundRoutine()
    {
        RunningAI = AIState.WalkAround;
        SetWalkDir();
        RotateToWalkDir();
        CalcWalkTime();
    }
    private void SetWalkDir()
    {
        List<Vector2> AbleDir = new List<Vector2>();
        for (int i = 0; i < 4; ++i)
            AbleDir.Add(WalkDirGroup[i]);
        while (true)
        {
            int Index = Random.Range(0, AbleDir.Count);
            RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y) + AbleDir[Index] * 45f, AbleDir[Index], 4000f);
            for (int i = 0; i < hit.Length; ++i)
            {
                if (hit[i].collider.tag.Equals("Wall"))
                {
                    Vector2 delta;
                    delta = hit[i].point - new Vector2(transform.position.x, transform.position.y);
                    float ToDis = Mathf.Sqrt((delta.x * delta.x) + (delta.y * delta.y));
                    if (ToDis < 800f)
                    {
                        AbleDir.RemoveAt(Index);
                        break;
                    }
                    else if (ToDis >= 800f)
                    {
                        if (ToDis > 1500f)
                            ToDis = 1500f;
                        ToDis -= 100f;
                        WalkDir = AbleDir[Index];
                        WalkDis = ToDis;
                        Debug.DrawRay(transform.position, WalkDir * WalkDis, Color.red, 10f);
                        return;
                    }
                }
            }
        }
    }
    private void RotateToWalkDir()
    {
        for (int i = 0; i < 4; ++i)
        {
            if (WalkDirGroup[i] == WalkDir)
            {
                AnimCntrl.ChangeIdleAnim(i);
            }
        }
        //AnimCntrl.ChangeIdleArcordingAngle(ApplyState);
    }
    private void CalcWalkTime()
    {
        WalkTime = WalkDis / MoveSpeed;
    }
    #endregion
}
