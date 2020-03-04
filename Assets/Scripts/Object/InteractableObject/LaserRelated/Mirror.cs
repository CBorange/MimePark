using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MirrorDirection
{
    public MirrorDirection(Vector2 Direction, Vector2 reflectableVec1, Vector2 reflectableVec2, Vector2 reflectVec1, Vector2 reflectVec2)
    {
        this.Direction = Direction;
        ReflectableVectors = new Vector2[2];
        ReflectableVectors[0] = reflectableVec1;
        ReflectableVectors[1] = reflectableVec2;
        ReflectDirections = new Vector2[2];
        ReflectDirections[0] = reflectVec1;
        ReflectDirections[1] = reflectVec2;
    }
    public Vector2 Direction;
    /**반사가능 각 과 반사각은 같은 Index를 사용**/
    public Vector2[] ReflectableVectors;
    public Vector2[] ReflectDirections;
}
public class Mirror : MonoBehaviour, InteractAble, LaserReactionObject
{
    private LaserController laserController;
    private SpriteRenderer spriteRenderer;
    private Sprite[] spritesByAngle;

    private MirrorDirection[] mirrorDirections;  // 회전 시 실제 회전이 아닌 이미지 회전이므로 방향 벡터 필요
    private int mirrorDirIdx = 0;
    private Dictionary<string, Vector2> incomeLaserInfos;

    private void Start()
    {
        incomeLaserInfos = new Dictionary<string, Vector2>();
        laserController = GetComponent<LaserController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        LoadSpritesByAngles();
        CachingMirrorDirection();
    }

    public void Interact()
    {
        RotateMirror();
    }

    public void ReactionForLaser(Vector2 launchDir, string launchObjectName)
    {

        incomeLaserInfos.Add(launchObjectName, launchDir);
        TruncateAndReflect(launchDir);
    }

    // 입사각 소수점 1자리수 뒤에 버리고 반사
    private void TruncateAndReflect(Vector2 launchDir)
    {
        Vector2 incidenceLaserDir = launchDir;

        incidenceLaserDir.x *= 10;
        incidenceLaserDir.y *= 10;

        incidenceLaserDir.x = (float)Math.Truncate(incidenceLaserDir.x);
        incidenceLaserDir.y = (float)Math.Truncate(incidenceLaserDir.y);

        incidenceLaserDir.x /= 10;
        incidenceLaserDir.y /= 10;

        for (int i = 0; i < 2; ++i)
        {
            if (mirrorDirections[mirrorDirIdx].ReflectableVectors[i].x == incidenceLaserDir.x &&
                mirrorDirections[mirrorDirIdx].ReflectableVectors[i].y == incidenceLaserDir.y)
            {
                laserController.LaunchLaser(mirrorDirections[mirrorDirIdx].ReflectDirections[i]);
                return;
            }
        }
    }

    public void LaserMoveOff(string moveOffObjectName)
    {
        incomeLaserInfos.Remove(moveOffObjectName);
        if (incomeLaserInfos.Count == 0)
        {
            laserController.MoveOffLaser();
        }
    }

    // 거울 각도에 따른 스프라이트 로드
    private void LoadSpritesByAngles()
    {
        spritesByAngle = new Sprite[8];
        for (int i = 0; i < 8; ++i)
        {
            spritesByAngle[i] = Resources.Load<Sprite>($"Sprite/Mirror_0{i}");
        }
    }

    // 각도 캐싱
    private void CachingMirrorDirection()
    {
        mirrorDirections = new MirrorDirection[8];
        mirrorDirections[0] = new MirrorDirection(new Vector2(0, -1), new Vector2(-0.7f, 0.7f), new Vector2(0.7f, 0.7f),
            new Vector2(-0.7f, -0.7f), new Vector2(0.7f, -0.7f));
        mirrorDirections[1] = new MirrorDirection(new Vector2(0.7f, -0.7f), new Vector2(-1, 0), new Vector2(0, 1),
            new Vector2(0, -1), new Vector2(1, 0));
        mirrorDirections[2] = new MirrorDirection(new Vector2(1, 0), new Vector2(-0.7f, -0.7f), new Vector2(-0.7f, 0.7f),
            new Vector2(0.7f, -0.7f), new Vector2(0.7f, 0.7f));
        mirrorDirections[3] = new MirrorDirection(new Vector2(0.7f, 0.7f), new Vector2(-1, 0), new Vector2(0, -1),
            new Vector2(0, 1), new Vector2(1, 0));
        mirrorDirections[4] = new MirrorDirection(new Vector2(0, 1), new Vector2(-0.7f, -0.7f), new Vector2(0.7f, -0.7f),
            new Vector2(-0.7f, 0.7f), new Vector2(0.7f, 0.7f));
        mirrorDirections[5] = new MirrorDirection(new Vector2(-0.7f, 0.7f), new Vector2(0, -1), new Vector2(1, 0),
            new Vector2(-1, 0), new Vector2(0, 1));
        mirrorDirections[6] = new MirrorDirection(new Vector2(-1, 0), new Vector2(0.7f, -0.7f), new Vector2(0.7f, 0.7f),
            new Vector2(-0.7f, -0.7f), new Vector2(-0.7f, 0.7f));
        mirrorDirections[7] = new MirrorDirection(new Vector2(-0.7f, -0.7f), new Vector2(1, 0), new Vector2(0, 1),
            new Vector2(0, -1), new Vector2(-1, 0));
    }

    // 거울 회전
    private void RotateMirror()
    {
        // 거울 회전
        mirrorDirIdx += 1;
        if (mirrorDirIdx > 7)
            mirrorDirIdx = 0;
        spriteRenderer.sprite = spritesByAngle[mirrorDirIdx];

        // 레이저 처리
        laserController.MoveOffLaser();
        foreach(var key in incomeLaserInfos.Keys)
        {
            Vector2 incomeDir = incomeLaserInfos[key];
            TruncateAndReflect(incomeDir);
        }
    }
}
