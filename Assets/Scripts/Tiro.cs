using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tiro : MonoBehaviour
{
    private bool Bala = false;
    public GameObject bullet;
    public Transform spawn;
    public float speed = 10;
    private int municao = 0;
    public NavMeshAgent enemy;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            Bala = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            Bala = false;
        }
    }

    private void Update()
    {
        if (Bala && Input.GetKeyDown(KeyCode.E))
        {
            bullet.SetActive(false); // Desativa o item
            bullet.transform.position = spawn.position;
            municao++;
        }

        if(Input.GetKeyDown(KeyCode.F) && municao>0)
        {
            municao--;
            bullet.SetActive(true);
            var tiro = Instantiate(bullet, spawn.position, spawn.rotation);
            tiro.GetComponent<Rigidbody>().velocity = spawn.forward*speed;
            Debug.Log("Tiro disparado");
        }

    }
}
