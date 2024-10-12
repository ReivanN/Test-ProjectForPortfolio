using UnityEngine;

public class DynamicResolutionManager : MonoBehaviour
{
    void Start()
    {
        ScalableBufferManager.ResizeBuffers(0.8f, 0.8f);
    }

    void Update()
    {
        float targetFrameRate = 60.0f;
        float currentFrameRate = 1.0f / Time.deltaTime;
        
        if (currentFrameRate < targetFrameRate)
        {
            ScalableBufferManager.ResizeBuffers(0.5f, 0.5f);
        }
        else
        {
            ScalableBufferManager.ResizeBuffers(1.0f, 1.0f);
        }
    }
}