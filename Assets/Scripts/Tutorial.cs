using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialControlador : MonoBehaviour
{
    public GameObject tutorial;
    private static bool tutorialJaExibido = false;
    void Start()
    {
        if (!tutorialJaExibido)
        {
            tutorial.SetActive(true);
            StartCoroutine(Painel());
            tutorialJaExibido = true; // marca como já exibido
        }
        else
        {
            tutorial.SetActive(false);
        }
    }

    void Update()
    {

    }

    IEnumerator Painel()
    {
        yield return new WaitForSeconds(5f);
        tutorial.SetActive(false);
    }
    public void QuitToMenu()
    {
        if (Item.ObterContador() < 6)
        {
            Item.DefinirContador(0);
        }

        tutorial.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
