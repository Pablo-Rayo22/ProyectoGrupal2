using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class JugadorVida : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    private bool muerto = false;
    //private bool checkPointAlcanzado = false;
    //private Vector3 posicionActual;
    //private Vector3 posicionInicial;
    [Header("MUERTE")]
    public float tiempoMuerte = 2f;
    private MovimientoJugador movimiento;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        movimiento = GetComponent<MovimientoJugador>();
        // Guardar posición inicial SOLO la primera vez que aparece en la escena
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

        if (movimiento != null)
        {
            movimiento.enabled = false;
        }
        if (GameManager.instancia.vidas > 0)
        {
            StartCoroutine(ReiniciarEscena(gameObject));
        }
        else 
        { 
            StartCoroutine(GameOver());
        }

    }

    private IEnumerator ReiniciarEscena(GameObject jugador)
    {
        yield return new WaitForSeconds(tiempoMuerte);

        GameManager.instancia.cajasRotas = 0;

        Rigidbody rb = jugador.GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (GameManager.instancia.hayCheckPoint)
        {
            Vector3 respawnPos = GameManager.instancia.posicionCheckpoint;

            rb.isKinematic = true;
            rb.position = respawnPos;
            yield return null;
            rb.isKinematic = false;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            yield break;
        }

        // 🔥 RESET DE ESTADO
        muerto = false;
        //MonoBehaviour movimiento = GetComponent<MovimientoJugador>();
        if (movimiento != null)
        {
            movimiento.enabled = true;
        }

        animator.ResetTrigger("Morir");
        animator.Play("Idle");
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(tiempoMuerte);
        GameManager.instancia.reiniciarUI();
        GameManager.instancia.posicionCheckpoint = Vector3.zero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}