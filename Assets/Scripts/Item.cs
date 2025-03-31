using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool playerDentro = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colidiu");
            playerDentro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDentro = false;
        }
    }

    private void Update()
    {
        if (playerDentro && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }
}
