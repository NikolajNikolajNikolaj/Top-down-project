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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movePoint.parent = null; 

        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;
        bool Left_true = false;
        char Derecshen = 'f';


        if (transform.position == movePoint.position) 
        {
            anim.SetBool("run_LR", false);
            anim.SetBool("run_B", false);
            anim.SetBool("run_f", false);
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movespeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {


            if (Keyboard.current.dKey.isPressed) // the Totoreal was 6 years old so Input.GetAxisRaw("horizontal") was out dated as i was using the player input sistem
            {
                horizontal = 1f;

                Left_true = false;
                Derecshen = 'l';

                playerSpriteRenderer.flipX = false; //  move Rithe

                anim.SetBool("run_B",false);
                anim.SetBool("run_f",false);

                anim.SetBool("run_LR",true);

                anim.SetBool("idal_f",false);
                anim.SetBool("idal_B",false);
                anim.SetBool("idal_LR",false);

            } 
            else if (Keyboard.current.aKey.isPressed) 
            {
                horizontal = -1f;

                Left_true = true;
                Derecshen = 'l';

                playerSpriteRenderer.flipX = true; // move left

                anim.SetBool("run_B",false);
                anim.SetBool("run_f",false);

                anim.SetBool("run_LR",true);

                anim.SetBool("idal_f",false);
                anim.SetBool("idal_B",false);
                anim.SetBool("idal_LR",false);

            }


            if (horizontal == 0f) // preventing you from diagnale movment 
            {
                if (Keyboard.current.wKey.isPressed) // and after a good time of surching i found it Keyboard.current.wKey.isPressed CONTROLS
                { 
                    vertical = 1f;

                    Derecshen = 'b';

                    playerSpriteRenderer.flipX = false; // move up

                    anim.SetBool("run_f",false);
                    anim.SetBool("run_LR",false);

                    anim.SetBool("run_B",true);

                    anim.SetBool("idal_f",false);
                    anim.SetBool("idal_B",false);
                    anim.SetBool("idal_LR",false);

                    Left_true = false;

                } 
                else if (Keyboard.current.sKey.isPressed) 
                {
                    vertical = -1f;

                    Derecshen = 'f';

                    playerSpriteRenderer.flipX = false; // move dawen

                    anim.SetBool("run_B",false);
                    anim.SetBool("run_LR",false);

                    anim.SetBool("run_f",true);

                    anim.SetBool("idal_f",false);
                    anim.SetBool("idal_B",false);
                    anim.SetBool("idal_LR",false);

                    Left_true = false;

                }
            }

            if (horizontal != 0f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position +  new Vector3(horizontal, 0f, 0f) , .2f , whatStopsMovement))
                {
                    movePoint.position += new Vector3(horizontal, 0f, 0f);
                }
            }
            
            if (vertical != 0f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, vertical, 0f), .2f , whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, vertical, 0f);
                }
            }
        }
        if(transform.position ==  movePoint.position) 
        {
            if(Derecshen == 'f')
            {
                anim.SetBool("idal_f",true);

                anim.SetBool("idal_B",false);
                anim.SetBool("idal_LR",false);
            }
            else if (Derecshen == 'b')
            {
                anim.SetBool("idal_B",true);

                anim.SetBool("idal_f",false);
                anim.SetBool("idal_LR",false);
            }
            else if (Derecshen == 'l')
            {
                anim.SetBool("idal_LR",true);

                anim.SetBool("idal_f",false);
                anim.SetBool("idal_B",false);
            }

        }

        
        if (anim.GetBool("idal_f") || anim.GetBool("idal_LR") || anim.GetBool("idal_B"))
        {
            if(Keyboard.current.eKey.isPressed)
            {
                Debug.Log("E");
                if (anim.GetBool("idal_f"))
                {
                    playerSpriteRenderer.flipX = false;
                    anim.SetTrigger("interact_f");
                }
                else if (anim.GetBool("idal_LR"))
                {
                    if(Left_true)
                    {
                        playerSpriteRenderer.flipX = true;
                        anim.SetTrigger("interact_LR");
                    }
                    else{
                        playerSpriteRenderer.flipX = false;
                        anim.SetTrigger("interact_LR");
                    }
                }
                else if (anim.GetBool("idal_B"))
                {
                    playerSpriteRenderer.flipX = false;
                    anim.SetTrigger("interact_B");
                }

            }
        }
    }
}
