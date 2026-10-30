using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaDamage : MonoBehaviour
{
    public GameObject TheAC; // we drag and drop the AC gameobject
    public GameObject Player; // we drag and drop the player Dameobject

    public float howclose = 0.2f;

    private Animator TheACAnim;
    private Animator ThePlayerAnim;
    private bool hasACTackenDamage = false;
    private bool hasPlayerTackenDamage = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TheACAnim = TheAC.GetComponent<Animator>();
        ThePlayerAnim = Player.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject == TheAC)
        {
            if (hasACTackenDamage) return;
            float distanceToCenter = Vector2.Distance(other.transform.position, transform.position); 

            if (distanceToCenter <= howclose)
            {

                Debug.Log("AC trigerd damag");
                ACTackDamage();
            }
        }
        else if(other.gameObject ==  Player)
        {
            if (hasPlayerTackenDamage) return;
            float distanceToCenter = Vector2.Distance(other.transform.position, transform.position); 

            if (distanceToCenter <= howclose)
            {
                Debug.Log("Player trigerd damag");
                PlayerTackDamage();
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Naxt time just you what");
        if (other.gameObject == TheAC)
        {
            hasACTackenDamage = false;
            Debug.Log("AC has tacken damage reset");
        }
        else if (other.gameObject == Player)
        {
            hasPlayerTackenDamage = false;
            Debug.Log("player has tacken damage reset");
        }
    }

    private void ACTackDamage()
    {
        hasACTackenDamage = true;
        TheAC.transform.position = transform.position; // was off so not enemore
        TheACAnim.SetTrigger("AC_Damaged"); // play the anamashen
        Debug.Log("AC damage tacken");
        TheAC.gameObject.GetComponent<AcMovment>().HPAC -= 1; // HPAC is a public int in script AcMovment
    }

    private void PlayerTackDamage()
    {
        hasPlayerTackenDamage = true;
        Player.transform.position = transform.position; // was off so not enemore
        ThePlayerAnim.SetTrigger("Damaged_box"); // play the anamashen
        Debug.Log("Player damage tacken");
        Player.gameObject.GetComponent<movmentForQube>().HP_player -= 1;  // HP_player is a public int in script movmentForQube
    }
}
