using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ResetDroppedMallet : MonoBehaviour
{
    public Transform malletStartPoint;
    public XRGrabInteractable malletGrab;
    public Rigidbody malletRigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            if (malletGrab != null && !malletGrab.isSelected)
            {
                transform.SetPositionAndRotation(
                    malletStartPoint.position,
                    malletStartPoint.rotation
                );

                malletRigidbody.linearVelocity = Vector3.zero;
                malletRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}