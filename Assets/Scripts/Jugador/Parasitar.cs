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

            
            enemigo.enabled = true; // Activamos  el control del enemigo

            
            camara.objetivo = enemigo.transform; // Pasamos la cámara a enemigo
        }

    }
        private void OcultarPersonaje() {
        
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
            if (enemigo != null)
            {
                Destroy(enemigo.gameObject);
            }

            jugador.transform.position = new Vector3(enemigo.transform.position.x, transform.position.y, transform.position.z);
            Vector3 rotacionDestino = jugador.transform.eulerAngles;
            rotacionDestino.y = enemigo.transform.eulerAngles.y;
            jugador.transform.eulerAngles = rotacionDestino;

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
}
