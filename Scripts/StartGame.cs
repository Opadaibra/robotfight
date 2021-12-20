using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
   public void loadscene()
    {   
        SceneManager.LoadScene(1);
    }
    public void Restart()
    {
        FindObjectOfType<AudioManger>().play("Theme");
        SceneManager.LoadScene(1);
    }
    public void Credit()
 {
        SceneManager.LoadScene(3);
    }
    public void Backtomain()
    {
        SceneManager.LoadScene(0);
    }
}
