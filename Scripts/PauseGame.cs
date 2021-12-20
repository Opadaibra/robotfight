using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public bool IsPaused=false;
    public GameObject Panel;
    
    private void Update()
    {
        if(!IsPaused&& Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        else if (IsPaused&& Input.GetKeyDown(KeyCode.Escape))
        {
            Resum();
        }   
    }
    public void Resum()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        Panel.gameObject.SetActive(false);
        IsPaused = false;
    }
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        Panel.gameObject.SetActive(true);
        IsPaused = true;
    }  
    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
