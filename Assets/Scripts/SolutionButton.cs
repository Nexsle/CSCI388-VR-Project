using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class HintButton : MonoBehaviour
{
    public GameObject hintText;

    private XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        interactable.selectEntered.AddListener(ShowHint);
    }

    void ShowHint(SelectEnterEventArgs args)
    {
        hintText.SetActive(true);
    }

    void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(ShowHint);
    }
}