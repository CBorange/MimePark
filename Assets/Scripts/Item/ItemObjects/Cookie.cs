using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : ItemObject
{
    private PlayerStatManager statManager;
    private void Start()
    {
        IsConsumable = true;
        ItemImage = GetComponent<SpriteRenderer>().sprite;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PLAYER")
        {
            statManager = col.GetComponent<PlayerStatManager>();
            ItemSlotManager.GetInstance().AttachItemInSlot(this);
            gameObject.SetActive(false);
        }
    }
    public override bool ItemActive()
    {
        statManager.GetHeal(0.5f);
        return true;
    }
}
