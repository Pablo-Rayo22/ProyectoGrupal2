using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class JugadorVida : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    private bool muerto = false;

    [Header("MUERTE")]
    public float tiempoMuerte = 2f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void Morir()
    {
        if (muerto)
            return;

        muerto = true;

        GameManager.instancia.vidas--;

        GameManager.instancia.ActualizarUI();

        rb.linearVelocity = Vector3.zero;

        animator.SetTrigger("Morir");

        MonoBehaviour movimiento = GetComponent<MovimientoJugador>();

        if (movimiento != null)
        {
            movimiento.enabled = false;
        }

        StartCoroutine(ReiniciarEscena());
    }

    IEnumerator ReiniciarEscena()
    {
        yield return new WaitForSeconds(tiempoMuerte);

        GameManager.instancia.cajasRotas = 0;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
