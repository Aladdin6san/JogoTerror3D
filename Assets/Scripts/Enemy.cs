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
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();      
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
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
