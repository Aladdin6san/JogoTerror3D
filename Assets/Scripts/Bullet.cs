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

            Enemy inimigo = enemy.GetComponent<Enemy>();
            if (inimigo != null)
            {
                inimigo.PararTemporariamente(3f);
            }
        }
    }
}
