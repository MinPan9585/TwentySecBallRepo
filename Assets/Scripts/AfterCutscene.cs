using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterCutscene : MonoBehaviour
{
    float timer = 5f;
    public string nextScene;

    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;

        if(timer < 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
