using UnityEngine;

public class Danio : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        DaniarJugador(collision.gameObject);
    }

    private void DaniarJugador(GameObject collider)
    {
        if (collider.CompareTag("Player")) {
            Debug.Log("Jugador tocado: " + collider.name);
        }
    }


}
