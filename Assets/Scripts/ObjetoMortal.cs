using UnityEngine;

public class ObjetoMortal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("He entrado en el Trigger");
            JugadorVida jugador =
                other.GetComponent<JugadorVida>();

            if (jugador != null)
            {
                jugador.Morir();
            }
        }
    }
}
