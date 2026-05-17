using Unity.VisualScripting;
using UnityEngine;

public class Parasitar : MonoBehaviour
{
    [SerializeField] private GameObject proyectil;
    [SerializeField] private MovimientoJugador jugador;
    [SerializeField] private MovimientoEnemigo enemigo;
    [SerializeField] private MovimientoEnemigoFire enemigo_fire;
    [SerializeField] private IAenemigo iaEnemigo;
    [SerializeField] private PatrullaEnemigo patrulla;
    [SerializeField] private CamaraSeguimiento camara;

    private bool enemigoParasitado = false;
    private string nombre_enemigo;
    private Renderer[] renderers;
    private string[] tipos_enemigos = { "Golem", "Fire" };
    private string tipo_enemigo="";


    private void Start()
    {
        tipo_enemigo=this.transform.gameObject.name;
        enemigo.enabled = false;
        enemigo_fire.enabled = false;
        
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

            if (tipo_enemigo == tipos_enemigos[0])
            {
                enemigo.enabled = true; // Activamos  el control del enemigo
                patrulla.enabled = false; // Desactivamos su patrulla
                camara.objetivo = enemigo.transform;
            }

            if (tipo_enemigo == tipos_enemigos[1])
            {
                enemigo_fire.enabled = true; // Activamos  el control del enemigo
                iaEnemigo.enabled = false; // Desactivamos su IA
                camara.objetivo = enemigo_fire.transform;
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

            if (tipo_enemigo== tipos_enemigos[0]) { 
               if (enemigo != null) Destroy(enemigo.gameObject);
            }

            if (tipo_enemigo == tipos_enemigos[1])
            {
               if (enemigo_fire != null) Destroy(enemigo_fire.gameObject);
            }

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
}