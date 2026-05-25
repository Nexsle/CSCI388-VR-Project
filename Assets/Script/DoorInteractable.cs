using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorInteractable : MonoBehaviour
{
    [SerializeField] private string nextScene;

    private XRSimpleInteractable interactable;

    private bool openDoor = false;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnInteract);
    }

    private void OnInteract(SelectEnterEventArgs args)
    {
        if (!openDoor)
        {
            Debug.Log("Puzzle not solved yet!");
            return;
        }

        Debug.Log("Changing Scene");
        SceneManager.LoadScene(nextScene);
    }

    public void OpenDoor()
    {
        openDoor = true;
    }
}
