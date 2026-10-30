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

    public int HP_player = 3; // the HP of the AC
    public int PlayerLives = 3;
    private bool isDead = false;

    public float respon_x;
    public float respon_y;

    float horizontal = 0f;
    float vertical = 0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("on start");
        movePoint.parent = null;

        anim = gameObject.GetComponent<Animator>();
    }


    private void  Respon() 
    { 
        horizontal = 0f;
        vertical = 0f;
        
        Vector3 spawnPos = new Vector3(respon_x, respon_y, 0f); // this is were you and the pointer respon
        
        
        transform.position = spawnPos; // teleport palyer
        movePoint.position = spawnPos; // teleport pointer 

        came.position = new Vector3(spawnPos.x, spawnPos.y, -10f);

        Iam_i_moving = false;
        Iam_idel = true;
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
        if(!isDead)
        {
            if(HP_player <= 0)
            {
                if(PlayerLives <= 0)
                {
                    isDead = true;
                    anim.SetTrigger("Die_box");
                    float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
                    Destroy(gameObject, animationLength*2f);
                    //need to restet level
                }
                else
                {
                    //need to create dead body
                    anim.SetTrigger("Die_box");
                    PlayerLives--;
                    HP_player = 3;
                    Respon();
                }
            }
        }

        horizontal = 0f;
        vertical = 0f;

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

            // you wont beleve it wile wocking a nuther video i found a sulushen to my camera problem from erelear
            // olso the camera onle folows you wen you stop it more suted for a open world then a puzel game but watever it looks good
            Vector3 targetCameraPos = new Vector3(movePoint.position.x, movePoint.position.y, -10f);
            came.position = Vector3.MoveTowards(came.position, targetCameraPos, movespeed * Time.deltaTime * 0.5f);
            

        }
    }

}
