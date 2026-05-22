using UnityEngine;

public class Cristal : MonoBehaviour
{
    public float velocidadRotacion = 100f;

    public AudioClip sonidoRecoger;

    void Update()
    {
        transform.parent.Rotate(0, velocidadRotacion * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sonidoRecoger != null)
            {
                AudioSource.PlayClipAtPoint(
                    sonidoRecoger,
                    transform.position
                );
            }

            GameManager.instancia.AniadirDiamante();

            Destroy(transform.parent.gameObject);
        }
    }
}