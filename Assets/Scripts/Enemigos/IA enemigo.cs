using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IAenemigo : MonoBehaviour
{
    private float velocidad = 500f;
    private Vector3 posicion_inicial;
    private Vector3 posicion_final;
    public GameObject puntoA;
    public GameObject puntoB;
    private float duracion = 9f;
    private float tiempo_transcurrido = 0f;
    bool izquierda;

    private Animator animator;


    void Start()
    {
        posicion_inicial = new Vector3(puntoA.transform.position.x,puntoA.transform.position.y,puntoA.transform.position.z);
        posicion_final = new Vector3(puntoB.transform.position.x, puntoB.transform.position.y,puntoB.transform.position.z);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", velocidad);

        /*if (transform.position == posicion_final || transform.position == posicion_inicial) tiempo_transcurrido = 0f;
        if (transform.position == posicion_final) izquierda = false;
        if (transform.position == posicion_inicial) izquierda = true;

        if (izquierda)
        {
            transform.LookAt(posicion_final);
            tiempo_transcurrido += Time.deltaTime;
            float completado = tiempo_transcurrido / duracion;

            transform.position = Vector3.Lerp(posicion_inicial, posicion_final, completado);

        }
        else
        {
            transform.LookAt(posicion_inicial);
            tiempo_transcurrido += Time.deltaTime;
            float completado = tiempo_transcurrido / duracion;

            transform.position = Vector3.Lerp(posicion_final, posicion_inicial, completado);
        }*/

    }
}
