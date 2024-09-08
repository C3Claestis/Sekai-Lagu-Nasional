using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float speed = 300f; // Kecepatan note bergerak ke bawah (dalam satuan pixel per detik)
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gerakkan note ke bawah sepanjang sumbu Y
        rectTransform.anchoredPosition += Vector2.down * speed * Time.deltaTime;
    }
}
