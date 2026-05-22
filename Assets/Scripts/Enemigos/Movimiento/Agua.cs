using UnityEngine;

public class Agua : MonoBehaviour
{

    public AudioClip sonidoAgua;
    private MeshCollider collider;
    private Vector3 posicionInicial;
    private Vector3 posicionFinal;
    private float duracion = 9f;
    private float tiempoTranscurrido = 0f;


    void Start()
    {
        collider = GetComponent<MeshCollider>();


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Fire")
        {

            collider.isTrigger = true;
            posicionInicial = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
            posicionFinal = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y - 3, collision.gameObject.transform.position.z);
            float completado = 0f;

            while (completado < 1f)
            {
                tiempoTranscurrido += Time.deltaTime;
                completado = tiempoTranscurrido / duracion;

                collision.gameObject.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, completado);
                AudioSource.PlayClipAtPoint(
                  sonidoAgua,
                     posicionFinal
                 );
            }
            Destroy(collision.gameObject);
            collider.isTrigger = false;
        }

    }
}
