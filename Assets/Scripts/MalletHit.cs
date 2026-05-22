using UnityEngine;

public class MalletHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Mole mole = collision.gameObject.GetComponent<Mole>();

        if (mole != null && mole.CanBeHit())
        {
            mole.Hit();
        }
    }
}