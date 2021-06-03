using UnityEngine;

public class HeadSelectColor : MonoBehaviour
{
    private HeadMove headMoveScr;
    private Renderer render;
    private Eat eatScr;
    public Color myColor;

    private void Start()
    {
        render = GetComponent<Renderer>();
        headMoveScr = GetComponent<HeadMove>();
        eatScr = this.transform.Find("Eat").GetComponent<Eat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Color")
        {
            myColor = other.GetComponent<SelectColor>().myColor;
            render.material.color = myColor;

            eatScr.countCrystalFever = 0;
            headMoveScr.contactColor = true;
        }
    }
}
