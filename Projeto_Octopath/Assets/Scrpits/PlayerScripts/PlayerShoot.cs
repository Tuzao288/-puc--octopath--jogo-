using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;       
    public GameObject projectilePrefab;

    
    public float fireRate = 0.2f;     
    public float projectileSpeed = 30f;
    private float fireTimer = 0f;
    private bool isFiring = false;

   
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isFiring = true; // Botão pressionado
        }
        else if (context.canceled)
        {
            isFiring = false; // Botão solto
        }
    }

    void Update()
    {
        if (isFiring)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }
    }
}