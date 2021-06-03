using UnityEngine;

public class Death : MonoBehaviour
{
    private Body bodyScr;
    private HeadMove headMScr;
    private ReloadScene reloadScene;

    public bool fever = false;

    private void Start()
    {
        reloadScene = Camera.main.GetComponent<ReloadScene>();
        bodyScr = transform.parent.transform.parent.transform.Find("Body").GetComponent<Body>();
        headMScr = transform.parent.GetComponent<HeadMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (headMScr.fever) return;

        if (!bodyScr.death && (other.tag == "Enemy" || other.tag == "Spike"))
        {
            bodyScr.death = true;
            Lose();
            reloadScene.NewGame();
        }
    }

    private void Lose()
    {
        transform.parent.GetComponent<MeshRenderer>().enabled = false;
        headMScr.enabled = false;

        bodyScr.StartLose();

        Destroy(transform.parent.transform.Find("Eat").gameObject);
        Destroy(transform.parent.transform.Find("ContactBoard").gameObject);

        this.enabled = false;
    }
}
