using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public Animation animFog;
    public HeadMove headMoveScr;

    private void Start()
    {
        StartCoroutine(GameStart());
    }

    public void NewGame()
    {
        StartCoroutine(GameReload());
    }

    IEnumerator GameReload()
    {
        yield return new WaitForSeconds(1f);
        animFog.Play("ReloadGame");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("_Scene_Game");
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.5f);
        animFog.Play("StartGame");
        yield return new WaitForSeconds(2f);
        headMoveScr.StartGame();
    }
}
