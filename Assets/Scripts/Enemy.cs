using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public string GameOver;

    public float distanciaLimite = 15f; // distância para trocar de velocidade
    public float fatorAceleracao = 2f;  // quanto multiplicar a velocidade original quando longe

    private float velocidadeOriginal;

    private bool paralisado = false;
    private float tempoParalisado = 0f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        velocidadeOriginal = agent.speed; // salva a velocidade configurada no Inspector
    }

    void Update()
    {
        if (paralisado)
        {
            tempoParalisado -= Time.deltaTime;

            if (tempoParalisado <= 0)
            {
                paralisado = false;
            }
            else
            {
                agent.speed = 0;
                return; // Não faz nada enquanto estiver paralisado
            }
        }

        agent.destination = player.position;

        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia >= distanciaLimite)
        {
            agent.speed = velocidadeOriginal * fatorAceleracao;
        }
        else
        {
            agent.speed = velocidadeOriginal;
        }
    }

    // Novo método público para pausar o inimigo externamente
    public void PararTemporariamente(float tempo)
    {
        paralisado = true;
        tempoParalisado = tempo;
        agent.speed = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(GameOver);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Item.ResetarItens();
        }
    }

}
