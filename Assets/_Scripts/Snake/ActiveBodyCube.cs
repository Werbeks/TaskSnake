using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBodyCube : MonoBehaviour
{
    public GameObject[] bodyCubeGO;

    void Start()
    {
        StartCoroutine(ActiveGO());
    }

    IEnumerator ActiveGO()
    {
        for(int i = 0; i < bodyCubeGO.Length; i++)
        {
            bodyCubeGO[i].SetActive(true);
            yield return null;
        }

        yield break;
    }
}
