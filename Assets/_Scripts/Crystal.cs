using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public enum eTypeGO
    {
        Crystal,
        Spike
    }

    [System.Serializable]
    public class Variants
    {
        public eTypeGO[] instantiateGO;
    }

    public Variants[] variants;
    public GameObject prefCrystal;
    public GameObject prefSpike;

    private int numberRow = 0;

    private void Start()
    {
        StartCoroutine(InstantiateAllGO(Random.Range(0, variants.Length)));
    }

    IEnumerator InstantiateAllGO(int num)
    {
        Vector3 instPos = new Vector3(6f, 0.5f, -21f);
        GameObject go;
        for (int i = 0; i < variants[num].instantiateGO.Length; i++)
        {
            if (variants[num].instantiateGO[i] == eTypeGO.Crystal)
            {
                go = Instantiate(prefCrystal, this.transform);
                go.transform.localPosition = instPos;

                go.GetComponent<CrystalNumber>().num = numberRow;
            }
            else
            {
                go = Instantiate(prefSpike, this.transform);
                go.transform.localPosition = instPos;
            }

            if (instPos.x > -6f)
            {
                instPos.x -= 6f;
            }
            else
            {
                instPos.x = 6f;
                instPos.z += 8f;

                numberRow += 1;
            }

            yield return null;
        }

        yield break;
    }
}
