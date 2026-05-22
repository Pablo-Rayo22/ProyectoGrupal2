using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class MenuInicio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string nombreEscena;
    public AudioSource audioClic;
    public float tiempoCarga = 1f;

    public void CargarEscena ()
    {
        StartCoroutine(CargarNivel(nombreEscena));
    }

    private IEnumerator CargarNivel (string nombreEscena)
    {
        reproducirSonidoClic();
        yield return new WaitForSeconds(tiempoCarga);
        SceneManager.LoadScene (nombreEscena);
    }

    public void Salir()
    {
        Application.Quit();
        reproducirSonidoClic();
        Debug.Log("Salir del juego.");
    }

    public void reproducirSonidoClic ()
    {
        if (!audioClic.isPlaying)
        {
            audioClic.Play();
        }
        else
        {
            audioClic.Stop();
        }
    }
}
