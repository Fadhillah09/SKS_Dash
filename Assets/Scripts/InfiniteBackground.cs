using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    private Transform cam;
    private float bgWidth;
    private SpriteRenderer sr;

    // Duplikat background kiri dan kanan
    private GameObject bgLeft;
    private GameObject bgRight;

    void Start()
    {
        cam = Camera.main.transform;
        sr = GetComponent<SpriteRenderer>();
        bgWidth = sr.bounds.size.x;

        // Buat duplikat kiri dan kanan otomatis
        bgLeft = Instantiate(gameObject, transform.position - new Vector3(bgWidth, 0, 0), Quaternion.identity);
        bgRight = Instantiate(gameObject, transform.position + new Vector3(bgWidth, 0, 0), Quaternion.identity);

        // Hapus script di duplikat biar tidak spawn lagi
        Destroy(bgLeft.GetComponent<InfiniteBackground>());
        Destroy(bgRight.GetComponent<InfiniteBackground>());
    }

    void LateUpdate()
    {
        // Background tengah ikut kamera
        transform.position = new Vector3(
            cam.position.x,
            transform.position.y,
            transform.position.z
        );

        // Duplikat kiri dan kanan ikut tengah
        bgLeft.transform.position = transform.position - new Vector3(bgWidth, 0, 0);
        bgRight.transform.position = transform.position + new Vector3(bgWidth, 0, 0);
    }
}