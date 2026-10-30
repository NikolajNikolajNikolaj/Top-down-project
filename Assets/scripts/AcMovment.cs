using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcMovment : MonoBehaviour
{
    public float movespeed = 5f; 
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public string targetTag = "Player"; // The tag of Box_player

    private Animator TheACAnim;

    public int HPAC = 3; // the HP of the AC
    private bool isDead = false;

    private bool isMovingBox = false; // is moving box yes or no

    void Start()
    {
        TheACAnim = gameObject.GetComponent<Animator>();
        movePoint.parent = null; // just like in the player
    }

    void Update()
    {
        if(!isDead)
        {
            if(HPAC <= 0)
            {
                isDead = true;
                TheACAnim.SetTrigger("AC_Die");
            }
        }
        else if (!TheACAnim.GetCurrentAnimatorStateInfo(0).IsName("AC_Die"))
        {
            TheACAnim.SetTrigger("AC_Die");
        }
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movespeed * Time.deltaTime);  // move towords nicele
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead) return;
        if (collision.gameObject.CompareTag(targetTag)) //is player?
        {
            
            if (Vector3.Distance(transform.position, movePoint.position) > .05f) return; // complet movment?

            
            Animator playerAnim = collision.gameObject.GetComponent<Animator>(); // anmamater ofr player

            if (playerAnim != null && !isMovingBox)
            {
                
                if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Interact_Box")) //is anmashen playd
                {
                    Debug.Log("interacting");
                    StartCoroutine(MoveBoxWithDelay(collision));
                }
            }
        }
    }
    
    private IEnumerator MoveBoxWithDelay(Collision2D collision) // after anmashen
    {
        isMovingBox = true;

       
        yield return new WaitForSeconds(0.2f); // wait a second!!!!!!!!!!!!!!!!!!!!!

        
        Vector2 collisionNormal = collision.contacts[0].normal; // what side olso you wond beleve me that i used 4 difrent coliders befro just waching a video on how its dun i fell stuped :(

        
        if (Mathf.Abs(collisionNormal.x) > Mathf.Abs(collisionNormal.y)) // horesontal movment
        {
            if (collisionNormal.x < -0.5f) // left
            {
                MoveLeft();
            }
            else if (collisionNormal.x > 0.5f) // rith
            {
                MoveRight();
            }
        }
        
        else // vertical movment
        {
            if (collisionNormal.y < -0.5f) // down
            {
                MoveDown();
            }
            else if (collisionNormal.y > 0.5f) // up 
            {
                MoveUp();
            }
        }

        
        yield return new WaitForSeconds(0.5f); // stop you from oving it instentle
        isMovingBox = false; // ot moving
    }

    private void MoveLeft()
    { 
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f, 0f), .2f, whatStopsMovement))
        {
            Debug.Log("AC left"); 
            movePoint.position += new Vector3(-1f, 0f, 0f);
        }
    }

    private void MoveRight() 
    { 
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f, 0f), .2f, whatStopsMovement))
        {
            Debug.Log("AC rithe"); 
            movePoint.position += new Vector3(1f, 0f, 0f);
        }
    }

    private void MoveUp()    
    {
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .2f, whatStopsMovement))
        {
            Debug.Log("AC Up");    
            movePoint.position += new Vector3(0f, 1f, 0f);
        }
    }

    private void MoveDown()  
    { 
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .2f, whatStopsMovement))
        {
            Debug.Log("AC Down");   
            movePoint.position += new Vector3(0f, -1f, 0f);
        }
    }
}