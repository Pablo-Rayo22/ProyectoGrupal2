using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("VIDAS")]
    public int vidas = 5;
    public TextMeshProUGUI textoVidas;

    [Header("DIAMANTES")]
    public int diamantes = 0;
    public TextMeshProUGUI textoDiamantes;

    [Header("CAJAS")]
    public int cajasRotas = 0;
    public TextMeshProUGUI textoCajas;

    void Awake()
    {
        instancia = this;

        ActualizarUI();
    }

    void ActualizarUI()
    {
        textoVidas.text = "x" + vidas;
        textoDiamantes.text = "x" + diamantes;
        textoCajas.text = "x" + cajasRotas;
    }

    public void AñadirDiamante()
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
}
