using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private GameObject laser;
    private LaserReactionObject connectedObject;

    private void Start()
    {
        CreateLaser();
    }

    // 레이저 생성
    private void CreateLaser()
    {
        GameObject laserPool = GameObject.Find("LaserPool");
        laser = Instantiate(Resources.Load<GameObject>("Prefabs/laser"));
        laser.transform.parent = laserPool.transform;
        laser.transform.position = Vector3.zero;
        laser.transform.localScale = Vector3.zero;
    }

    public void LaunchLaser(Vector2 launchDir)
    {
        laser.SetActive(true);
        int layerMask = (1 << LayerMask.NameToLayer("Object")) + (1 << LayerMask.NameToLayer("ReflectAble"));
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, launchDir, 4000f, layerMask);
        Debug.DrawRay(transform.position, launchDir * 4000f, Color.blue, 10);
        for (int i = 0; i < hits.Length; ++i)
        {
            if (hits[i].transform.name != gameObject.name)
            {
                laser.SetActive(true);
                // 두 물체 사이의 중간 좌표 계산
                Vector2 targetPos;
                if (hits[i].collider.tag == "Mirror")
                    targetPos = hits[i].transform.position;
                else
                    targetPos = hits[i].point;
                float centerPosX = (transform.position.x + targetPos.x) / 2;
                float centerPosY = (transform.position.y + targetPos.y) / 2;
                laser.transform.position = new Vector3(centerPosX, centerPosY);

                // 두 물체 사이의 거리 계산
                Vector2 differentVec = targetPos - (new Vector2(transform.position.x, transform.position.y));
                float distance = Mathf.Sqrt((differentVec.x * differentVec.x) + (differentVec.y * differentVec.y));
                laser.transform.localScale = new Vector3(1920, distance);
                laser.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(launchDir.y, launchDir.x) * Mathf.Rad2Deg) + 90);

                connectedObject = hits[i].transform.GetComponent<LaserReactionObject>();
                if (connectedObject != null)
                {
                    connectedObject.ReactionForLaser(launchDir, gameObject.name);
                }
                return;
            }
        }
    }

    public void MoveOffLaser()
    {
        laser.SetActive(false);
        if (connectedObject != null)
        {
            connectedObject.LaserMoveOff(gameObject.name);
        }
        connectedObject = null;
    }
}
