using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButten : MonoBehaviour
{
    private Animator anim;

    public string targetTag = "AC";

    public bool Butten_prest = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (!Butten_prest && collision.gameObject.CompareTag(targetTag))
        {
            anim.SetTrigger("Butten_on");
            Butten_prest = true;
            Debug.Log("The AC prest butten");
        }
    }
}
