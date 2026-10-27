using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AcMovment : MonoBehaviour
{
    public float movespeed = 1f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public Animator anim;

    float horizontal = 0f;
    float vertical = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (gameObject.name) 
        {
            case "Top":
                Debug.Log("iteract top");
                IteractTopCollision(other);
                break;

            case "Botem":
                Debug.Log("iteract botem");
                IteractBotemCollision(other);
                break;

            case "Left":
                Debug.Log("iteract left");
                IteractLeftCollision(other);
                break;

            case "Rithe":
                Debug.Log("iteract rithe");
                IteractRitheCollision(other);
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("on start");
        movePoint.parent = null;

        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = 0f;
        vertical = 0f;

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            // grid movment
            if (horizontal != 0f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontal, 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(horizontal, 0f, 0f);
                    Debug.Log("horizontal movment ");
                }
            }
            if (vertical != 0f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, vertical, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, vertical, 0f);
                    Debug.Log("vertical movment ");
                }
            }
        }
        
    }


    private void IteractTopCollision(Collider2D other)
    {
        vertical = 1f;
        Debug.Log("Sucsess");
    }
    private void IteractBotemCollision(Collider2D other)
    {
        vertical = -1f;
        Debug.Log("Sucsess");
    }
    private void IteractLeftCollision(Collider2D other)
    {
        horizontal = -1f;
        Debug.Log("Sucsess");
    }
    private void IteractRitheCollision(Collider2D other) 
    {
        horizontal = 1f;
        Debug.Log("Sucsess");
    }
}
