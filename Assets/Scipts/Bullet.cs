using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;

    public void Start()
    {
         //damage = 1f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
    
        BlockStandart block = other.gameObject.GetComponent<BlockStandart>();
        if (block != null)
        {
            Debug.LogError("TakeDamage");
            block.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "floor")
        {
            Destroy(gameObject);
            
        }
        
        
    }
}
