using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerMainMenu : MonoBehaviour
{
    [SerializeField] GameObject panel_start, button_manager;
   
    // Start is called before the first frame update
    void Start()
    {
        ClosePanelStart();
    }

    public void OpenPanelStart()
    {
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
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }
}
