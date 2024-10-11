using UnityEngine;

public class CustomCollision : MonoBehaviour
{
    private Collider triggerCollider;

    void Start()
    {
        GroundTrigger trigger = FindObjectOfType<GroundTrigger>();

        if (trigger != null)
        {
            triggerCollider = trigger.GetComponent<Collider>();
        }
        else
        {
            Debug.LogError("GroundTrigger не найден в сцене!");
            return;
        }

        Collider[] allColliders = FindObjectsOfType<Collider>();

        foreach (Collider col in allColliders)
        {
            if (col != triggerCollider && col != GetComponent<Collider>())
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), col);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == triggerCollider)
        {
            Debug.Log("Коллайдер взаимодействует с целевым объектом GroundTrigger!");
        }
    }
}