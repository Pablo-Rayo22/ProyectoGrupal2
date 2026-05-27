using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalNivel : MonoBehaviour
{
    private bool terminado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (terminado)
            return;

        if (other.CompareTag("Player"))
        {
            terminado = true;

            gameObject.SetActive(false);

            GameObject musica = GameObject.FindGameObjectWithTag("Musica");

            if (musica != null)
            {
                Destroy(musica);
            }

            SceneManager.LoadScene("EscenaVictoria");
        }
    }
}