using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ResetDroppedLauncher : MonoBehaviour
{
    public DuckShooterGame gameManager;
    public XRGrabInteractable launcherGrab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            if (launcherGrab != null && !launcherGrab.isSelected)
            {
                gameManager.ResetLauncher();
            }
        }
    }
}