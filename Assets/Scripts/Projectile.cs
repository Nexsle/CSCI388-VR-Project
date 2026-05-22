using UnityEngine;

public class Projectile : MonoBehaviour
{
    public DuckShooterGame gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Duck"))
        {
            if (gameManager != null)
            {
                gameManager.DuckHit(collision.gameObject);
            }
        }

        Destroy(gameObject);
    }
}