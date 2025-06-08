using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialControlador : MonoBehaviour
{
    public GameObject tutorial;
    void Start()
    {
        StartCoroutine(Painel());
    }

    void Update()
    {

    }

    IEnumerator Painel()
    {
        yield return new WaitForSeconds(3f);
        tutorial.SetActive(false);
    }

    public void QuitToMenu()
    {
        tutorial.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
