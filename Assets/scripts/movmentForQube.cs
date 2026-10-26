using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movmentForQube : MonoBehaviour
{
    public float movespeed = 1f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public Animator anim;

    private SpriteRenderer playerSpriteRenderer;

    
    private bool Iam_i_moving = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movePoint.parent = null;

        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        anim = gameObject.GetComponent<Animator>();
    }

    private void ResetAllAnimationBools() // this is temperere
    {
        anim.SetBool("Idal_box", false);
        anim.SetTrigger("Run_box");
        anim.SetTrigger("Die_box");
        anim.SetTrigger("Damaged_box");
        anim.SetTrigger("Interact_Box");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Reset movement flag if we have arrived exactly at the move point
        if (transform.position == movePoint.position)
        {
            Iam_i_moving = false;
        }

        // Idle animation logic when stationary
        if (transform.position == movePoint.position && !Iam_i_moving) 
        {
            anim.SetBool("Idal_box", true);
        }

        // if idel you can interact
        if (anim.GetBool("Idal_box"))
        {
            if (Keyboard.current.eKey.isPressed) //if E was prest at this moment  Keyboard.current.eKey.wasPressedThisFrame
            {
                anim.SetTrigger("Interact_Box");
            }
        }

        // continue moving if not at target
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movespeed * Time.deltaTime);

        // small delay befor you can input agen i cant seem to notes it
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Keyboard.current.dKey.isPressed)
            {
                Iam_i_moving = true;
                horizontal = 1f;
                anim.SetTrigger("Run_box");
            }
            else if (Keyboard.current.aKey.isPressed)
            {
                Iam_i_moving = true;
                horizontal = -1f;
                anim.SetTrigger("Run_box");
            }

            if (horizontal == 0f) 
            {
                if (Keyboard.current.wKey.isPressed)
                {
                    Iam_i_moving = true;
                    vertical = 1f;
                    anim.SetTrigger("Run_box");
                }
                else if (Keyboard.current.sKey.isPressed)
                {
                    Iam_i_moving = true;
                    vertical = -1f;
                    anim.SetTrigger("Run_box");
                }
            }

            // Grid target updates
            if (horizontal != 0f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontal, 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(horizontal, 0f, 0f);
                }
            }
            if (vertical != 0f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, vertical, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, vertical, 0f); 
                }
            }
        }
    }

}
