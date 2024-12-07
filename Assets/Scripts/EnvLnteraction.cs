using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvLnteraction : MonoBehaviour
{
    public GameObject destroyVFX;

    public void DestroyObject()
    {
        if(destroyVFX != null)
        {
            Instantiate(destroyVFX, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    
}
