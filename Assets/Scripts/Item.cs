using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public FP scriptFP;
    private bool playerDentro = false;
    public TextMeshProUGUI texto;
    private static int contador = 0;
    private static int Maxcontador = 7;
    public GameObject E;
    public GameObject canvasMinigame; // Arraste o canvas do minigame aqui no Inspector
    private bool minigameAtivo = false;

    void Start()
    {
        AtualizarTexto();
        canvasMinigame.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDentro = true;
            E.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDentro = false;
            E.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerDentro && Input.GetKeyDown(KeyCode.E) && !minigameAtivo)
        {
            minigameAtivo = true;
            E.SetActive(false);
            canvasMinigame.SetActive(true);
            Time.timeScale = 0f; // Pausa o jogo
            scriptFP.enabled = false;
            MinigameController.Instance.AtivarMinigame(this);
        }
    }

    // Este método deve ser chamado pelo botão "concluir" do minigame
    public void MinigameConcluido()
    {
        canvasMinigame.SetActive(false);
        Time.timeScale = 1f;
        if (contador < Maxcontador)
        {
            contador++;
            AtualizarTexto();
            scriptFP.enabled = true;
        }
        Destroy(gameObject);
    }

    private void AtualizarTexto()
    {
        texto.text = contador + " / " + Maxcontador;
    }

    public static void ResetarItens()
    {
        contador = 0;
    }

}
