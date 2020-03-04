using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, LaserReactionObject
{
    public ScriptManager scriptManager;
    public bool IsLast;

    private SpriteRenderer Renderer;
    public Sprite UnlockedSprite;
    private bool doorIsUnlocked = false;
    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }
    public void UnlockDoor()
    {
        Renderer.sprite = UnlockedSprite;
        doorIsUnlocked = true;
    }

    public void ReactionForLaser(Vector2 launchDir, string launchObjectName)
    {
        if (doorIsUnlocked)
        {
            StageClear();
        }
    }

    public void LaserMoveOff(string moveOffObjectName)
    {

    }

    private void StageClear()
    {
        if (IsLast)
        {
            scriptManager.StartDialog();
            return;
        }
        SceneManager.LoadScene(1);
    }
}
