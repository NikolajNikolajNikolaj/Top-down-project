using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFall : MonoBehaviour
{
    public string targetTag = "AC"; // the tag of the AC
    public GameObject TheAC;         // add the AC object 
    private Animator TheACAnim;       // anamater ofr AC
    public float howclose = 0.2f; // the distens from the senter of pit 

    private bool hasFallen = false;  // is it dead yet?

    void Start()
    {
        TheACAnim = TheAC.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)// pic ecolocater
    {
        if (hasFallen) return; // FALL true ?

        if (other.CompareTag(targetTag)) // is it the AC
        {
            float distanceToCenter = Vector2.Distance(other.transform.position, transform.position); // the AC center and pit center distens

            if (distanceToCenter <= howclose) // FALL IF close
            {
                Debug.Log("DEPLOY THE AC");
                StartCoroutine(ACFallSequence());//AC fall
            }
        }
    }

    private IEnumerator ACFallSequence()
    {
        hasFallen = true;
        TheAC.transform.position = transform.position; // was off so not enemore
        TheACAnim.SetTrigger("AC_fall"); // play the anamashen
        yield return new WaitForSeconds(1.0f); // befor destrucshen
        Destroy(TheAC);
        Debug.Log("AC destroyed!");
    }
}