using System.Collections;
using UnityEngine;

/*
SE MOVERÁ DE PUNTO A PUNTO EN EL MAPA DAÑANDO SI SE LE TOCA.

AL SER PARASITADO PODRÁ MOVERSE LENTO Y DAR GOLPES QUE ROMPEN ROCAS U OBSTÁCULOS ROMPIBLES.

NO SALTO.
*/

public class EnemigoGolem : MonoBehaviour
{
    public float alcanceGolpe = 1f;
    private bool golpe = false;
    private void Awake ()
    {

    }

    private void Update ()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Atacar();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        DaniarJugador(collision.gameObject);
    }

    private void Atacar()
    {
        if (!golpe)
        {
            golpe = true;
            Destroy(gameObject);
        }
        
    }

    private void DaniarJugador (GameObject collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Jugador tocado: " + collider.name);
        }
    }

}
