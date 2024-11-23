using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    bool isHit = false;
    bool startAction = false;
    public GameObject bullet;

    void Update()
    {
        if (isHit)
        {
            StopAllCoroutines();
            StartCoroutine(PauseThree());
        }
        else if (!isHit && !startAction)
        {
            startAction = true;
            //jump 2 times, shoot 3 bullets, then repeat
            StartCoroutine(EnemyOneAction());
        }
    }

    IEnumerator EnemyOneAction()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.position += new Vector3(0.3f, 0f, 0f);
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < 3; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        startAction = false;
    }

    IEnumerator PauseThree()
    {
        yield return new WaitForSeconds(3);
        isHit = false;
        startAction = false;
    }
}
