using System.Collections;
using UnityEngine;

public class HeadMove : MonoBehaviour
{
    private Rigidbody rigidB;
    private Body bodyScr;
    private Eat eatScr;

    //Вектор направления движения
    private Vector3 moveVector = new Vector3(0f, 0f, 1f);
    
    //Серидина экрана
    private float middleOfTheScreen;

    //Скорость передвижения змеи
    public float moveSpeed = 15f;
    private float coefMoveSpeedFever = 1f;

    //Ограничение движения вправо и влево
    public bool notMoveRight = false;
    public bool notMoveLeft = false;

    public float uSmall;
    public float uBig;

    public bool fever = false;
    private bool stop = false;

    private bool _contactColor = false;
    private bool waitContactColor = false;

    public Vector3 pos
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public bool contactColor
    {
        get { return _contactColor; }
        set 
        { 
            if (waitContactColor) 
            {
                _contactColor = value;
            } 
        }
    }

    private void Start()
    {
        bodyScr = transform.parent.transform.Find("Body").GetComponent<Body>();
        rigidB = GetComponent<Rigidbody>();
        middleOfTheScreen = Screen.width / 2;
        eatScr = transform.Find("Eat").GetComponent<Eat>();

        bodyScr.u = 0f;
        stop = true;
        coefMoveSpeedFever = 0f;
    }

    public void StartGame()
    {
        bodyScr.u = uBig;
        stop = false;
        coefMoveSpeedFever = 1f;
    }

    private void Update()
    {
        if (fever || stop) return;

        if (Input.GetMouseButtonUp(0))
        {
            moveVector = new Vector3(0f, 0f, 1f);
            bodyScr.u = uBig;
        }

        if (Input.GetMouseButton(0))
        {
            //print(Input.mousePosition);
            float mousePositionX = Input.mousePosition.x;
            float proc;
            float c;

            proc = (mousePositionX - middleOfTheScreen) / middleOfTheScreen * 100f;
            c = 0.01f * proc;

            if (notMoveLeft)
            {
                if (c > 0)
                {
                    moveVector = new Vector3(c, 0f, 1f - Mathf.Abs(c));
                    bodyScr.u = uSmall;
                }
                else
                {
                    moveVector = new Vector3(0f, 0f, 1f);
                    bodyScr.u = uBig;
                }
            }
            else if (notMoveRight)
            {
                if (c < 0)
                {
                    moveVector = new Vector3(c, 0f, 1f - Mathf.Abs(c));
                    bodyScr.u = uSmall;
                }
                else
                {
                    moveVector = new Vector3(0f, 0f, 1f);
                    bodyScr.u = uBig;
                }
            }
            else
            {
                moveVector = new Vector3(c, 0f, 1f - Mathf.Abs(c));
                bodyScr.u = uSmall;
            }
        }
    }

    private void FixedUpdate()
    {
        rigidB.velocity = moveVector * (moveSpeed * coefMoveSpeedFever);
        LookAhead();
    }

    private void LookAhead()
    {
        //Ориентировать куб в направлении движения
        transform.LookAt(pos + rigidB.velocity);
    }

    public void StartFever()
    {
        fever = true;
        coefMoveSpeedFever = 3f;
        moveVector = new Vector3(0f, 0f, 1f);
        bodyScr.u = uBig * 1.83f;

        StartCoroutine(OffsetToTheCenter());
        StartCoroutine(CounterTimeFever());
    }

    public void StopFever()
    {
        waitContactColor = false;

        fever = false;
        coefMoveSpeedFever = 1f;
        bodyScr.u = uBig;

        eatScr.NullCountCrystal();
    }

    public void Stop()
    {
        stop = true;
        moveVector = new Vector3(0f, 0f, 1f);

        StopFever();
        StartCoroutine(StopMove());
    }

    IEnumerator OffsetToTheCenter()
    {
        if (transform.localPosition.x == 0) yield break;

        float timeStart = Time.time;
        float x0 = this.transform.position.x;
        float x1 = 0f;
        float x01;

        float timeDurtation = 1f;

        while (true)
        {
            float u = (Time.time - timeStart) / timeDurtation;

            if (u >= 1)
            {
                u = 1f;

                x01 = (1 - u) * x0 + u * x1;

                Vector3 _pos = this.transform.position;
                _pos.x = x01;

                pos = _pos;

                yield break;
            }

            x01 = (1 - u) * x0 + u * x1;

            Vector3 newPos = this.transform.position;
            newPos.x = x01;

            pos = newPos;

            yield return null;
        }
    }

    IEnumerator CounterTimeFever()
    {
        yield return new WaitForSeconds(5f);

        waitContactColor = true;

        while (true)
        {
            if (contactColor)
            {
                contactColor = false;

                StopFever();

                yield break;
            }

            yield return null;
        }
    }

    IEnumerator StopMove()
    {
        float timeStart = Time.time;
        float s0 = moveSpeed;
        float s1 = 0f;
        float s01;

        float u0 = bodyScr.u;
        float u1 = 0f;
        float u01;

        float timeDurtation = 0.5f;

        while (true)
        {
            float u = (Time.time - timeStart) / timeDurtation;

            if (u >= 1)
            {
                u = 1f;

                s01 = (1 - u) * s0 + u * s1;
                moveSpeed = s01;

                u01 = (1 - u) * u0 + u * u1;
                bodyScr.u = u01;

                yield break;
            }

            s01 = (1 - u) * s0 + u * s1;
            moveSpeed = s01;

            u01 = (1 - u) * u0 + u * u1;
            bodyScr.u = u01;

            yield return null;
        }
    }
}
