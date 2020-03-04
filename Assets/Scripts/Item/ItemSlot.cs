using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private Image ItemImage;
    public int SlotIndex;
    public ItemObject AttachedItem;

    private void Start()
    {
        ItemImage = transform.Find("Image").GetComponent<Image>();
        ItemImage.gameObject.SetActive(false);
    }
    public void ContainItem(ItemObject AttachItem)
    {
        this.AttachedItem = AttachItem;
        ItemImage.sprite = AttachedItem.ItemImage;
        ItemImage.gameObject.SetActive(true);
    }
    public void UseItemInSlot()
    {
        if (AttachedItem != null)
        {
            if (!AttachedItem.ItemActive())
                return;
            if (AttachedItem.IsConsumable)
            {
                ItemImage.gameObject.SetActive(false);
                AttachedItem = null;
                ItemSlotManager.GetInstance().AttachAbleSlot.Insert(0, SlotIndex);
            }
        }
    }
}
