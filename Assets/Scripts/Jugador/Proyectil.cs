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

            GameManager.instancia.GenerarDiamantes(collision.collider);
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