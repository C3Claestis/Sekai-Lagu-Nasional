using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerMainMenu : MonoBehaviour
{
    [SerializeField] GameObject panel_start, button_manager;
    [SerializeField] AudioClip sfx;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        ClosePanelStart();
    }

    public void OpenPanelStart()
    {
        audioSource.PlayOneShot(sfx);
        panel_start.SetActive(true);
        button_manager.SetActive(false);
    }
    public void ClosePanelStart()
    {
        panel_start.SetActive(false);
        button_manager.SetActive(true);
    }
    public void StartGame(int scene)
    {
        audioSource.PlayOneShot(sfx);
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }
}
