using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float tiempoVida = 5f;

    public AudioClip sonidoImpacto;
    public AudioClip sonidoRomperCaja;

    public GameObject particulasImpacto;

    public GameObject cristalPrefab;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 puntoImpacto = collision.contacts[0].point;

        // PARTÍCULAS
        if (particulasImpacto != null)
        {
            Instantiate(particulasImpacto, puntoImpacto, Quaternion.identity);
        }

        // SI ES CAJA
        if (collision.gameObject.CompareTag("Caja"))
        {
            // SONIDO CAJA
            if (sonidoRomperCaja != null)
            {
                AudioSource.PlayClipAtPoint(sonidoRomperCaja, puntoImpacto);
            }

            // GENERAR CRISTALES
            int cantidad = Random.Range(1, 2);

            for (int i = 0; i < cantidad; i++)
            {
                Vector3 posicion = collision.transform.position + Vector3.up;

                GameObject cristal = Instantiate(
                    cristalPrefab,
                    posicion,
                    Quaternion.identity
                );

                Rigidbody rb = cristal.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    Vector3 fuerza = new Vector3(
                    Random.Range(-0.5f, 0.5f),
                    Random.Range(1f, 1.5f),
                    Random.Range(-0.5f, 0.5f)
                    );

                    rb.AddForce(fuerza, ForceMode.Impulse);
                }
            }

            Destroy(collision.gameObject);
        }
        else
        {
            // SONIDO NORMAL
            if (sonidoImpacto != null)
            {
                AudioSource.PlayClipAtPoint(sonidoImpacto, puntoImpacto);
            }
        }

        Destroy(gameObject);
    }
}