using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObject : MonoBehaviour
{
    public bool IsConsumable;
    public Sprite ItemImage;
    public abstract bool ItemActive();
}
