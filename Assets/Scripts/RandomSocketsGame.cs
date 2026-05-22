using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class RandomSocketsGame : MonoBehaviour
{
    public Renderer indicatorRenderer;

    public Transform guessObject;
    public Rigidbody guessObjectRigidbody;
    public XRGrabInteractable guessObjectGrab;

    public Transform guessObjectStartPoint;

    public Material neutralMaterial;
    public Material correctMaterial;
    public Material wrongMaterial;

    public XRSocketInteractor[] sockets;

    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private int correctSocket;
    private bool roundActive = false;
    private bool isResetting = false;

    void Start()
    {
        StartNewRound();
    }

    public void CheckSocket1()
    {
        CheckSocket(1);
    }

    public void CheckSocket2()
    {
        CheckSocket(2);
    }

    public void CheckSocket3()
    {
        CheckSocket(3);
    }

    void CheckSocket(int socketNumber)
    {
        if (!roundActive || isResetting) return;

        roundActive = false;

        if (socketNumber == correctSocket)
        {
            indicatorRenderer.material = correctMaterial;

            if (audioSource != null && correctSound != null)
            {
                audioSource.PlayOneShot(correctSound);
            }
        }
        else
        {
            indicatorRenderer.material = wrongMaterial;

            if (audioSource != null && wrongSound != null)
            {
                audioSource.PlayOneShot(wrongSound);
            }
        }

        StartCoroutine(ResetRoundRoutine());
    }

    IEnumerator ResetRoundRoutine()
    {
        isResetting = true;

        yield return new WaitForSeconds(3f);

        DisableSockets();

        ForceReleaseGuessObject();

        yield return null;

        ResetGuessObjectTransform();

        if (neutralMaterial != null)
        {
            indicatorRenderer.material = neutralMaterial;
        }

        yield return new WaitForSeconds(0.15f);

        EnableSockets();

        isResetting = false;
        StartNewRound();
    }

    void StartNewRound()
    {
        correctSocket = Random.Range(1, 4);
        roundActive = true;

        if (!isResetting && neutralMaterial != null)
        {
            indicatorRenderer.material = neutralMaterial;
        }
    }

    void DisableSockets()
    {
        foreach (var socket in sockets)
        {
            if (socket != null)
            {
                socket.socketActive = false;
            }
        }
    }

    void EnableSockets()
    {
        foreach (var socket in sockets)
        {
            if (socket != null)
            {
                socket.socketActive = true;
            }
        }
    }

    void ForceReleaseGuessObject()
    {
        if (guessObjectGrab != null && guessObjectGrab.isSelected)
        {
            var interactors = new List<IXRSelectInteractor>(guessObjectGrab.interactorsSelecting);

            foreach (var interactor in interactors)
            {
                guessObjectGrab.interactionManager.SelectExit(interactor, guessObjectGrab);
            }
        }
    }

    void ResetGuessObjectTransform()
    {
        guessObject.SetPositionAndRotation(
            guessObjectStartPoint.position,
            guessObjectStartPoint.rotation
        );

        guessObjectRigidbody.linearVelocity = Vector3.zero;
        guessObjectRigidbody.angularVelocity = Vector3.zero;
    }
}