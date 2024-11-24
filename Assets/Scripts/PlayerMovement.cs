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

    private bool listenChange = true;
    //Vector3 direction;

    private void Awake()
    {
        previewArrow = GetComponent<Transform>().GetChild(1);
        rb = GetComponent<Rigidbody>();
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
            //rb.AddForce(direction.normalized * 10, ForceMode.Impulse);
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
        }

        //Debug.Log(rb.velocity.magnitude);

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

        if(rb.velocity.x >= 0.5f)
        {
            transform.GetChild(0).localScale = new Vector3(-imageLocalScale, imageLocalScale, imageLocalScale);
        }
        else if (rb.velocity.x <= -0.5f)
        {
            transform.GetChild(0).localScale = new Vector3(imageLocalScale, imageLocalScale, imageLocalScale);
        }

    }

    
}
