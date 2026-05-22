using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ResetDroppedGuessObject : MonoBehaviour
{
    public Transform startPoint;
    public XRGrabInteractable grab;
    public Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            if (grab != null && !grab.isSelected)
            {
                transform.SetPositionAndRotation(
                    startPoint.position,
                    startPoint.rotation
                );

                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}