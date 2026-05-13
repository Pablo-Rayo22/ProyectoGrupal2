using Unity.VisualScripting;
using UnityEngine;

public class Parasitar : MonoBehaviour
{
    public GameObject proyectil;
    public MovimientoJugador jugador;
    public MovimientoEnemigo enemigo;
    public IAEnemigo IAEnemigo;
    public PatrullaEnemigo patrulla;
    public CamaraSeguimiento camara;

    private bool enemigoParasitado = false;
    private Renderer[] renderers;
    private void Awake()
    {
        enemigo.enabled = false;
        
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
            Debug.Log("Enemigo infectado: " + enemigo.name);

            OcultarPersonaje();

            
            enemigo.enabled = true; // Activamos  el control del enemigo
            IAEnemigo.enabled = false; //desactivamos su IA
            patrulla.enabled = false; //Desactivamos su patrulla
            

            
            camara.objetivo = enemigo.transform; // Pasamos la cámara a enemigo
        }

    }
        private void OcultarPersonaje() {
        
            renderers = jugador.GetComponentsInChildren<Renderer>();
            for (int i = 0; i<renderers.Length; i++) {
                //Desactiva el renderizado del jugador y sus componentes hijos
                renderers[i].enabled = false;
            }
            jugador.GetComponent<Rigidbody>().isKinematic = true;
            jugador.GetComponent<CapsuleCollider>().enabled = false; // Desactiva el collider del jugador

            jugador.enabled = false;
            jugador.audioPasos.Stop();
            jugador.audioPasos.enabled = false;
            jugador.GetComponentInChildren<AudioSource>().Stop();
        }

    private void Desinfectar() {
            enemigoParasitado = false;
            if (enemigo != null)
            {
                Destroy(enemigo.gameObject);
            }

            jugador.transform.position = enemigo.transform.position;

            renderers = jugador.GetComponentsInChildren<Renderer>();


            for (int i = 1; i < renderers.Length; i++)
            {
                renderers[i].enabled = true;
            }
            jugador.GetComponent<Rigidbody>().isKinematic = false;
            jugador.GetComponent<CapsuleCollider>().enabled = true; // Activa el collider del jugador

            jugador.enabled = true; //Devolvemos el control al jugador
            camara.objetivo = jugador.transform; //Pasamos la cámara al jugador
            jugador.audioPasos.enabled = true;
            jugador.audioPasos.Play();
            
    }
}
