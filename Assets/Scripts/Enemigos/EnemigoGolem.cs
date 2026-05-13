using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;

/*
SE MOVERÁ DE PUNTO A PUNTO EN EL MAPA DAÑANDO SI SE LE TOCA.

AL SER PARASITADO PODRÁ MOVERSE LENTO Y DAR GOLPES QUE ROMPEN ROCAS U OBSTÁCULOS ROMPIBLES.

NO SALTO.
*/

public class EnemigoGolem : MonoBehaviour
{
    public float alcanceGolpe = 1.5f;
    //private CharacterController controller;

    private void Awake ()
    {
        //controller = GetComponent<CharacterController>();
    }

    private void Update ()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Atacar();
        }
    }

    private void Atacar()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, alcanceGolpe);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("Caja"))
            {
                    Destroy(colliders[i].gameObject);
            }
        }        
    }
}
