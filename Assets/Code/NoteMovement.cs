using UnityEngine;
using System.Collections;
public class NoteMovement : MonoBehaviour
{
    public float speed = 300f; // Kecepatan note bergerak di sepanjang jalur (satuan pixel per detik)
    private RectTransform rectTransform;
    private LinePath linePath; // Reference ke LinePath yang akan diikuti
    private RectTransform[] pathPoints;
    private int currentPointIndex = 0;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Pastikan LinePath ditemukan
        if (linePath == null)
        {
            Debug.LogError("No LinePath found for this note!");
            return;
        }

        // Ambil semua titik dari LinePath
        pathPoints = new RectTransform[linePath.GetPoints.Length + 2];
        pathPoints[0] = linePath.GetStartPoint;
        pathPoints[pathPoints.Length - 1] = linePath.GetEndPoint;
        for (int i = 0; i < linePath.GetPoints.Length; i++)
        {
            pathPoints[i + 1] = linePath.GetPoints[i];
        }

        // Mulai dari titik pertama
        rectTransform.position = pathPoints[currentPointIndex].position;
    }

    void Update()
    {
        // Jika note telah mencapai titik terakhir, hancurkan objek
        if (currentPointIndex >= pathPoints.Length)
        {
            // Gerakkan note ke bawah sepanjang sumbu Y
            rectTransform.anchoredPosition += Vector2.down * speed * Time.deltaTime;

            StartCoroutine(DestroyAfterDelay(5f));
            return;
        }

        // Gerakkan note menuju titik berikutnya
        Vector3 targetPosition = pathPoints[currentPointIndex].position;
        rectTransform.position = Vector3.MoveTowards(rectTransform.position, targetPosition, speed * Time.deltaTime);

        // Cek jika note sudah sangat dekat dengan targetPosition
        if (Vector3.Distance(rectTransform.position, targetPosition) < 0.1f)
        {
            currentPointIndex++; // Pindah ke titik berikutnya
        }
    }
    // Coroutine untuk menunggu 5 detik sebelum menghancurkan objek
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Tunggu selama 5 detik
        Destroy(gameObject); // Hancurkan objek
    }
    // Fungsi untuk mengatur LinePath
    public void SetLinePath(LinePath linePath)
    {
        this.linePath = linePath;
    }
}
