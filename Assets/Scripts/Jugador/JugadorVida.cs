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

    void Awake()
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
        if (GameManager.instancia.vidas > 0)
        {
            StartCoroutine(ReiniciarEscena());
        }
        else 
        { 
            StartCoroutine(GameOver());
        }

    }

    IEnumerator ReiniciarEscena()
    {
        yield return new WaitForSeconds(tiempoMuerte);

        GameManager.instancia.cajasRotas = 0;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(tiempoMuerte);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        GameManager.instancia.reiniciarUI();
    }
}
