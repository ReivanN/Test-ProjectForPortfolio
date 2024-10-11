using UnityEngine;
using TMPro;

public class BlockStandart : MonoBehaviour
{
    private float speed = 0.3f;
    public GameObject block;
    public GameObject Bonus_Cannon;
    public GameObject Bonus_speedBullet;
    public GameObject Bonus_Lazer;
    public float HP = 5f;
    private float currentHealth;
    public int kills;

    public TextMeshProUGUI healthText;
    private CannonScript cannonScript;

    public LayerMask layerMask;
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
        if (currentHealth == 0)
        {
            if(IsPrefabInLayer(gameObject))
            {
                Instantiate(Bonus_Cannon, block.transform.position, Quaternion.identity);
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
        healthText.text = "" + currentHealth;
    }


    bool IsPrefabInLayer(GameObject prefab)
    {
        return (layerMask == (layerMask | (1 << prefab.layer)));
    }
}
