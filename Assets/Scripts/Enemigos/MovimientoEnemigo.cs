using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{


    private CharacterController controller;
    //private //abc //abc;
    private Transform cameraTransform;


    public float velocidadCaminando = 5;
    public float velocidadCorriendo = 8f;
    public float velocidadRotacion = 100f;
    public float alturaSalto = 1.5f;
    float gravedad = -9.81f;



    private Vector3 velocidad;
    private bool enSuelo;
    private float velocidadActual;

    // 🔥 DISPARO
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float fuerzaDisparo = 15f;

    // 🔊 SONIDO PASOS
    public AudioSource audioPasos;
    public AudioClip sonidoPasos;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
       // //abc = GetComponent<//abc>();
        cameraTransform = Camera.main.transform;

    }

    void Update()
    {
        //  Verificar si está en el suelo
        enSuelo = controller.isGrounded;

        //  Correr (Shift izquierdo)
        bool estaCorriendo = Input.GetKey(KeyCode.LeftShift);

        if (enSuelo && velocidad.y < 0)
        {
            velocidad.y = -2f;
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

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? velocidadCorriendo : velocidadCaminando;

        // Mover al personaje
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

        
        // Hacer que el personaje gire hacia donde se está moviendo
        //if (moveDirection.magnitude > 0.1f)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        //}
                
        //  Salto
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            velocidad.y = Mathf.Sqrt(alturaSalto * -2f * gravedad);
            //abc.SetTrigger("Jump"); // Animación de salto
        }

        //  Aplicar gravedad
        velocidad.y += gravedad * Time.deltaTime;
        controller.Move(velocidad * Time.deltaTime);

        // ACTUALIZAR ANIMACIONES
        UpdateAnimations(moveDirection.magnitude, estaCorriendo);

        /* DISPARO de bolas de fuego
    if (Input.GetMouseButtonDown(0))
    {
           //Disparar();
    }*/

        // 🔊 SONIDO DE PASOS

        if (moveDirection.magnitude > 0.1f && enSuelo)
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
        //abc.SetFloat("Speed", moveMagnitude);

        // Parámetro para saber si está corriendo
        //abc.SetBool("IsRunning", isRunning && moveMagnitude > 0.1f);

        // Parámetro para saber si está en el suelo
        //abc.SetBool("IsGrounded", isGrounded);

        // Velocidad vertical para animaciones de caída
        //abc.SetFloat("VerticalVelocity", velocity.y);
    }
}