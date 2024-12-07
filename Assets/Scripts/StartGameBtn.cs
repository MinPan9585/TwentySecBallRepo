using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameBtn : MonoBehaviour
{
    public GameObject charaIdle;
    public GameObject charaEnter;
    //public GameObject coverIdle;
    public GameObject coverEnter;

    public void EnterGame()
    {
        StartCoroutine(StartGame());
        
    }

    IEnumerator StartGame()
    {
        //load scene after animation

        charaIdle.SetActive(false);
        charaEnter.SetActive(true);
        //coverIdle.SetActive(false);
        yield return new WaitForSeconds(3f);

        coverEnter.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Sun");
    }
}
