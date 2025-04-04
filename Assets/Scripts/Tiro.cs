using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Tiro : MonoBehaviour
{
    private bool podeColetar = false; // Verifica se o jogador est� perto da bala
    public GameObject bulletPrefab; // Prefab da bala para atirar
    public Transform spawn; // Local onde a bala ser� instanciada
    public float speed = 10;
    private int municao = 0;
    public TextMeshProUGUI texto;

    private float tempoProximoTiro = 0f;
    public float delayTiro = 4f;

    private GameObject balaNaCena; // Refer�ncia para o objeto colet�vel na cena

    private void Start()
    {
        AtualizarTexto();
    }

    private void AtualizarTexto()
    {
        texto.text = municao + " X ";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            podeColetar = true;
            balaNaCena = other.gameObject; // Guarda a refer�ncia para o objeto colet�vel
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            podeColetar = false;
            balaNaCena = null; // Remove a refer�ncia quando sai da �rea da bala
        }
    }

    private void Update()
    {
        // Coletar a bala
        if (podeColetar && Input.GetKeyDown(KeyCode.E))
        {
            if (balaNaCena != null) // Verifica se tem uma bala para coletar
            {
                Destroy(balaNaCena); // Destroi o objeto colet�vel da cena
                municao++;
                AtualizarTexto();
            }
        }

        // Disparar a bala
        if (Input.GetKeyDown(KeyCode.Mouse0) && municao > 0 && Time.time >= tempoProximoTiro)
        {
            municao--;
            AtualizarTexto();

            // Criar uma c�pia do prefab da bala para ser disparada
            GameObject novaBala = Instantiate(bulletPrefab, spawn.position, spawn.rotation);
            novaBala.GetComponent<Rigidbody>().velocity = spawn.forward * speed;

            Debug.Log("Tiro disparado");

            tempoProximoTiro = Time.time + delayTiro;
        }
    }
}
