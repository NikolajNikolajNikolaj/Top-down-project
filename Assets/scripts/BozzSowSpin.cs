using UnityEngine;

public class BozzSowSpin : MonoBehaviour
{
    public LayerMask whatStopsMovement;
    public bool isMovingUpAndDawon;
    public Transform movePoint;

    public GameObject Player; // we drag and drop the player Dameobject

    public float howclose = 0.2f;
    public float movespeed = 5f; 

    public float moveDelay = 1.0f; // how fas it pic next slot
    private float currentTimer = 0f; // timer for delay

    private Animator ThePlayerAnim;
    private bool hasPlayerTackenDamage = false;

    private bool HorizontalSwich = false;
    private bool VerticalSwich = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movePoint.parent = null;
        ThePlayerAnim = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movespeed * Time.deltaTime); 
        currentTimer -= Time.deltaTime;

        
        if (currentTimer <= 0f)
        {
            if(isMovingUpAndDawon)
            {
                if(!HorizontalSwich)
                {
                    MoveLeft();
                }
                else
                {
                    MoveRight();
                }
            }
            else
            {
                if(!VerticalSwich)
                {
                    MoveUp();
                }
                else
                {
                    MoveDown();
                }
            }
            currentTimer = moveDelay;
        }
    }

    private void MoveLeft()
    { 
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f, 0f), .2f, whatStopsMovement))
        {
            Debug.Log("Buzzsow Left"); 
            movePoint.position += new Vector3(-1f, 0f, 0f);
        }
        else
        {
            HorizontalSwich = true;
        }
    }

    private void MoveRight() 
    { 
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f, 0f), .2f, whatStopsMovement))
        {
            Debug.Log("Buzzsow rithe"); 
            movePoint.position += new Vector3(1f, 0f, 0f);
        }
        else
        {
            HorizontalSwich = false;
        }
    }

    private void MoveUp()    
    {
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .2f, whatStopsMovement))
        {
            Debug.Log("Buzzsow Up");    
            movePoint.position += new Vector3(0f, 1f, 0f);
        }
        else
        {
            VerticalSwich = true;
        }
    }

    private void MoveDown()  
    
    { 
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .2f, whatStopsMovement))
        {
            Debug.Log("Buzzsow Down");   
            movePoint.position += new Vector3(0f, -1f, 0f);
        }
        else
        {
            VerticalSwich = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.gameObject ==  Player)
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
        if (other.gameObject == Player)
        {
            hasPlayerTackenDamage = false;
            Debug.Log("player has tacken damage reset");
        }
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
