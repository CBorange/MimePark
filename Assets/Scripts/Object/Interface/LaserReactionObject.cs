using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LaserReactionObject
{
    void ReactionForLaser(Vector2 launchDir, string launchObjectName);
    void LaserMoveOff(string moveOffObjectName);
}
