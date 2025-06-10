using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sair : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Item.ObterContador() == 7)
        {
            animator1.SetBool("Open", true);
            animator2.SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator1.SetBool("Open", false);
            animator2.SetBool("Open", false);
        }
    }
}
