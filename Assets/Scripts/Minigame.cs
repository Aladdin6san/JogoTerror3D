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

    private float tempoDentroDaFaixa = 0f;
    private const float TEMPO_NECESSARIO = 4f;
    private bool minigameAtivo = false;
    private bool estaDentroDaFaixa = false;

    public AudioSource musicaDeFundo;

    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        float valorInicial = Mathf.Round(scrollbar.value * 10f) / 10f;

        do
        {
            valorAlvo = Mathf.Round(Random.Range(0f, 1f) * 10f) / 10f;
        } while (Mathf.Abs(valorAlvo - 0f) < TOLERANCIA);

        Debug.Log("Valor alvo: " + valorAlvo);
    }

    void Update()
    {
        if (!minigameAtivo) return;
        scrollbar.value = Mathf.Round(scrollbar.value * 10f) / 10f;

        float distancia = Mathf.Abs(scrollbar.value - valorAlvo);
        bool dentro = distancia < TOLERANCIA;

        if (dentro)
        {
            tempoDentroDaFaixa += Time.unscaledDeltaTime;

            if (!estaDentroDaFaixa)
            {
                estaDentroDaFaixa = true;

                // Trocar som
                if (itemAtual.audioForaFaixa != null)
                    itemAtual.audioForaFaixa.Stop();
                if (itemAtual.objetoSomForaFaixa != null)
                    itemAtual.objetoSomForaFaixa.SetActive(false);

                if (itemAtual.objetoSomDentroFaixa != null)
                    itemAtual.objetoSomDentroFaixa.SetActive(true);
                if (itemAtual.audioDentroFaixa != null)
                    itemAtual.audioDentroFaixa.Play();
            }

            if (tempoDentroDaFaixa >= TEMPO_NECESSARIO)
            {
                ConcluirMinigame();
            }
        }
        else
        {
            tempoDentroDaFaixa = 0f;

            if (estaDentroDaFaixa)
            {
                estaDentroDaFaixa = false;

                // Voltar ao som fora da faixa
                if (itemAtual.audioDentroFaixa != null)
                    itemAtual.audioDentroFaixa.Stop();
                if (itemAtual.objetoSomDentroFaixa != null)
                    itemAtual.objetoSomDentroFaixa.SetActive(false);

                if (itemAtual.objetoSomForaFaixa != null)
                    itemAtual.objetoSomForaFaixa.SetActive(true);
                if (itemAtual.audioForaFaixa != null)
                    itemAtual.audioForaFaixa.Play();
            }
        }

    }

    public void AtivarMinigame(Item item)
    {
        itemAtual = item;
        painelMinigame.SetActive(true);
        scrollbar.value = 0f;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        tempoDentroDaFaixa = 0f;
        estaDentroDaFaixa = false;
        minigameAtivo = true;

        // Pausar música de fundo
        if (musicaDeFundo != null && musicaDeFundo.isPlaying)
            musicaDeFundo.Pause();

        // Ativar objeto e som de fora da faixa
        if (item.objetoSomForaFaixa != null)
            item.objetoSomForaFaixa.SetActive(true);

        if (item.audioForaFaixa != null)
            item.audioForaFaixa.Play();
    }


    public void Aumentar()
    {
        scrollbar.value = Mathf.Min(scrollbar.value + incremento, 1f);
    }

    public void Diminuir()
    {
        scrollbar.value = Mathf.Max(scrollbar.value - incremento, 0f);
    }

    private void ConcluirMinigame()
    {
        minigameAtivo = false;

        painelMinigame.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Parar e desativar todos os sons
        if (itemAtual.audioDentroFaixa != null)
            itemAtual.audioDentroFaixa.Stop();
        if (itemAtual.objetoSomDentroFaixa != null)
            itemAtual.objetoSomDentroFaixa.SetActive(false);

        if (itemAtual.audioForaFaixa != null)
            itemAtual.audioForaFaixa.Stop();
        if (itemAtual.objetoSomForaFaixa != null)
            itemAtual.objetoSomForaFaixa.SetActive(false);

        // Retomar música de fundo
        if (musicaDeFundo != null)
            musicaDeFundo.UnPause();

        if (itemAtual != null)
        {
            itemAtual.MinigameConcluido();
            itemAtual = null;
        }
    }

}
