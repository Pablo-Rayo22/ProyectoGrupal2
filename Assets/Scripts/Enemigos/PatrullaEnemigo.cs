using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class PatrullaEnemigo : MonoBehaviour
{
    private bool corutinaActiva = false;
    private Vector3 posicionInicial;
    public Transform objetivo;
    public Quaternion rotacionFinal;
    public float tiempoInterpolacion = 0f;
    public float tiempoRotacion = 0f;

    private void Start()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        if (!corutinaActiva)
        {
            corutinaActiva = true;
            StartCoroutine(Rotar());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Patrullar()
    {
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoInterpolacion)
        {
            transform.position = Vector3.Lerp(posicionInicial, objetivo.position, tiempoTranscurrido / tiempoInterpolacion);
            tiempoTranscurrido += Time.deltaTime;
            yield return null; // Para detener la corrutina en el siguiente frame
        }
        objetivo.position = transform.position;
        cambiarObjetivo();
    }
    private IEnumerator Rotar()
    {
        Vector3 direccion = (objetivo.position - transform.position).normalized;

        Quaternion rotacionInicial = transform.rotation;
        float rotacionY = cambiarRotacionY();
        float tiempoTranscurrido = 0f;
        rotacionFinal = Quaternion.Euler(0f, rotacionY, 0f);
        while (tiempoTranscurrido < tiempoRotacion)
        {
            transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionFinal, tiempoTranscurrido / tiempoRotacion);
            tiempoTranscurrido += Time.deltaTime;
            yield return null; // Para detener la corrutina en el siguiente frame
        }
        transform.rotation = rotacionFinal;
        yield return StartCoroutine(Patrullar());
        corutinaActiva = false;
    }

    private void cambiarObjetivo()
    {
         
        objetivo.position = posicionInicial;
        posicionInicial = transform.position;
        
    }

    private float cambiarRotacionY()
    {
        float rotacionDerecha = 90f;
        float rotacionIzquierda = -90f;
        if (objetivo.position.x > transform.position.x)
        {
            return rotacionDerecha;
        }
        else
        {
            return rotacionIzquierda;
        }
    }
}
