using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;
    private float horizontal;
    private float foward;
    private Rigidbody rb;
    public bool Onground = true;

    public Image stamina;
    private bool isSprinting = false;
    private Coroutine staminaCoroutine;
    private bool canSprint = true;

    public GameObject controle;
    private bool ligar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stamina.fillAmount = 1f;
        stamina.gameObject.SetActive(false);
    }

    void Update()
    {
        HandleSprintInput();

        horizontal = Input.GetAxis("Horizontal");
        foward = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * foward);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontal);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ligar = !ligar;
            controle.SetActive(ligar);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (ligar == false)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (ligar)
                Time.timeScale = 0f; // pausa o jogo
            else
                Time.timeScale = 1f; // retoma o jogo
        }



    }

    private void HandleSprintInput()
    {
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && canSprint)
        {
            if (!isSprinting && stamina.fillAmount > 0)
            {
                isSprinting = true;
                speed = 7.5f;
                stamina.gameObject.SetActive(true);

                if (staminaCoroutine != null)
                    StopCoroutine(staminaCoroutine);

                staminaCoroutine = StartCoroutine(DrainStamina());
            }
        }
        else
        {
            if (isSprinting)
            {
                isSprinting = false;
                speed = 5.0f;

                if (staminaCoroutine != null)
                    StopCoroutine(staminaCoroutine);

                staminaCoroutine = StartCoroutine(RecoverStamina());
            }
        }
    }


    private IEnumerator DrainStamina()
    {
        while (stamina.fillAmount > 0f && isSprinting)
        {
            stamina.fillAmount -= 0.1f;
            yield return new WaitForSeconds(0.5f);
        }

        if (stamina.fillAmount <= 0f)
        {
            isSprinting = false;
            speed = 3.0f;
            canSprint = false; //trava o sprint
            staminaCoroutine = StartCoroutine(RecoverStamina());
        }
    }


    private IEnumerator RecoverStamina()
    {
        while (stamina.fillAmount < 1f && !isSprinting)
        {
            stamina.fillAmount += 0.1f;
            yield return new WaitForSeconds(1f);
        }

        if (stamina.fillAmount >= 1f)
        {
            stamina.fillAmount = 1f;
            stamina.gameObject.SetActive(false);
            canSprint = true; //  permite sprint de novo
            speed = 5.0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Impede sprint
            canSprint = false;

            // Se estiver correndo, para imediatamente
            if (isSprinting)
            {
                isSprinting = false;
                speed = 3.0f;

                // Para de gastar stamina
                if (staminaCoroutine != null)
                    StopCoroutine(staminaCoroutine);

                // Começa a recuperar stamina
                staminaCoroutine = StartCoroutine(RecoverStamina());
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Impede sprint
            canSprint = false;

            // Se estiver correndo, para imediatamente
            if (isSprinting)
            {
                isSprinting = false;
                speed = 3.0f;

                // Para de gastar stamina
                if (staminaCoroutine != null)
                    StopCoroutine(staminaCoroutine);

                // Começa a recuperar stamina
                staminaCoroutine = StartCoroutine(RecoverStamina());
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Permite sprint de novo ao sair da colisão com a parede
            canSprint = true;

            // Restaura velocidade padrão (caso não esteja sprintando)
            if (!isSprinting)
            {
                speed = 5.0f;
            }
        }
    }


}