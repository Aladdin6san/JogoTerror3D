using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saida : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && Item.ObterContador() == 7)
        {
            SceneManager.LoadScene("Fim");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
