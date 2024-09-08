using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] NoteTag noteTag = NoteTag.NoteUp; // Pilih tag untuk note dari enum
    [SerializeField] GameManager gameManager;
    private float allowedDistance = 100f; // Jarak toleransi (dalam satuan pixel) untuk menentukan apakah note berada di posisi yang tepat

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

        // Simpan referensi note yang akan dihapus
        List<RectTransform> notesToRemove = new List<RectTransform>();

        // Cek apakah posisi note hampir sama dengan posisi button
        foreach (RectTransform noteTransform in noteTransforms)
        {
            if (noteTransform == null) continue; // Lewati jika noteTransform sudah dihapus

            if (Vector3.Distance(noteTransform.position, GetComponent<RectTransform>().position) <= allowedDistance)
            {
                Debug.Log("Perfect Hit!");
                gameManager.SetIndexScore(10);
                Destroy(noteTransform.gameObject); // Hancurkan note yang sesuai
                notesToRemove.Add(noteTransform); // Tandai untuk dihapus dari list
            }
        }

        // Hapus note yang ditandai setelah loop selesai
        foreach (RectTransform noteTransform in notesToRemove)
        {
            noteTransforms.Remove(noteTransform);
        }

        if (notesToRemove.Count == 0)
        {
            Debug.Log("Miss!");
            // Logika jika gagal
        }
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
