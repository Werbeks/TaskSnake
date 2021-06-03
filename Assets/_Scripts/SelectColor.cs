using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColor : MonoBehaviour
{
    private AllColor allColor;

    public Color manColor;
    public Color enemyColor;
    public Color myColor;

    private void Awake()
    {
        allColor = Camera.main.GetComponent<AllColor>();

        int numColor = Random.Range(0, allColor.colors.Length);

        manColor = allColor.colors[numColor];

        numColor += Random.Range(1, allColor.colors.Length - 1);
        if (numColor >= allColor.colors.Length)
        {
            numColor -= allColor.colors.Length;


        }

        enemyColor = allColor.colors[numColor];

        myColor = manColor;
        myColor.a = 0.8f;

        GetComponent<Renderer>().material.color = myColor;

        myColor.a = 1f;
    }
}
