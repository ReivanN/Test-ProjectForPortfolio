using UnityEngine;
using UnityEngine.iOS;

public class TimeSet : MonoBehaviour
{
    void Start()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            switch (Device.generation)
            {
                case DeviceGeneration.iPhoneX:
                case DeviceGeneration.iPhoneXS:
                    Screen.SetResolution(1125, 2436, true); // Разрешение для iPhone X, XS
                    break;
                case DeviceGeneration.iPhone11:
                case DeviceGeneration.iPhone11Pro:
                    Screen.SetResolution(828, 1792, true); // Пример для iPhone 11
                    break;
                default:
                    Screen.SetResolution(Screen.width, Screen.height, true);
                    break;
            }
        }
        Time.timeScale = 1f;
    }
}
