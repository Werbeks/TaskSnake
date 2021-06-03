using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : MonoBehaviour
{
    public GameObject prefMan;

    private void Start()
    {
        StartCoroutine(InstPref(Random.Range(3, 5)));
    }

    IEnumerator InstPref(int count)
    {
        Vector3[] posPref = new Vector3[count];
        posPref[0] = new Vector3(Random.Range(0f, 0.5f), 0f, Random.Range(0f, 0.5f));

        GameObject instGO;
        for (int i = 0; i < count; i++)
        {
            instGO = Instantiate(prefMan, this.transform);
            instGO.transform.localPosition = posPref[i];

            if (i + 1 < count)
            {
                posPref[i + 1].x += Random.Range(-1.3f, 1.3f);
                posPref[i + 1].z += Random.Range(-1.3f, 1.3f);

                for (int j = 0; j < i + 1; j++)
                {
                    float abs = Mathf.Abs(posPref[j].x - posPref[i + 1].x);

                    if (abs < 0.9f)
                    {
                        if (posPref[i + 1].x < posPref[j].x)
                        {
                            posPref[i + 1].x -= 0.9f - abs;
                        }
                        else
                        {
                            posPref[i + 1].x += 0.9f - abs;
                        }
                    }

                    abs = Mathf.Abs(posPref[j].z - posPref[i + 1].x);

                    if (abs < 0.7f)
                    {
                        if (posPref[i + 1].z < posPref[j].z)
                        {
                            posPref[i + 1].z -= 0.9f - abs;
                        }
                        else
                        {
                            posPref[i + 1].z += 0.9f - abs;
                        }
                    }
                }
            }

            yield return null;
        }

        yield break;
    }
}
