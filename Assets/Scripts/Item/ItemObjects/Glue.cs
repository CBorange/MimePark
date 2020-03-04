using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : ItemObject
{
    private void Start()
    {
        IsConsumable = true;
        ItemImage = GetComponent<SpriteRenderer>().sprite;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PLAYER")
        {
            ItemSlotManager.GetInstance().AttachItemInSlot(this);
            gameObject.SetActive(false);
        }
    }
    public override bool ItemActive()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/GlueTrap"), GameObject.Find("Player").transform.position, Quaternion.identity);
        return true;
    }
}
