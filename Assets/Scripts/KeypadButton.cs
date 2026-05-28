using System.Collections;
using NavKeypad;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class KeypadButton : MonoBehaviour
{
    [SerializeField] private string buttonValue;
    [Header("Button Animation Settings")]
    [SerializeField] private float bttnspeed = 0.1f;
    [SerializeField] private float moveDist = 0.0025f;
    [SerializeField] private float buttonPressedTime = 0.1f;
    [Header("Hover Highlight")]
    [SerializeField] private Color hoverColor = Color.yellow;

    private bool moving = false;
    private XRSimpleInteractable interactable;
    private Renderer rend;
    private Color originalColor;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;

        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
        interactable.selectEntered.AddListener(PressButton);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (rend != null)
            rend.material.color = hoverColor;
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        if (rend != null)
            rend.material.color = originalColor;
    }


    public void PressButton(SelectEnterEventArgs args)
    {
        if (!moving)
        {
            Debug.Log($"Button Value: {buttonValue}");
            KeypadController.Instance.ReceiveInput(buttonValue);
            StartCoroutine(MoveSmooth());
        }
    }

    private IEnumerator MoveSmooth()
    {

        moving = true;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = transform.localPosition + new Vector3(0, 0, moveDist);

        float elapsedTime = 0;
        while (elapsedTime < bttnspeed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / bttnspeed);

            transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }
        transform.localPosition = endPos;
        yield return new WaitForSeconds(buttonPressedTime);
        startPos = transform.localPosition;
        endPos = transform.localPosition - new Vector3(0, 0, moveDist);

        elapsedTime = 0;
        while (elapsedTime < bttnspeed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / bttnspeed);

            transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }
        transform.localPosition = endPos;

        moving = false;
    }
}
