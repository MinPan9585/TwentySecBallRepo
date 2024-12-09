using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutPanel : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    public EndGame eg;

    public void Once()
    {
        image1.SetActive(false);
        image2.SetActive(true);

        Debug.Log("111");
    }

    public void Twice()
    {
        image2.SetActive(false);

        eg.isStarted = true;
    }
}
