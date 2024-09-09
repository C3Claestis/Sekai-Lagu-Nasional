using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] AudioSource musicSource; // Tambahkan reference ke AudioSource
    [SerializeField] PointSpawnNote[] pointSpawnNote;
    [SerializeField] GameObject panel_finish;
    private List<NoteMovement> noteMovements = new List<NoteMovement>();
    private int indexScore;
    // Start is called before the first frame update
    void Start()
    {
        if (musicSource != null)
        {
            musicSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score : " + indexScore.ToString();

        // Cek jika musik masih berjalan dan array pointSpawnNote sudah terisi
        if (musicSource != null && !musicSource.isPlaying && pointSpawnNote != null && pointSpawnNote.Length > 0)
        {
            //Matikan sistem Spawn Note
            foreach (PointSpawnNote spawnNote in pointSpawnNote)
            {
                if (spawnNote != null)
                {
                    spawnNote.SetIsPlaying(false);
                }
            }

            //Mulai mencari semua note yang tersisa
            SearchNote();

             // Jika semua note telah dihancurkan dan belum selesai
            if (noteMovements.Count == 0)
            {
                ShowFinishUI();
            }
        }
    }

    void ShowFinishUI()
    {
        panel_finish.SetActive(true);
    }
    void SearchNote()
    {
       // Cari semua objek di scene yang memiliki komponen NoteMovement
        NoteMovement[] foundNotes = FindObjectsOfType<NoteMovement>();

        // Tambahkan ke dalam list jika belum ada
        foreach (NoteMovement note in foundNotes)
        {
            if (!noteMovements.Contains(note))
            {
                noteMovements.Add(note);
            }
        }
        // Hapus note yang sudah dihancurkan dari list
        noteMovements.RemoveAll(note => note == null);
    }
    public void SetIndexScore(int index) => indexScore += index;
}
