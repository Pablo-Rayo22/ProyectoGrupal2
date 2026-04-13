using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    public Transform objetivo;
    public Vector3 offset;
    public float suavizado = 5f;

    void LateUpdate()
    {
        if (objetivo == null) return;

        // Posición deseada
        Vector3 posicionDeseada = objetivo.position + offset;

        // Movimiento suave
        transform.position = Vector3.Lerp(
            transform.position,
            posicionDeseada,
            suavizado * Time.deltaTime
        );

        // Mirar al jugador
        transform.LookAt(objetivo);
    }
}