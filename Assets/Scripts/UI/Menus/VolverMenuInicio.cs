using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class VolverMenuInicio : MonoBehaviour
{
    public string nombreEscena;
    public AudioSource auidoClic;
    public MenuInicio inicio;
    //public void CargarEscena()
    //{
    //    StartCoroutine(CargarEscenaDerrota(nombreEscena));
    //}
    public void CargarEscenaInicio (string nombreEscena)
    {
        inicio.reproducirSonidoClic();
        SceneManager.LoadScene(nombreEscena);
    } 
    
}
