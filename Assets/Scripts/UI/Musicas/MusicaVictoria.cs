using UnityEngine;

public class MusicaVictoria : MonoBehaviour
{
    private static MusicaVictoria instancia;

    private void Awake()
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
