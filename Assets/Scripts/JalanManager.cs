using UnityEngine;

public class JalanManager : MonoBehaviour
{
    [Header("Semua Object Jalan")]
    public GameObject[] semuaJalan; // drag semua jalan ke sini di Inspector

    void Start()
    {
        AktifkanSemuaJalan();
    }

    void AktifkanSemuaJalan()
    {
        foreach (GameObject jalan in semuaJalan)
        {
            if (jalan != null)
                jalan.SetActive(true);
        }
    }
}