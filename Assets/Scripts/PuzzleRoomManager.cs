using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PuzzleRoomManager : MonoBehaviour
{
    public XRSocketInteractor redSocket;
    public XRSocketInteractor blueSocket;
    public XRSocketInteractor greenSocket;

    public GameObject redCube;
    public GameObject blueSphere;
    public GameObject greenCylinder;

    public GameObject finishButton;

    public AudioSource audioSource;

    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip puzzleCompleteSound;

    private bool puzzleSolved = false;

    private bool redChecked = false;
    private bool blueChecked = false;
    private bool greenChecked = false;

    void Update()
    {
        if (puzzleSolved) return;

        CheckSocket(redSocket, redCube, ref redChecked);
        CheckSocket(blueSocket, blueSphere, ref blueChecked);
        CheckSocket(greenSocket, greenCylinder, ref greenChecked);

        bool redCorrect =
            redSocket.hasSelection &&
            redSocket.firstInteractableSelected.transform.gameObject == redCube;

        bool blueCorrect =
            blueSocket.hasSelection &&
            blueSocket.firstInteractableSelected.transform.gameObject == blueSphere;

        bool greenCorrect =
            greenSocket.hasSelection &&
            greenSocket.firstInteractableSelected.transform.gameObject == greenCylinder;

        if (redCorrect && blueCorrect && greenCorrect)
        {
            PuzzleSolved();
        }
    }

    void CheckSocket(
        XRSocketInteractor socket,
        GameObject correctObject,
        ref bool alreadyChecked
    )
    {
        if (socket.hasSelection && !alreadyChecked)
        {
            alreadyChecked = true;

            if (
                socket.firstInteractableSelected.transform.gameObject
                == correctObject
            )
            {
                if (audioSource != null && correctSound != null)
                {
                    audioSource.PlayOneShot(correctSound);
                }
            }
            else
            {
                if (audioSource != null && wrongSound != null)
                {
                    audioSource.PlayOneShot(wrongSound);
                }
            }
        }

        if (!socket.hasSelection)
        {
            alreadyChecked = false;
        }
    }

    void PuzzleSolved()
    {
        puzzleSolved = true;

        finishButton.SetActive(true);

        if (audioSource != null && puzzleCompleteSound != null)
        {
            audioSource.PlayOneShot(puzzleCompleteSound);
        }
    }
}