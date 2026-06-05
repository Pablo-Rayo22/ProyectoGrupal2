using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class MovimientoEnemigoGolem : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Transform cameraTransform;

    public float velocidadCaminando = 5f;
    public float velocidadCorriendo = 10f;
    public float velocidadRotacion = 100f;
    public float alcanceGolpe = 1.5f;
    public float tiempoEnAtacar = 0.6f;
    public AudioSource audioPasos;
    public AudioSource audioGolpe;
    public AudioClip sonidoPasos;
    public AudioClip sonidoGolpe;

    private float gravedad = -9.81f;
    private Vector3 velocidad;
    private bool enSuelo;
    private float velocidadActual;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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

        Vector3 moverDireccion = (camForward * z) + (camRight * x);

        velocidadActual = Input.GetKey(KeyCode.LeftShift) ? velocidadCorriendo : velocidadCaminando;

        // Mover al personaje
        controller.Move(moverDireccion * velocidadActual * Time.deltaTime);


        // Hacer que el personaje gire hacia donde se está moviendo
        if (moverDireccion.magnitude > 0.1f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(moverDireccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, 10f * Time.deltaTime);
        }

        //  Aplicar gravedad
        velocidad.y += gravedad * Time.deltaTime;
        controller.Move(velocidad * Time.deltaTime);

        // ACTUALIZAR ANIMACIONES
        ActualizarAnimaciones(moverDireccion.magnitude, estaCorriendo);

        // Atacar
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Atacar(tiempoEnAtacar));
        }
        // Pasos
        reproducirSonidoPasos(moverDireccion);
    }
    private IEnumerator Atacar(float tiempoEnAtacar)
    { 
        animator.SetTrigger("Atacando");
        yield return new WaitForSeconds(tiempoEnAtacar);
        Collider[] colliders = Physics.OverlapSphere(transform.position, alcanceGolpe);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Caja"))
            {
                GameManager.instancia.GenerarDiamantes(colliders[i]);
                reproducirSonidoGolpe();
            }

            if (colliders[i].CompareTag("Roca"))
            {
                RocaRompible roca = colliders[i].GetComponent<RocaRompible>();

                if (roca != null)
                {
                    roca.Romper();
                    reproducirSonidoGolpe();
                }
            }
        }
    }

    void ActualizarAnimaciones(float moverMagnitud, bool estaCorriendo)
    {
        
        // Parámetro para velocidad de movimiento 
        animator.SetFloat("Velocidad", moverMagnitud);
    }

    public void reproducirSonidoPasos(Vector3 moverDireccion)
    {
        if (moverDireccion.magnitude > 0.1f && enSuelo)
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

    private void reproducirSonidoGolpe ()
    {
        audioGolpe.clip = sonidoGolpe;
        audioGolpe.loop = false;
        audioGolpe.Play();
    }
}

