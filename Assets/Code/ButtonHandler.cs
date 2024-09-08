using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public NoteTag noteTag = NoteTag.NoteUp; // Pilih tag untuk note dari enum

    float allowedDistance = 100f; // Jarak toleransi (dalam satuan pixel) untuk menentukan apakah note berada di posisi yang tepat

    private List<RectTransform> noteTransforms = new List<RectTransform>(); // List untuk menampung RectTransform dari semua note

    void Update()
    {
        // Mencari semua GameObject dengan tag yang dipilih dan menambahkannya ke dalam list jika belum ada
        GameObject[] noteObjects = GameObject.FindGameObjectsWithTag(noteTag.ToString());

        // Gunakan HashSet untuk menyimpan referensi RectTransform yang telah ditambahkan
        HashSet<RectTransform> existingTransforms = new HashSet<RectTransform>(noteTransforms);

        foreach (GameObject noteObject in noteObjects)
        {
            RectTransform rectTransform = noteObject.GetComponent<RectTransform>();
            if (rectTransform != null && !existingTransforms.Contains(rectTransform))
            {
                noteTransforms.Add(rectTransform);
                existingTransforms.Add(rectTransform); // Tambahkan ke HashSet untuk menghindari duplikasi
            }
        }
    }

    // Method ini dipanggil ketika button ditekan
    public void OnButtonPress()
    {
        if (noteTransforms.Count == 0)
        {
            Debug.LogWarning("No notes with the specified tag found!");
            return;
        }

        // Cek apakah posisi note hampir sama dengan posisi button
        foreach (RectTransform noteTransform in noteTransforms)
        {
            if (Vector3.Distance(noteTransform.position, GetComponent<RectTransform>().position) <= allowedDistance)
            {
                Debug.Log("Perfect Hit!");
                // Tambahkan logika untuk menghitung skor atau efek lainnya
                return; // Keluar dari loop setelah menemukan hit yang sempurna
            }
        }

        Debug.Log("Miss!");
        // Logika jika gagal
    }

    // Method untuk menggambarkan cakupan area dengan Gizmos
    void OnDrawGizmos()
    {
        if (noteTransforms.Count > 0)
        {
            // Mengatur warna Gizmos
            Gizmos.color = Color.green;

            // Menggambar lingkaran di sekitar posisi button
            Gizmos.DrawWireSphere(transform.position, allowedDistance);
        }
    }
}

// Enum untuk menentukan tag yang tersedia
public enum NoteTag
{
    NoteUp, 
    NoteDown, 
    NoteLeft, 
    NoteRight
}
