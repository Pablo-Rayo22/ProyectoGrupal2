using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float tiempoVida = 5f;

    public AudioClip sonidoImpacto;
    public GameObject particulasImpacto;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 🔊 SONIDO
        if (sonidoImpacto != null)
        {
            AudioSource.PlayClipAtPoint(sonidoImpacto, transform.position);
        }

        // 💥 PARTÍCULAS
        if (particulasImpacto != null)
        {
            Instantiate(particulasImpacto, transform.position, Quaternion.identity);
        }

        // DESTRUIR PROYECTIL
        Destroy(gameObject);
    }
}