using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridPlayerMovment : MonoBehaviour
{
    public float movespeed = 1f;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public Animator anim;

    private SpriteRenderer playerSpriteRenderer;

    // State tracking moved here so they persist between frames
    private bool Left_true = false;
    private char Derecshen = 'f';
    private bool Iam_i_moving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movePoint.parent = null;

        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        anim = gameObject.GetComponent<Animator>();
    }

    private void ResetAllAnimationBools()
    {
        anim.SetBool("idal_f", false);
        anim.SetBool("idal_B", false);
        anim.SetBool("idal_LR", false);
        anim.SetBool("run_LR", false);
        anim.SetBool("run_B", false);
        anim.SetBool("run_f", false);
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
            if (Derecshen == 'f' && !anim.GetBool("idal_f")) 
            {
                playerSpriteRenderer.flipX = false;
                ResetAllAnimationBools();
                anim.SetBool("idal_f", true);
            } 
            else if (Derecshen == 'b' && !anim.GetBool("idal_B")) 
            {
                playerSpriteRenderer.flipX = false;
                ResetAllAnimationBools();
                anim.SetBool("idal_B", true);
            } 
            else if (Derecshen == 'l' && !anim.GetBool("idal_LR")) 
            {
                playerSpriteRenderer.flipX = true;
                ResetAllAnimationBools();
                anim.SetBool("idal_LR", true);
            } 
            else if (Derecshen == 'r' && !anim.GetBool("idal_LR")) 
            {
                playerSpriteRenderer.flipX = false;
                ResetAllAnimationBools();
                anim.SetBool("idal_LR", true);
            } 
        }

        // if idel you can interact
        if (anim.GetBool("idal_f") || anim.GetBool("idal_LR") || anim.GetBool("idal_B"))
        {
            if (Keyboard.current.eKey.isPressed) //if E was prest at this moment  Keyboard.current.eKey.wasPressedThisFrame
            {
                if (anim.GetBool("idal_f"))
                {
                    playerSpriteRenderer.flipX = false;
                    anim.SetTrigger("interact_f");
                }
                else if (anim.GetBool("idal_LR"))
                {
                    playerSpriteRenderer.flipX = Left_true; // see if you need to flip the sprith or not
                    anim.SetTrigger("interact_LR");
                }
                else if (anim.GetBool("idal_B"))
                {
                    playerSpriteRenderer.flipX = false;
                    anim.SetTrigger("interact_B");
                }
            }
        }

        // continue moving if not at target
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movespeed * Time.deltaTime);

        // small delay befor you can input agen i cant seem to notes it
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Keyboard.current.dKey.isPressed)
            {
                horizontal = 1f;

                Left_true = false;
                Iam_i_moving = true;

                Derecshen = 'r';
                playerSpriteRenderer.flipX = false;

                anim.SetBool("run_B", false);
                anim.SetBool("run_f", false);

                anim.SetBool("run_LR", true);

                anim.SetBool("idal_f", false);
                anim.SetBool("idal_B", false);
                anim.SetBool("idal_LR", false);
            }
            else if (Keyboard.current.aKey.isPressed)
            {
                horizontal = -1f;

                Left_true = true;
                Iam_i_moving = true;

                Derecshen = 'l';
                playerSpriteRenderer.flipX = true;

                anim.SetBool("run_B", false);
                anim.SetBool("run_f", false);

                anim.SetBool("run_LR", true);

                anim.SetBool("idal_f", false);
                anim.SetBool("idal_B", false);
                anim.SetBool("idal_LR", false);
            }

            if (horizontal == 0f) 
            {
                if (Keyboard.current.wKey.isPressed)
                {
                    Iam_i_moving = true;

                    vertical = 1f;
                    Derecshen = 'b';

                    playerSpriteRenderer.flipX = false;

                    anim.SetBool("run_f", false);
                    anim.SetBool("run_LR", false);

                    anim.SetBool("run_B", true);

                    anim.SetBool("idal_f", false);
                    anim.SetBool("idal_B", false);
                    anim.SetBool("idal_LR", false);

                    Left_true = false;
                }
                else if (Keyboard.current.sKey.isPressed)
                {
                    Iam_i_moving = true;

                    vertical = -1f;
                    Derecshen = 'f';

                    playerSpriteRenderer.flipX = false;

                    anim.SetBool("run_B", false);
                    anim.SetBool("run_LR", false);
                    anim.SetBool("run_f", true);

                    anim.SetBool("idal_f", false);
                    anim.SetBool("idal_B", false);
                    anim.SetBool("idal_LR", false);

                    Left_true = false;
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
