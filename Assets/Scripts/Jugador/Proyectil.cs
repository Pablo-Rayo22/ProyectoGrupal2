using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float tiempoVida = 5f;

    public AudioClip sonidoImpacto;
    public AudioClip sonidoRomperCaja;

    public GameObject particulasImpacto;
    public GameObject cristalPrefab;

    // EVITAR DOBLE COLISIÓN
    private bool haImpactado = false;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void OnCollisionEnter(Collision collision)
    {
        bool flowControl = NewMethod(collision);
        if (!flowControl)
        {
            return;
        }

        // DESTRUIR PROYECTIL
        Destroy(gameObject);
    }

    private bool NewMethod(Collision collision)
    {
        // SI YA IMPACTÓ, NO HACER NADA
        if (haImpactado)
            return false;

        haImpactado = true;

        Vector3 puntoImpacto = collision.contacts[0].point;

        // PARTÍCULAS
        if (particulasImpacto != null)
        {
            Instantiate(
                particulasImpacto,
                puntoImpacto,
                Quaternion.identity
            );
        }

        // SI ES CAJA
        if (collision.gameObject.CompareTag("Caja"))
        {
            // SONIDO
            if (sonidoRomperCaja != null)
            {
                AudioSource.PlayClipAtPoint(
                    sonidoRomperCaja,
                    puntoImpacto
                );
            }

            // SOLO ENTRE 1 Y 5
            int cantidad = Random.Range(1, 4);

            for (int i = 0; i < cantidad; i++)
            {
                Vector3 posicion =
                    collision.transform.position + Vector3.up;

                GameObject cristal = Instantiate(
                    cristalPrefab,
                    posicion,
                    Quaternion.identity
                );

                Rigidbody rb = cristal.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    Vector3 fuerza = new Vector3(
                        Random.Range(-0.1f, 0.1f),
                        Random.Range(0.5f, 1f),
                        Random.Range(-0.1f, 0.1f)
                    );

                    rb.AddForce(
                        fuerza,
                        ForceMode.Impulse
                    );
                }
            }

            // DESTRUIR CAJA
            Destroy(collision.gameObject);
        }
        else
        {
            // SONIDO NORMAL
            if (sonidoImpacto != null)
            {
                AudioSource.PlayClipAtPoint(
                    sonidoImpacto,
                    puntoImpacto
                );
            }
        }

        return true;
    }
}