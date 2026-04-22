using UnityEngine;

[RequireComponent (typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;

    public float gravedadExtra = 20f;
    public float gravedadCaida = 45f;

    // 🔥 DISPARO
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float fuerzaDisparo = 15f;

    // 🔊 SONIDO PASOS
    public AudioSource audioPasos;
    public AudioClip sonidoPasos;

    private Rigidbody rb;
    private Animator animator;

    private bool enSuelo;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        float movZ = Input.GetAxisRaw("Vertical");

        Vector3 direccion = new Vector3(movX, 0, movZ).normalized;

        // ROTACIÓN
        if (direccion != Vector3.zero)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, 15f * Time.deltaTime);
        }

        // SALTO
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, fuerzaSalto, rb.linearVelocity.z);
        }

        // DISPARO
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }

        // 🔊 SONIDO DE PASOS
        if (direccion.magnitude > 0.1f && enSuelo)
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

        // ANIMACIONES
        animator.SetFloat("Velocidad", direccion.magnitude);
        animator.SetBool("EnSuelo", enSuelo);
    }

    void FixedUpdate()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        float movZ = Input.GetAxisRaw("Vertical");

        Vector3 direccion = new Vector3(movX, 0, movZ).normalized;

        // DETECTAR SUELO
        enSuelo = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // DETECTAR PARED
        bool hayPared = Physics.Raycast(transform.position, transform.forward, 0.7f);

        // MOVIMIENTO
        Vector3 velocidadFinal;

        if (!enSuelo && hayPared)
        {
            velocidadFinal = new Vector3(0, rb.linearVelocity.y, 0);
        }
        else
        {
            velocidadFinal = new Vector3(
                direccion.x * velocidad,
                rb.linearVelocity.y,
                direccion.z * velocidad
            );
        }

        rb.linearVelocity = velocidadFinal;

        // GRAVEDAD
        if (!enSuelo)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.AddForce(Vector3.down * gravedadExtra, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(Vector3.down * gravedadCaida, ForceMode.Acceleration);
            }
        }

        // EVITAR GIROS LOCOS
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    void Disparar()
    {
        GameObject proyectil = Instantiate(
            proyectilPrefab,
            puntoDisparo.position,
            transform.rotation
        );

        Rigidbody rbProyectil = proyectil.GetComponent<Rigidbody>();
        rbProyectil.linearVelocity = transform.forward * fuerzaDisparo;
    }
}