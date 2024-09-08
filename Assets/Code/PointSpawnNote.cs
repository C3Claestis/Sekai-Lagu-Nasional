using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PointSpawnNote : MonoBehaviour
{
    [SerializeField] NoteMovement note;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
}
