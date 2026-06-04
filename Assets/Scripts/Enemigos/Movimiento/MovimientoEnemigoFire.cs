using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MovimientoEnemigoFire : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Transform cameraTransform;


    public float velocidadCaminando = 10f;
    public float velocidadCorriendo = 20f;
    public float velocidadRotacion = 100f;
    public float alturaSalto = 1.5f;

    private float gravedad = -9.81f;
    private Vector3 velocidad;
    private bool enSuelo;
    private float velocidadActual;
    private AudioSource audioPasos;
    public GameObject bola_fuego;
    public Transform spawnPoint;
    private GameObject player;
    public MeshCollider rio;
    


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        audioPasos = GetComponent<AudioSource>();
        player = GameObject.Find("Jugador");
        rio.isTrigger = false;
       

    }

    void Update()
    {





        //  Verificar si est� en el suelo
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

        Vector3 moverDireccion = (camForward * z) + (camRight * x);

        velocidadActual = Input.GetKey(KeyCode.LeftShift) ? velocidadCorriendo : velocidadCaminando;

        // Mover al personaje
        controller.Move(moverDireccion * velocidadActual * Time.deltaTime);


        // Hacer que el personaje gire hacia donde se est� moviendo
        if (moverDireccion.magnitude > 0.1f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(moverDireccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, 10f * Time.deltaTime);
        }

        //  Salto
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            velocidad.y = Mathf.Sqrt(alturaSalto * -2f * gravedad);
            animator.SetTrigger("Jump"); // Animaci�n de salto
        }

        //  Aplicar gravedad
        velocidad.y += gravedad * Time.deltaTime;
        controller.Move(velocidad * Time.deltaTime);

        // ACTUALIZAR ANIMACIONES
        ActualizarAnimacionesFire(moverDireccion.magnitude, estaCorriendo);

        //DISPARO de bolas de fuego
        if (Input.GetMouseButtonDown(0))
        {
            GameObject nuevaBola;
            nuevaBola = Instantiate(bola_fuego, spawnPoint.position, spawnPoint.rotation);
        }

        // 🔊 SONIDO DE PASOS
        if (moverDireccion.magnitude > 0.1f && enSuelo)
        {
            if (!audioPasos.isPlaying)
            {
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


        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f))
        {
            if (hit.collider.tag == "Agua")
            {
                if (!rio.isTrigger)
                    rio.isTrigger = !rio.isTrigger;
                else
                {
                    JugadorVida jugador = player.GetComponent<JugadorVida>();
                    jugador.Morir();
                }
            }
            
        }


        
    }
      
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pocion"))
        {
            GameManager.instancia.vidas++;
            Destroy(other.gameObject);
            GameManager.instancia.ActualizarUI();
        }
    }

    void ActualizarAnimacionesFire(float moverMagnitud, bool estaCorriendo)
    {
        // Par�metro para velocidad de movimiento 
         animator.SetFloat("Velocidad", moverMagnitud);

        //Par�metro para saber si est� corriendo
        animator.SetBool("EstaCorriendo", estaCorriendo && moverMagnitud > 0.1f);

        //Par�metro para saber si est� en el suelo
        animator.SetBool("EnSuelo", enSuelo);

        
    }

       
}