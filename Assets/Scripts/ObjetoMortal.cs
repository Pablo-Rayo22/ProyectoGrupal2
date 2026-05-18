using UnityEngine;

public class ObjetoMortal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            JugadorVida jugador =
                other.GetComponent<JugadorVida>();

            if (jugador != null)
            {
                jugador.Morir();
            }
        }
    }
}
