using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{

    private CharacterController controller;
    private Animator animator;
    private Transform cameraTransform;


    float walkSpeed = 50f;
    float runSpeed = 100f;
    float rotationSpeed = 100f;
    float jumpHeight = 1.5f;
    float gravity = -9.81f;



    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;

    // 🔥 DISPARO
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float fuerzaDisparo = 15f;

    // 🔊 SONIDO PASOS
    public AudioSource audioPasos;
    public AudioClip sonidoPasos;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;

    }

    void Update()
    {
        //  Verificar si está en el suelo
        isGrounded = controller.isGrounded;

        //  Correr (Shift izquierdo)
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //  Movimiento horizontal
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = (camForward * z) + (camRight * x);

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Mover al personaje
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

        
        // Hacer que el personaje gire hacia donde se está moviendo
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
                
        //  Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump"); // Animación de salto
        }

        //  Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // ACTUALIZAR ANIMACIONES
        UpdateAnimations(moveDirection.magnitude, isRunning);

        /* DISPARO de bolas de fuego
    if (Input.GetMouseButtonDown(0))
    {
           //Disparar();
    }*/

        // 🔊 SONIDO DE PASOS

        if (moveDirection.magnitude > 0.1f && isGrounded)
        {
            if (!audioPasos.isPlaying)
            {
                audioPasos.clip = sonidoPasos;
                audioPasos.loop = true;
                audioPasos.Play();
            }
        }
        else
        {
            if (audioPasos.isPlaying)
            {
                audioPasos.Stop();
            }
        }
    }

    void UpdateAnimations(float moveMagnitude, bool isRunning)
    {
        // Parámetro para velocidad de movimiento 
        animator.SetFloat("Speed", moveMagnitude);

        // Parámetro para saber si está corriendo
        animator.SetBool("IsRunning", isRunning && moveMagnitude > 0.1f);

        // Parámetro para saber si está en el suelo
        animator.SetBool("IsGrounded", isGrounded);

        // Velocidad vertical para animaciones de caída
        animator.SetFloat("VerticalVelocity", velocity.y);
    }


    
    





}


   
