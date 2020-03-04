using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InteractionManager : MonoBehaviour
{
    public GameObject interactableObjectPool;

    public void InteractAbleCheck()
    {
        for (int count = 0; count < interactableObjectPool.transform.childCount; ++count)
        {
            if (transform.position.x - transform.localScale.x * 0.5f < interactableObjectPool.transform.GetChild(count).position.x + 100f &&
                transform.position.y + transform.localScale.y * 0.5f > interactableObjectPool.transform.GetChild(count).position.y - 100f &&
                transform.position.x + transform.localScale.x * 0.5f > interactableObjectPool.transform.GetChild(count).position.x - 100f &&
                transform.position.y - transform.localScale.y * 0.5f < interactableObjectPool.transform.GetChild(count).position.y + 100f)  
            {
                interactableObjectPool.transform.GetChild(count).GetComponent<InteractAble>().Interact();
                return;
            }
        }
    }
}
