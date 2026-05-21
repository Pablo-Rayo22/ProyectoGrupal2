using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IAenemigo : MonoBehaviour
{
    private float velocidad = 10f;
    private Vector3 posicionInicial;
    private Vector3 posicionFinal;
    public GameObject puntoA;
    public GameObject puntoB;
    private float duracion = 9f;
    private float tiempoTranscurrido = 0f;
    bool izquierda;

    private Animator animator;

    public GameObject bolaFuego;
    private GameObject player;
    public Transform spawnPoint;
    private float tiempoRatioDisparo = 0;
    private float ratioDisparo = 2f;
    private AudioSource audioPasos;

    void Start()
    {
        posicionInicial = new Vector3(puntoA.transform.position.x,puntoA.transform.position.y,puntoA.transform.position.z);
        posicionFinal = new Vector3(puntoB.transform.position.x, puntoB.transform.position.y,puntoB.transform.position.z);
        animator = GetComponent<Animator>();
        audioPasos = GetComponent<AudioSource>();
        player = GameObject.Find("Jugador");
    }

    void Update()
    {
        

        float dist = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));

        if (dist < 12.5f)
        {
            transform.LookAt(player.transform);
            ratioDisparo = 1f;
            velocidad = 0f;

            if (Time.time > tiempoRatioDisparo)
            {
                GameObject nuevaBola;
                nuevaBola = Instantiate(bolaFuego, spawnPoint.position, spawnPoint.rotation);
                tiempoRatioDisparo = Time.time + ratioDisparo;

            }
        }
        else
        {
            ratioDisparo = 2f;
            velocidad = 10f;
            

            if (Time.time > tiempoRatioDisparo)
            {
                GameObject newbola;
                newbola = Instantiate(bolaFuego, spawnPoint.position, spawnPoint.rotation);
                tiempoRatioDisparo = Time.time + ratioDisparo;

            }

            if (transform.position == posicionFinal || transform.position == posicionInicial) tiempoTranscurrido = 0f;
            if (transform.position == posicionFinal) izquierda = false;
            if (transform.position == posicionInicial) izquierda = true;

            if (izquierda)
            {
                transform.LookAt(posicionFinal);
                tiempoTranscurrido += Time.deltaTime;
                float completado = tiempoTranscurrido / duracion;

                transform.position = Vector3.Lerp(posicionInicial, posicionFinal, completado);

            }
            else
            {
                transform.LookAt(posicionInicial);
                tiempoTranscurrido += Time.deltaTime;
                float completado = tiempoTranscurrido / duracion;

                transform.position = Vector3.Lerp(posicionFinal, posicionInicial, completado);
            }
        }
        animator.SetFloat("Velocidad", velocidad);

        // 🔊 SONIDO DE PASOS
        if (velocidad > 0.1f)
        {
            if (!audioPasos.isPlaying)
            {
                audioPasos.loop = true;
                audioPasos.Play();
            }
        }
        else
        {
            if (audioPasos.isPlaying)
            {
                audioPasos.Stop();
            }
        }
    }
}
