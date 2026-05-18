using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class Parasitar : MonoBehaviour
{
    public float tiempoEspera = 3f;

    [SerializeField] private GameObject proyectil;
    [SerializeField] private MovimientoJugador jugador;
    [SerializeField] private MovimientoEnemigoGolem enemigoGolem;
    [SerializeField] private MovimientoEnemigoFire enemigoFire;
    [SerializeField] private IAenemigo iaEnemigo;
    [SerializeField] private PatrullaEnemigo patrulla;
    [SerializeField] private CamaraSeguimiento camara;

    private bool enemigoParasitado = false;
    private string nombreEnemigo;
    private Renderer[] renderers;
    private string[] tiposEnemigos = { "Golem", "Fire" };
    private string tipoEnemigo = "";
    private Animator animator;
    private bool muerto;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        tipoEnemigo = this.transform.gameObject.name;
        enemigoGolem.enabled = false;
        enemigoFire.enabled = false;
        
    }
    
    private void Update()
    {
        if (enemigoParasitado && Input.GetKey(KeyCode.Escape))
        {
            Desinfectar();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Infectar(collision.gameObject);
    }

    void Infectar(GameObject collider)
    {
        if (!enemigoParasitado && collider.CompareTag("Proyectil")) {
            enemigoParasitado = true;
            
            OcultarPersonaje();

            if (tipoEnemigo == tiposEnemigos[0])
            {
                enemigoGolem.enabled = true; // Activamos  el control del enemigo
                patrulla.enabled = false; // Desactivamos su patrulla
                camara.objetivo = enemigoGolem.transform;
            }

            if (tipoEnemigo == tiposEnemigos[1])
            {
                enemigoFire.enabled = true; // Activamos  el control del enemigo
                iaEnemigo.enabled = false; // Desactivamos su IA
                camara.objetivo = enemigoFire.transform;
            }
                         
        }

    }

    private void OcultarPersonaje() {
            
            jugador.GetComponent<Rigidbody>().isKinematic = true; 
            jugador.GetComponent<CapsuleCollider>().enabled = false; //Desactivamos el collider
            renderers = jugador.GetComponentsInChildren<Renderer>();
            for (int i = 0; i<renderers.Length; i++) {
                //Desactiva el renderizado del jugador y sus componentes hijos
                renderers[i].enabled = false;
            }

            jugador.enabled = false;
            jugador.audioPasos.Stop();
            jugador.audioPasos.enabled = false;
            jugador.GetComponentInChildren<AudioSource>().Stop();
    }

    private void Desinfectar() {
        enemigoParasitado = false;

        StartCoroutine(morir(tiempoEspera));

        jugador.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        jugador.GetComponent<Rigidbody>().isKinematic = false;
        jugador.GetComponent<CapsuleCollider>().enabled = true; //Activamos el collider

        renderers = jugador.GetComponentsInChildren<Renderer>();


        for (int i = 1; i < renderers.Length; i++)
        {
            renderers[i].enabled = true;
        }
            
        jugador.enabled = true; //Devolvemos el control al jugador
        camara.objetivo = jugador.transform; //Pasamos la cámara al jugador
        jugador.audioPasos.enabled = true;
        jugador.audioPasos.Play();
    }
    //private void morir()
    //{
    //    animator.SetTrigger("Muerto");
    //}
    private IEnumerator morir(float tiempoEspera)
    {
        animator.SetTrigger("Muerto");
        yield return new WaitForSeconds(tiempoEspera);
        if (tipoEnemigo == tiposEnemigos[0]) {
            if (enemigoGolem != null)
            {
                Destroy (enemigoGolem.gameObject);
            }
        }
        if (tipoEnemigo == tiposEnemigos[1])
        {
            if (enemigoFire != null)
            {
                Destroy (enemigoFire.gameObject);
            }
        }
    }
}