using UnityEngine;

public class BlockBreakingBad : MonoBehaviour 
{ 
    private Animator brickAnim; 
    [SerializeField] private PressButten targetButton; 

    private bool hasBroken = false; 

    void Start() 
    { 
        brickAnim = GetComponent<Animator>(); 
    } 

    void Update() 
    { 
        if (!hasBroken) 
        {
            if (targetButton.Butten_prest) 
            { 
                TriggerBrickBreak();
            } 
        }

        
    } 

    private void TriggerBrickBreak()
    {
        hasBroken = true; 
        brickAnim.SetTrigger("Block_brocken"); 

        float animationLength = brickAnim.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animationLength*2f);
        Debug.Log("Block brockin"); 
    }
}