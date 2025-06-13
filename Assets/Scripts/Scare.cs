using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scare : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject jao;
    public Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jao.SetActive(true);
            animator.SetBool("Scare", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            jao.SetActive(false);
            animator.SetBool("Scare", false);
            this.gameObject.SetActive(false);
        }
    }
}
