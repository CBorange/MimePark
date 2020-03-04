using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDropDoor : MonoBehaviour, LaserReactionObject
{
    private GameObject Key;
    private GameObject GhostPrefab;
    public GameObject monsterPool;

    private bool AlreadyDropKey = false;

    public Vector2 DropDir;
    public bool IsTrap;
    private void Start()
    {
        Key = Resources.Load<GameObject>("Prefabs/Clear_Key");
        GhostPrefab = Resources.Load<GameObject>("Prefabs/MirrorGhost");
    }

    public void ReactionForLaser(Vector2 launchDir, string launchObjectName)
    {
        if (!AlreadyDropKey && !IsTrap)
        {
            Instantiate(Key, transform.position + (new Vector3(DropDir.x, DropDir.y, 0) * 300f), Quaternion.identity);
            AlreadyDropKey = true;
        }
        else if (IsTrap)
        {
            StartCoroutine(IE_GhostSpawn());
        }
    }

    public void LaserMoveOff(string moveOffObjectName)
    {

    }

    private IEnumerator IE_GhostSpawn()
    {
        for (int i = 0; i < 3; ++i)
        {
            GameObject newMonster = Instantiate(GhostPrefab, transform.position + (new Vector3(DropDir.x, DropDir.y, 0) * 300f), Quaternion.identity);
            newMonster.transform.parent = monsterPool.transform;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
