using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 mousePos;
    public Transform previewArrow;
    public Rigidbody rb;
    public Animator anim;
    float imageLocalScale = 0.09f;
    //Vector3 direction;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Log the name of the object hit by the raycast
            Debug.Log("Hit: " + hit.collider.name);
            //direction = hit.point - transform.position;
            previewArrow.LookAt(new Vector3(hit.point.x, 0, hit.point.z));
        }

        if (Input.GetMouseButtonDown(0))
        {
            //rb.AddForce(direction.normalized * 10, ForceMode.Impulse);
            rb.AddForce(previewArrow.forward * 10, ForceMode.Impulse);
        }

        Debug.Log(rb.velocity.magnitude);

        if (rb.velocity.magnitude > 0.55f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
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
