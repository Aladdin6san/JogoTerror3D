using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    public NavMeshAgent enemy;
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
        if (other.gameObject.CompareTag("Tiro"))
        {
            Destroy(other.gameObject);
            StartCoroutine(PauseEnemySpeed(3f));
        }
    }

    // Coroutine para pausar a velocidade do inimigo e restaurar após 3 segundos
    private IEnumerator PauseEnemySpeed(float delay)
    {
        float originalSpeed = enemy.speed; // Armazena a velocidade original do inimigo
        enemy.speed = 0; // Zera a velocidade do inimigo

        yield return new WaitForSeconds(delay); // Espera o tempo definido

        enemy.speed = originalSpeed; // Restaura a velocidade original do inimigo
    }
}
