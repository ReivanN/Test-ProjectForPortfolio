using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class CannonScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float minX, maxX;
    public GameObject bulletPrefab;
    public GameObject CannonParent;
    public GameObject Cannon;
    public Transform bulletSpawnPoint;
    public Transform cannonSpawnPoint;
    public float bulletSpeed = 10f;
    private float currentFireRate = 0.5f;
    private float bonusDamage = 1f;
    private float startDamage = 1f;
    private float nextFireTime = 0f;
    public int kills;
    public bool win = false;
    [SerializeField] private Bullet bullet;

    [SerializeField] private Bonus_Canon bonus_Canon;

    public AudioClip shootSound;
    public AudioClip PickupSound;
    public AudioSource audioSource;
    public AudioSource pickUpSource;
    void Start()
    {
        bullet.damage = startDamage;
        Camera cam = Camera.main;
        Vector3 screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        minX = cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = screenBounds.x;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource = audioSources[0];
        pickUpSource = audioSources[1];
    }

    void Update()
    {
        //Debug.LogError("Current bullet speed: " + currentFireRate);
        //Debug.LogError(bullet.damage + "damage");

        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + currentFireRate;
        }
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
        rb.linearVelocity = bulletSpawnPoint.up * bulletSpeed;
        audioSource.PlayOneShot(shootSound);
    }
    public void OnBlockDestroy()
    {
        kills++;  
        if(kills == 1)
        {   
            //currentFireRate = 0.5f;  
        }
        else if (kills == 2)
        {
            //currentFireRate = 0.3f; 
        }
        else if(kills >= 3)
        {
            //currentFireRate = 0.1f;  
        }

        if(kills == 20)
        {
            win =true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "bonus_cannon")
        {
            Instantiate(Cannon, cannonSpawnPoint.position, cannonSpawnPoint.rotation);
            Cannon.transform.SetParent(CannonParent.transform);
            Cannon.transform.localScale = new Vector3(1f, 3f, 1f);
            pickUpSource.PlayOneShot(PickupSound);
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "bonus_speed")
        {
            pickUpSource.PlayOneShot(PickupSound);   
            bullet.damage += bonusDamage;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "bonus_lazer")
        {
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocityX = CannonParent.transform.localScale.x * bulletSpeed;
            currentFireRate = 0.01f;
            bullet.damage = 0.5f;
            pickUpSource.PlayOneShot(PickupSound);
            Destroy(other.gameObject);
        }
    }
}