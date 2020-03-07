using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptUtil
{
    public static Sprite ValidateSpriteResource(string URL)
    {
        Sprite resource = Resources.Load<Sprite>(URL);
        if (resource == null)
        {
            Debug.Log($"{URL} : Resource Is Not Founded");
            return null;
        }
        else
            return resource;
    }
}
