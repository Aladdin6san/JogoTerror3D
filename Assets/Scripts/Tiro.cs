using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Tiro : MonoBehaviour
{
    private bool podeColetar = false; // Verifica se o jogador está perto da bala
    public GameObject bulletPrefab; // Prefab da bala para atirar
    public Transform spawn; // Local onde a bala será instanciada
    public float speed = 10;
    private int municao = 0;
    public TextMeshProUGUI texto;
    private float tempoProximoTiro = 0f;
    public float delayTiro = 4f;

    private GameObject balaNaCena; // Referência para o objeto coletável na cena
    public GameObject E;

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
            balaNaCena = other.gameObject; // Guarda a referência para o objeto coletável
            E.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            podeColetar = false;
            balaNaCena = null; // Remove a referência quando sai da área da bala
            E.SetActive(false);
        }
    }

    private void Update()
    {
        if (podeColetar && Input.GetKeyDown(KeyCode.E))
        {
            if (balaNaCena != null && balaNaCena.activeSelf) // Verifica se a bala ainda está ativa
            {
                balaNaCena.SetActive(false); // Desativa o objeto coletável da cena
                municao++;
                AtualizarTexto();
                E.SetActive(false);

                // Resetar variáveis para evitar múltiplas coletas
                balaNaCena = null;
                podeColetar = false;
            }
        }

        // Disparar a bala
        if (Input.GetKeyDown(KeyCode.F) && municao > 0 && Time.time >= tempoProximoTiro)
        {

            municao--;
            AtualizarTexto();

            // Criar uma cópia do prefab da bala para ser disparada
            GameObject novaBala = Instantiate(bulletPrefab, spawn.position, spawn.rotation);
            novaBala.GetComponent<Rigidbody>().velocity = spawn.forward * speed;

            Debug.Log("Tiro disparado");

            tempoProximoTiro = Time.time + delayTiro;
        }
    }

    public int ObterMunicao()
    {
        return municao;
    }

    public void DefinirMunicao(int valor)
    {
        municao = valor;
        AtualizarTexto();
    }

}
