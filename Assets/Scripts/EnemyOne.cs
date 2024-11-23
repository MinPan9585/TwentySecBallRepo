using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    bool isHit = false;
    bool isPausing = false;
    bool startAction = false;
    public GameObject bullet;
    Animator anim;
    Vector3 playerPos;
    PlayerMovement pm;
    Rigidbody rb;
    public float bulletSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        pm = GameObject.Find("Character").GetComponent<PlayerMovement>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (pm.isMoving)
            {
                isHit = true;
            }
            else
            {
                pm.GetComponent<PlayerHealth>().GetHurt();
            }
        }
    }

    void Update()
    {
        //Debug.Log(isHit);
        playerPos = GameObject.Find("Character").transform.position;

        if (isHit && !isPausing)
        {
            isPausing = true;
            StopAllCoroutines();
            StartCoroutine(PauseThree());
        }
        if (!isHit && !startAction)
        {
            startAction = true;
            //jump 2 times, shoot 3 bullets, then repeat
            StartCoroutine(EnemyOneAction());
        }
    }

    IEnumerator EnemyOneAction()
    {
        //Debug.Log("Starting EnemyOneAction");

        for (int i = 0; i < 2; i++)
        {
            //Debug.Log("Jump " + (i + 1));
            anim.SetTrigger("jump");
            Vector3 dir = playerPos - transform.position;
            //transform.position += new Vector3(0.3f, 0f, 0f);
            rb.AddForce(dir.normalized * 4, ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
        }

        for (int i = 0; i < 3; i++)
        {
            //Debug.Log("Attack " + (i + 1));
            anim.SetTrigger("attack");
            GameObject bulletInstance = Instantiate(bullet, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
            bulletInstance.GetComponent<Rigidbody>().AddForce((playerPos - transform.position).normalized * bulletSpeed, ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
        }

        startAction = false;
        //Debug.Log("Ending EnemyOneAction");
    }

    IEnumerator PauseThree()
    {
        Debug.Log("start");
        anim.SetTrigger("getHit");
        yield return new WaitForSeconds(3);
        isHit = false;
        startAction = false;
        isPausing = false;
        Debug.Log("end");
    }
}
