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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movePoint.parent = null; 
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movespeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            float horizontal = 0f;
            float vertical = 0f;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) {horizontal = 1f;} // the Totoreal was6 years old so Input.GetAxisRaw("Horizontal") was out dated as i was using the player input sistem
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) {horizontal = -1f;}

            if (horizontal == 0f) // preventing you from diagnale movment 
            {
                if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) {vertical = 1f;} // and after a good time of surching i found it Keyboard.current.wKey.isPressed CONTROLS
                else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) {vertical = -1f;}
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
    }
}
