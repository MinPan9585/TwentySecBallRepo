using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectEnergy : MonoBehaviour
{
    bool isCollecting = true;
    int energy = 8;
    public Image energyBarImage;

    private void Start()
    {
        energyBarImage.fillAmount = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && isCollecting)
        {
            
            energy++;
            energyBarImage.fillAmount = energy / 10f;
            if (energy == 10)
            {
                // change to final state
                isCollecting = false;
                PlayerMovement.isChange = true;
            }
        }
    }
}
