using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private List<Transform> listGOBody;
    private Transform head;

    public float u = 0.30f;
    public float timeDurationAnim = 0.4f;
    public GameObject prefCube;
    public float[] sizeCube;
    public bool death = false;

    public int countListGOBody
    {
        get { return listGOBody.Count; }
    }

    private void Awake()
    {
        head = transform.parent.transform.Find("Head");

        listGOBody = new List<Transform>();

        //Заносим все объекты тела змеи в список
        foreach (Transform body in this.transform)
        {
            listGOBody.Add(body.transform);
        }
    }

    public void Index(Transform tr)
    {
        print(listGOBody.IndexOf(tr));
    }

    public bool ObjectInTheList(Transform tr)
    {
        return listGOBody.Contains(tr);
    }

    public void AddListGOBody(Transform tr)
    {
        listGOBody.Add(tr);
    }

    public Transform PointGO(Transform tr)
    {
        int number = listGOBody.IndexOf(tr);

        if (number != -1)
        {
            if (number == 0)
            {
                return head;
            }
            else
            {
                return listGOBody[number - 1];
            }
        }
        else
        {
            listGOBody.Add(tr);
            return listGOBody[listGOBody.IndexOf(tr) - 1];
        }
    }

    public GameObject NextGOListBody(Transform tr)
    {
        int number = listGOBody.IndexOf(tr);

        if (number + 1 < listGOBody.Count)
        {
            return listGOBody[number + 1].gameObject;
        }
        else
        {
            return null;
        }
    }

    public void StartAnimEat()
    {
        listGOBody[0].GetComponent<BodySize>().EatMan();
    }

    public void StartLose()
    {
        StartCoroutine(Lose());
    }

    IEnumerator Lose()
    {
        foreach(Transform tr in listGOBody)
        {
            tr.GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(0.05f);
        }

        yield break;
    }
}
