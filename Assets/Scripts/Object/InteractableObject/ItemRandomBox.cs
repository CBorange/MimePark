using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomBox : MonoBehaviour, InteractAble
{
    private GameObject[] RandomItem;
    private void Start()
    {
        RandomItem = new GameObject[2];
        RandomItem[0] = Resources.Load<GameObject>("Prefabs/Glue");
        RandomItem[1] = Resources.Load<GameObject>("Prefabs/Cookie");
    }
    public  void Interact()
    {
        RandomItemDrop();
    }
    private void RandomItemDrop()
    {
        Instantiate(RandomItem[Random.Range(0, 2)], transform.position, Quaternion.identity);
        Destroy(this);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("PLAYER"))
        {

        }
    }
}
