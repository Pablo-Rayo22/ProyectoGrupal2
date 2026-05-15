using UnityEngine;
using System.Collections;

public class PatrullaEnemigo : MonoBehaviour
{
    private bool corutinaActiva = false;
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private float segundos;
    public Transform objetivo;
    public Quaternion rotacionFinal;
    public float tiempoInterpolacion = 0f;
    public float tiempoRotacion = 1f;

    private void Awake()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        if (!corutinaActiva)
        {
            corutinaActiva = true;
            //StartCoroutine(Rotar(rotacionInicial, rotacionFinal, tiempoInterpolacion));
            //StartCoroutine(Patrullar(posicionInicial, objetivo, tiempoInterpolacion));
            StartCoroutine(Rotar());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    //private IEnumerator SecuenciaPatrulla()
    //{
    //    yield return StartCoroutine(Rotar());
    //    yield return StartCoroutine(Patrullar());
    //    cambiarObjetivo();
    //    corutinaActiva = false;
    //}

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
        //corutinaActiva = false;
    }
    private IEnumerator Rotar()
    {
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        float rotacionY = cambiarRotacionY();

        rotacionInicial = transform.rotation;

        rotacionFinal = Quaternion.Euler(270f, rotacionY, 0f);
        float tiempoTranscurrido = 0f;
        //segundos = 2f; 
        while (tiempoTranscurrido < tiempoRotacion)
        {
            transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionFinal, tiempoTranscurrido / tiempoRotacion);
            tiempoTranscurrido += Time.deltaTime;
            yield return null; // Para detener la corrutina en el siguiente frame
        }
        transform.rotation = rotacionFinal;
        yield return StartCoroutine(Patrullar());
        //cambiarRotacion();
        corutinaActiva = false;
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

    private void cambiarObjetivo()
    {
        // 
        objetivo.position = posicionInicial;
        posicionInicial = transform.position;
    }

    //private void cambiarRotacion()
    //{
    //    //rotacionFinal = transform.rotation;

    //    //rotacionFinal = rotacionInicial;
    //    //rotacionInicial = transform.rotation;
    //}
}
