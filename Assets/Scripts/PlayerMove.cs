using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;
    public float jump = 5.0f;
    private float horizontal;
    private float foward;
    private Rigidbody rb;
    public bool Onground = true;
    private int JumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pega os inputs
        horizontal = Input.GetAxis("Horizontal");
        foward = Input.GetAxis("Vertical");

        //Mexer o jogador para frente e para trás
        transform.Translate(Vector3.forward *Time.deltaTime *speed*foward);
        //Mexe o jogador para os lados
        transform.Translate(Vector3.right*Time.deltaTime *speed*horizontal);

        //Pulo 
        if(Input.GetKeyDown(KeyCode.Space) && Onground && JumpCount<2)
        {
            rb.AddForce(Vector3.up*jump,ForceMode.Impulse);
            JumpCount++;
            if(JumpCount>2)
            {
                Onground = false;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Chao"))
        {
            JumpCount = 0;
            Onground =true;
        }
    }
}
