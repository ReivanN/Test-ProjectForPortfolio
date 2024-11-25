using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject PauseButton;
    [SerializeField] private GameObject PausePanel;
    public void OnPause()
    {
        PauseButton.SetActive(false);
        PausePanel.SetActive(true);
        Time.timeScale = 0f;        
    }

    public void Resume()
    {
        PauseButton.SetActive(true);
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
