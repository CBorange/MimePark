using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMoveCntrl : MonoBehaviour
{
    public enum AIState
    {
        WalkAround,
        Trace,
        Attack,
        Idle
    };
    private const float MoveSpeed = 200f;
    private GhostAnimCntrl AnimCntrl;
    private Transform PlayerTrans;
    public Vector2 ToPlayerDir;
    public AIState RunningAI;

    #region TracePlayer
    private float ToPlayerAngle;
    #endregion
    #region WalkAround
    public Vector2[] WalkDirGroup;
    public Vector2 WalkDir;
    public float WalkDis;
    public float WalkTime;
    public float AroundRateTime = 0;
    #endregion

    private void Start()
    {
        PlayerTrans = GameObject.Find("Player").transform;
        AnimCntrl = GetComponent<GhostAnimCntrl>();

        WalkDirGroup = new Vector2[8];
        WalkDirGroup[0] = new Vector2(0, 1);
        WalkDirGroup[1] = new Vector2(0.7f, 0.7f);
        WalkDirGroup[2] = new Vector2(1, 0);
        WalkDirGroup[3] = new Vector2(0.7f, -0.7f);
        WalkDirGroup[4] = new Vector2(0, -1);
        WalkDirGroup[5] = new Vector2(-0.7f, -0.7f);
        WalkDirGroup[6] = new Vector2(-0.7f, 0.7f);
        WalkDirGroup[7] = new Vector2(0, 1);

        RunningAI = AIState.WalkAround;
        WalkAroundRoutine();
    }
    private void Update()
    {
        if (RunningAI == AIState.Trace)
        {
            CalcToPlayerPos();
            RotateToPlayerDir();
            MoveToPlayer();
            return;
        }
        else if (RunningAI == AIState.WalkAround)
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
    #region TracePlayer
    private void CalcToPlayerPos()
    {
        ToPlayerDir = PlayerTrans.position - transform.position;
        ToPlayerAngle = Mathf.Atan2(ToPlayerDir.y, ToPlayerDir.x) * Mathf.Rad2Deg;
    }
    private void RotateToPlayerDir()
    {
        float ApplyAngle = 0;
        int ApplyState = 0;
        //위쪽
        if (ToPlayerAngle > 60 && ToPlayerAngle < 120)
        {
            ApplyState = 0;
            ApplyAngle = -90f;
        }
        //아래쪽
        else if (ToPlayerAngle < -60 && ToPlayerAngle > -120)
        {
            ApplyState = 1;
            ApplyAngle = -270f;
        }
        //오른쪽
        else if (ToPlayerAngle > -60 && ToPlayerAngle < 60)
        {
            ApplyState = 2;
            ApplyAngle = 0;
        }
        //왼쪽
        else if ((ToPlayerAngle < -120 && ToPlayerAngle > -180) ||
                (ToPlayerAngle > 120 && ToPlayerAngle < 180))
        {
            ApplyState = 3;
            ApplyAngle = -180f;
        }
        AnimCntrl.ChangeIdleArcordingAngle(ApplyState);
        transform.rotation = Quaternion.Euler(0, 0, ToPlayerAngle + ApplyAngle);
    }
    private void MoveToPlayer()
    {
        transform.Translate(ToPlayerDir.normalized * MoveSpeed * Time.deltaTime, Space.World);
    }
    #endregion

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
        for (int i = 0; i < 8; ++i)
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
                        return;
                    }
                }
            }
        }
    }
    private void RotateToWalkDir()
    {
        float ApplyAngle = 0;
        int ApplyState = 0;
        //위쪽
        if (WalkDir == WalkDirGroup[0])
        {
            ApplyState = 0;
        }
        //아래쪽
        else if (WalkDir == WalkDirGroup[4])
        {
            ApplyState = 1;
        }
        //오른쪽위 대각선
        else if (WalkDir == WalkDirGroup[1])
        {
            ApplyState = 2;
            ApplyAngle = 45f;
        }
        //오른쪽
        else if (WalkDir == WalkDirGroup[2])
        {
            ApplyState = 2;
        }
        //오른쪽아래 대각선
        else if (WalkDir == WalkDirGroup[3])
        {
            ApplyState = 2;
            ApplyAngle = -45f;
        }
        //왼쪽아래 대각선
        else if (WalkDir == WalkDirGroup[5])
        {
            ApplyState = 3;
            ApplyAngle = 45f;
        }
        else if (WalkDir == WalkDirGroup[6])
        {
            ApplyState = 3;
        }
        else if (WalkDir == WalkDirGroup[7])
        {
            ApplyState = 3;
            ApplyAngle = -45f;
        }
        AnimCntrl.ChangeIdleArcordingAngle(ApplyState);
        transform.rotation = Quaternion.Euler(0, 0, ApplyAngle);
    }
    private void CalcWalkTime()
    {
        WalkTime = WalkDis / MoveSpeed;
    }
    #endregion
}
