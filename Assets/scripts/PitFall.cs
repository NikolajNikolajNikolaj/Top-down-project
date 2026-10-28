using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFall : MonoBehaviour
{
    public string targetTag = "AC"; // Tag attached to your moving box
    
    [Header("AC Components")]
    public GameObject TheAC;          // Drag your Box GameObject here
    private Animator TheACAnim;       // Will store the Box's Animator component

    [Header("Alignment Settings")]
    [Tooltip("How close to the center of the pit must the box be before falling?")]
    public float fallDistanceThreshold = 0.2f; 

    private bool hasFallen = false;  // Prevents the script from running multiple times

    void Start()
    {
        if (TheAC != null)
        {
            TheACAnim = TheAC.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("ERROR: You forgot to drag your AC GameObject into 'The AC' slot on the PitFall script!", this);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (hasFallen) return;

        if (other.CompareTag(targetTag))
        {
            // Calculate the distance between the Box's center and the Pit's center
            float distanceToCenter = Vector2.Distance(other.transform.position, transform.position);

            // ONLY drop the box if it has slid close enough to the center of the pit
            if (distanceToCenter <= fallDistanceThreshold)
            {
                Debug.Log("AC perfectly centered! Dropping the AC.");
                StartCoroutine(ACFallSequence());
            }
        }
    }

    private IEnumerator ACFallSequence()
    {
        hasFallen = true;

        // Snap the AC's final position perfectly to the center of the pit tile so the animation looks flawless
        TheAC.transform.position = transform.position;

        // Play the falling animation
        if (TheACAnim != null)
        {
            TheACAnim.SetTrigger("AC_fall");
        }

        // Wait for the animation to finish playing (Adjust 1.0f to match your animation's length)
        yield return new WaitForSeconds(1.0f);

        Destroy(TheAC);
        Debug.Log("AC destroyed!");
    }
}