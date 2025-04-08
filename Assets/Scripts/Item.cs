using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    private bool playerDentro = false;
    public TextMeshProUGUI texto;
    private static int contador = 0;
    private static int Maxcontador = 7;
    public GameObject textoUI;


    void Start()
    {
        AtualizarTexto(); // Define o valor inicial
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colidiu");
            playerDentro = true;
            textoUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDentro = false;
            textoUI.SetActive(false);
        }
    }

    private void AtualizarTexto()
    {
        texto.text = contador + " / " + Maxcontador;
    }

    private void Update()
    {
        if (playerDentro && Input.GetKeyDown(KeyCode.E))
        {
            if (contador < Maxcontador)
            {
                contador++;
                AtualizarTexto();
            }

            Destroy(gameObject);
        }
    }

}
