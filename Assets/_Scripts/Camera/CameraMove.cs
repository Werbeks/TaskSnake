using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject poi; //Point of Interest
    public float u = 0.22f;
    public Vector3 p0, p1, p01;

    private void FixedUpdate()
    {
        p0 = this.transform.position;
        p1 = p0;
        p1.z = poi.transform.position.z;
        p1.z -= 15f;

        p01 = (1 - u) * p0 + p1 * u;

        this.transform.position = p01;
    }
}
