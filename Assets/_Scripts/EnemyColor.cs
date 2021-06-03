using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColor : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material.color = transform.parent.transform.parent.transform.parent.transform.parent.transform.Find("Color").GetComponent<SelectColor>().enemyColor;
    }
}
