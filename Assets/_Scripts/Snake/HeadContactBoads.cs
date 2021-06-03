using UnityEngine;

public class HeadContactBoads : MonoBehaviour
{
    private HeadMove headMoveScr;

    private void Start()
    {
        headMoveScr = transform.parent.GetComponent<HeadMove>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "BoardLeft")
        {
            headMoveScr.notMoveLeft = true;
        }
        else if (other.tag == "BoardRight")
        {
            headMoveScr.notMoveRight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BoardLeft")
        {
            headMoveScr.notMoveLeft = false;
        }
        else if (other.tag == "BoardRight")
        {
            headMoveScr.notMoveRight = false;
        }
    }
}
