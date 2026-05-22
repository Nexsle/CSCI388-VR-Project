using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float launchForce = 5f;
    public float reloadTime = 1f;

    public AudioSource audioSource;
    public AudioClip shootSoud;

    public DuckShooterGame gameManager;

    
    public Vector3 spawnOffset = new Vector3(0.5f, 0f, 0f);

    private bool canShoot = true;

    public void Launch()
    {
        Debug.Log("i just launhc");

        if (audioSource != null && shootSoud != null)
        {
            audioSource.PlayOneShot(shootSoud);
        }

        if (!canShoot) return;

        canShoot = false;

        Vector3 spawnPos =
            transform.position +
            transform.right * spawnOffset.x +
            transform.up * spawnOffset.y +
            transform.forward * spawnOffset.z;


        GameObject projectile = Instantiate(
            projectilePrefab,
            spawnPos,
            Quaternion.identity
        );

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.right * launchForce;
        }

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.gameManager = gameManager;
        }

        Invoke(nameof(ResetShoot), reloadTime);
    }

    void ResetShoot()
    {
        canShoot = true;
    }
}