using UnityEngine;
using TMPro;

public class BlockStandart : MonoBehaviour
{
    private float speed = 0.3f;
    public GameObject block;
    public GameObject Bonus_Cannon;
    public GameObject Bonus_SpeedBullet;
    public GameObject Bonus_Laser;
    public float HP = 5f;
    private float currentHealth;
    public int kills;

    public TextMeshProUGUI healthText;
    private CannonScript cannonScript;

    // LayerMasks for different bonuses
    public LayerMask cannonLayerMask;
    public LayerMask speedBulletLayerMask;
    public LayerMask laserLayerMask;

    private void Start()
    {
        currentHealth = HP;
        UpdateHealthText();
        cannonScript = FindFirstObjectByType<CannonScript>();
    } 
    
    void Update()
    {
        Debug.LogError("Kills " + kills);
        if(block != null)
        {
            block.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // Check which layer the object is in and instantiate the appropriate bonus
            if(IsPrefabInLayer(gameObject, cannonLayerMask))
            {
                Instantiate(Bonus_Cannon, block.transform.position, Quaternion.identity);
            }
            else if(IsPrefabInLayer(gameObject, speedBulletLayerMask))
            {
                Instantiate(Bonus_SpeedBullet, block.transform.position, Quaternion.identity);
            }
            else if(IsPrefabInLayer(gameObject, laserLayerMask))
            {
                Instantiate(Bonus_Laser, block.transform.position, Quaternion.identity);
            }

            cannonScript.OnBlockDestroy();
            DestroyBlock();
        }
        UpdateHealthText();
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }

    void UpdateHealthText()
    {
        healthText.text = currentHealth.ToString();
    }

    bool IsPrefabInLayer(GameObject prefab, LayerMask mask)
    {
        return (mask == (mask | (1 << prefab.layer)));
    }
}