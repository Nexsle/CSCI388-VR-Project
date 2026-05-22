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

    public Transform door;

    private bool doorOpened = false;
    private Vector3 doorTargetPosition;
    public float doorSpeed = 2f;

    void Update()
    {
        if (!doorOpened)
        {
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
                OpenDoor();
            }
        }

        if (doorOpened)
        {
            door.position = Vector3.Lerp(
                door.position,
                doorTargetPosition,
                Time.deltaTime * doorSpeed
            );
        }
    }

    void OpenDoor()
    {
        doorOpened = true;

        doorTargetPosition = door.position + new Vector3(0f, 3f, 0f);
    }
}