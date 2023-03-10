using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] GameObject poofEffect;

    private void OnMouseDown()
    {
        SoundManager.instance.playSound(SoundManager.instance.sound8);
        GameHandler.instance.BalloonActivated();
        Spawner.instance.BalloonActivated();
        Instantiate(poofEffect, transform.position, Quaternion.EulerRotation(90, 0, 0));
        gameObject.SetActive(false);
    }

    public void DeActivateBalloon()
    {
        gameObject.SetActive(false);
    }
}
