using UnityEngine;

public class Bonus_Canon : MonoBehaviour
{
   public bool CannonBonus;
   public AudioClip bonusClip;
   public AudioSource audioSource;
   public void Start()
    {
        CannonBonus = true;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(bonusClip);
    }
}
