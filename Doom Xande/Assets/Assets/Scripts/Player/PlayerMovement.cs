using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement & Rotate")]

    [Tooltip("Controla a velocidade do Player")]
    [SerializeField] float speed = 2f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float damping = 5f;

    [Tooltip("Controla a rotação do Player")]
    [SerializeField] float rotateSpeed = 2f;

    [Header("HeadBob")]

    [Tooltip("Frequencia da balançada de cabeça")]
    [SerializeField] float bobFrequency = 1.5f;

    [Tooltip("O quanto vai pra cima")]
    [SerializeField] float bobHeight = 0.05f;

    [Tooltip("O quanto vai pros lados")]
    [SerializeField] float bobWidth = 0.05f;     

    Rigidbody rb;
    Vector3 originalCameraPosition;
    float bobTimer;

    [HideInInspector] public bool isDead;
    bool isMoving = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        originalCameraPosition = Camera.main.transform.localPosition;  
    }

    private void Update()
    {
        if (!isDead)
        {
            Walk();
            Rotate();
            Headbob();
        }
       
    }

    public void Walk()
    {
        float verti = Input.GetAxis("Vertical");
        isMoving = verti != 0;
        if (isMoving) 
        {
            Vector3 movement = transform.forward * verti * speed;
            rb.AddForce(movement, ForceMode.VelocityChange);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        else
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * damping);



    }

    public void Rotate()
    {
        float hori = Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.fixedDeltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, hori, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    public void Headbob()
    {
        if (isMoving)
        {
            bobTimer += Time.deltaTime * bobFrequency;

            float bobbingAmountY = Mathf.Sin(bobTimer) * bobHeight;
            float bobbingAmountX = Mathf.Sin(bobTimer * 2) * bobWidth;

            Camera.main.transform.localPosition = originalCameraPosition + new Vector3(bobbingAmountX, bobbingAmountY, 0f);
        }
        else
        {
            bobTimer = 0f;
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, originalCameraPosition, Time.deltaTime * bobFrequency);
        }
    }
}
