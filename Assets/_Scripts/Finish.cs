using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private ReloadScene sceneScr;
    public HeadMove headMoveScr;

    private void Start()
    {
        sceneScr = Camera.main.GetComponent<ReloadScene>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            headMoveScr.Stop();
            sceneScr.NewGame();
        }
    }
}
