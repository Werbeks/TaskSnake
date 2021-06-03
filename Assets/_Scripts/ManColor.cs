using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManColor : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material.color = transform.parent.transform.parent.transform.parent.transform.parent.transform.Find("Color").GetComponent<SelectColor>().manColor;
    }
}
