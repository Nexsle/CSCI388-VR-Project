using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    private XRSimpleInteractable _interactable;

    void Awake()
    {
        _interactable = GetComponent<XRSimpleInteractable>();

        _interactable.selectEntered.AddListener(OnTriggered);
    }

    private void OnTriggered(SelectEnterEventArgs args)
    {
        SceneManager.LoadScene(
            RoomOrderManager.Instance.GetNextRoom()
        );
    }

    void OnDestroy()
    {
        _interactable.selectEntered.RemoveListener(OnTriggered);
    }
}