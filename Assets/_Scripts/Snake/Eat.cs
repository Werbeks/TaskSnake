using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Eat : MonoBehaviour
{
    private Transform headTr;
    private HeadMove headMScr;
    private Body bodyScr;

    public bool fever = false;

    public Text textCrystal;
    public Text textEat;

    private int countCrystalText = 0;
    private int countEatText = 0;

    //�����
    public int countCrystalFever = 0;
    private int lastNumberCrystal;

    private void Start()
    {
        headTr = transform.parent.transform;
        headMScr = headTr.GetComponent<HeadMove>();
        bodyScr = transform.parent.transform.parent.transform.Find("Body").GetComponent<Body>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (headMScr.fever)
        {
            if(other.tag == "Man" || other.tag == "Crystal" || other.tag == "Enemy" || other.tag == "Spike")
            {
                StartCoroutine(EatGO(other.gameObject, true));
                countEatText += 1;
                textEat.text = "" + countEatText;

                if (other.tag == "Crystal")
                {
                    countCrystalText += 1;
                    textCrystal.text = "" + countCrystalText;
                }
            }
        }
        else
        {
            if (other.tag == "Man")
            {
                StartCoroutine(EatGO(other.gameObject, true));
                countEatText += 1;
                textEat.text = "" + countEatText;
            }
            else if (other.tag == "Crystal")
            {
                countCrystalText += 1;
                textCrystal.text = "" + countCrystalText;
                StartCoroutine(EatGO(other.gameObject, false));

                //����� ���� ������������ ��������
                int numberCrystal = other.GetComponent<CrystalNumber>().num;

                //���� ���������� ����������� ������ ��������� ����� 0
                if (countCrystalFever == 0)
                {
                    //������� +1 ����������� �������, ��� �� ������� ����� �������� ��� ��������� ��������
                    countCrystalFever += 1;
                    lastNumberCrystal = numberCrystal;
                }
                //����� ���� ����� ������� �������� �� �������
                else if (lastNumberCrystal + 1 == numberCrystal || lastNumberCrystal == numberCrystal)
                {
                    countCrystalFever += 1;

                    //���� ���������� ����������� ������ ��������� ������ 3
                    if (countCrystalFever > 3)
                    {
                        //�������� �����
                        headMScr.StartFever();

                        //������� ���������� ���������
                        countCrystalFever = 0;
                    }
                    else
                    {
                        //������� ����� �������� ��� ��������� �������� 
                        lastNumberCrystal = numberCrystal;
                    }
                }
                //����� ���� ����� ������� ��������� �� �� �������
                else
                {
                    countCrystalFever = 0;
                }
            }
        }
    }

    public void NullCountCrystal()
    {
        countCrystalText = 0;
        textCrystal.text = "" + countCrystalText;
    }

    IEnumerator EatGO(GameObject eatGO, bool increase)
    {
        float timeStart = Time.time;
        float timeDuration = 0.16f;

        Transform tr = eatGO.transform;

        Vector3 p0 = tr.position;
        //Vector3 p1 = headTr.position;

        Vector3 s0 = tr.localScale;
        Vector3 s1 = Vector3.one / 3f;

        Vector3 interpolationValue;

        while (true)
        {
            float u = (Time.time - timeStart) / timeDuration;

            if (u >= 1)
            {
                u = 1f;

                Destroy(eatGO);
                if (increase)
                {
                    bodyScr.StartAnimEat();
                }

                yield break;
            }

            interpolationValue = (1 - u) * p0 + u * headTr.position;
            tr.position = interpolationValue;

            interpolationValue = (1 - u) * s0 + u * s1;
            tr.localScale = interpolationValue;

            yield return null;
        }
    }
}
