using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public string sceneToLoad;

    private XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        interactable.selectEntered.AddListener(OnPressed);
    }

    private void OnPressed(SelectEnterEventArgs args)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnPressed);
    }
}