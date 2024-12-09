using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMenu : MonoBehaviour
{
    public GameObject creditPage;

    public void TurnOnPage()
    {
        creditPage.SetActive(true);
    }

    public void TurnOffPage()
    {
        creditPage.SetActive(false);
    }
}
