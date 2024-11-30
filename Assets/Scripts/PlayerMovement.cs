using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 mousePos;
    Transform previewArrow;
    Rigidbody rb;
    Animator anim;
    float imageLocalScale = 0.09f;
    public bool isMoving = false;
    public static bool isChange = false;
    public RuntimeAnimatorController animator_Change;
    public float DashDistance = 5f;
    private Vector3 DashDirection = Vector3.zero;
    private Vector3 StartPosition = Vector3.zero;
    private float distanceTraveled;
    
    

    private bool listenChange = true;
    //Vector3 direction;

    private void Awake()
    {
        previewArrow = GetComponent<Transform>().GetChild(1);
        rb = gameObject.GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //ChangeAvatar
        if (listenChange)
        {
            if (isChange)
            {
                transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = animator_Change;
                anim = transform.GetChild(0).GetComponent<Animator>();
                
                listenChange = false;
            }
        }
        
        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Log the name of the object hit by the raycast
            //Debug.Log("Hit: " + hit.collider.name);
            //direction = hit.point - transform.position;
            previewArrow.LookAt(new Vector3(hit.point.x, 0, hit.point.z));
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (!isMoving)
            {   
                Debug.unityLogger.Log("开始冲刺");
                DashDirection = previewArrow.forward;
                StartPosition= gameObject.transform.position;
                //StartCoroutine(Dash(DashDirection));
                isMoving = true;
            }
            /*
            if (isChange)
            {
                rb.AddForce(previewArrow.forward * 12, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(previewArrow.forward * 10, ForceMode.Impulse);
            }
            if (!isMoving)
            {

            }
            */            
        }

        //Debug.Log(rb.velocity);
        /*
        if (rb.velocity.magnitude > 0.55f)
        {
            anim.SetBool("isMoving", true);
            isMoving = true;
        }
        else
        {
            anim.SetBool("isMoving", false);
            isMoving = false;
        }
        */
        
        if(rb.velocity.x >= 0.5f)
        {
            transform.GetChild(0).localScale = new Vector3(-imageLocalScale, imageLocalScale, imageLocalScale);
        }
        else if (rb.velocity.x <= -0.5f)
        {
            transform.GetChild(0).localScale = new Vector3(imageLocalScale, imageLocalScale, imageLocalScale);
        }

    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            if (distanceTraveled < DashDistance)
            {
                Debug.Log("dashdirection:"+DashDirection);
                rb.velocity = DashDirection * DashDistance * Time.deltaTime * 10f;
                distanceTraveled += Vector3.Distance(StartPosition, gameObject.transform.position);
                Debug.Log(distanceTraveled);
            }
            else
            {
                // 停止运动
                rb.velocity = Vector3.zero;
                distanceTraveled = 0;
                isMoving = false;
            }
        }
    }
    

    void OnCollisionEnter(Collision collision)
    {
        // 获取碰撞法线
        Vector3 normal = collision.contacts[0].normal;

        // 计算弹射方向
        Vector3 reflectDirection = Vector3.Reflect(rb.velocity.normalized, normal);
        //reflectDirection = new Vector3(-reflectDirection.x, 0, reflectDirection.z);

        // 继续冲刺剩余距离
        DashDirection = reflectDirection;
    }
    
}
