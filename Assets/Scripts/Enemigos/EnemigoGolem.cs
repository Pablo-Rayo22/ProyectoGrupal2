using UnityEngine;

/*
SE MOVERÁ DE PUNTO A PUNTO EN EL MAPA DAÑANDO SI SE LE TOCA.

AL SER PARASITADO PODRÁ MOVERSE LENTO Y DAR GOLPES QUE ROMPEN ROCAS U OBSTÁCULOS ROMPIBLES.

NO SALTO.
*/

public class EnemigoGolem : MonoBehaviour
{
    private Vector3 posicionInicial;
    public Vector3 posicionFinal;
    public float tiempoInterpolacion;
    private void Awake()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        Movimiento();
    }

    private void OnCollisionEnter(Collision collision)
    {
        DaniarJugador(collision.gameObject);
    }

    //private void Atacar() {

    //}

    private void Movimiento()
    {
        transform.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempoInterpolacion);
    }
    
    private void DaniarJugador (GameObject collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Jugador tocado: " + collider.name);
        }
    }
}
