using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AfterCutscene : MonoBehaviour
{
    float timer = 4.8f;
    public string nextScene;
    public Image image;

    void Start()
    {
        StartCoroutine(Delay());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(timer);
        StartCoroutine(FadeInImage());
    }
    IEnumerator FadeInImage()
    {
        float duration = 1f; // 渐变时长
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(nextScene);
    }
}
