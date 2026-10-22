using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rigidbodyplayer; // colishen? or sumthing like that

    public float playerSpeed = 5; // speed of movment

    private Vector2 movementbrecshen; // player movment

    private Animator animator; // inchard of anamashens

    private SpriteRenderer playerSpriteRenderer; // for fliping sprith

    //public PlayerAudioController playerAudio; // 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbodyplayer = gameObject.GetComponent<Rigidbody2D>(); // calling Rigidbody2D at the start 

        animator = gameObject.GetComponent<Animator>(); // calling Animator at the start

        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>(); // calling SpriteRenderer at the start
    }

    // Update is called once per frame
    void Update()
    { // -Y = fowerd    y =  up 
        if(movementbrecshen != Vector2.zero)// checking if movment isint 0
        {
            //animator.SetBool("Walking",true); // starts Walking animaton
            
            //playerAudio.PlayerWalkingAudio(true);
            if (movementbrecshen.x < 0)
            {
                playerSpriteRenderer.flipX = true; // fliping sprith
            }
            else
            {
                playerSpriteRenderer.flipX = false; // stop fliping sprith
            }
        }
        else{
            //animator.SetBool("Walking",false); //ending Walking animaton
            //playerAudio.PlayerWalkingAudio(false);
        }
    }

    private void FixedUpdate() // updats at a fixt time
    {
        rigidbodyplayer.MovePosition(rigidbodyplayer.position + (movementbrecshen * playerSpeed * Time.fixedDeltaTime)); // calculats movment
    }

    void OnMove(InputValue MovePosition){ // sets movement derecshen?
        movementbrecshen = MovePosition.Get<Vector2>();
    }

    void OnAttack(){
        if(movementbrecshen == Vector2.zero){
            //animator.SetTrigger("Attack"); // attack
            //playerAudio.PlayAttackAudio();
        }
    }

}
