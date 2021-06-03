using System.Collections;
using UnityEngine;

public class BodySize : MonoBehaviour
{
    private Body bodyScr;

    public int selectSizeCube = 0;
    private bool increase = false;

    private void Start()
    {
        bodyScr = transform.parent.GetComponent<Body>();
    }

    public void EatMan()
    {
        if (selectSizeCube + 1 < bodyScr.sizeCube.Length)
        {
            selectSizeCube += 1;
            if (!increase)
            {
                StartCoroutine(AnimEatMan(selectSizeCube - 1));
            }
        }
        else
        {
            if (!increase)
            {
                StartCoroutine(AnimEatMan(selectSizeCube));
            }
        }
    }

    IEnumerator AnimEatMan(int size)
    {
        increase = true;

        float timeStart = Time.time;
        Vector3 s0 = Vector3.one * bodyScr.sizeCube[size];
        Vector3 s1 = Vector3.one * (bodyScr.sizeCube[size] + 0.6f);
        Vector3 s01;

        bool move = true;
        float timeDuration = bodyScr.timeDurationAnim / 2f;

        while (move)
        {
            float u = (Time.time - timeStart) / timeDuration;

            if (u >= 1)
            {
                u = 1f;

                move = false;
            }

            s01 = (1 - u) * s0 + u * s1;

            transform.localScale = s01;

            yield return null;
        }

        timeStart = Time.time;
        s0 = s1;
        s1 = Vector3.one * bodyScr.sizeCube[selectSizeCube];

        move = true;

        while (move)
        {
            float u = (Time.time - timeStart) / timeDuration;

            if (u >= 1)
            {
                u = 1f;

                move = false;
            }

            s01 = (1 - u) * s0 + u * s1;

            transform.localScale = s01;

            yield return null;
        }

        //Берём следующий куб из списка кубов тела
        GameObject go = bodyScr.NextGOListBody(this.transform);

        //Если он существует, запустим у него анимацию 
        if (go != null)
        {
            go.GetComponent<BodySize>().EatMan();
        }
        //Иначе, если мы не умерли создадим новый куб в хвосте
        else if (!bodyScr.death)
        {
            Vector3 newPos = this.transform.localPosition;
            newPos.z -= 1.1f;
            go = Instantiate(bodyScr.prefCube, transform.parent);
            go.transform.localPosition = newPos;
            bodyScr.AddListGOBody(go.transform);
        }

        increase = false;

        yield break;
    }
}
