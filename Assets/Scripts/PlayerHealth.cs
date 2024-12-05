using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    int currentHealth;
    public Image hpBar;
    public GameObject gameOverText;
    public GameObject hurtvfx;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            gameOverText.SetActive(true);
            Time.timeScale = 0;
        }
        hpBar.fillAmount = (float)currentHealth / maxHealth;
    }

    public void GetHurt()
    {
        currentHealth--;
        //play sfx and vfx
        Instantiate(hurtvfx, transform.position, Quaternion.identity);
    }
}
