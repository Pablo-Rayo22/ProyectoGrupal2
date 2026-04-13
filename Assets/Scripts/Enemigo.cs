using UnityEngine;

public class EnemigoVolador : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;

    public float velocidad = 3f;
    public float distanciaCambio = 0.2f;

    private Transform objetivoActual;

    void Start()
    {
        objetivoActual = puntoB;
    }

    void Update()
    {
        // DIRECCIÓN
        Vector3 direccion = (objetivoActual.position - transform.position).normalized;

        // MOVIMIENTO
        transform.position += direccion * velocidad * Time.deltaTime;

        // ROTACIÓN (opcional pero queda mejor)
        if (direccion != Vector3.zero)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, 5f * Time.deltaTime);
        }

        // CAMBIO DE OBJETIVO
        float distancia = Vector3.Distance(transform.position, objetivoActual.position);

        if (distancia < distanciaCambio)
        {
            if (objetivoActual == puntoA)
                objetivoActual = puntoB;
            else
                objetivoActual = puntoA;
        }
    }
}