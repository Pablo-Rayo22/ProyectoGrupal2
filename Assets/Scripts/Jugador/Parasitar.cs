using Unity.VisualScripting;
using UnityEngine;

public class Parasitar : MonoBehaviour
{
    public GameObject proyectil;
    public MovimientoJugador jugador;
    public MovimientoEnemigo enemigo;
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

            // Activas control del enemigo
            enemigo.enabled = true;

            // Pasar cámara a enemigo
            camara.objetivo = enemigo.transform;
        }

    }
        private void OcultarPersonaje() {
        
        renderers = jugador.GetComponentsInChildren<Renderer>();
        for (int i = 0; i<renderers.Length; i++) {
            //Desactiva el renderizado del jugador y sus componentes hijos
            renderers[i].enabled = false;
        }
            jugador.audioPasos.Stop();
            jugador.audioPasos.enabled = false;
            jugador.GetComponentInChildren<AudioSource>().Stop();

            //jugador.GetComponent<Collider>().enabled = false;
    }

    private void Desinfectar() {
            enemigoParasitado = false;
            if (enemigo != null)
            {
                Destroy(enemigo.gameObject);
            }
            
            renderers = jugador.GetComponentsInChildren<Renderer>();

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].enabled = true;
            }
            jugador.audioPasos.enabled = true;
            jugador.audioPasos.Play();

    }
}
