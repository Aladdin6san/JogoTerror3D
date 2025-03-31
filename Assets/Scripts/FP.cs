using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP : MonoBehaviour
{

    public Transform player;
    public float mouse = 2f;
    float rotVertical = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        float inputX = Input.GetAxis("Mouse X")*mouse;
        float inputY = Input.GetAxis("Mouse Y") * mouse;

        rotVertical -= inputY;
        rotVertical = Math.Clamp(rotVertical, -90f, 90f);
        transform.localEulerAngles = Vector3.right * rotVertical;

        player.Rotate(Vector3.up * inputX);

    }
}
