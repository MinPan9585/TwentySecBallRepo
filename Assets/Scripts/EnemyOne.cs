using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    bool isHit = false;
    public GameObject bullet;

    void Update()
    {
        if (isHit)
        {
            //stop action for 3 seconds
        }
        if (!isHit)
        {
            //jump 2 times, shoot 3 bullets, then repeat
        }
    }


}
