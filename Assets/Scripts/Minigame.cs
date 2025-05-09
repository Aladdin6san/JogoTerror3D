using UnityEngine.UI;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    public static MinigameController Instance;

    public Scrollbar scrollbar;
    public GameObject painelMinigame;

    private Item itemAtual;
    private float valorAlvo;
    private float incremento = 0.1f;
    private const float TOLERANCIA = 0.001f;

    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        float valorInicial = Mathf.Round(scrollbar.value * 10f) / 10f;

        // Gera um valor aleatório diferente do inicial
        do
        {
            valorAlvo = Mathf.Round(Random.Range(0f, 1f) * 10f) / 10f;
        } while (Mathf.Abs(valorAlvo - valorInicial) < TOLERANCIA);

        Debug.Log("Valor alvo: " + valorAlvo);
    }

    public void AtivarMinigame(Item item)
    {
        itemAtual = item;
        painelMinigame.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Aumentar()
    {
        scrollbar.value = Mathf.Min(scrollbar.value + incremento, 1f);
        VerificarConclusao();
    }

    public void Diminuir()
    {
        scrollbar.value = Mathf.Max(scrollbar.value - incremento, 0f);
        VerificarConclusao();
    }

    private void VerificarConclusao()
    {
        if (Mathf.Abs(scrollbar.value - valorAlvo) < TOLERANCIA)
        {
            ConcluirMinigame();
        }
    }

    private void ConcluirMinigame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        painelMinigame.SetActive(false);
        Time.timeScale = 1f;

        if (itemAtual != null)
        {
            itemAtual.MinigameConcluido();
            itemAtual = null;
        }
    }
}
