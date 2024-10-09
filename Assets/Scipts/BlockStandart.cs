using UnityEngine;
using TMPro;

public class BlockStandart : MonoBehaviour
{
    private float speed = 1f;
    public GameObject block;
    public float HP = 5f;
    private float currentHealth;
    public int kills;

    public TextMeshProUGUI healthText;
    private CannonScript cannonScript;
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
}
