using UnityEngine;

public class BodySelectColor : MonoBehaviour
{
    private Renderer render;
    private HeadSelectColor headColor;

    private void Start()
    {
        render = GetComponent<Renderer>();
        headColor = transform.parent.transform.parent.transform.Find("Head").GetComponent<HeadSelectColor>();
        NewColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Color")
        {
            NewColor();
        }
    }

    public void NewColor()
    {
        render.material.color = headColor.myColor;
    }
}
