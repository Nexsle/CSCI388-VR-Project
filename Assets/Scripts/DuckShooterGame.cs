using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DuckShooterGame : MonoBehaviour
{
    public List<GameObject> ducks = new List<GameObject>();

    public Transform launcherStartPoint;
    public Transform projectileLauncher;
    public Rigidbody launcherRigidbody;
    public XRGrabInteractable launcherGrab;

    public AudioSource audioSource;
    public AudioClip hitSound;

    private int ducksHit = 0;

    public void DuckHit(GameObject duck)
    {

        if(audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        if (duck.activeSelf)
        {
            duck.SetActive(false);
            ducksHit++;

            if (ducksHit >= 5)
            {
                StartCoroutine(ResetGameAfterDelay(2f));
            }
        }
    }

    IEnumerator ResetGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject duck in ducks)
        {
            duck.SetActive(true);
        }

        ducksHit = 0;
    }

    public void ResetLauncher()
    {
        if (launcherGrab != null && launcherGrab.isSelected)
        {
            foreach (var interactor in launcherGrab.interactorsSelecting)
            {
                launcherGrab.interactionManager.SelectExit(interactor, launcherGrab);
                break;
            }
        }

        projectileLauncher.SetPositionAndRotation(
            launcherStartPoint.position,
            launcherStartPoint.rotation
        );

        launcherRigidbody.linearVelocity = Vector3.zero;
        launcherRigidbody.angularVelocity = Vector3.zero;
    }
}