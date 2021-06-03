using UnityEngine;

public class BodyCube : MonoBehaviour
{
    private Transform pointTransform;
    private Body bodyScr;
    private Vector3 rigidVelocity;
    private Vector3 p0, p1, p01;

    private float u;

    private void Awake()
    {
        bodyScr = transform.parent.gameObject.GetComponent<Body>();
    }

    void Start()
    {
        if (bodyScr.ObjectInTheList(this.transform))
        {
            pointTransform = bodyScr.PointGO(this.transform);
        }
        else
        {
            bodyScr.AddListGOBody(this.transform);
            pointTransform = bodyScr.PointGO(this.transform);
        }
    }

    private void FixedUpdate()
    {
        u = bodyScr.u;

        p0 = this.transform.position;
        p1 = pointTransform.position;

        p01 = (1 - u) * p0 + p1 * u;

        this.transform.position = p01;

        LookAhead();
    }

    void LookAhead()
    {
        //Ориентировать куб в сторону следующего куба
        transform.LookAt(pointTransform);
    }
}
