using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] NoteTag noteTag = NoteTag.NoteUp; // Pilih tag untuk note dari enum
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject Miss, Perfect, Great, Good;
    private float PerfectDistance = 20f; // Jarak toleransi (dalam satuan pixel) untuk Perfect
    private float GreatDistance = 40f;   // Jarak untuk Great
    private float GoodDistance = 80f;    // Jarak untuk Good
    private float MissDistance = 120f;   // Jarak untuk Miss
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

            float distance = Vector3.Distance(noteTransform.position, GetComponent<RectTransform>().position);

            if (distance <= PerfectDistance)
            {
                Debug.Log("Perfect Hit!");
                gameManager.SetIndexScore(10);
                Destroy(noteTransform.gameObject); // Hancurkan note yang sesuai
                notesToRemove.Add(noteTransform); // Tandai untuk dihapus dari list
                StartCoroutine(SpawnNoteEffect(Perfect, 1f));
            }
            else if (distance <= GreatDistance)
            {
                Debug.Log("Great Hit!");
                gameManager.SetIndexScore(7);
                Destroy(noteTransform.gameObject);
                notesToRemove.Add(noteTransform);
                StartCoroutine(SpawnNoteEffect(Great, 1f));
            }
            else if (distance <= GoodDistance)
            {
                Debug.Log("Good Hit!");
                gameManager.SetIndexScore(5);
                Destroy(noteTransform.gameObject);
                notesToRemove.Add(noteTransform);
                StartCoroutine(SpawnNoteEffect(Good, 1f));
            }
            else if (distance <= MissDistance)
            {
                Debug.Log("Miss!");
                gameManager.SetIndexScore(0);
                Destroy(noteTransform.gameObject);
                notesToRemove.Add(noteTransform);
                StartCoroutine(SpawnNoteEffect(Miss, 1f));
            }
        }

        // Hapus note yang ditandai setelah loop selesai
        foreach (RectTransform noteTransform in notesToRemove)
        {
            noteTransforms.Remove(noteTransform);
        }
    }

    IEnumerator SpawnNoteEffect(GameObject effectPrefab, float timingDestroy)
    {
        GameObject effectObj = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        effectObj.transform.SetParent(transform, false);
        yield return new WaitForSeconds(timingDestroy);
        Destroy(effectObj);
    }

    // Method untuk menggambarkan cakupan area dengan Gizmos
    void OnDrawGizmos()
    {
        // Menggambar lingkaran untuk setiap jarak dengan warna yang berbeda
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, PerfectDistance); // Perfect

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, GreatDistance); // Great

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GoodDistance); // Good

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MissDistance); // Miss
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
