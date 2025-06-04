using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{
    public Animator animator; // arraste o Animator no Inspector
    public GameObject caixa;
    public GameObject outro;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            caixa.SetActive(true);
            animator.SetBool("Fall", true); // ativa o bool Fall
            Destroy(outro);
        }
    }
}
