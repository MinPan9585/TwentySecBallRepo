using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    //延迟时间
    public float waitTime;
    private bool _isSpike = false;
    bool isUp = true;
    float speed = 1f;
    float timer = 3f;
    Animator anim;
    BoxCollider bc;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        bc = gameObject.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _isSpike = false;
        Invoke("Wait", waitTime);
    }

    void Update()
    {
        if (_isSpike)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isUp = !isUp;
                timer = 3f;
            }

            if (isUp)
            {
                anim.SetTrigger("up");
                bc.enabled = true;
                //if(transform.position.y <= 0f)
                //{
                //    transform.position += Vector3.up * speed * Time.deltaTime;
                //}
                //else
                //{
                //    return;
                //}
            }
            else
            {
                anim.SetTrigger("down");
                bc.enabled = false;
                //if (transform.position.y >= -0.5f)
                //{
                //    transform.position += Vector3.down * speed * Time.deltaTime;
                //}
                //else
                //{
                //    return;
                //}
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(SpikeHurtCd1.instance.canHurt == false)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            SpikeHurtCd1.instance.cdTimer = 1;
            SpikeHurtCd1.instance.canHurt = false;
            
            other.GetComponent<PlayerHealth>().GetHurt();
            SFXManager.instance.PlaySFX(SFXManager.instance.spikeClip);
        }
    }

    private void Wait()
    {
        _isSpike = true;
    }
}
