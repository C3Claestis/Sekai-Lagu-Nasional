using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PointSpawnNote : MonoBehaviour
{
    [SerializeField] NoteMovement note;

    public float minSpawnTime = 1f; // Waktu minimum antara spawn
    public float maxSpawnTime = 10f; // Waktu maksimum antara spawn
    private float timeToNextSpawn;
    private float elapsedTime;

    void Start()
    {
        // Jadwalkan spawn pertama kali
        ScheduleNextSpawn();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Cek jika waktu sudah tiba untuk spawn
        if (elapsedTime >= timeToNextSpawn)
        {
            SpawnNote();
            ScheduleNextSpawn(); // Jadwalkan spawn berikutnya
        }

        // Spawn note jika tombol P ditekan
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnNote();
        }
    }

    void ScheduleNextSpawn()
    {
        // Atur waktu spawn berikutnya secara acak
        timeToNextSpawn = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
        elapsedTime = 0f; // Reset waktu yang telah berlalu
    }

    void SpawnNote()
    {
        // Spawn note sebagai child dari PointSpawnNote
        GameObject spawnedNote = Instantiate(note.gameObject, transform.position, quaternion.identity);
        spawnedNote.transform.SetParent(transform, false); // Set parent tanpa mengubah posisi

        // Atur RectTransform.x menjadi 0
        RectTransform rectTransform = spawnedNote.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector2 anchoredPosition = rectTransform.anchoredPosition;
            anchoredPosition.x = 0;
            rectTransform.anchoredPosition = anchoredPosition;
        }
    }
}
