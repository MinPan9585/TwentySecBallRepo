using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    bool isHit = false;
    bool startAction = false;
    public GameObject bullet;
    Animator anim;

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

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
        Debug.Log("Starting EnemyOneAction");

        for (int i = 0; i < 2; i++)
        {
            Debug.Log("Jump " + (i + 1));
            anim.SetTrigger("jump");
            transform.position += new Vector3(0.3f, 0f, 0f);
            yield return new WaitForSeconds(1f);
        }

        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Attack " + (i + 1));
            anim.SetTrigger("attack");
            Instantiate(bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }

        startAction = false;
        Debug.Log("Ending EnemyOneAction");
    }

    IEnumerator PauseThree()
    {
        anim.SetTrigger("getHit");
        yield return new WaitForSeconds(3);
        isHit = false;
        startAction = false;
    }
}
