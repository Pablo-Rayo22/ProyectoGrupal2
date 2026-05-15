using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float tiempoVida = 5f;

    public AudioClip sonidoImpacto;
    public AudioClip sonidoRomperCaja;

    public GameObject particulasImpacto;

    public GameObject cristalPrefab;

    private bool haImpactado = false;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (haImpactado)
            return;

        haImpactado = true;

        Vector3 puntoImpacto = collision.contacts[0].point;


        if (particulasImpacto != null)
        {
            Instantiate(
                particulasImpacto,
                puntoImpacto,
                Quaternion.identity
            );
        }


        if (collision.gameObject.CompareTag("Caja"))
        {

            if (sonidoRomperCaja != null)
            {
                AudioSource.PlayClipAtPoint(
                    sonidoRomperCaja,
                    puntoImpacto
                );
            }

            GameManager.instancia.RomperCaja();

            int cantidad = Random.Range(1, 3);

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

            Destroy(collision.gameObject);
        }
        else
        {
            // 🔊 SONIDO NORMAL
            if (sonidoImpacto != null)
            {
                AudioSource.PlayClipAtPoint(
                    sonidoImpacto,
                    puntoImpacto
                );
            }
        }

        // ❌ DESTRUIR PROYECTIL
        Destroy(gameObject);
    }
}