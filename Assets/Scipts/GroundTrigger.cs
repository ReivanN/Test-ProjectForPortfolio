using Unity.VisualScripting;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    public Canvas canvas;
    public GameObject YouLose;
    private void OnTriggerEnter2D(Collider2D other)
    {
    
        BlockStandart block = other.gameObject.GetComponent<BlockStandart>();
        if (block != null)
        {
            Debug.LogError("You Lose");
            Time.timeScale = 0f;
            canvas.gameObject.SetActive(true);
            YouLose.SetActive(true);

        }
        
    }
}
