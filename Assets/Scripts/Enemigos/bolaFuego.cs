using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolaFuego : MonoBehaviour
{
    public float poder = 20f;
    public float tiempoVida = 5f;
    private float deltatime = 0f;
    public AudioClip sonidoImpacto;
    public AudioClip sonidoRomperCaja;

    public GameObject cristalPrefab;
    private Rigidbody rb;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = this.transform.forward * poder;

    }

    void Update()
    {

        deltatime += Time.deltaTime;
        if (deltatime >= tiempoVida)
        {
            Destroy(this.gameObject);
        }
    }



    private void OnTriggerEnter(Collider collision)
    {
        Vector3 puntoImpacto = collision.gameObject.transform.position;

        AudioSource.PlayClipAtPoint(
           sonidoImpacto,
           puntoImpacto
        );

        if (collision.tag == "Player")
        {

            Transform tr = collision.gameObject.transform;
            Destroy(this.gameObject);


        }

        if (collision.tag == "Caja")
        {

            GameManager.instancia.GenerarDiamantes(collision);

        }
    }
}
    
