using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHurtCd1 : MonoBehaviour
{
    public static SpikeHurtCd1 instance;    
    public float cdTimer = 1;
    public bool canHurt;

    private void Awake()
    {
        instance = this;

    }

    void Update()
    {
        cdTimer -= Time.deltaTime;
        if(cdTimer <= 0)
        {
            canHurt = true;

        }
    }
}
