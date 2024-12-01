using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{
    public GameObject gameOverText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("碰到结束点");
            gameOverText.GetComponent<Text>().text = "You Win!";
            gameOverText.SetActive(true);
            Time.timeScale = 0;
        }
        //throw new NotImplementedException();
    }
}
