using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : ItemObject
{
    private Transform playerTrans;
    private List<Transform> ExitDoorTrans;
    private void Start()
    {
        IsConsumable = true;
        ItemImage = GetComponent<SpriteRenderer>().sprite;
        ExitDoorTrans = new List<Transform>();

        GameObject[] doorList = GameObject.FindGameObjectsWithTag("ExitDoor");
        for (int i = 0; i < doorList.Length; ++i)
        {
            ExitDoorTrans.Add(doorList[i].transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PLAYER")
        {
            playerTrans = col.gameObject.transform;
            gameObject.SetActive(false);
            StageDataManager.GetInstance().HaveKey += 1;
            ItemSlotManager.GetInstance().AttachItemInSlot(this);
        }
    }
    public override bool ItemActive()
    {
        Debug.Log("Key");
        for (int count = 0; count < ExitDoorTrans.Count; ++count)
        {
            if (playerTrans.position.x - playerTrans.localScale.x * 0.5f < ExitDoorTrans[count].position.x + 100f &&
                playerTrans.position.y + playerTrans.localScale.y * 0.5f > ExitDoorTrans[count].position.y - 100f &&
                playerTrans.position.x + playerTrans.localScale.x * 0.5f > ExitDoorTrans[count].position.x - 100f &&
                playerTrans.position.y - playerTrans.localScale.y * 0.5f < ExitDoorTrans[count].position.y + 100f)
            {
                ExitDoorTrans[count].GetComponent<Door>().UnlockDoor();
                ExitDoorTrans.RemoveAt(count);
                return true;
            }
        }
        return false;
    }
}
