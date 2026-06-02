using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public AudioClip sonidoCheckpoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!GameManager.instancia.hayCheckPoint)
            {
                GameManager.instancia.hayCheckPoint = true;
                Debug.Log("Checkpoint activado por: " + other.gameObject.name);
                AudioSource.PlayClipAtPoint(sonidoCheckpoint, transform.position);
                GameManager.instancia.GuardarCheckpoint(transform.position);
            }

        }
    }
}
