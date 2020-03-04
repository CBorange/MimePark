using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotManager : MonoBehaviour
{
    private static ItemSlotManager instance;
    private static GameObject container;
    public List<int> AttachAbleSlot = new List<int>();
    public ItemSlot[] itemSlot = new ItemSlot[5];

    private void Init()
    {
        for (int i = 0; i < 5; ++i)
        {
            AttachAbleSlot.Add(i);
            itemSlot[i] = GameObject.Find("ItemSlot_" + i.ToString()).GetComponent<ItemSlot>();
        }
    }
    public static ItemSlotManager GetInstance()
    {
        if (!instance)
        {
            container = new GameObject("ItemSlotManager");
            instance = container.AddComponent<ItemSlotManager>();
            GetInstance().Init();
        }
        return instance;
    }
    public void AttachItemInSlot(ItemObject newItem)
    {
        if (AttachAbleSlot.Count > 1)
        {
            itemSlot[AttachAbleSlot[0]].ContainItem(newItem);
            itemSlot[AttachAbleSlot[0]].SlotIndex = AttachAbleSlot[0];
            AttachAbleSlot.RemoveAt(0);
        }
    }
}
