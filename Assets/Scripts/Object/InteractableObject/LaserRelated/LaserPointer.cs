using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour, InteractAble
{
    private LaserController laserController;

    private void Start()
    {
        laserController = GetComponent<LaserController>();
        LaunchLaser();
    }

    public void Interact()
    {
        RotatePointer();
        MoveOffLaser();
        LaunchLaser();
    }

    // 포인터 회전
    private void RotatePointer()
    {
        transform.Rotate(0, 0, -45);
    }

    private void LaunchLaser()
    {
        laserController.LaunchLaser(transform.up);
    }
    private void MoveOffLaser()
    {
        laserController.MoveOffLaser();
    }
}
