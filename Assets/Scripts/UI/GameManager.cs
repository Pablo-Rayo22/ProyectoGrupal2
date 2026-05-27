using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("VIDAS")]
    public int vidasIniciales = 5;
    public int vidas;
    
    public TextMeshProUGUI textoVidas;

    [Header("DIAMANTES")]
    public int diamantes = 0;
    public TextMeshProUGUI textoDiamantes;

    [Header("CAJAS")]
    public int cajasRotas = 0;
    public TextMeshProUGUI textoCajas;

    [Header("GENERACION DIAMANTES")]
    public AudioClip sonidoImpacto;
    public AudioClip sonidoRomperCaja;
    public GameObject cristalPrefab;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        vidas = vidasIniciales;
    }
    
    void Update()
    {
        if (textoVidas == null)
        {
            GameObject obj = GameObject.Find("TextoVidas");

            if (obj != null)
                textoVidas = obj.GetComponent<TextMeshProUGUI>();
        }

        if (textoDiamantes == null)
        {
            GameObject obj = GameObject.Find("TextoDiamantes");

            if (obj != null)
                textoDiamantes = obj.GetComponent<TextMeshProUGUI>();
        }

        if (textoCajas == null)
        {
            GameObject obj = GameObject.Find("TextoCajas");

            if (obj != null)
                textoCajas = obj.GetComponent<TextMeshProUGUI>();
        }

        ActualizarUI();
    }

    public void ActualizarUI()
    {
        if (textoVidas != null)
            textoVidas.text = "x" + vidas;

        if (textoDiamantes != null)
            textoDiamantes.text = "x" + diamantes;

        if (textoCajas != null)
            textoCajas.text = "x" + cajasRotas;
    }

    public void AniadirDiamante()
    {
        diamantes++;

        if (diamantes % 50 == 0)
        {
            vidas++;
        }

        ActualizarUI();
    }

    public void RomperCaja()
    {
        cajasRotas++;

        ActualizarUI();
    }

    public void GenerarDiamantes(Collider collider)
    {
        Vector3 puntoImpacto = collider.transform.position;
        AudioSource.PlayClipAtPoint(
               sonidoRomperCaja,
               puntoImpacto
            );


        RomperCaja();

        int cantidad = Random.Range(1, 3);

        for (int i = 0; i < cantidad; i++)
        {
            Vector3 posicion =
            collider.transform.position + Vector3.up;

            GameObject cristal = Instantiate(
                cristalPrefab,
                posicion,
                 Quaternion.identity
            );

            Rigidbody rb = cristal.GetComponent<Rigidbody>();


            Vector3 fuerza = new Vector3(
               Random.Range(-0.1f, 0.1f),
               Random.Range(0.5f, 1f),
               Random.Range(-0.1f, 0.1f)
            );

            rb.AddForce(
               fuerza,
               ForceMode.Impulse
            );


            Destroy(collider.gameObject);
        }
    }
    public void reiniciarUI()
    {
        vidas = vidasIniciales;
        diamantes = 0;
        cajasRotas = 0;

        ActualizarUI();
    }
}
