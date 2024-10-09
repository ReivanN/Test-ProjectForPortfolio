using UnityEngine;

public class BlockEvent : MonoBehaviour
{
    public delegate void BlockDestroyedHandler();
    public event BlockDestroyedHandler BlockDestroyed;

    public void DestroyBlock()
    {
        BlockDestroyed?.Invoke();
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        // Логика получения урона
        // Например, уменьшение здоровья
        // ...

        // Если здоровье <= 0, вызываем DestroyBlock
        // if (health <= 0)
        // {
        //     DestroyBlock();
        // }
    }
}