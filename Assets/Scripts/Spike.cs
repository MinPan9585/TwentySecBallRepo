using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    bool isUp = true;
    float speed = 1f;
    float timer = 3f;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isUp = !isUp;
            timer = 3f;
        }

        if (isUp)
        {
            if(transform.position.y <= 0f)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
            else
            {
                return;
            }
        }
        else
        {
            if (transform.position.y >= -0.5f)
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
            else
            {
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().GetHurt();
            SFXManager.instance.PlaySFX(SFXManager.instance.spikeClip);
        }
    }
}
