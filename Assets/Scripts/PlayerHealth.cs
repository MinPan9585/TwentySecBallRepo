using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    int currentHealth;
    Image hpBar;
    public GameObject gameOverText;
    public GameObject gameOverImage;
    public GameObject hurtvfx;

    private void Awake()
    {
        hpBar = GameObject.Find("HealthBarImage").GetComponent<Image>();
    }

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
            //gameOverText.SetActive(true);
            gameOverImage.SetActive(true);
            gameObject.GetComponent<PlayerControl>().anim.SetBool("isDead",true);
            gameObject.GetComponent<PlayerControl>().PlayerDie();
            //Time.timeScale = 0;
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
