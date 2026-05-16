using Unity.VisualScripting;
using UnityEngine;

public class Parasitar : MonoBehaviour
{
    [SerializeField] private GameObject proyectil;
    [SerializeField] private MovimientoJugador jugador;
    [SerializeField] private MovimientoEnemigo enemigo;
    //[SerializeField] private IAEnemigo iaEnemigo;
    [SerializeField] private PatrullaEnemigo patrulla;
    [SerializeField] private CamaraSeguimiento camara;

    private bool enemigoParasitado = false;
    private Renderer[] renderers;
    private void Awake()
    {
        enemigo.enabled = false;
        enemigo.GetComponent<CharacterController>().enabled = false;
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

            enemigo.GetComponent<CharacterController>().enabled = true;
            enemigo.enabled = true; // Activamos  el control del enemigo
            //iaEnemigo.enabled = false; // Desactivamos su IA
            patrulla.enabled = false; // Desactivamos su patrulla



            camara.objetivo = enemigo.transform ; // Pasamos la cámara a enemigo
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
            if (enemigo != null)
            {
                Destroy(enemigo.gameObject);
            }

            jugador.transform.position = new Vector3(enemigo.transform.position.x, transform.position.y, transform.position.z);

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
}