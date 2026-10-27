using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movmentForQube : MonoBehaviour
{
    public float movespeed = 1f;
    public Transform movePoint;

    public Transform came;

    public LayerMask whatStopsMovement;
    public Animator anim;

    
    private bool Iam_i_moving = false;
    private bool Iam_idel = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("on start");
        movePoint.parent = null;

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

    private void AmIdel()
    {
        anim.SetBool("Idal_box", true);
        Debug.Log("is idel");
    }

    // Update is called once per frame


    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // reset movment if we rech the point
        if (transform.position == movePoint.position)
        {
            Iam_i_moving = false;
        }

        // Idle animation logic when stationary   
        if (transform.position == movePoint.position && !Iam_i_moving && !Iam_idel)
        {
            Iam_idel = true;
            AmIdel();
            Debug.Log("is idel once");
        }

        // if idel you can interact
        if (Keyboard.current.eKey.wasPressedThisFrame) //if E was prest at this moment  
        {
            Debug.Log("Atempt to interact ");
            if (anim.GetBool("Idal_box"))
            {
                anim.SetTrigger("Interact_Box");
                Debug.Log("interact sucsess!");
            }
        }
        

        // continue moving if not at target
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movespeed * Time.deltaTime * 0.5f); 

        //came.position = Vector3.MoveTowards(came.position, movePoint.position, movespeed * Time.deltaTime * 0.5f); // it bracks becus it can i dont know way it worcks for a moment then it bracks
        
        // small delay befor you can input agen i cant seem to notes it
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Keyboard.current.dKey.isPressed)
            {
                Iam_idel = false;
                Iam_i_moving = true;
                horizontal = 1f;
                anim.SetTrigger("Run_box");
                Debug.Log("Moving rith");
            }
            else if (Keyboard.current.aKey.isPressed)
            {
                Iam_idel = false;
                Iam_i_moving = true;
                horizontal = -1f;
                anim.SetTrigger("Run_box");
                Debug.Log("Moving left");
            }

            if (horizontal == 0f) 
            {
                if (Keyboard.current.wKey.isPressed)
                {
                    Iam_idel = false;
                    Iam_i_moving = true;
                    vertical = 1f;
                    anim.SetTrigger("Run_box");
                    Debug.Log(" Moving up");
                }
                else if (Keyboard.current.sKey.isPressed)
                {
                    Iam_idel = false;
                    Iam_i_moving = true;
                    vertical = -1f;
                    anim.SetTrigger("Run_box");
                    Debug.Log("Moving dawen");
                }
            }

            // grid movment
            if (horizontal != 0f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontal, 0f, 0f), .2f, whatStopsMovement))
                { // the resen i plaset the camera in here was becus i whantet do mack that wen the box moves it rotats left then rithe wen moving but for some resn it rotesta the camera to so fuck that 
                    came.position += new Vector3(horizontal, 0f, 0f);  // cudent figer out how to have the camera move slowle so insted we have a moshen sikness macker
                    movePoint.position += new Vector3(horizontal, 0f, 0f);
                    Debug.Log("horizontal movment ");
                }
            }
            if (vertical != 0f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, vertical, 0f), .2f, whatStopsMovement))
                {
                    came.position += new Vector3(0f, vertical, 0f);  // cudent figer out how to have the camera move slowle so insted we have a moshen sikness macker
                    movePoint.position += new Vector3(0f, vertical, 0f);
                    Debug.Log("vertical movment ");
                }
            }
        }
    }

}
