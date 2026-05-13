using UnityEngine;
using System.Collections;

public class PatrullaEnemigo : MonoBehaviour
{
    private bool corutinaActiva = false;
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private Coroutine corutinaPatrullar;
    public Transform objetivo;
    public Quaternion rotacionFinal;
    public float tiempoInterpolacion = 0f;

    private void Awake()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    private void Update()
    {
        if (!corutinaActiva)
        {
            corutinaActiva = true;
            corutinaPatrullar = StartCoroutine(Patrullar(posicionInicial, objetivo, tiempoInterpolacion));
            //StartCoroutine(Rotar(rotacionInicial, rotacionFinal, tiempoInterpolacion));
        }
    }

    private void OnDisable()
    {
        StopCoroutine(corutinaPatrullar);
    }

    private IEnumerator Patrullar(Vector3 posicionInicial, Transform objetivo, float tiempoInterpolacion)
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
        corutinaActiva = false;
    }
    //private IEnumerator Rotar(Quaternion rotacionInicial, Quaternion rotacionFinal, float tiempoInterpolacion)
    //{
    //    float tiempoTranscurrido = 0f;
    //    while (tiempoTranscurrido < tiempoInterpolacion)
    //    {
    //        transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionFinal, tiempoTranscurrido / tiempoInterpolacion);
    //        tiempoTranscurrido += Time.deltaTime;
    //        yield return null; // Para detener la corrutina en el siguiente frame
    //    }
    //    rotacionFinal = transform.rotation;
    //    cambiarRotacion();
    //    corutinaActiva = false;
    //}

    private void cambiarObjetivo()
    {
        // 
        objetivo.position = posicionInicial;
        posicionInicial = transform.position;
    }

    //private void cambiarRotacion ()
    //{
    //    rotacionFinal = rotacionInicial;
    //    rotacionInicial = transform.rotation;
    //}
}
