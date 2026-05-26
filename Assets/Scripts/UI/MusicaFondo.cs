using UnityEngine;

public class MusicaFondo : MonoBehaviour
{
    private static MusicaFondo instancia;

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
        }
    }
}
