using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void Restartlevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
