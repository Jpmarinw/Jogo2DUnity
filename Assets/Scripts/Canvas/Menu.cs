using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene(1);        
    }

    public void Pause(){
        Time.timeScale = 0;
    }

    public void PlayGame(){
        Time.timeScale = 1;
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void MenuGame(){
        SceneManager.LoadScene(0);
    }
}
