using UnityEngine;
using System.Collections;

public class CannonScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float minX, maxX;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    private float currentFireRate = 0.11f;   // Используем только эту переменную для частоты стрельбы
    private float nextFireTime = 0f;
    public int kills;
    public bool win = false;

    void Start()
    {
        Camera cam = Camera.main;
        Vector3 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        minX = cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = screenBounds.x;
    }

    void Update()
    {
        Debug.LogError("Current bullet speed: " + currentFireRate);

        // Стреляем, если время для следующего выстрела наступило
        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + currentFireRate; // Используем текущую частоту стрельбы
        }

        // Управление перемещением с помощью тач или мыши
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            MoveBlock(touchPosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            MoveBlock(mousePosition);
        }
    }

    void MoveBlock(Vector3 targetPosition)
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(transform.position.x, targetPosition.x, moveSpeed * Time.deltaTime);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = bulletSpawnPoint.up * bulletSpeed; // Скорость пули
    }

    // Метод, который вызывается при уничтожении блока
    public void OnBlockDestroy()
    {
        kills++;  // Увеличиваем счетчик убийств

        // Увеличиваем частоту стрельбы в зависимости от количества убийств
        if(kills == 1)
        {   
            //currentFireRate = 0.5f;  // Увеличиваем частоту (меньший интервал)
        }
        else if (kills == 2)
        {
            //currentFireRate = 0.3f; // Еще быстрее
        }
        else if(kills >= 3)
        {
            //currentFireRate = 0.1f;   // Максимальная скорость
        }

        if(kills == 20)
        {
            win =true;
        }
    }
}