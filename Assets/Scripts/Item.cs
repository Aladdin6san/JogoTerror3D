using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public FP scriptFP;
    private bool playerDentro = false;
    public TextMeshProUGUI texto;
    private static int contador = 0;
    private static int Maxcontador = 7;
    public GameObject E;
    public GameObject canvasMinigame;
    private bool minigameAtivo = false;

    public GameObject inimigo;         
    public Transform spawnPoint;

    public delegate void ItensColetadosAction(int quantidade);
    public static event ItensColetadosAction OnItensColetados;

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
            Time.timeScale = 0f;
            scriptFP.enabled = false;
            MinigameController.Instance.AtivarMinigame(this);
        }
    }

    public void MinigameConcluido()
    {
        canvasMinigame.SetActive(false);
        Time.timeScale = 1f;

        if (contador < Maxcontador)
        {
            contador++;
            AtualizarTexto();
            OnItensColetados?.Invoke(contador);
            scriptFP.enabled = true;

            // Verifica se coletou todos os itens
            if (contador == Maxcontador)
            {
                AtivarInimigo();
            }
        }

        gameObject.SetActive(false);
    }

    private void AtualizarTexto()
    {
        texto.text = contador + " / " + Maxcontador;
    }

    public static void ResetarItens()
    {
        contador = 0;
    }

    private void AtivarInimigo()
    {
        if (inimigo != null && spawnPoint != null)
        {
            inimigo.transform.position = spawnPoint.position;
            inimigo.transform.rotation = spawnPoint.rotation;
            inimigo.SetActive(true);
        }
    }

    public static int ObterContador()
    {
        return contador;
    }

    public static void DefinirContador(int valor)
    {
        contador = valor;
    }
    public static int GetContador()
    {
        return contador;
    }

}
