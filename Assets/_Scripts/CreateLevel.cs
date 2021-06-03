using System.Collections;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public GameObject[] prefFieldMan;
    public GameObject prefFieldCrystal;
    public GameObject finishObject;

    public int countFieldMan;

    void Start()
    {
        StartCoroutine(InstantiateLevelBlock());
    }

    IEnumerator InstantiateLevelBlock()
    {
        Vector3 prefPos = new Vector3(0f, 0f, 75f);
        GameObject instGO;

        for(int i = 0; i < countFieldMan; i++)
        {
            instGO = Instantiate(prefFieldMan[Random.Range(0, prefFieldMan.Length)], this.transform);
            instGO.transform.localPosition = prefPos;

            prefPos.z += 75f;

            yield return null;

            instGO = Instantiate(prefFieldCrystal, this.transform);
            instGO.transform.localPosition = prefPos;

            prefPos.z += 75f;

            for(int j = 0; j < 15; j++)
            {
                yield return null;
            }
        }

        prefPos.z -= 25f;

        finishObject.transform.localPosition = prefPos;

        yield break;
    }
}
